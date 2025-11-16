using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PasswordProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: dotnet run <command> [parameters]");
                return;
            }

            string command = args[0].ToLower(); // Första argumentet är kommandot

            switch (command)
            {
                case "init":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: dotnet run init <clientName> <serverName>");
                        return;
                    }
                    Init(args[1], args[2]);
                    break;

                case "create":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: dotnet run create <clientName> <serverName>");
                        return;
                    }
                    Create(args[1], args[2]);
                    break;

                case "get":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: dotnet run get <clientName> <serverName> [prop]");
                        return;
                    }

                    string props = args.Length > 3 ? args[3] : null;

                    Get(args[1], args[2], props);
                    break;

                case "set":
                    if (args.Length < 4)
                    {
                        Console.WriteLine("Usage: dotnet run set <clientName> <serverName> <prop> [--generate or -g]");
                        return;
                    }

                    string clientName = args[1];
                    string serverName = args[2];
                    string prop = args[3];

                    bool generate = false;

                    // Kolla om användaren har angivit --generate eller -g
                    if (args.Length > 4)
                    {
                        if (args[4] == "-g" || args[4] == "--generate")
                        {
                            generate = true;
                        }
                    }

                    Set(clientName, serverName, prop, generate);
                    break;

                case "delete":
                    if (args.Length < 4)
                    {
                        Console.WriteLine("Usage: dotnet run delete <clientName> <serverName> <prop>");
                        return;
                    }
                    Delete(args[1], args[2], args[3]);
                    break;

                case "secret":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Usage: dotnet run secret <clientName>");
                        return;
                    }
                    Secret(args[1]);
                    break;

                case "change":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Usage: dotnet run delete <clientName> <serverName>");
                        return;
                    }
                    Change(args[1], args[2]);
                    break;


                default:
                    Console.WriteLine("Invalid command. Available commands: init, create, get, set, delete, secret.");
                    break;
            }
        }


        public static void Init(string clientName, string serverName)
        {
            Console.WriteLine("What is the master password?:");
            string psw = Console.ReadLine();

            Dictionary<string, Dictionary<string, string>> vault = new Dictionary<string, Dictionary<string, string>>();

            vault[serverName] = new Dictionary<string, string>();

            byte[] IV = GenerateIV();
            byte[] secretKey = CreateClient(); 

            JsonFileClient(secretKey, clientName);

            string encryptedVault = CreateVault(vault, psw, secretKey, IV);
            JsonFileServer(encryptedVault, IV, serverName);
        }

        static void Get(string clientName, string serverName, string prop = null)
        {
            string clientPath = $"{clientName}.json";
            string serverPath = $"{serverName}.json";

            if (!File.Exists(clientPath) || !File.Exists(serverPath))
            {
                Console.WriteLine("Client or Server is missing.");
                return;
            }

            var clientJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(clientPath));
            if (clientJson == null || !clientJson.TryGetValue("secretKey", out string secretKeyBase64))
            {
                Console.WriteLine("Wrong client.json");
                return;
            }

            byte[] secretKey = Convert.FromBase64String(secretKeyBase64);
            Console.WriteLine("What is your masterpassword?: ");
            string mpsw = Console.ReadLine();

            var serverJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(serverPath));
            if (serverJson == null || !serverJson.TryGetValue("Vault", out string encryptedVault) ||
                !serverJson.TryGetValue("IV", out string ivBase64))
            {
                Console.WriteLine("Wrong server.json");
                return;
            }

            byte[] IV = Convert.FromBase64String(ivBase64);

            byte[] vaultKey = GenerateVaultKey(mpsw, secretKey);

            string decryptedVault;
            try
            {
                decryptedVault = DecryptVault(encryptedVault, vaultKey, IV);
            }
            catch
            {
                Console.WriteLine("Couldn't decrypt vault.");
                return;
            }

            var vaultDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(decryptedVault);
            if (vaultDict == null)
            {
                Console.WriteLine("Couldn't convert vault.");
                return;
            }

            if (string.IsNullOrEmpty(prop)) 
            {
                Console.WriteLine("All passwords in vault: :");
                foreach (var entry in vaultDict)
                {
                    foreach (var subEntry in entry.Value)
                    {
                        Console.WriteLine($"{subEntry.Key}: {subEntry.Value}");
                    }
                }
            }
            else 
            {
                foreach (var entry in vaultDict)
                {
                    if (entry.Value.ContainsKey(prop))
                    {
                        Console.WriteLine($"The password for {prop}: {entry.Value[prop]}");
                        return;
                    }
                }
                Console.WriteLine($"Could not find {prop} in vault.");
            }
        }

        static void Set(string clientName, string serverName, string prop, bool generate = false)
        {
            string clientPath = $"{clientName}.json";
            string serverPath = $"{serverName}.json";

            if (!File.Exists(clientPath) || !File.Exists(serverPath))
            {
                Console.WriteLine("Client or Server file is missing.");
                return;
            }

            var clientJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(clientPath));
            if (clientJson == null || !clientJson.TryGetValue("secretKey", out string secretKeyBase64))
            {
                Console.WriteLine("Wrong client.json");
                return;
            }

            byte[] secretKey = Convert.FromBase64String(secretKeyBase64);

            Console.WriteLine("Enter master password: ");
            string mpsw = Console.ReadLine();
            if (mpsw == null)
            {
                Console.WriteLine("Invalid code");
                return;

            }


            var serverJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(serverPath));
            if (serverJson == null || !serverJson.TryGetValue("Vault", out string encryptedVault) ||
                !serverJson.TryGetValue("IV", out string ivBase64))
            {

                Console.WriteLine("Wrong server.json");
                return;
            }

            byte[] IV = Convert.FromBase64String(ivBase64);


            byte[] vaultKey = GenerateVaultKey(mpsw, secretKey);
            string decryptedVault;

            try
            {
                decryptedVault = DecryptVault(encryptedVault, vaultKey, IV);
            }
            catch
            {
                Console.WriteLine("Faild to decrypt vault. Wrong master password.");
                return;
            }


            var vaultDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(decryptedVault);
            if (vaultDict == null)
            {
                Console.WriteLine("Faild to convert vault.");
                return;
            }

            string newPassword;
            if (generate)
            {
                newPassword = GenerateRandomPassword(20);
                Console.WriteLine($"Generated password: {newPassword}");
            }
            else
            {
                Console.Write("Enter new password to save: ");
                newPassword = Console.ReadLine();
            }
            if (!vaultDict.ContainsKey(serverName))
            {
                vaultDict[serverName] = new Dictionary<string, string>();
            }
            vaultDict[serverName][prop] = newPassword;

            string encryptedUpdatedVault = EncryptVault(vaultDict, vaultKey, IV);

            serverJson["Vault"] = encryptedUpdatedVault;
            string updatedServerJson = JsonSerializer.Serialize(serverJson);
            File.WriteAllText(serverPath, updatedServerJson);

            Console.WriteLine("The password has been saved!");
        }
        static void Create(string clientName, string serverName)
        {
            string serverPath = $"{serverName}.json";

            if (!File.Exists(serverPath))
            {
                Console.WriteLine("Server file is missing.");
                return;
            }

            Console.WriteLine("Enter your master password: ");
            string masterPassword = Console.ReadLine();

            Console.WriteLine("Enter your secretKey (Base64-coded): ");
            string userSecretKeyBase64 = Console.ReadLine();

            byte[] secretKey;

            try
            {
                secretKey = Convert.FromBase64String(userSecretKeyBase64);
            }
            catch
            {
                Console.WriteLine("Wrong Base64-string for secretKey.");
                return;
            }

            var serverJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(serverPath));
            if (serverJson == null || !serverJson.TryGetValue("Vault", out string encryptedVault) ||
                !serverJson.TryGetValue("IV", out string ivBase64))
            {
                Console.WriteLine("Wrong server.json");
                return;
            }

            byte[] IV = Convert.FromBase64String(ivBase64);

            byte[] vaultKey = GenerateVaultKey(masterPassword, secretKey);

            string decryptedVault;
            try
            {
                decryptedVault = DecryptVault(encryptedVault, vaultKey, IV);
            }
            catch
            {
                Console.WriteLine("Wrong password or secretKey.");
                return;
            }

            Console.WriteLine("Vault decrypted sucsessfully.");

            JsonFileClient(secretKey, clientName);
        }
        static void Delete(string clientName, string serverName, string prop)
        {
            string clientPath = $"{clientName}.json";
            string serverPath = $"{serverName}.json";
            if (!File.Exists(clientPath) || !File.Exists(serverPath))
            {
                Console.WriteLine("Client or Server file is missing.");
                return;
            }
            Console.WriteLine("Enter your masterpassword: ");
            string mpsw = Console.ReadLine();
            var clientJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(clientPath));
            if (clientJson == null || !clientJson.TryGetValue("secretKey", out string secretKeyBase64))
            {
                Console.WriteLine("Wrong client.json");
                return;
            }
            byte[] secretKey = Convert.FromBase64String(secretKeyBase64);

            var serverJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(serverPath));
            if (serverJson == null || !serverJson.TryGetValue("Vault", out string encryptedVault) ||
                !serverJson.TryGetValue("IV", out string ivBase64))
            {

                Console.WriteLine("Wrong server.json");
                return;
            }

            byte[] IV = Convert.FromBase64String(ivBase64);


            byte[] vaultKey = GenerateVaultKey(mpsw, secretKey);
            string decryptedVault;

            try
            {
                decryptedVault = DecryptVault(encryptedVault, vaultKey, IV);
            }
            catch
            {
                Console.WriteLine("Failed to decrypt vault. Wrong master password.");
                return;
            }


            var vaultDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(decryptedVault);
            if (vaultDict == null)
            {
                Console.WriteLine("Failed to convert vault.");
                return;
            }

            foreach (var entry in vaultDict)
            {
                if (entry.Value.ContainsKey(prop))
                {
                    bool removed = entry.Value.Remove(prop);

                    if (removed)
                    {
                        Console.WriteLine("The prop was removed");
                        string encryptedUpdatedVault = EncryptVault(vaultDict, vaultKey, IV);

                        // Uppdatera serverfilen med det nya krypterade vaultet
                        serverJson["Vault"] = encryptedUpdatedVault;
                        string updatedServerJson = JsonSerializer.Serialize(serverJson);
                        File.WriteAllText(serverPath, updatedServerJson);

                        return;
                    }
                    else
                    {
                        Console.WriteLine("The prop did not exist");
                        return;
                    }
                }
            }

        }
        static void Secret(string clientName)
        {
            string clientPath = $"{clientName}.json";

            if (!File.Exists(clientPath))
            {
                Console.WriteLine("Client file is missing.");
                return;
            }
            var clientJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(clientPath));
            if (clientJson == null || !clientJson.TryGetValue("secretKey", out string secretKeyBase64))
            {
                Console.WriteLine("Wrong client.json");
                return;
            }

            Console.WriteLine(secretKeyBase64);
        }
        static void Change(string clientName, string serverName)
        {
            string clientPath = $"{clientName}.json";
            string serverPath = $"{serverName}.json";
            if (!File.Exists(clientPath) || !File.Exists(serverPath))
            {
                Console.WriteLine("Client or Server file is missing.");
                return;
            }
            Console.WriteLine("Enter your masterpassword: ");
            string mpsw = Console.ReadLine();
            var clientJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(clientPath));
            if (clientJson == null || !clientJson.TryGetValue("secretKey", out string secretKeyBase64))
            {
                Console.WriteLine("Wrong client.json");
                return;
            }
            byte[] secretKey = Convert.FromBase64String(secretKeyBase64);

            var serverJson = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(serverPath));
            if (serverJson == null || !serverJson.TryGetValue("Vault", out string encryptedVault) ||
                !serverJson.TryGetValue("IV", out string ivBase64))
            {

                Console.WriteLine("Wrong server.json");
                return;
            }

            byte[] IV = Convert.FromBase64String(ivBase64);


            byte[] vaultKey = GenerateVaultKey(mpsw, secretKey);
            string decryptedVault;

            try
            {
                decryptedVault = DecryptVault(encryptedVault, vaultKey, IV);
            }
            catch
            {
                Console.WriteLine("Failed to decrypt vault. Wrong master password.");
                return;
            }

            var vaultDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(decryptedVault);
            if (vaultDict == null)
            {
                Console.WriteLine("Failed to convert vault.");
                return;
            }
            Console.WriteLine("Enter new master password: ");
            string newpsw = Console.ReadLine();
            byte[] newVaultKey = GenerateVaultKey(newpsw, secretKey);

            string encryptedUpdatedVault = EncryptVault(vaultDict, newVaultKey, IV);
            serverJson["Vault"] = encryptedUpdatedVault;
            string updatedServerJson = JsonSerializer.Serialize(serverJson);
            File.WriteAllText(serverPath, updatedServerJson);

        }

        static byte[] GenerateVaultKey(string masterPsw, byte[] secretKey)
        {
            byte[] bytesMasterPsw = Encoding.UTF8.GetBytes(masterPsw);
            byte[] vaultKey = new byte[bytesMasterPsw.Length + secretKey.Length];
            Buffer.BlockCopy(bytesMasterPsw, 0, vaultKey, 0, bytesMasterPsw.Length);
            Buffer.BlockCopy(secretKey, 0, vaultKey, bytesMasterPsw.Length, secretKey.Length);

            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(vaultKey); 
            }
        }

        static string EncryptVault(Dictionary<string, Dictionary<string, string>> vault, byte[] vaultKey, byte[] IV)
        {
            string json = JsonSerializer.Serialize(vault);
            byte[] encryptedData = EncryptString(json, vaultKey, IV);

            return Convert.ToBase64String(encryptedData);
        }

        static byte[] EncryptString(string plainText, byte[] vaultKey, byte[] IV)
        {
            using Aes aes = Aes.Create();
            aes.Key = vaultKey;
            aes.IV = IV;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random rand = new Random();
            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[rand.Next(validChars.Length)];
            }
            return new string(password);
        }

        static string DecryptVault(string encryptedVault, byte[] vaultKey, byte[] IV)
        {
            byte[] encryptedData = Convert.FromBase64String(encryptedVault);
            return DecryptString(encryptedData, vaultKey, IV);
        }

        static string DecryptString(byte[] encryptedData, byte[] vaultKey, byte[] IV)
        {
            using Aes aes = Aes.Create();
            aes.Key = vaultKey;
            aes.IV = IV;
            aes.Padding = PaddingMode.PKCS7;  

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        private static string CreateVault(Dictionary<string, Dictionary<string, string>> vault, string psw, byte[] secretKey, byte[] IV)
        {
            byte[] vaultKey = GenerateVaultKey(psw, secretKey);
            string encryptedVault = EncryptVault(vault, vaultKey, IV);
            return encryptedVault;
        }

        private static void JsonFileServer(string encryptedVault, byte[] ivBase64, string name)
        {
            var vaultData = new
            {
                Vault = encryptedVault,
                IV = Convert.ToBase64String(ivBase64)
            };

            string json = JsonSerializer.Serialize(vaultData);
            File.WriteAllText(name + ".json", json);
        }

        private static void JsonFileClient(byte[] secretKeyArray, string name)
        {
            var clientData = new
            {
                secretKey = Convert.ToBase64String(secretKeyArray)
            };
            string json = JsonSerializer.Serialize(clientData);
            File.WriteAllText(name + ".json", json);
        }

        private static byte[] GenerateIV()
        {
            byte[] IV = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(IV);
            }
            return IV;
        }

        private static byte[] CreateClient()
        {
            byte[] secretKeyArray = new byte[10];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(secretKeyArray);
            return secretKeyArray;
        }

    }

}
