<?php

//with table code show all words in table

//SELECT * FROM `wg-01`;

//

include('Connection.php');


$TableCode =  $_GET["Code"]  ;


$sth = $conn->query("SELECT * FROM `" . $TableCode . "` ; ");
$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0)
{

    foreach ($result as $r)
    {
        echo $r['ID'], "_";
        echo $r['Word'], "_";

    }
}

?>
