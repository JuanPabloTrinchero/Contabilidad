// Modificar FechaEncargo
function mostrarFechaEncargo(elemento) {
    const contenedor = elemento.closest('.container-fechaEncargo');

    contenedor.querySelector('.vistaFechaEncargo').style.display = 'block';
    contenedor.querySelector('.vistaModificarFechaEncargo').style.display = 'none';
}
function mostrarModificarFechaEncargo(elemento) {
    const contenedor = elemento.closest('.container-fechaEncargo');

    contenedor.querySelector('.vistaFechaEncargo').style.display = 'none';
    contenedor.querySelector('.vistaModificarFechaEncargo').style.display = 'block';
}

// Modificar Cantidad
function mostrarCantidad(elemento) {
    const contenedor = elemento.closest('.container-cantidad');

    contenedor.querySelector('.vistaCantidad').style.display = 'block';
    contenedor.querySelector('.vistaModificarCantidad').style.display = 'none';
}
function mostrarModificarCantidad(elemento) {
    const contenedor = elemento.closest('.container-cantidad');

    contenedor.querySelector('.vistaCantidad').style.display = 'none';
    contenedor.querySelector('.vistaModificarCantidad').style.display = 'block';
}

// Modificar Descripcion
function mostrarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-descripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'none';
}
function mostrarModificarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-descripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'block';
}

// Modificar Precio Unitario
function mostrarPrecioUnitario(elemento) {
    const contenedor = elemento.closest('.container-precioUnitario');

    contenedor.querySelector('.vistaPrecioUnitario').style.display = 'block';
    contenedor.querySelector('.vistaModificarPrecioUnitario').style.display = 'none';
}
function mostrarModificarPrecioUnitario(elemento) {
    const contenedor = elemento.closest('.container-precioUnitario');

    contenedor.querySelector('.vistaPrecioUnitario').style.display = 'none';
    contenedor.querySelector('.vistaModificarPrecioUnitario').style.display = 'block';
}

//MOSTRAR COSTOS
function mostrarCostos(elemento) {
    const contenedor = elemento.closest('.container-costos');

    contenedor.querySelector('.vistaBotonVerCostos').style.display = 'none';
    contenedor.querySelector('.vistaCostos').style.display = 'block';
}
function ocultarCostos(elemento) {
    const contenedor = elemento.closest('.container-costos');

    contenedor.querySelector('.vistaBotonVerCostos').style.display = 'block';
    contenedor.querySelector('.vistaCostos').style.display = 'none';
}

// Modificar Encargado
function mostrarEncargado(elemento) {
    const contenedor = elemento.closest('.container-encargado');

    contenedor.querySelector('.vistaEncargado').style.display = 'block';
    contenedor.querySelector('.vistaModificarEncargado').style.display = 'none';
}
function mostrarModificarEncargado(elemento) {
    const contenedor = elemento.closest('.container-encargado');

    contenedor.querySelector('.vistaEncargado').style.display = 'none';
    contenedor.querySelector('.vistaModificarEncargado').style.display = 'block';
}

// Subir Archivo a Pedido
function mostrarAgregarArchivoAPedido(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivoAPedido').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivoAPedido').style.display = 'block';
}
function ocultarAgregarArchivoAPedido(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivoAPedido').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivoAPedido').style.display = 'none';
}

// Subir Archivo a Pieza
function mostrarAgregarArchivoAPieza(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivoAPieza').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivoAPieza').style.display = 'block';
}
function ocultarAgregarArchivoAPieza(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivoAPieza').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivoAPieza').style.display = 'none';
}

// Limpiar Archivo Seleccionado
function limpiarArchivoPedido(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    const vista = contenedor.querySelector('.vistaAgregarArchivoAPedido');

    const input = vista.querySelector('input[type="file"]');
    input.value = '';
}

// Limpiar Archivo Seleccionado
function limpiarArchivoPieza(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    const vista = contenedor.querySelector('.vistaAgregarArchivoAPieza');

    const input = vista.querySelector('input[type="file"]');
    input.value = '';
}

// AGREGAR NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'none';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'block';
    contenedor.querySelector('.vistaNotas').style.display = 'block';
    contenedor.querySelector('.botonOcultarNotas').style.display = 'none';
    contenedor.querySelector('.botonVerNotas').style.display = 'none';


}
function ocultarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'block';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'none';


    contenedor.querySelector('.vistaBotonVerNotas').style.display = 'block';
    contenedor.querySelector('.vistaNotas').style.display = 'none';
    contenedor.querySelector('.botonOcultarNotas').style.display = 'block';
    contenedor.querySelector('.botonVerNotas').style.display = 'block';
}


// MOSTRAR NOTAS
function mostrarNotas(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonVerNotas').style.display = 'none';
    contenedor.querySelector('.vistaNotas').style.display = 'block';
}
function ocultarNotas(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonVerNotas').style.display = 'block';
    contenedor.querySelector('.vistaNotas').style.display = 'none';
}

// MODIFICAR NOTA
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

// SELECCIONAR QUIEN HISO EN PEDIDO
document.querySelectorAll('.opcion-empleado').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id = this.getAttribute('data-id');
        const nombre = this.getAttribute('data-nombre');

        document.getElementById('btnEmpleado').innerText = nombre;

        if (!id || id === "") {
            document.getElementById('idEmpleadoSeleccionado').value = "";
        } else {
            document.getElementById('idEmpleadoSeleccionado').value = id;
        }
    });
});

// BORRAR PEDIDO
function mostrarBorrarPedido(elemento) {
    const contenedor = elemento.closest('.container-borrar');

    contenedor.querySelector('.vistaBotonBorrarPedido').style.display = 'none';
    contenedor.querySelector('.vistaBorrarPedido').style.display = 'block';
}
function ocultarBorrarPedido(elemento) {
    const contenedor = elemento.closest('.container-borrar');

    contenedor.querySelector('.vistaBotonBorrarPedido').style.display = 'block';
    contenedor.querySelector('.vistaBorrarPedido').style.display = 'none';
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
    const icono = document.getElementById('iconoOjo');

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

// DESELECCIONAR RADIO
document.addEventListener('click', (e) => {
    if (e.target.type === 'radio') {
        const radio = e.target;
        if (radio.dataset.wasChecked === 'true') {
            radio.checked = false;
            radio.dataset.wasChecked = 'false';
        } else {
            // Resetea el estado temporal de los demás radios del grupo
            document.querySelectorAll(`input[type="radio"][name="${radio.name}"]`)
                .forEach(el => el.dataset.wasChecked = 'false');

            radio.dataset.wasChecked = 'true';
        }
    }
});