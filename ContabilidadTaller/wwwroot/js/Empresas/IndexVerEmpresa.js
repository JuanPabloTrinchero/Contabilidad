// MODIFICAR NOMBRE EMPRESA
function mostrarModificarNombreEmpresa() {
    document.querySelector('.vistaNombreEmpresa').style.display = 'none';
    document.querySelector('.vistaModificarNombre').style.display = 'block';

    document.querySelector('#botonModifNombre').disabled = true;
}

function ocultarModificarNombreEmpresa() {
    document.querySelector('.vistaNombreEmpresa').style.display = 'block';
    document.querySelector('.vistaModificarNombre').style.display = 'none';

    document.querySelector('#botonModifNombre').disabled = false;
}

// MODIFICAR CUIT
function mostrarModificarCuit() {
    document.querySelector('.vistaCuit').style.display = 'none';
    document.querySelector('.vistaModificarCuit').style.display = 'block';

    document.querySelector('#botonModifCuit').disabled = true;
}
function ocultarModificarCuit() {
    document.querySelector('.vistaCuit').style.display = 'block';
    document.querySelector('.vistaModificarCuit').style.display = 'none';

    document.querySelector('#botonModifCuit').disabled = false;
}
// COPIAR CUIT
function copiarCuit(cuit) {
    navigator.clipboard.writeText(cuit).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

// MODIFICAR DIRECCION
function mostrarModificarDireccion() {
    document.querySelector('.vistaDireccion').style.display = 'none';
    document.querySelector('.vistaModificarDireccion').style.display = 'block';

    document.querySelector('#botonModifDireccion').disabled = true;
}
function ocultarModificarDireccion() {
    document.querySelector('.vistaDireccion').style.display = 'block';
    document.querySelector('.vistaModificarDireccion').style.display = 'none';

    document.querySelector('#botonModifDireccion').disabled = false;
}

// MOSTRAR EMPLEADOS
function mostrarEmpleados(elemento) {
    const contenedor = elemento.closest('.container-empleados');

    contenedor.querySelector('.vistaBotonMostrarEmpleados').style.display = 'none';
    contenedor.querySelector('.vistaMostrarEnpleados').style.display = 'block';
}
function ocultarEmpleados(elemento) {
    const contenedor = elemento.closest('.container-empleados');

    contenedor.querySelector('.vistaBotonMostrarEmpleados').style.display = 'block';
    contenedor.querySelector('.vistaMostrarEnpleados').style.display = 'none';
}
// COPIAR EMAIL;
function copiarEmail(email) {
    navigator.clipboard.writeText(email).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

// VER NOTAS
function mostrarNotas(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonVerNotas').style.display = 'none';
    contenedor.querySelector('.vistaVerNotas').style.display = 'block';
}
function ocultarNotas(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonVerNotas').style.display = 'block';
    contenedor.querySelector('.vistaVerNotas').style.display = 'none';
}

// MODIFICAR NOTAS
function mostrarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaNota').style.display = 'none';
    contenedor.querySelector('.vistaModificarNota').style.display = 'block';
}

function ocultarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaNota').style.display = 'block';
    contenedor.querySelector('.vistaModificarNota').style.display = 'none';
}
// AGREGAR NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-nuevaNota');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'none';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'block';
}

function ocultarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-nuevaNota');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'block';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'none';
}

// BORRAR EMPRESA
function mostrarEliminarEmpresa() {
    document.querySelector('.container-borrarEmpresa').style.display = 'block';

    const boton = document.querySelector('#botonBorrarEmpresa');
    boton.disabled = true;
    boton.style.color = 'grey';
}

function ocultarEliminarEmpresa(elemento) {
    document.querySelector('.container-borrarEmpresa').style.display = 'none';

    const boton = document.querySelector('#botonBorrarEmpresa');
    boton.disabled = false;
    boton.style.color = 'white';
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

