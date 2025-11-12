<? session_start();
    if(!isset($_SESSION['user_id'])) 
    {
        header("Location: login.php");
        exit; 
    }
    $database = new SQLite3("db/databas.db");

    include 'header.php'; 
?>

    <link rel="stylesheet" href="css/profile.css">
    </head>
            
    <body>
        <div class="flex-container">
            <div class = "head">
                <a href="profile.php">Back</a>
            </div>
            <div class="flex-item">
                <form id="form" name="edit" action="edit_profile.php" enctype="multipart/form-data" method="post"> 
                    <label for="username">Username</label><br>
                    <input type="text" name="username" id="username" pattern = ".{4,}" placeholder= <?echo $_SESSION["username"]?>> <br><br>

                    <label for ="password">Password</label><br>
                    <input type ="text" name="password" id="password" pattern = "(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}" placeholder= "*********"><br><br>

                    <label for ="email">Email</label><br>
                    <input type ="text" name="email" id="email" pattern=".+@.+\..+" placeholder= <?echo $_SESSION["email"]?>><br><br>   
                    
                    <label for="profile_picture">Profile Picture</label><br>
                    <input type="file" name="profile_picture" id="profile_picture"><br><br>
                    
                    <input type="submit" value="Save changes">
                </form>

                <?                    
                    if($_SERVER["REQUEST_METHOD"] == "POST")
                    {
                        $user_id = $_SESSION['user_id']; 

                        if(!empty($_POST['username']))
                        {
                            $new = strtolower($_POST['username']);
                            $q = "SELECT COUNT(*) as count FROM users WHERE username = :username";
                            $s = $database->prepare($q);
                            $s->bindParam(':username', $new);
                            $r = $s->execute();
                            $row = $r->fetchArray(SQLITE3_ASSOC);
                            $count = $row['count'];

                            if ($count > 0)
                            {
                                echo "<div class='fail'>";
                                echo "Username is taken";
                                echo "</div>";
                            }
                            else
                            {
                                if(strlen($new) < 4)
                                {
                                    header("Location: edit_profile.php");
                                    echo "<div class='fail'>";
                                    echo "Username is too short";
                                    echo "</div>";
                                }
                                else
                                {
                                    $query = "UPDATE users SET username = :new WHERE user_id = :user_id";
                                    $statement = $database->prepare($query);
                                    $statement ->bindParam(':new', $new);
                                    $statement->bindParam(':user_id', $user_id);
                                    $statement->execute();
                                    $_SESSION['username'] = $new;
                                    echo "<div class='succeed'>";
                                    echo "Username has been updated";
                                    echo "<br>";
                                    echo "</div>";
                                }
                            } 
                        }

                        if(!empty($_POST['password']))
                        {
                            $pattern = '/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/';

                            if(!preg_match($pattern, $_POST['password']))
                            {
                                echo "<div class='fail'>";
                                echo "Password does not follow pattern";
                                echo "</div>";
                            }
                            else
                            {
                                $new = password_hash($_POST['password'], PASSWORD_DEFAULT);
                                $query = "UPDATE users SET password = :new WHERE user_id = :user_id";
                                $statement = $database->prepare($query);
                                $statement ->bindParam(':new', $new);
                                $statement->bindParam(':user_id', $user_id);
                                $statement->execute();
                                $_SESSION['password'] = $new;
                                echo "<div class='succeed'>";
                                echo "Password has been updated";
                                echo "<br>";
                                echo "</div>";
                            }
                        }

                        if(!empty($_POST['email']))
                        {
                            $new = strtolower($_POST['email']);
                            $q = "SELECT COUNT(*) as count FROM users WHERE email = :email";
                            $s = $database->prepare($q);
                            $s->bindParam(':email', $new);
                            $r = $s->execute();
                            $row = $r->fetchArray(SQLITE3_ASSOC);
                            $count = $row['count'];

                            if ($count > 0)
                            {
                                echo "<div class='fail'>";
                                echo "Email is taken";
                                echo "<br>";
                                echo "</div>";

                            }
                            else if (!filter_var($_POST['email'],FILTER_VALIDATE_EMAIL))
                            {
                                echo "<div class='fail'>";
                                echo "Email is invalid";
                                echo "</div>";
                            }
                            else
                            {
                                    $query = "UPDATE users SET email = :new WHERE user_id = :user_id";
                                    $statement = $database->prepare($query);
                                    $statement ->bindParam(':new', $new);
                                    $statement->bindParam(':user_id', $user_id);
                                    $statement->execute();
                                    $_SESSION['email'] = $new;
                                    echo "<div class='succeed'>";
                                    echo "Email has been updated";
                                    echo "<br>";
                                    echo "</div>";
                            }
                        } 

                        if(isset($_FILES['profile_picture']) && $_FILES['profile_picture']['error'] === UPLOAD_ERR_OK) 
                        {
                            $fileTmpPath = $_FILES['profile_picture']['tmp_name'];
                            $fileName = $_FILES['profile_picture']['name'];
                            $fileSize = $_FILES['profile_picture']['size'];
                            $fileType = $_FILES['profile_picture']['type'];
                            $fileNameCmps = explode(".", $fileName);
                            $fileExtension = strtolower(end($fileNameCmps));

                            $uploadFileDir = 'img/'; 
                            $dest_path = $uploadFileDir . $fileName;

                            $query = "UPDATE users SET picture = :pic WHERE user_id = :user_id";
                            $statement = $database->prepare($query);
                            $statement ->bindParam(':pic', $dest_path);
                            $statement->bindParam(':user_id', $user_id);
                            $statement->execute();
                            $_SESSION['picture'] = $dest_path;

                            if (move_uploaded_file($fileTmpPath, $dest_path)) 
                            {
                                echo "<div class='succeed'>";
                                echo 'File is successfully uploaded';
                                echo "<br>";
                                echo "</div>";
                            }
                            else
                            {
                                echo "<div class='fail'>";
                                echo "Problem uploading file";
                                echo "<br>";
                                echo "</div>";
                            }
                        }
                    }  
                ?>
            </div>
        </div>
    </body>

