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

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formPieza");
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