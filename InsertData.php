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
<li>Student ID:</li><li><input type="text" name="StudentID" /></li>
<li>Full Name:</li><li><input type="text" name="fname" /></li>
<li>Email:</li><li><input type="text" name="email" /></li>
<li>Class:</li><li><input type="text" name="classname" /></li>
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
        "host=ec2-174-129-240-67.compute-1.amazonaws.com;port=5432;user=wrflrxtavasvqh;password=fbfef36049fbd28f1200e3a775a389e014838e86522765e67782f9cf7a3f516b;dbname=d3mmhribgmc6bf",
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
$sql = "INSERT INTO student(stuid, fname, email, classname)"
        . " VALUES('$_POST[StudentID]','$_POST[fname]','$_POST[email]','$_POST[classname]')";
$stmt = $pdo->prepare($sql);
//$stmt->execute();
 if (is_null($_POST[StudentID])) {
   echo "StudentID must be not null";
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
