<!DOCTYPE html>
<html>
    <head>
<title>Insert data to PostgreSQL with php - creating a simple web application</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style>
li {
list-style: none;
}
</style>
</head>
<body>
<h1>INSERT DATA TO DATABASE</h1>
<h2>Enter data into student table</h2>
<ul>
    <form name="InsertData" action="InsertData.php" method="POST" >
<li>Ma khach hang:</li><li><input type="text" name="makhachhang" /></li>
<li>Ten khach hang:</li><li><input type="text" name="tenkhachhang" /></li>
<li>SDT khach hang:</li><li><input type="text" name="sdtkhachhang" /></li>
<li>Dia chi khach hang:</li><li><input type="text" name="diachi" /></li>
<li><input type="submit" /></li>
</form>
</ul>

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

//Khởi tạo Prepared Statement
//$stmt = $pdo->prepare('INSERT INTO student (stuid, fname, email, classname) values (:id, :name, :email, :class)');

//$stmt->bindParam(':id','SV03');
//$stmt->bindParam(':name','Ho Hong Linh');
//$stmt->bindParam(':email', 'Linhhh@fpt.edu.vn');
//$stmt->bindParam(':class', 'GCD018');
//$stmt->execute();
//$sql = "INSERT INTO student(stuid, fname, email, classname) VALUES('SV02', 'Hong Thanh','thanhh@fpt.edu.vn','GCD018')";
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
        echo "Record inserted successfully.";
    } else {
        echo "Error inserting record: ";
    }
 }
?>
</body>
</html>
