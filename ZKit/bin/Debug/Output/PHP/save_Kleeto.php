<?php include 'connect.php';

try
{
$inserts = array();
$con = new PDO("mysql:host=$server;dbname=$db", $user, $pass);
$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$Grotto = null;

if(isset($_POST['Grotto']))
{
$Grotto = json_decode($_POST['Grotto']);
$tempSql = $con=>prepare('INSERT INTO tblGrotto (`Temk`, `Corenc`) VALUES (:Temk, :Corenc);' );

$tempSql->bindParam(':Temk', $Temk, PDO::PARAM_STR);
$tempSql->bindParam(':Corenc', $Corenc);
$inserts.push($tempSql);

}

foreach($inserts as $insert)
{
$insert->execute();
}

}
catch(Exception $ex)
{
echo('An error occured: '.$ex->Message); );
}

?>
