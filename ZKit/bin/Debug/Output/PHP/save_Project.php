<?php include 'connect.php';

try
{
$totalRows = 0;
$inserts = array();
$con = new PDO("mysql:host=$server;dbname=$db", $user, $pass);
$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$Person = null;

if(isset($_POST['Person']))
{
$Person = json_decode($_POST['Person'], true);
$tempSql = $con->prepare('INSERT INTO tblPerson (`Name`, `Age`, `Height`) VALUES (:Name, :Age, :Height)' );

$tempSql->bindParam(':Name', $Person['Name'], PDO::PARAM_STR);
$tempSql->bindParam(':Age', $Person['Age'], PDO::PARAM_INT);
$tempSql->bindParam(':Height', $Person['Height']);
$inserts[] = $tempSql;

}

foreach($inserts as $insert)
{
$insert->execute();
$totalRows = $totalRows + $insert->rowCount();
}

echo($totalRows);
}
catch(Exception $ex)
{
echo('An error occured: '.$ex->Message);
}

?>
