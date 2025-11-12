<?
session_start();
if(isset($_SESSION['user_id'])) 
{
    header("Location: index.php");
    exit;
} 

$error = '';
$database = new SQLite3("db/databas.db");

if($_SERVER["REQUEST_METHOD"] == "POST") 
{
    $identifier = strtolower($_POST["identifier"]);
    $password = $_POST["password"];

    if (filter_var($identifier, FILTER_VALIDATE_EMAIL)) 
    {
        $query = "SELECT * FROM users WHERE email = :identifier";
    } 
    else 
    {
        $query = "SELECT * FROM users WHERE username = :identifier";
    }

    $statement = $database->prepare($query);
    $statement->bindParam(':identifier', $identifier);
    $result = $statement->execute();
    $user = $result->fetchArray(SQLITE3_ASSOC);

    if ($user) 
    {
        if (password_verify($password, $user['password'])) 
        {
            $_SESSION['username'] = $user['username'];
            $_SESSION['password'] = $password; 
            $_SESSION['user_id'] = $user['user_id'];
            $_SESSION['email'] = $user['email'];
            $_SESSION['picture'] = $user['picture'];

            header("Location: index.php");
            exit;
        } 
        else 
        {
            $error = "Invalid password";
        }
    } 
    else 
    {
        $error = "User not found";
    }
}

include 'header.php'; 
?>

<link rel="stylesheet" href="css/styles.css">
</head>

<body>
    <h1>PLANTBOK</h1>
    <div class="flex-container">
        <div id="fi1" class="flex-item">
            <img src="img/login.jpg">    
            <h2>SIGN IN</h2>
            <form id="login" name="log in" action="login.php" method="post"> 
                <label for="identifier"></label>
                <input type="text" name="identifier" id="identifier" placeholder="Enter email or username" required> <br>
                <label for="password"></label>
                <input type="password" name="password" id="password" placeholder="Enter password" required><br>
                <input type="submit" value="Sign in">
            </form>
            
            <div class = "error">
                <? if ($error): ?>
                    <p class="error"><? echo $error; ?></p>
                <? endif; ?>
            </div>

            <a class="register-link" href="register.php">Register</a>
        </div>
        <div id="fi2" class="flex-item">
            <img src="img/signup.jpg">
            <h2>ABOUT US</h2>
            <p>
                Welcome to Plantbook!<br><br>
                This is a website for fellow plantlovers. <br>
                We provide a platform where you can chat all about plants.
                Whether you're an experient plant owner or just getting started, we offer a 
                space for you to learn and share your knowledge.<br><br>
                Join us!

            </p>
        </div>
    </div>
</body>
</html>