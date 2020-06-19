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

$sql = "SELECT * FROM student ORDER BY stuid";
$stmt = $pdo->prepare($sql);
//Thiết lập kiểu dữ liệu trả về
$stmt->setFetchMode(PDO::FETCH_ASSOC);
$stmt->execute();
$resultSet = $stmt->fetchAll();
echo '<p>Students information:</p>';

?>
<div id="container">
<table class="table table-bordered table-condensed">
    <thead>
      <tr>
        <th>Ma khach hang</th>
        <th>Ten khach hang</th>
        <th>sdt khach hang</th>
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

<div id="container">
<table class="table table-bordered table-condensed">
    <thead>
      <tr>
        <th>Ma hoa don</th>
        <th>Ngay hoa don</th>
        <th>Ma nhan vien</th>
        <th>Ma khach hang</th>
      </tr>
    </thead>
    <tbody>
      <?php
      // tạo vòng lặp 
         //while($r = mysql_fetch_array($result)){
             foreach ($resultSet as $row) {
      ?>
   
      <tr>
        <td scope="row"><?php echo $row['mahoadon'] ?></td>
        <td><?php echo $row['ngayhoadon'] ?></td>
        <td><?php echo $row['manhanvien'] ?></td>
        <td><?php echo $row['makhachhang'] ?></td>
        
      </tr>
     
      <?php
        }
      ?>
    </tbody>
  </table>
</div>

<div id="container">
<table class="table table-bordered table-condensed">
    <thead>
      <tr>
        <th>Ma hoa don</th>
        <th>Ma san pham</th>
        <th>So luong</th>
        <th>Gia san pham</th>
      </tr>
    </thead>
    <tbody>
      <?php
      // tạo vòng lặp 
         //while($r = mysql_fetch_array($result)){
             foreach ($resultSet as $row) {
      ?>
   
      <tr>
        <td scope="row"><?php echo $row['mahoadon'] ?></td>
        <td><?php echo $row['masanpham'] ?></td>
        <td><?php echo $row['so luong'] ?></td>
        <td><?php echo $row['gia san pham'] ?></td>
        
      </tr>
     
      <?php
        }
      ?>
    </tbody>
  </table>
</div>

<div id="container">
<table class="table table-bordered table-condensed">
    <thead>
      <tr>
        <th>Ma san pham</th>
        <th>Ten san pham</th>
        <th>Size</th>
        <th>Gia san pham</th>
        <th>Ma cung cap</th>
        <th>So du</th>
      </tr>
    </thead>
    <tbody>
      <?php
      // tạo vòng lặp 
         //while($r = mysql_fetch_array($result)){
             foreach ($resultSet as $row) {
      ?>
   
      <tr>
        <td scope="row"><?php echo $row['masanpham'] ?></td>
        <td><?php echo $row['tensanpham'] ?></td>
        <td><?php echo $row['size'] ?></td>
        <td><?php echo $row['gia san pham'] ?></td>
        <td><?php echo $row['macungcap'] ?></td>
        <td><?php echo $row['sodu'] ?></td>
        
      </tr>
     
      <?php
        }
      ?>
    </tbody>
  </table>
</div>
</body>
</html>
