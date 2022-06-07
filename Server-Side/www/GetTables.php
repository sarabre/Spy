
<?php

// get all table and sow thier name

include('Connection.php');


$sth = $conn->query('SELECT * FROM `wordsgroup-list` WHERE `count` > 0; ');
$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0)
{

    foreach ($result as $r)
    {
        echo $r['ID'], "_";
        echo $r['name'], "_";
        echo $r['name-code'], "_";


    }
}

?>
