// PREGUNTAS OPCIONES
function mostrarInput() {
    document.getElementById('sectionPregunta').style.display = 'none';
    document.getElementById('sectionInputManual').style.display = 'block';
    document.getElementById('sectionContinuarConUno').style.display = 'none';

    document.getElementById('labelInput').innerText = 'Ingrese el número inicial:';

    document.getElementById('inputFinalRemito').type = 'number';
    document.getElementById('inputFinalRemito').focus();
}

function continuarConUno() {
    document.getElementById('sectionPregunta').style.display = 'none';
    document.getElementById('sectionInputManual').style.display = 'none';
    document.getElementById('sectionContinuarConUno').style.display = 'block';

    document.getElementById('labelInput').innerText = 'Se usará el número: 1';

    var input = document.getElementById('inputFinalRemito');
    input.value = 1;
    input.type = 'hidden';
}

// FILTRAR POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pedido");

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

Paginacion('tabla-pedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'fila-pedido');

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formNuevoRemito");
    const btnGuardar = document.getElementById("btnGuardar");

    const inputNumero = document.querySelector('input[name="numeroRemito"]');
    const inputFecha = document.querySelector('input[name="fecha"]');

    function validarFormulario() {
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";

        const origenMarcado = form.querySelectorAll('.origen-pedido:checked').length > 0;
        const destinoMarcado = form.querySelectorAll('.destino-entrega:checked').length > 0;

        const pedidoSeleccionado = origenMarcado || destinoMarcado;

        btnGuardar.disabled = !(numeroValido && fechaValida && pedidoSeleccionado);
    }

    if (inputNumero) {
        inputNumero.addEventListener("input", validarFormulario);
    }

    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }

    form.addEventListener("change", function (e) {
        if (e.target.classList.contains("origen-pedido") || e.target.classList.contains("destino-entrega")) {
            validarFormulario();
        }
    });

    validarFormulario();
});

// SELECCIONAR PEDIDO O ENTREGAS
function seleccionarTodos(origen) {
    const fila = origen.closest('.fila-pedido');

    const destinos = fila.querySelectorAll('.destino-entrega');

    destinos.forEach(destino => {
        destino.checked = origen.checked;
    });
}

function verificarDestinos(destinoClickeado) {
    const fila = destinoClickeado.closest('.fila-pedido');
    if (!fila) return;

    const origen = fila.querySelector('.origen-pedido');
    const destinos = fila.querySelectorAll('.destino-entrega');

    const algunoMarcado = Array.from(destinos).some(destino => destino.checked);

    if (origen && !origen.disabled) {
        origen.checked = algunoMarcado;
    }
}

// ENVIAR DATOS
document.getElementById('formNuevoRemito').addEventListener('submit', function (e) {

    let todosLosInputs = this.querySelectorAll('input[name^="items"]');
    todosLosInputs.forEach(input => {

        if (!input.dataset.originalName) {
            input.dataset.originalName = input.name;
        }

        input.removeAttribute('name');
    });

    let indexPedido = 0;

    let filasPedidosMarcadas = document.querySelectorAll('.fila-pedido:has(.origen-pedido:checked)');

    filasPedidosMarcadas.forEach(fila => {

        let checkboxPedido = fila.querySelector('.origen-pedido');
        checkboxPedido.name = `items[${indexPedido}].IdPedido`;

        let indexEntrega = 0;

        let entregasMarcadas = fila.querySelectorAll('.destino-entrega:checked');

        entregasMarcadas.forEach(checkboxRecep => {

            checkboxRecep.name = `items[${indexPedido}].Entregas[${indexEntrega}].idEntrega`;

            indexEntrega++;
        });

        if (indexEntrega > 0) {
            indexPedido++;
        } else {
            checkboxOC.removeAttribute('name');
        }
    });
});

// LIMPIAR ARCHIVO
function LimpiarArchivo(elemento) {
    const contenedor = elemento.closest('.cargarArchivo');
    const inputArchivo = contenedor.querySelector('input[name="archivo"]');

    if (inputArchivo) {
        inputArchivo.value = '';
        inputArchivo.files = new DataTransfer().files;
    }
}

// MOSTRAR BUSCAR POR DESCRIPCION
function mostrarBuscarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaBotonBuscarDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarDescripcion').style.display = 'block';
}
function ocultarBuscarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaBotonBuscarDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarDescripcion').style.display = 'none';
}