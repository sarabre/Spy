<?php

//check admins

//SELECT * FROM `admin` WHERE `username` = '' AND`password` = '';

include('Connection.php');


$UserName =  $_GET["UserName"]  ;
$Password =  $_GET["Password"]  ;


//$sth = $conn->query("SELECT * FROM `" . $TableCode . "` ; ");
$sth = $conn->query("SELECT * FROM admin WHERE username = '" . $UserName . "' AND password = '" . $Password . "';");
$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0)
{
    foreach ($result as $r)
    {
      echo $r['name'], " _";
    }
}

?>
