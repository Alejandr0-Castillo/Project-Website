<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "aventura_extrema";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Conexión fallida: " . $conn->connect_error);
}

$nombre = $_POST['nombre'];
$email = $_POST['email'];
$password = password_hash($_POST['password'], PASSWORD_DEFAULT);

$sql = "INSERT INTO usuarios (nombre, email, password) VALUES (?, ?, ?)";
$stmt = $conn->prepare($sql);
$stmt->bind_param("sss", $nombre, $email, $password);

if ($stmt->execute()) {
    echo "Usuario registrado con éxito";
} else {
    echo "Error: " . $stmt->error;
}

$stmt->close();
$conn->close();
?>