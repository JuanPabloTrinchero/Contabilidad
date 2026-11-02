// NUMERO PRESUPUESTO
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

    var input = document.getElementById('inputFinalPresupuesto');
    input.value = 1;
    input.type = 'hidden';
}

// PEDIDOS
function ocultarPedidos(elemento) {
    const contenedor = elemento.closest('.container-agregarItems');

    contenedor.querySelector('.VistaBoton').style.display = 'block';
    contenedor.querySelector('.VistaPedidos').style.display = 'none';
}

function mostrarPedidos(elemento) {
    const contenedor = elemento.closest('.container-agregarItems');

    contenedor.querySelector('.VistaBoton').style.display = 'none';
    contenedor.querySelector('.VistaPedidos').style.display = 'block';
}

// AGREGAR NUEVA NOTAS
function agregarNuevaNota() {
    const contenedor = document.getElementById('listaNotas');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                        <textarea type="text" id="archivoCargado" name="notas" class="form-control-sm" style="width: 50%; height: 100px" placeholder="Nueva nota..."></textarea>
                        <button type="button" class="btn btn-danger btn-eliminarNotaNueva2 btn-sm px-3 text-nowrap">Quitar fila</button>
                            `;

    fila.querySelector('.btn-eliminarNotaNueva2').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}

// AGREGAR NUEVO ARCHIVO
function agregarNuevoArchivo() {
    const contenedor = document.getElementById('listaArchivos');

    const fila = document.createElement('div');
    fila.className = 'archivo-item d-flex align-items-center';

    fila.innerHTML = `
                            <input type="file" id="archivoCargado" name="archivos" class="form-control-sm">
                            <button type="button" class="btn btn-warning btn-limpiar btn-sm px-3 text-nowrap">Limpiar archivo</button>
                            <button type="button" class="btn btn-danger btn-eliminar btn-sm px-3 text-nowrap">Quitar fila</button>
                        `;

    fila.querySelector('.btn-limpiar').addEventListener('click', function () {
        fila.querySelector('input[type="file"]').value = '';
    });

    fila.querySelector('.btn-eliminar').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}


// FILTRAR POR DESCRIPCION
function filtrarTabla() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pedido");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcion");
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

    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaPedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'fila-pedido');

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formNuevoPresupuesto");
    const btnGuardar = document.getElementById("btnGuardar");

    const inputNumero = document.querySelector('input[name="numeroPresupuesto"]');
    const inputFecha = document.querySelector('input[name="fecha"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";

        const checkboxes = form.querySelectorAll('input[name="idItems"]:checked');
        const pedidoSeleccionado = checkboxes.length > 0;

        btnGuardar.disabled = !(fechaValida && pedidoSeleccionado && numeroValido);
    }

    if (inputNumero) {
        inputNumero.addEventListener("change", validarFormulario);
    }

    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }

    form.addEventListener("change", function (e) {
        if (e.target.name === "idItems") {
            validarFormulario();
        }
    });

    validarFormulario();
});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formPresupuestoReemplazo");
    const btnGuardar = document.getElementById("btnGuardar");

    const inputFecha = document.querySelector('input[id="fecha"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";

        btnGuardar.disabled = !fechaValida;
    }

    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }

    validarFormulario();
});

// MOSTRAR BUSCAR POR DESCRIPCION
function mostrarBuscarPorDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaBotonBuscarDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarDescripcion').style.display = 'block';
}
function ocultarBuscarPorDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaBotonBuscarDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarDescripcion').style.display = 'none';
}