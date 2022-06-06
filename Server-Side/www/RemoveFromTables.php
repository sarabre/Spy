<?php

//remove this word from suggested word

//DELETE FROM `suggested-words` WHERE word = ""

include('Connection.php');


$Word =  $_GET["Word"]  ;


$sth = $conn->query("DELETE FROM `suggested-words` WHERE word = '".$Word."'");
$sth->setFetchMode(PDO::FETCH_ASSOC);
