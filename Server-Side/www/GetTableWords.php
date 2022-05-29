<?php

//with table code show all words in table

//SELECT * FROM `wg-01`;


$servername = "localhost";
$username = "root";
$password = "";
$dbname = "spy-db";
$secretKey = 2003;

try {
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    $conn->exec("set names utf8mb4");
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

} catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}


$TableCode =  $_GET["Code"]  ;


$sth = $conn->query("SELECT * FROM `" . $TableCode . "` ; ");
$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0)
{

    foreach ($result as $r)
    {
        echo $r['ID'], "\n _";
        echo $r['Word'], "\n _";

    }
}

?>
