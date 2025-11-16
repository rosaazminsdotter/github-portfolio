<?
    session_start();
    if(!isset($_SESSION['user_id'])) 
    {
        header("Location: login.php"); 
        exit;
    }

    if (!isset($_POST['search']) || empty(trim($_POST['search']))) 
    { 
        header("Location: index.php"); 
        exit;
    }
    
    include 'header.php';
?>

<link rel="stylesheet" href="css/search.css">
</head>

<body>

    <div class = "head">
        <a href="index.php">Back</a>
    </div>

    <div class = "body">
        <?
        $database = new SQLite3("db/databas.db");

        $search = $_POST["search"];
        $search = "%" . strtolower($search) . "%"; 

        $query = 
        "SELECT * 
        FROM comment
        JOIN users ON comment.user_id = users.user_id 
        WHERE LOWER(comment.comment) LIKE :search
        ORDER BY comment.date DESC";

        $statement = $database->prepare($query);
        $statement->bindValue(':search', $search, SQLITE3_TEXT);
        $result = $statement->execute();

        $comments = 0;
        
        while ($row = $result->fetchArray(SQLITE3_ASSOC)) 
        {
            echo "<div class='comment-box'>";
            echo "<div class='comment-header'>";
            echo '<img src="' . $row['picture'] . '" alt="Profile Picture" class="profile-picture">';
            echo "<span class='username'>{$row['username']}</span>";
            echo "<span class='date'>{$row['date']}</span>";
            echo "</div>";
            echo "<div class='comment-content'>";
            echo "<p>{$row['comment']}</p>";
            echo "</div>";
            echo "</div>";
            $comments = $comments + 1;

        }

        if($comments == 0)
        {
            echo "<div class='error'>";
            echo "No comments found";
            echo "</div>";
        }
        
        $database->close();
        ?> 
    </div>
</body>
   

