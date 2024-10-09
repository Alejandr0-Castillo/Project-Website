<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "aventura_extrema";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Conexión fallida: " . $conn->connect_error);
}

$servicio = $_POST['tipoServicio'];
$fecha = $_POST['fechaReserva'];
$usuario_id = $_POST['usuario_id']; // Asumimos que se envía el ID del usuario

// Aquí podrías implementar la lógica real de verificación de cupos
// Por ejemplo, verificar si hay disponibilidad para el servicio en la fecha dada

$sql = "INSERT INTO reservas (usuario_id, servicio, fecha) VALUES (?, ?, ?)";
$stmt = $conn->prepare($sql);
$stmt->bind_param("iss", $usuario_id, $servicio, $fecha);

if ($stmt->execute()) {
    echo "Reserva realizada con éxito";
} else {
    echo "Error: " . $stmt->error;
}

$stmt->close();
$conn->close();
?>