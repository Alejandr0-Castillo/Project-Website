document.addEventListener('DOMContentLoaded', function() {
    const registroForm = document.getElementById('registroForm');
    const reservaForm = document.getElementById('reservaForm');

    registroForm.addEventListener('submit', function(e) {
        e.preventDefault();
        const formData = new FormData(registroForm);
        fetch('registrar_usuario.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.text())
        .then(data => {
            alert(data);
            registroForm.reset();
        })
        .catch(error => console.error('Error:', error));
    });

    reservaForm.addEventListener('submit', function(e) {
        e.preventDefault();
        const formData = new FormData(reservaForm);
        fetch('hacer_reserva.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.text())
        .then(data => {
            alert(data);
            reservaForm.reset();
        })
        .catch(error => console.error('Error:', error));
    });

    const alojamientoServicio = document.getElementById('alojamiento-servicio');
    const alojamientoContenido = alojamientoServicio.querySelector('.servicio-contenido');
    const alojamientoImagenes = alojamientoServicio.querySelector('.servicio-imagenes');

    alojamientoServicio.addEventListener('mouseenter', function() {
        alojamientoContenido.style.opacity = '0';
        alojamientoImagenes.style.opacity = '1';
    });

    alojamientoServicio.addEventListener('mouseleave', function() {
        alojamientoContenido.style.opacity = '1';
        alojamientoImagenes.style.opacity = '0';
    });
});

function mostrarPromocion() {
    alert("¡Oferta especial! 20% de descuento en todas nuestras excursiones este mes. ¡Reserva ahora!");
}