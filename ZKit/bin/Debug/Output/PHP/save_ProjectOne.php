<?php include 'connect.php';

try
{
$inserts = array();
$con = new PDO("mysql:host=$server;dbname=$db", $user, $pass);
$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$Klorp = null;

if(isset($_POST['Klorp']))
{
$Klorp = json_decode($_POST['Klorp']);
$tempSql = $con=>prepare('INSERT INTO tblKlorp (``, ``, `Name`, `Vort`) VALUES (:, :, :Name, :Vort);' );

$tempSql->bindParam(':', $);
$tempSql->bindParam(':', $);
$tempSql->bindParam(':Name', $Name, PDO::PARAM_STR);
$tempSql->bindParam(':Vort', $Vort);
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
