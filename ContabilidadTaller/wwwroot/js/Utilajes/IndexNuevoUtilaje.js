// AGREGAR NUEVA NOTA
function agregarNuevaNota() {
    const contenedor = document.getElementById('listaNotas');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                            <textarea type="text" id="archivoCargado" name="notas" class="form-control-sm" style="width: 50%; height: 100px" placeholder="Nueva nota..."></textarea>
                            <button type="button" class="btn btn-danger btn-eliminarNotaNueva btn-sm px-3 text-nowrap">Quitar fila</button>
                                `;

    fila.querySelector('.btn-eliminarNotaNueva').addEventListener('click', function () {
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

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formNuevoUtilaje");
    const btnGuardar = document.getElementById("btnGuardar");
    const requiredFields = form.querySelectorAll("[required]");

    function validarFormulario() {
        let formValido = true;

        requiredFields.forEach(function (field) {
            if (field.value.trim() === "") {
                formValido = false;
            }
        });

        btnGuardar.disabled = !formValido;
    }

    form.addEventListener("input", validarFormulario);
    validarFormulario();
});

function mostrarAsignarPieza(elemento) {
    const contenedor = elemento.closest('.container-piezas');

    contenedor.querySelector('.vistaBotonAsignarPieza').style.display = 'none';
    contenedor.querySelector('.vistaAsignarPieza').style.display = 'block';
}
function ocultarAsignarPieza(elemento) {
    const contenedor = elemento.closest('.container-piezas');

    contenedor.querySelector('.vistaBotonAsignarPieza').style.display = 'block';
    contenedor.querySelector('.vistaAsignarPieza').style.display = 'none';
}

document.querySelectorAll('input[type="radio"][name="idPiezaAsociada"]').forEach(radio => {
    radio.addEventListener('click', function (e) {
        if (this.wasChecked) {
            this.checked = false;
            this.wasChecked = false;
        } else {
            // Desmarcar la propiedad 'wasChecked' de los demás radios del mismo grupo
            document.querySelectorAll(`input[name="${this.name}"]`).forEach(r => r.wasChecked = false);
            this.wasChecked = true;
        }
    });
});

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

// FILTRAR POR DESCRIPCION
function filtrarTablaConjunto() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pieza");

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

    const selectCant = document.getElementById('cantPiezas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaPiezas', 'prevPiezas', 'nextPiezas', 'numPiezas', 'cantPiezas', 'fila-pieza');