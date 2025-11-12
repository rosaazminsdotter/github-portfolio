<?php include 'header.php'; ?>
<link rel="stylesheet" href="css/posts.css">
</head>

<body>
<?php

if ($_SERVER['REQUEST_METHOD'] !== 'POST' || empty($_SERVER['HTTP_X_REQUESTED_WITH']) || strtolower($_SERVER['HTTP_X_REQUESTED_WITH']) !== 'xmlhttprequest') 
{
    header("Location: index.php");
}

session_start();
$database = new SQLite3("db/databas.db");
$newCount = $_POST['newCount'];

if ($_SERVER['REQUEST_METHOD'] == 'POST' && isset($_POST['comment'])) 
{
    $user_id = $_SESSION['user_id'];
    $date = date("Y-m-d H:i:s");
    $comment = $_POST['comment'];

    $query = "INSERT INTO comment (user_id, date, comment) VALUES (:user_id, :date, :comment)";
    $statement = $database->prepare($query);
    $statement->bindParam(':user_id', $user_id);
    $statement->bindParam(':date', $date);
    $statement->bindParam(':comment', $comment);
    $statement->execute();
}

$query = "SELECT * 
FROM comment 
JOIN users ON comment.user_id = users.user_id 
ORDER BY comment.date DESC 
LIMIT $newCount";

$result = $database->query($query);

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
}

$database->close();
?>
</body>