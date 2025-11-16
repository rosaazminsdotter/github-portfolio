<?
    session_start();
    if(!isset($_SESSION['user_id'])) 
    {
        header("Location: login.php"); 
        exit;
    }
    else
    {
        $username = $_SESSION['username']; 
        $password = $_SESSION['password']; 
        $email = $_SESSION['email']; 
        $user_id = $_SESSION['user_id']; 
    } 

    include 'header.php';

?>
<link rel="stylesheet" href="css/profile.css">
</head>
<body>
    <div class="flex-container">
        <div class = "head">
            <a href="index.php">Back</a>
        </div>

        <div class="flex-item">
            <img src="<?php echo $_SESSION['picture']; ?>" alt="Sun" width="10%"><br>   
            <h2><?php echo $username; ?></h2>
                <p>
                    <br>
                    Email: <?php echo $email; ?><br>
                    User ID: <?php echo $user_id; ?><br><br><br>
                    <a href="edit_profile.php">Edit profile</a>
                    <br>
                    <a href="logout.php">Sign out</a>
                </p>
        </div>
    </div>
</body>





