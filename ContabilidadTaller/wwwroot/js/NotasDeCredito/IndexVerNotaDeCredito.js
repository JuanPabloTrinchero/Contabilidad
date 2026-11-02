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

// AGREGAR FACTURA
function mostrarAgregarFactura(elemento) {
    const contenedor = elemento.closest('.container-agregarFactura');

    contenedor.querySelector('.vistaBotonAgregarFactura').style.display = 'none';
    contenedor.querySelector('.vistaAgregarFactura').style.display = 'block';
}
function ocultarAgregarFactura(elemento) {
    const contenedor = elemento.closest('.container-agregarFactura');

    contenedor.querySelector('.vistaBotonAgregarFactura').style.display = 'block';
    contenedor.querySelector('.vistaAgregarFactura').style.display = 'none';
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
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaNota').style.display = 'none';
    contenedor.querySelector('.vistaModificarNota').style.display = 'block';
}
function ocultarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaNota').style.display = 'block';
    contenedor.querySelector('.vistaModificarNota').style.display = 'none';
}

// BORRAR NOTA DE CREDITO
function mostrarBorrarNotaDeCredito() {
    const contenedor = document.querySelector('.container-borrarNDC').style.display = 'block';
}
function ocultarBorrarNotaDeCredito() {
    const contenedor = document.querySelector('.container-borrarNDC').style.display = 'none';
}

// DESMARCAR INPUT RADIO
function ToggleRadio(radio) {
    if (radio.getAttribute('data-checked') === 'true') {
        radio.checked = false;
        radio.removeAttribute('data-checked');
    } else {
        document.querySelectorAll(`input[name="${radio.name}"]`).forEach(r => {
            r.removeAttribute('data-checked');
        });
        radio.checked = true;
        radio.setAttribute('data-checked', 'true');
    }
}

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

// LIMPIAR ARCHIVO A CARGAR
document.getElementById('btnLimpiarArchivo').addEventListener('click', function () {
    document.getElementById('cargarArchivo').value = '';
});

// AGREGAR ARCHIVO
function mostrarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';
}

// FILTRAR FACTURA POR CODIGO
function filtrarTabla() {
    let input = document.getElementById("filtroCodigoFactura");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-factura");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-codigo");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.classList.remove('filtrado-oculto');
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    const selectCant = document.getElementById('cantFacturas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaFacturas', 'prevFacturas', 'nextFacturas', 'numFacturas', 'cantFacturas', 'fila-factura');


// BUSCAR FACTURA POR CODIGO
function mostrarBuscarCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaBotonBuscarPorCodigo').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorCodigo').style.display = 'block';
}
function ocultarBuscarCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaBotonBuscarPorCodigo').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorCodigo').style.display = 'none';
}