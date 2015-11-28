<?php include 'connect.php';

try
{
$con = new PDO("mysql:host=$server;dbname=$db", $user, $pass);
$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$sel = 'SELECT * FROM tblPerson';
$query = $con->prepare($sel);
$query->execute();

$result = $query->fetchAll();
echo(json_encode($result));
}
catch(Exception $ex)
{
echo('An error occured: '.$ex->Message);
}

?>
