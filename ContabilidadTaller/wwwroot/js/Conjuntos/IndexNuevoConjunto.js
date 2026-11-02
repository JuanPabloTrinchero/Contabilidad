// AGREGAR NUEVA NOTA
function agregarNuevaNota() {
    const contenedor = document.getElementById('listaNotas');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';
    fila.style = 'background-color: ${color}';

    fila.innerHTML = `
    <textarea type="text" name="notas" class="form-control-sm" style="width: 50%; height: 100px" placeholder="Nueva nota..."></textarea>
    <button type="button" class="btn btn-danger btn-eliminarNotaNueva btn-sm px-3 text-nowrap">Quitar Nota</button>
    `;
    fila.querySelector('.btn-eliminarNotaNueva').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formConjunto");
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