// MODIFICAR NUMERO
function mostrarModificarNumero() {
    const contenedor = document.querySelector('.container-numero');

    contenedor.querySelector('.vistaNumero').style.display = 'none';
    contenedor.querySelector('.vistaModificarNumero').style.display = 'block';
}
function ocultarModificarNumero(elemento) {
    const contenedor = elemento.closest('.container-numero');

    contenedor.querySelector('.vistaNumero').style.display = 'block';
    contenedor.querySelector('.vistaModificarNumero').style.display = 'none';
}

// MODIFICAR FECHA
function mostrarModificarFecha() {
    const contenedor = document.querySelector('.container-fecha');

    contenedor.querySelector('.vistaFecha').style.display = 'none';
    contenedor.querySelector('.vistaModificarFecha').style.display = 'block';
}
function ocultarModificarFecha(elemento) {
    const contenedor = elemento.closest('.container-fecha');

    contenedor.querySelector('.vistaFecha').style.display = 'block';
    contenedor.querySelector('.vistaModificarFecha').style.display = 'none';
}

// AGREGAR ITEMS
function mostrarAgregarItems(elemento) {
    const contenedor = elemento.closest('.container-agregarItems');

    contenedor.querySelector('.vistaBotonAgregarItems').style.display = 'none';
    contenedor.querySelector('.vistaAgregarItems').style.display = 'block';
}
function ocultarAgregarItems(elemento) {
    const contenedor = elemento.closest('.container-agregarItems');

    contenedor.querySelector('.vistaBotonAgregarItems').style.display = 'block';
    contenedor.querySelector('.vistaAgregarItems').style.display = 'none';
}

// AGREGAR UN ARCHIVO
function mostrarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';
}

// AGREGAR UNA NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-agregarNota');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'none';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'block';
}

function ocultarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-agregarNota');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'block';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'none';
}

// MODIFICAR NOTA
function mostrarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaModificarNota').style.display = 'block';
    contenedor.querySelector('.vistaNota').style.display = 'none';
}

function ocultarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaModificarNota').style.display = 'none';
    contenedor.querySelector('.vistaNota').style.display = 'block';
}


// BORRAR PRESUPUESTO
function mostrarBorrarPresupuesto(elemento) {
    const contenedor = document.querySelector('.container-borrar').style.display = 'block';
}
function ocultarBorrarPresupuesto() {
    const contenedor = document.querySelector('.container-borrar').style.display = 'none';
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

// OPCION ENVIAR
function mostrarEnviar(elemento) {
    const contenedor = elemento.closest('.container-enviar');

    contenedor.querySelector('.vistaBotonEnviar').style.display = 'none';
    contenedor.querySelector('.vistaEnviar').style.display = 'block';
}

function ocultarEnviar(elemento) {
    const contenedor = elemento.closest('.container-enviar');

    contenedor.querySelector('.vistaBotonEnviar').style.display = 'block';
    contenedor.querySelector('.vistaEnviar').style.display = 'none';
}


// FILTRAR POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFila");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcion");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaAgregarPedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'claseFila');

function validarFormularioEnvio() {
    const checkboxes = document.querySelectorAll('.chk-empleado:checked');

    if (checkboxes.length === 0) {
        alert('Debe seleccionar al menos un empleado para continuar.');
        return false;
    }
    return confirm('Confirmar envio?');
}

// MOSTRAR BUSCAR POR DESCRIPCION
function mostrarBuscarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaBotonBuscarPorDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorDescripcion').style.display = 'block';
}
function ocultarBuscarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaBotonBuscarPorDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorDescripcion').style.display = 'none';
}

// ASIGNAR SOCIO
function mostrarAsignarSocio(elemento) {
    const contenedor = elemento.closest('.container-socio');

    contenedor.querySelector('.vistaSocio').style.display = 'none';
    contenedor.querySelector('.vistaAsignarSocio').style.display = 'block';
}
function ocultarAsignarSocio(elemento) {
    const contenedor = elemento.closest('.container-socio');

    contenedor.querySelector('.vistaSocio').style.display = 'block';
    contenedor.querySelector('.vistaAsignarSocio').style.display = 'none';
}

// LIMPIAR INPUT ARCHIVO
function LimpiarArchivo(elemento) {
    const contenedor = elemento.closest('.vistaAgregarArchivo');
    const inputArchivo = contenedor.querySelector('input[id="archivoPresupuesto"]');

    if (inputArchivo) {
        inputArchivo.value = '';
        inputArchivo.files = new DataTransfer().files;
    }
}