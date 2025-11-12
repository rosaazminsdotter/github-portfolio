<?
session_start();
if(!isset($_SESSION['user_id'])) 
{
    header("Location: login.php");
    exit;
}   
    $database = new SQLite3("db/databas.db");
    include 'header.php';
?>
    <script
        src="https://code.jquery.com/jquery-3.7.1.min.js"
        integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo="
        crossorigin="anonymous">
    </script>
    <link rel="stylesheet" href="css/posts.css">
    <script>
       $(document).ready(function() 
       {
            var count = 4; 
            $("button").click(function()
            {
                count = count + 4;
                $(".comments").load("load-comment.php", {newCount: count})
            })  

            $("#comForm").submit(function(event) 
            {        
                count = count + 1;   
                event.preventDefault();
                $.ajax(
                {
                    url: "addPost.php",
                    type: "POST",
                    data: $(this).serialize() + "&newCount=" + count,
                    success: function(response) 
                    {
                        $(".comments").html(response);
                        $("#comForm")[0].reset(); 
                    }
                });
            });

        });
    </script>
</head>

<body>
<h1>PLANTBOK</h1>
    <div class="flex-container">    
        <div class="head">
            <div class = "sf">
                <form id = "searForm" name = "search" action = "searches.php" method="post"> 
                    <label id ="search" for="search"></label>
                    <input type="text" name="search" id="search" placeholder= "Search..." required>
                    <input id = "subSear" type="submit" value="Search">
                </form>
            </div>
            
            <div class="profile">
                <p>
                    <a href="profile.php">
                    <img src="<?php echo $_SESSION['picture']; ?>" alt="Profile Picture" class="profile-link-picture">
                    <br>
                    </a>
                    <a id = "logout" href="logout.php">Sign out</a>
                </p>
            </div>
        </div>  
        
        <div class="body">
            
            <div class = "cf">
                <form id = "comForm" name = "comment" action = "" method="post"> 
                    <label for="comment"></label>
                    <input type="text" name="comment" id="comment" placeholder="Write your comment..." required>
                    <br><br>
                    <input type="submit" id="subcom" value="Comment">
                </form>

                <br> <br>
            </div>

            <div class ="comments">
                <?php
                $query = "SELECT * FROM comment JOIN users ON comment.user_id = users.user_id ORDER BY comment.date DESC LIMIT 4";
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
            </div>
            <button id = "show">Show more comments </button>
        </div>
    </div>
</body>

