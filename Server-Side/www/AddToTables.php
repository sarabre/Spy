<?php

//add this word to table

//INSERT INTO `wg-01` (`ID`, `Word`) VALUES ('5000131', 'test');
//UPDATE `wordsgroup-list` SET `Count`= `Count` + 1 WHERE `ID`='50004'

include('Connection.php');

// ----------------------- Add to Table


$TableCode =  $_GET["TableCode"]  ;
$WordID =  $_GET["WordID"]  ;
$Word =  $_GET["Word"]  ;
echo $Word;


$sth = $conn->query("INSERT INTO `".$TableCode."` VALUES ('".$WordID."', '".$Word."');");
$sth->setFetchMode(PDO::FETCH_ASSOC);




// ---------------------------- Change wg Count in DataBase

$TableID =  $_GET["TableID"]  ;

$sth = $conn->query("UPDATE `wordsgroup-list` SET `Count`= `Count` + 1 WHERE `ID`='".$TableID."'");
$sth->setFetchMode(PDO::FETCH_ASSOC);
