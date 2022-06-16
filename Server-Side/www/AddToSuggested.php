<?php

// add suggested word to table with table code

//INSERT INTO `suggested-words` VALUES ('کلمه پیشنهادی', '50002', 'wg-01');

include('Connection.php');

        $sth = $conn->prepare('INSERT INTO `suggested-words` VALUES (:Word , :wg_code ,:wg_name)');
        try
        {
                $sth->bindParam(':Word', $_GET['word'], PDO::PARAM_STR);
                $sth->bindParam(':wg_code', $_GET['wgcode'], PDO::PARAM_INT);
                $sth->bindParam(':wg_name', $_GET['wgname'], PDO::PARAM_STR);
                $sth->execute();
        }
        catch(Exception $e)
        {
        echo '<h1>An error has ocurred.</h1><pre>',
        $e->getMessage() ,'</pre>';
        }


?>
