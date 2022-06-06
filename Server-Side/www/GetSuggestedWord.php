<?php

// Get suggested word for admin from DataBase

include('Connection.php');


$sth = $conn->query("SELECT * FROM `suggested-words`; ");
$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0)
{

    foreach ($result as $r)
    {
        echo $r['Word'], " _";
        echo $r['wg-code'], " _";
        echo $r['wg-name'], " _";

    }
}

?>
