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

// MODIFICAR NOTA
function mostrarModificarNota(elemento, indexNota) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector(`.vistaNota-${indexNota}`).style.display = 'none';
    contenedor.querySelector(`.vistaModificarNota-${indexNota}`).style.display = 'block';
}

function ocultarModificarNota(elemento, indexNota) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector(`.vistaNota-${indexNota}`).style.display = 'block';
    contenedor.querySelector(`.vistaModificarNota-${indexNota}`).style.display = 'none';
}

// ASIGNAR PIEZA
function mostrarAsignarPieza() {
    const contenedor = document.querySelector('.container-piezaAsignada');

    contenedor.querySelector('.vistaAsignarPieza').style.display = 'block';
}
function ocultarAsignarPieza() {
    const contenedor = document.querySelector('.container-piezaAsignada');

    contenedor.querySelector('.vistaAsignarPieza').style.display = 'none';
}

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

// AGREGAR ARCHIVO
function mostrarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-agregarArchivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';
}

// BORRAR UTILAJE
function mostrarBorrarUtilaje() {
    const contenedor = document.querySelector('.container-borrarUtilaje').style.display = 'block';
}
function ocultarBorrarUtilaje() {
    const contenedor = document.querySelector('.container-borrarUtilaje').style.display = 'none';
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

// DESMARCAR INPUT RADIO
function ToggleRadio(radio) {
    if (radio.getAttribute('data-checked') === 'true') {
        radio.checked = false;
        radio.removeAttribute('data-checked');
    } else {
        document.querySelectorAll(`input[name="${radio.name}"]`).forEach(r => {
            r.removeAttribute('data-checked');
        });
        radio.checked = true;
        radio.setAttribute('data-checked', 'true');
    }
}

// MOSTRAR BUSCAR POR DESCRIPCION
function mostrarBuscarPorDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorDescripcion').style.display = 'block';
}
function ocultarBuscarPorDescripcion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorDescripcion').style.display = 'none';
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

// MOSTRAR BUSCAR POR CODIGO
function mostrarBuscarPorCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaCodigo').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorCodigo').style.display = 'block';
}
function ocultarBuscarPorCodigo(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigo');

    contenedor.querySelector('.vistaCodigo').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorCodigo').style.display = 'none';
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

