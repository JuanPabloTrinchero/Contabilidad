// MODIFICAR CODIGO
function mostrarModificarCodigo() {
    const contenedor = document.querySelector('.container-codigo');

    contenedor.querySelector('.vistaCodigo').style.display = 'none';
    contenedor.querySelector('.vistaModificarCodigo').style.display = 'block';
}

function ocultarModificarCodigo() {
    const contenedor = document.querySelector('.container-codigo');

    contenedor.querySelector('.vistaCodigo').style.display = 'block';
    contenedor.querySelector('.vistaModificarCodigo').style.display = 'none';
}

// MODIFICAR FECHA
function mostrarModificarFecha() {
    const contenedor = document.querySelector('.container-fecha');

    contenedor.querySelector('.vistaFecha').style.display = 'none';
    contenedor.querySelector('.vistaModificarFecha').style.display = 'block';
}
function ocultarModificarFecha() {
    const contenedor = document.querySelector('.container-fecha');

    contenedor.querySelector('.vistaFecha').style.display = 'block';
    contenedor.querySelector('.vistaModificarFecha').style.display = 'none';
}

// AGREGAR RECEPCION
function mostrarAgregarRecepcion(elemento) {
    const contenedor = elemento.closest('.container-recepciones');

    contenedor.querySelector('.vistaBotonAgregarRecepcion').style.display = 'none';
    contenedor.querySelector('.vistaAgregarRecepcion').style.display = 'block';
}

function ocultarAgregarRecepcion(elemento) {
    const contenedor = elemento.closest('.container-recepciones');

    contenedor.querySelector('.vistaBotonAgregarRecepcion').style.display = 'block';
    contenedor.querySelector('.vistaAgregarRecepcion').style.display = 'none';
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

// AGREGAR PRESUPUESTOS
function mostrarAgregraPresupuesto(elemento) {
    const contenedor = elemento.closest('.container-agregarPresupuesto');

    contenedor.querySelector('.vistaBotonAgregarPresupuesto').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPresupuesto').style.display = 'block';
}

function ocultarAgregraPresupuesto(elemento) {
    const contenedor = elemento.closest('.container-agregarPresupuesto');

    contenedor.querySelector('.vistaBotonAgregarPresupuesto').style.display = 'block';
    contenedor.querySelector('.vistaAgregarPresupuesto').style.display = 'none';
}

// MODIFICAR RECEPCION FACTURADA
function mostrarEstadoFacturadaMostrarBotones(elemento) {
    const contenedor = elemento.closest('.container-opcionFacturada');

    contenedor.querySelector('.vistaPregunta').style.display = 'none';
    contenedor.querySelector('.vistaBotones').style.display = 'block';
}

function ocultarBotonesModificarEstado(elemento) {
    const contenedor = elemento.closest('.container-opcionFacturada');

    contenedor.querySelector('.vistaPregunta').style.display = 'block';
    contenedor.querySelector('.vistaBotones').style.display = 'none';
}

// CARGAR ARCHIVO
function mostrarCargarArchivo(elemento) {
    const contenedor = elemento.closest('.container-cargarArchivo');

    contenedor.querySelector('.vistaBotonCargarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarCargarArchivo(elemento) {
    const contenedor = elemento.closest('.container-cargarArchivo');

    contenedor.querySelector('.vistaBotonCargarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';

    document.getElementById('cargarArchivo').value = '';
}

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formAgregarPresupuestos");
    const btnGuardar = document.getElementById("btnGuardarAgregarPresupuestos");

    function validarFormulario() {
        const checkboxes = form.querySelectorAll('input[id="presupuestoSeleccion"]:checked');
        const presupuestosSeleccionados = checkboxes.length > 0;

        btnGuardar.disabled = !(presupuestosSeleccionados);
    }

    form.addEventListener("change", function (e) {
        if (e.target.id === "presupuestoSeleccion") {
            validarFormulario();
        }
    });

    validarFormulario();
});

// VER ITEMS EN PRESUPUESTO
function mostrarItemsPresupuesto(elemento) {
    const contenedor = elemento.closest('.container-itemsPresupuesto');

    contenedor.querySelector('.vistaBotonVerItemsPresupuesto').style.display = 'none';
    contenedor.querySelector('.vistaItemsPresupuesto').style.display = 'block';
}
function ocultarItemsPresupuesto(elemento) {
    const contenedor = elemento.closest('.container-itemsPresupuesto');

    contenedor.querySelector('.vistaBotonVerItemsPresupuesto').style.display = 'block';
    contenedor.querySelector('.vistaItemsPresupuesto').style.display = 'none';
}

// MODIFICAR NOTA
function mostrarModificarNota(elemento, index) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector(`.vistaNota-${index}`).style.display = 'none';
    contenedor.querySelector(`.vistaModificarNota-${index}`).style.display = 'block';
}
function ocultarModificarNota(elemento, index) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector(`.vistaNota-${index}`).style.display = 'block';
    contenedor.querySelector(`.vistaModificarNota-${index}`).style.display = 'none';
}

// LIMPIAR ARCHIVO SELECCIONADO
document.getElementById('btnLimpiarArchivo').addEventListener('click', function () {
    document.getElementById('cargarArchivo').value = '';
});

// FILTRAR PRESUPUESTO AGREGAR POR NUMERO

function filtrarTablaNumeroPresupuesto() {
    let input = document.getElementById("filtroNumeroPresupuesto");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-presupuesto");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-numero");
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

    const selectCant = document.getElementById('cantPresupuestos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaAgregarPresupuestos', 'prevPresupuestos', 'nextPresupuestos', 'numPresupuestos', 'cantPresupuestos', 'fila-presupuesto');

// MOSTRAR BUSCAR POR NUMERO PRESUPUESTO
function mostrarBuscarNumero(elemento) {
    const contenedor = elemento.closest('.container-tituloNumeroPresupuesto');

    contenedor.querySelector('.vistaBotonBuscarNumero').style.display = 'none';
    contenedor.querySelector('.vistaBuscarNumero').style.display = 'block';
}
function ocultarBuscarNumero(elemento) {
    const contenedor = elemento.closest('.container-tituloNumeroPresupuesto');

    contenedor.querySelector('.vistaBotonBuscarNumero').style.display = 'block';
    contenedor.querySelector('.vistaBuscarNumero').style.display = 'none';
}

// BORRAR ORDEN DE COMPRA
function mostrarBorrarOC() {
    const contenedor = document.querySelector('.container-borrar').style.display = 'block';
}
function ocultarBorrarOC() {
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