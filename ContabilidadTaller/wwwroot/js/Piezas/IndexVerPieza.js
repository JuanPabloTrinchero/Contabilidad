// DESCRIPCION
function mostrarModificarDescripcion() {
    document.querySelector('.VistaDescripcion').style.display = 'none';
    document.querySelector('.VistaModificarDescripcion').style.display = 'block';

    const boton = document.querySelector('#botonModifDescripcion');
    boton.disabled = true;
}
function ocultarModificarDescripcion() {
    document.querySelector('.VistaDescripcion').style.display = 'block';
    document.querySelector('.VistaModificarDescripcion').style.display = 'none';

    const boton = document.querySelector('#botonModifDescripcion');
    boton.disabled = false;
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

// AGREGAR CODIGO
function mostrarAgregarCodigo() {
    const contenedor = document.querySelector('.container-codigo');

    contenedor.querySelector('.vistaAgregarCodigo').style.display = 'block';
}
function ocultarAgregarCodigo(elemento) {
    const contenedor = document.querySelector('.container-codigo');

    contenedor.querySelector('.vistaAgregarCodigo').style.display = 'none';
}

// AGREGAR NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.MostrarSoloNotas').style.display = 'none';
    contenedor.querySelector('.MostrarAgregarNota').style.display = 'block';
}
function mostrarSoloNotas(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.MostrarSoloNotas').style.display = 'block';
    contenedor.querySelector('.MostrarAgregarNota').style.display = 'none';
}

// MODIFICAR NOTA
function mostrarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.VerNota').style.display = 'none';
    contenedor.querySelector('.VerModificarNota').style.display = 'block';
}
function ocultarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.VerNota').style.display = 'block';
    contenedor.querySelector('.VerModificarNota').style.display = 'none';
}

// AGREGAR ARCHIVO
function mostrarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-noArchivos');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-noArchivos');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';
}

// BORRAR PIEZA
function mostrarBorrarPieza() {
    document.querySelector('.container-borrarPieza').style.display = 'block';
    
    const boton = document.querySelector('#botonBorrarEmpleado');
    boton.disabled = true;
    boton.style.color = 'grey';
}
function ocultarBorrarPieza() {
    document.querySelector('.container-borrarPieza').style.display = 'none';

    const boton = document.querySelector('#botonBorrarEmpleado');
    boton.disabled = false;
    boton.style.color = 'white';
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
    // Si es password lo pasa a text, y viceversa
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}

// ASIGNAR PIEZA A CONJUNTO
function mostrarAsignarAConjunto(elemento) {
    const contenedor = elemento.closest('.container-asignarAConjunto');

    contenedor.querySelector('.vistaBotonAsignarAConjunto').style.display = 'none';
    contenedor.querySelector('.vistaAsignarAConjunto').style.display = 'block';
}
function ocultarAsignarAConjunto(elemento) {
    const contenedor = elemento.closest('.container-asignarAConjunto');

    contenedor.querySelector('.vistaBotonAsignarAConjunto').style.display = 'block';
    contenedor.querySelector('.vistaAsignarAConjunto').style.display = 'none';
}

// FILTRAR CONJUNTO POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-conjunto");

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

    const selectCant = document.getElementById('cantConjuntos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRAR CONJUNTO POR CODIGO
function filtrarTablaCodigo() {
    let input = document.getElementById("filtroCodigo");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-conjunto");

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

    const selectCant = document.getElementById('cantConjuntos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaConjuntos', 'prevConjuntos', 'nextConjuntos', 'numConjuntos', 'cantConjuntos', 'fila-conjunto');

// DESMARCAR RADIO ASIGNAR A CONJUNTO
let radioActivo = null;

function toggleRadio(radio) {
    if (radioActivo === radio) {
        radio.checked = false;
        radioActivo = null;
        validarFormulario();
    } else {
        radioActivo = radio;
    }
}


// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    window.form = document.getElementById("formAsignarAConjunto");
    window.btnGuardar = document.getElementById("btnGuardarAsignarAConjunto");

    window.validarFormulario = function () {
        const radiosMarcados = window.form.querySelectorAll('input[name="idConjunto"]:checked');
        const conjuntoSeleccionado = radiosMarcados.length > 0;

        window.btnGuardar.disabled = !conjuntoSeleccionado;
    }
    window.form.addEventListener("change", function (e) {
        if (e.target.name === "idConjunto") {
            radioActivo = e.target;
            validarFormulario();
        }
    });
    validarFormulario();
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