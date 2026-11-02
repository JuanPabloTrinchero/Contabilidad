// MODIFICAR DESCRIPCION
function mostrarModificarDescripcion() {
    const contenedor = document.querySelector('.container-descripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'block';
}
function ocultarModificarDescripcion() {
    const contenedor = document.querySelector('.container-descripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'none';
}

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

// AGREGRA PIEZAS A CONJUNTO
function mostrarBotonesAgregarPieza(elemento) {
    const contenedor = elemento.closest('.container-nuevaPiezaDeConjunto');

    contenedor.querySelector('.vistaBotonNuevaPiezaConjunto').style.display = 'block';
    contenedor.querySelector('.vistaAgregarPiezaExistente').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPiezaNueva').style.display = 'none';
}
function mostrarAgregarPiezaExistente(elemento) {
    const contenedor = elemento.closest('.container-nuevaPiezaDeConjunto');

    contenedor.querySelector('.vistaBotonNuevaPiezaConjunto').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPiezaExistente').style.display = 'block';
    contenedor.querySelector('.vistaAgregarPiezaNueva').style.display = 'none';
}
function mostrarAgregarPiezaNueva(elemento) {
    const contenedor = elemento.closest('.container-nuevaPiezaDeConjunto');

    contenedor.querySelector('.vistaBotonNuevaPiezaConjunto').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPiezaExistente').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPiezaNueva').style.display = 'block';
}

// FILTRAR POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFilaPieza");

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

// FILTRAR POR CODIGO
function filtrarTablaCodigo() {
    let input = document.getElementById("filtroCodigo");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFilaPieza");

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

    const selectCant = document.getElementById('cantPiezas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaPiezasExistentes', 'prevPiezas', 'nextPiezas', 'numPiezas', 'cantPiezas', 'claseFilaPieza');

// AGREGAR NUEVA NOTA
function agregarNuevaNota() {
    const contenedor = document.getElementById('listaNotas');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                        <textarea type="text" id="archivoCargado" name="notas" class="form-control-sm" style="width: 50%; height: 100px"></textarea>
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

// BORRAR CONJUNTO
function mostrarBorrarConjunto() {
    const contenedor = document.querySelector('.container-borrarConjunto').style.display = 'block';
}
function ocultarBorrarConjunto() {
    const contenedor = document.querySelector('.container-borrarConjunto').style.display = 'none';
}

// SELECCIONAR SOCIO
document.querySelectorAll('.opcion-socio').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id = this.getAttribute('data-id');
        const nombre = this.getAttribute('data-nombre');

        document.getElementById('btnSocio').innerText = nombre;

        document.getElementById('idSocioSeleccionado').value = id;
    });
});

// VER/OCULTAR CONTRASEñA
function togglePassword() {
    var input = document.getElementById("passSocio");
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

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS AGREGAR PIEZA EXISTENTE
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formAgregarPiezaExistente");
    const btnGuardar = document.getElementById("btnGuardarPiezaExistente");

    function validarFormulario() {

        const checkboxes = form.querySelectorAll('input[name="idsPiezas"]:checked');
        const pedidoSeleccionado = checkboxes.length > 0;

        btnGuardar.disabled = !(pedidoSeleccionado);
    }

    form.addEventListener("change", function (e) {
        if (e.target.name === "idsPiezas") {
            validarFormulario();
        }
    });

    validarFormulario();
});

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS AGREGAR PIEZA NUEVA
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formAgregarPiezaNueva");
    const btnGuardar = document.getElementById("btnGuardarPiezaNueva");

    const inputDescripcion = document.querySelector('input[id="inputDescripcion"]');

    function validarFormulario() {
        const descripcionValida = inputDescripcion.value.trim() !== "";

        btnGuardar.disabled = !(descripcionValida);
    }

    if (inputDescripcion) {
        inputDescripcion.addEventListener("input", validarFormulario);
    }

    validarFormulario();
});

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
function mostrarModificarNota(elemento, index) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector(`.vistaNota-${index}`).style.display = 'none';
    contenedor.querySelector(`.vistaModificarNota-${index}`).style.display = 'block';
}
function ocultarModificarNota(elemento, index) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector(`.vistaNota-${index}`).style.display = 'block';
    contenedor.querySelector(`.vistaModificarNota-${index}`).style.display = 'none';
}

// SUBIR ARCHIVO
function mostrarSubirArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivos');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarSubirArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivos');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';
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

// MOSTRAR BUSCAR POR CODIGO
function mostrarBuscarCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaBotonBuscarCodigo').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigo').style.display = 'block';
}
function ocultarBuscarCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaBotonBuscarCodigo').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigo').style.display = 'none';
}

function LimpiarArchivo() {
    const form = document.getElementById('formAgregarArchivo');

    form.querySelector('input[id="fileConjunto"]').value = '';
}