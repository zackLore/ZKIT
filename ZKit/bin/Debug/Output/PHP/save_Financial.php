<?php include 'connect.php';

try
{
$inserts = array();
$con = new PDO("mysql:host=$server;dbname=$db", $user, $pass);
$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$Product = null;

if(isset($_POST['Product']))
{
$Product = json_decode($_POST['Product']);
$tempSql = $con=>prepare('INSERT INTO tblProduct (``, ``, ``, `Name`, `Price`, `Count`) VALUES (:, :, :, :Name, :Price, :Count);' );

$tempSql->bindParam(':', $);
$tempSql->bindParam(':', $);
$tempSql->bindParam(':', $);
$tempSql->bindParam(':Name', $Name, PDO::PARAM_STR);
$tempSql->bindParam(':Price', $Price);
$tempSql->bindParam(':Count', $Count, PDO::PARAM_INT);
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
