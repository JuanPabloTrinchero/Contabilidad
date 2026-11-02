// AGREGAR NUEVA NOTA
function agregarNuevaNota() {
    const contenedor = document.getElementById('listaNotas');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                        <textarea type="text" id="archivoCargado" name="notas" class="form-control-sm" style="width: 40%; height: 100px" placeholder="Nueva nota..."></textarea>
                        <button type="button" class="btn btn-danger btn-eliminarNotaNueva btn-sm px-3 text-nowrap">Quitar fila</button>
                            `;

    fila.querySelector('.btn-eliminarNotaNueva').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}



// HABILITAR BOTON GUARDADO
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formEmpresa");
    const btnGuardar = document.getElementById("btnGuardar");
    const requiredFields = form.querySelectorAll("[required]");

    function validarFormulario() {
        let formValido = true;
        requiredFields.forEach(function (field) {
            if (field.value.trim() === "") {
                formValido = false;
            }
            if (field.name === "cuit" && field.value.trim().length !== 11) {
                formValido = false;
            }
        });
        btnGuardar.disabled = !formValido;
    }
    form.addEventListener("input", validarFormulario);
    validarFormulario();
});