// PREGUNTA RECEPCIONES
function mostrarConRecepciones(elemento) {
    const contenedor = elemento.closest('.vistaAgregarRecepcion');

    contenedor.querySelector('#btnAgregarRecepcion').disabled = false;
    contenedor.querySelector('#btnConRecepciones').disabled = true;
    contenedor.querySelector('#btnSinRecepciones').disabled = false;
}
function mostrarSinRecepciones(elemento) {
    const contenedor = elemento.closest('.vistaAgregarRecepcion');

    contenedor.querySelector('#btnAgregarRecepcion').disabled = true;
    contenedor.querySelector('#btnConRecepciones').disabled = false;
    contenedor.querySelector('#btnSinRecepciones').disabled = true;

    const listaRecepciones = document.getElementById('listaRecepciones');

    listaRecepciones.innerHTML = '';
}

// ASIGNAR PRESUPUESTOS
function mostrarAsignarPresupuestos(elemento) {
    const contenedor = elemento.closest('.container-asignarPresupuestos');

    contenedor.querySelector('.vistaBotonAsignarPresupuestos').style.display = 'none';
    contenedor.querySelector('.vistaAsignarPresupuestos').style.display = 'block';
}
function ocultarAsignarPresupuestos(elemento) {
    const contenedor = elemento.closest('.container-asignarPresupuestos');

    contenedor.querySelector('.vistaBotonAsignarPresupuestos').style.display = 'block';
    contenedor.querySelector('.vistaAsignarPresupuestos').style.display = 'none';
}

// AGREGAR RECEPCIONES
let contadorRecep = 0;

function actualizarVisibilidadTitulo() {
    const titulo = document.getElementById('siHayRecepciones');

    if (contadorRecep > 0) {
        titulo.style.display = "block";
    } else {
        titulo.style.display = "none";
    }
}

function agregarRecepcion() {
    const contenedor = document.getElementById('listaRecepciones');
    contadorRecep++;

    actualizarVisibilidadTitulo();

    const fila = document.createElement('div');
    fila.className = 'recepcion-item';

    fila.innerHTML = `
                            <input type="text" name="recepciones" placeholder="Nueva Recepcion..." style="width: 400px" required>
                            <button type="button" class="btn btn-danger btn-remove" onclick="contadorRecep--; actualizarVisibilidadTitulo(); this.parentElement.remove();">X</button>
                            `;
    contenedor.appendChild(fila);
}

// AGREGAR NUEVA NOTA CON RECEPCION
function agregarCampoNotas() {
    const contenedor = document.getElementById('listaNotas');

    const fila = document.createElement('div');
    fila.className = 'nota-item';

    fila.innerHTML = `
                        <textarea type="text" name="notas" placeholder="Nueva Nota..." style="width: 50%; height: 100px" required></textarea>
                        <button type="button" class="btn btn-danger btn-remove" onclick="this.parentElement.remove()">X</button>
                    `;
    contenedor.appendChild(fila);
}

// SUBIR ARCHIVO
function mostrarSubirArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonSubirArchivo').style.display = 'none';
    contenedor.querySelector('.vistaSubirArchivo').style.display = 'block';
}
function ocultarSubirArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonSubirArchivo').style.display = 'block';
    contenedor.querySelector('.vistaSubirArchivo').style.display = 'none';
}

// QUITAR ARCHIVO A CARGAR
document.getElementById('btnLimpiarArchivo').addEventListener('click', function () {
    document.getElementById('archivoCargado').value = '';
});

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formNuevaOC");
    const btnGuardar = document.getElementById("btnGuardar");

    const inputNumero = document.querySelector('input[id="codigo"]');
    const inputFecha = document.querySelector('input[id="fecha"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";

        const checkboxes = form.querySelectorAll('input[name="items"]:checked');
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
        if (e.target.name === "items") {
            validarFormulario();
        }
    });

    validarFormulario();
});


// FILTRAR POR NUMERO PRESUPUESTO
function filtrarTablaNumeroPresupuesto() {
    let input = document.getElementById("filtroNumeroPresupuesto");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".filaPresupuesto");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-numeroPresupuesto");
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

Paginacion('tablaPresupuestos', 'prevPresupuestos', 'nextPresupuestos', 'numPresupuestos', 'cantPresupuestos', 'filaPresupuesto');

// MOSTRAR BUSCAR PRESUPUESTO POR NUMERO
function mostrarBuscarNumero(elemento) {
    const contenedor = elemento.closest('.container-tituloNumero');

    contenedor.querySelector('.vistaBotonBuscarNumero').style.display = 'none';
    contenedor.querySelector('.vistaBuscarNumero').style.display = 'block';
}
function ocultarBuscarNumero(elemento) {
    const contenedor = elemento.closest('.container-tituloNumero');

    contenedor.querySelector('.vistaBotonBuscarNumero').style.display = 'block';
    contenedor.querySelector('.vistaBuscarNumero').style.display = 'none';
}