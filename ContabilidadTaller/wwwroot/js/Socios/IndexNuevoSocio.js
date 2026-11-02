// agregarEmail EMAILS
function agregarEmail() {
    const contenedor = document.getElementById('lista-inputsEmails');

    const grupo = document.createElement('div');
    grupo.className = 'input-group mb-2';

    const nuevoInput = document.createElement('input');
    nuevoInput.type = 'email';
    nuevoInput.name = 'emails';
    nuevoInput.style.width = '50%';
    nuevoInput.placeholder = 'ejemplo@dominio.com';

    const botonBorrar = document.createElement('button');
    botonBorrar.type = 'button';
    botonBorrar.className = 'btn btn-outline-danger';
    botonBorrar.innerHTML = '×';

    botonBorrar.onclick = function () {
        grupo.remove();
    };

    grupo.appendChild(nuevoInput);
    grupo.appendChild(botonBorrar);
    contenedor.appendChild(grupo);
}

// AGREGAR NOTAS
function agregarNota() {
    const contenedor = document.getElementById('lista-inputsNotas');

    const grupo = document.createElement('div');
    grupo.className = 'input-group mb-2';

    const nuevoInput = document.createElement('textarea');
    nuevoInput.type = 'text';
    nuevoInput.name = 'notas';
    nuevoInput.style.width = '50%';
    nuevoInput.style.height = '100px';
    nuevoInput.placeholder = 'Nueva nota...'

    const botonBorrar = document.createElement('button');
    botonBorrar.type = 'button';
    botonBorrar.className = 'btn btn-outline-danger';
    botonBorrar.innerHTML = '×';

    botonBorrar.onclick = function () {
        grupo.remove();
    };

    grupo.appendChild(nuevoInput);
    grupo.appendChild(botonBorrar);
    contenedor.appendChild(grupo);
}

// MOSTRAR CONTRASEñA
function togglePassword() {
    var input = document.getElementById("passwordNueva");
    const icono = document.getElementById('iconoOjo');

    if (input.type === "password") {
        input.type = "text";

        icono.classList.remove('bi-eye');
        icono.classList.add('bi-eye-slash');
    } else {
        input.type = "password";

        icono.classList.remove('bi-eye-slash');
        icono.classList.add('bi-eye');
    }
}

// HABILITAR BOTON GUARDADO
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formSocio");
    const btnGuardar = document.getElementById("btnGuardar");
    const requiredFields = form.querySelectorAll("[required]");

    function validarFormulario() {
        let formValido = true;

        requiredFields.forEach(function (field) {
            if (field.value.trim() === "") {
                formValido = false;
            }
            if (field.id === "passwordNueva" && field.value.trim().length < 8) {
                formValido = false;
            }
        });

        btnGuardar.disabled = !formValido;
    }
    form.addEventListener("input", validarFormulario);
    validarFormulario();
});