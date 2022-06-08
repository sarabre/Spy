<?php

// delete from wordgroup

//DELETE FROM `wg-01` WHERE 0

include('Connection.php');

// ----------------------- remove to Table

$WordID =  $_GET["WordID"]  ;
$TableCode =  $_GET["TableCode"]  ;


$sth = $conn->query("DELETE FROM `".$TableCode."` WHERE ID = ".$WordID."");
$sth->setFetchMode(PDO::FETCH_ASSOC);

// ---------------------------- Change wg Count in DataBase

$TableID =  $_GET["TableID"]  ;

$sth = $conn->query("UPDATE `wordsgroup-list` SET `Count`= `Count` - 1 WHERE `ID`='".$TableID."'");
$sth->setFetchMode(PDO::FETCH_ASSOC);

?>
