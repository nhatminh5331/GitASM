<?php
if (empty(getenv("DATABASE_URL"))){
    echo '<p>The DB does not exist</p>';
    $pdo = new PDO('pgsql:host=localhost;port=5432;dbname=mydb', 'postgres', '123456');
}  else {
     
   $db = parse_url(getenv("DATABASE_URL"));
   $pdo = new PDO("pgsql:" . sprintf(
        "host=ec2-54-86-170-8.compute-1.amazonaws.com;port=5432;user=bnsnwsctzafqeb;password=533d2e7c7b60bc1d45ee95519797a4864d859072633737b56b410db03902bfde;dbname=ddol0brkc18c4i",
        $db["host"],
        $db["port"],
        $db["user"],
        $db["pass"],
        ltrim($db["path"], "/")
   ));
}  

if($pdo === false){
     echo "ERROR: Could not connect Database";
}

/
$sql = "INSERT INTO bangkhachhang(makhachhang, tenkhachhang, sdtkhachhang, diachi)"
        . " VALUES('$_POST[makhachhang]','$_POST[tenkhachhang]','$_POST[sdtkhachhang]','$_POST[diachi]')";
$stmt = $pdo->prepare($sql);
//$stmt->execute();
 if (is_null($_POST[makhachhang])) {
   echo "makhachhang must be not null";
 }
 else
 {
    if($stmt->execute() == TRUE){
        echo "Data entry successful.";
    } else {
        echo "Error inserting record: ";
    }
 }
?>