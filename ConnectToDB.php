<!DOCTYPE html>
<html>
<body>

<h1>DATABASE CONNECTION</h1>

<?php
ini_set('display_errors', 1);
echo "Hello Cloud computing class 0818!";
?>

<?php


if (empty(getenv("DATABASE_URL"))){
    echo '<p>The DB does not exist</p>';
    $pdo = new PDO('pgsql:host=localhost;port=5432;dbname=mydb', 'postgres', '123456');
}  else {
     echo '<p>The DB exists</p>';
     echo getenv("dbname");
   $db = parse_url(getenv("DATABASE_URL"));
   $pdo = new PDO("pgsql:" . sprintf(
        "host=ec2-52-207-25-133.compute-1.amazonaws.com;port=5432;user=tkaburwugnialb;password=76d733e3f5de252357c400dead9f156bebe69c04a6198fd2be0b18d5a5795c5d;dbname=dec4erf3qenqd8",
        $db["host"],
        $db["port"],
        $db["user"],
        $db["pass"],
        ltrim($db["path"], "/")
   ));
}  

$sql = "SELECT * FROM khachhang ORDER BY makhachhang";
$stmt = $pdo->prepare($sql);
//Thiết lập kiểu dữ liệu trả về
$stmt->setFetchMode(PDO::FETCH_ASSOC);
$stmt->execute();
$resultSet = $stmt->fetchAll();
echo '<p>Thong tin khach hang:</p>';

?>
<div id="container">
<table class="table table-bordered table-condensed">
    <thead>
      <tr>
        <th>Ma khach hang</th>
        <th>Ten khach hang</th>
        <th>SDT khach hang</th>
        <th>Dia chi khach hang</th>
      </tr>
    </thead>
    <tbody>
      <?php
      // tạo vòng lặp 
         //while($r = mysql_fetch_array($result)){
             foreach ($resultSet as $row) {
      ?>
   
      <tr>
        <td scope="row"><?php echo $row['makhachhang'] ?></td>
        <td><?php echo $row['tenkhachhang'] ?></td>
        <td><?php echo $row['sdtkhachhang'] ?></td>
        <td><?php echo $row['diachikhachhang'] ?></td>
        
      </tr>
     
      <?php
        }
      ?>
    </tbody>
  </table>
</div>
</body>
</html>
