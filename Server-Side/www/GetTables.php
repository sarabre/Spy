
<?php



// get all table and sow thier name

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "spy-db";

try {
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    $conn->exec("set names utf8mb4");
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

} catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}



$sth = $conn->query('SELECT * FROM `wordsgroup-list` WHERE `count` > 0; ');
$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0)
{

    foreach ($result as $r)
    {
        echo $r['ID'], "\n _";
        echo $r['name'], "\n _";
        echo $r['name-code'], "\n _";


    }
}

?>
