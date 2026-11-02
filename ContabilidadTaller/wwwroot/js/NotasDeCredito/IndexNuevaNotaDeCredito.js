// MOSTRAR ITEMS
function mostrarItems(elemento) {
    const contenedor = elemento.closest('.container-items');

    contenedor.querySelector('.vistBotonVerItems').style.display = 'none';
    contenedor.querySelector('.vistaItems').style.display = 'block';
}
function ocultarItems(elemento) {
    const contenedor = elemento.closest('.container-items');

    contenedor.querySelector('.vistBotonVerItems').style.display = 'block';
    contenedor.querySelector('.vistaItems').style.display = 'none';
}


// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formNotaDeCredito");
    const btnGuardar = document.getElementById("btnGuardar");

    const inputFecha = document.querySelector('input[id="fecha"]');
    const inputPuesto = document.querySelector('input[id="puesto"]');
    const inputNumero = document.querySelector('input[id="numero"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const puestoValido = inputPuesto && inputPuesto.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";

        const checkboxes = form.querySelectorAll('input[id="idFacturaAsociada"]:checked');
        const pedidoSeleccionado = checkboxes.length > 0;

        btnGuardar.disabled = !(fechaValida && puestoValido && numeroValido && pedidoSeleccionado);
    }
    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }
    if (inputPuesto) {
        inputPuesto.addEventListener("input", validarFormulario);
    }
    if (inputNumero) {
        inputNumero.addEventListener("input", validarFormulario);
    }

    form.addEventListener("change", function (e) {
        if (e.target.id === "idFacturaAsociada") {
            validarFormulario();
        }
    });

    validarFormulario();
});

// FILTRAR POR CODIGO
function filtrarTabla() {
    let input = document.getElementById("filtroCodigo");
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

function mostrarBuscarFacturaPorCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaBotonBuscarCodigo').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigo').style.display = 'block';
}
function ocultarBuscarFacturaPorCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaBotonBuscarCodigo').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigo').style.display = 'none';
}