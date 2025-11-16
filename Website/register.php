<?
session_start();
if(isset($_SESSION['user_id'])) 
{
    header("Location: posts.php");
    exit;
} 

$error = '';

if ($_SERVER["REQUEST_METHOD"] == "POST") 
{
    $username = strtolower($_POST["username"]);
    $password = $_POST["password"];
    $email = strtolower($_POST["email"]);
    $password_pattern = '/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/';
    
    $database = new SQLite3("db/databas.db");

    $q1 = "SELECT COUNT(*) as count FROM users WHERE email = :email";
    $s1 = $database->prepare($q1);
    $s1->bindParam(':email', $email);
    $result1 = $s1->execute();
    $row1 = $result1->fetchArray(SQLITE3_ASSOC);
    $e_count = $row1['count'];

    $q2 = "SELECT COUNT(*) as count FROM users WHERE username = :username";
    $s2 = $database->prepare($q2);
    $s2->bindParam(':username', $username);
    $result2 = $s2->execute();
    $row2 = $result2->fetchArray(SQLITE3_ASSOC);
    $u_count = $row2['count'];

    if ($e_count > 0) 
    {
        $error = "Email is taken";
    } 
    else if ($u_count > 0) 
    {
        $error = "Username is taken";
    } 
    else 
    {
        if (empty($username) || strlen($username) < 4) 
        {
            $error = "error";
        } 
        else if (empty($password) || !preg_match($password_pattern, $password)) 
        {
            $error = "error";
        } 
        else if (empty($email) || !filter_var($email, FILTER_VALIDATE_EMAIL)) 
        {
            $error = "error";
        } 
        else 
        {
            $hashed_password = password_hash($password, PASSWORD_DEFAULT);
   
            $query = "INSERT INTO users(username, email, password) VALUES (:username, :email, :password)";
            $statement = $database->prepare($query);
            $statement->bindParam(':username', $username);
            $statement->bindParam(':email', $email);
            $statement->bindParam(':password', $hashed_password);
            $statement->execute();
   
            header("Location: register_confirm.php");
            exit;
        }
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
            <form id="form" name="register" action="register.php" method="post">
                <label for="username"></label>
                <input type="text" name="username" id="username" pattern=".{4,}" placeholder="Username" required>
                <label for="email"></label>
                <input type="text" name="email" id="email" pattern=".+@.+\..+" placeholder="Johan@gmail.com" required>
                <label for="password"></label>
                <input type="password" name="password" id="password" pattern="(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}" placeholder="Password" required>
                <input type="submit" value="Register">
            </form>
            
            <div class = "error">
                <? if ($error): ?>
                    <p class="error"><? echo $error; ?></p>
                <? endif; ?>
            </div>
            <a class="register-link" href="login.php">Sign in</a>
        </div>
        <div id="fi2" class="flex-item">
            <img src="img/signup.jpg">
            <h2>ABOUT US</h2>
            <p>
                Welcome to Plantbook!<br><br>
                This is a website for fellow plantlovers. <br>
                We provide a platform where you can chat all about plants.
                Whether you're an experient plant owner or just getting started, we offer a 
                space for you to learn and share your knowledge. <br><br>
                Join us!
            </p>
        </div>
    </div>
</body>
</html>




