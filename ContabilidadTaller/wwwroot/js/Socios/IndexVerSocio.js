// MODIFICAR NOMBRE Y APELLIDO
function mostrarModificarNombres() {
    const contenedor = document.querySelector('.container-nombreApellido');

    contenedor.querySelector('.vistaNombreApellido').style.display = 'none';

    contenedor.querySelector('.vistaModificarNombres').style.display = 'block';
    contenedor.querySelector('.vistaModificarApellido').style.display = 'none';
}

function mostrarModificarApellido() {
    const contenedor = document.querySelector('.container-nombreApellido');

    contenedor.querySelector('.vistaNombreApellido').style.display = 'none';

    contenedor.querySelector('.vistaModificarNombres').style.display = 'none';
    contenedor.querySelector('.vistaModificarApellido').style.display = 'block';
}

function ocultarModificarNombresApellido() {
    const contenedor = document.querySelector('.container-nombreApellido');

    contenedor.querySelector('.vistaNombreApellido').style.display = 'block';

    contenedor.querySelector('.vistaModificarNombres').style.display = 'none';
    contenedor.querySelector('.vistaModificarApellido').style.display = 'none';
}

// MODIFICAR CUIT
function mostrarModificarCUIT() {
    const contenedor = document.querySelector('.container-cuit');

    contenedor.querySelector('.vistaCUIT').style.display = 'none';

    contenedor.querySelector('.vistaModificarCUIT').style.display = 'block';
}
function ocultarModificarCUIT() {
    const contenedor = document.querySelector('.container-cuit');

    contenedor.querySelector('.vistaCUIT').style.display = 'block';

    contenedor.querySelector('.vistaModificarCUIT').style.display = 'none';
}

// MODIFICAR DIRECCION
function mostrarModificarDireccion() {
    const contenedor = document.querySelector('.container-direccion');

    contenedor.querySelector('.vistaDireccion').style.display = 'none';

    contenedor.querySelector('.vistaModificarDireccion').style.display = 'block';
}
function ocultarModificarDireccion() {
    const contenedor = document.querySelector('.container-direccion');

    contenedor.querySelector('.vistaDireccion').style.display = 'block';

    contenedor.querySelector('.vistaModificarDireccion').style.display = 'none';
}

// MODIFICAR TELEFONO
function mostrarModificarTelefono() {
    const contenedor = document.querySelector('.container-telefono');

    contenedor.querySelector('.vistaTelefono').style.display = 'none';

    contenedor.querySelector('.vistaModificarTelefono').style.display = 'block';
}
function ocultarModificarTelefono() {
    const contenedor = document.querySelector('.container-telefono');

    contenedor.querySelector('.vistaTelefono').style.display = 'block';

    contenedor.querySelector('.vistaModificarTelefono').style.display = 'none';
}

// AGREGAR EMAIL
function mostrarAgregarEmail(elemento) {
    const contenedor = elemento.closest('.container-emails');

    contenedor.querySelector('.vistaBotonAgregarEmail').style.display = 'none';
    contenedor.querySelector('.vistaAgregarEmail').style.display = 'block';
}
function ocultarAgregarEmail(elemento) {
    const contenedor = elemento.closest('.container-emails');

    contenedor.querySelector('.vistaBotonAgregarEmail').style.display = 'block';
    contenedor.querySelector('.vistaAgregarEmail').style.display = 'none';
}

// MODIFICAR EMAIL
function mostrarModificarEmail(elemento, index) {
    const contenedor = elemento.closest('.container-emails');

    contenedor.querySelector(`.vistaEmail-${index}`).style.display = 'none';
    contenedor.querySelector(`.vistaModificarEmail-${index}`).style.display = 'block';
}
function ocultarModificarEmail(elemento, index) {
    const contenedor = elemento.closest('.container-emails');

    contenedor.querySelector(`.vistaEmail-${index}`).style.display = 'block';
    contenedor.querySelector(`.vistaModificarEmail-${index}`).style.display = 'none';
}

// AGREGAR NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'none';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'block';
}
function ocultarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'block';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'none';
}

// MODIFICAR NOTA
function mostrarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaNota').style.display = 'none';
    contenedor.querySelector('.vistaModificarNota').style.display = 'block';
}
function ocultarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaNota').style.display = 'block';
    contenedor.querySelector('.vistaModificarNota').style.display = 'none';
}

// MODIFICAR CONTRASEñA
function mostrarModificarContraseña() {
    const contenedor = document.querySelector('.container-contraseña');

    contenedor.querySelector('.vistaModificarContraseña').style.display = 'block';
}
function ocultarModificarContraseña() {
    const contenedor = document.querySelector('.container-contraseña');

    contenedor.querySelector('.vistaModificarContraseña').style.display = 'none';
}

// MOSTRAR/OCULTAR CONTRASEñA
function togglePasswordActual() {
    var input = document.getElementById("passwordActual");
    const icono = document.getElementById('passwordActual');

    if (input.type === "password") {
        input.type = "text";

        icono.classList.remove('bi-eye');
        icono.classList.add('bi-eye-slash');
    } else {
        input.type = "password";

        icono.classList.remove('bi-eye-slash');
        icono.classList.add('bi-eye');
    }
}

function togglePasswordNueva() {
    var input = document.getElementById("passwordNueva");
    const icono = document.getElementById('iconoOjoNueva');

    if (input.type === "password") {
        input.type = "text";

        icono.classList.remove('bi-eye');
        icono.classList.add('bi-eye-slash');
    } else {
        input.type = "password";

        icono.classList.remove('bi-eye-slash');
        icono.classList.add('bi-eye');
    }
}

function togglePasswordNuevaRepetida() {
    var input = document.getElementById("passwordNuevaRepetida");
    const icono = document.getElementById('iconoOjoNuevaRepetida');

    if (input.type === "password") {
        input.type = "text";

        icono.classList.remove('bi-eye');
        icono.classList.add('bi-eye-slash');
    } else {
        input.type = "password";

        icono.classList.remove('bi-eye-slash');
        icono.classList.add('bi-eye');
    }
}

// BORRAR SOCIO
function mostrarBorrarSocio() {
    const contenedor = document.querySelector('.container-borrarSocio').style.display = 'block';
}
function ocultarBorrarSocio() {
    const contenedor = document.querySelector('.container-borrarSocio').style.display = 'none';
}

// SELECCIONAR SOCIO
document.querySelectorAll('.opcion-socio').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id = this.getAttribute('data-id');
        const nombre = this.getAttribute('data-nombre');

        document.getElementById('btnSocio').innerText = nombre;

        document.getElementById('idSocioSeleccionado').value = id;
    });
});

// VER/OCULTAR CONTRASEñA
function togglePassword() {
    var input = document.getElementById("passSocio");
    // Si es password lo pasa a text, y viceversa
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}

// COPIAR CUIT
function copiarCuit(cuit) {
    navigator.clipboard.writeText(cuit).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

// COPIAR EMAIL
function copiarEmail(cuit) {
    navigator.clipboard.writeText(cuit).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}