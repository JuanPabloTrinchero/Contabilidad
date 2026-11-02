// FILTRAR POR CODIGO
function filtrarTablaCodigo() {
    let input = document.getElementById("filtroCodigo");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-odc");

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

    const selectCant = document.getElementById('cantOCs');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRAR POR RECEPCION
function filtrarTablaRecepcion() {
    let input = document.getElementById("filtroRecepcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-odc");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-recepcion");
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

    const selectCant = document.getElementById('cantOCs');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaOCs', 'prevOCs', 'nextOCs', 'numOCs', 'cantOCs', 'fila-odc');

// MOSTRAR OCULTAR ITEMS
function mostrarItems(elemento) {
    const contenedor = elemento.closest('.container-items');

    contenedor.querySelector('.vistaBotonMostrarItems').style.display = 'none';
    contenedor.querySelector('.vistaItems').style.display = 'block';
}

function ocultarItems(elemento) {
    const contenedor = elemento.closest('.container-items');

    contenedor.querySelector('.vistaBotonMostrarItems').style.display = 'block';
    contenedor.querySelector('.vistaItems').style.display = 'none';
}

//FILTROS DE FECHAS
function mostrarBuscarPorIntervalo(elemento) {
    event.preventDefault();
    event.stopPropagation();

    const contenedor = elemento.closest('th');

    contenedor.querySelector('.vistaBuscarFechaIntervalo').style.display = 'block';
    contenedor.querySelector('.vistaBuscarFechaEspecifica').style.display = 'none';
}
function mostrarBuscarFechaEspecifica(elemento) {
    event.preventDefault();
    event.stopPropagation();

    const contenedor = elemento.closest('th');

    contenedor.querySelector('.vistaBuscarFechaIntervalo').style.display = 'none';
    contenedor.querySelector('.vistaBuscarFechaEspecifica').style.display = 'block';
}
function ocultarFiltrosDeFechas(elemento) {
    event.preventDefault();
    event.stopPropagation();

    const contenedor = elemento.closest('th');

    contenedor.querySelector('.vistaBuscarFechaIntervalo').style.display = 'none';
    contenedor.querySelector('.vistaBuscarFechaEspecifica').style.display = 'none';
}

// FILTRO POR ESTADO
function filtrarTablaEstados(valorFiltro) {
    let filter = valorFiltro.toLowerCase();
    let filas = document.querySelectorAll(".fila-odc");

    filas.forEach(fila => {
        let celdaEstado = fila.querySelector(".col-estado");

        if (celdaEstado) {
            let estadoCelda = (celdaEstado.getAttribute("data-estado") || "").trim().toLowerCase();

            if (filter === 'todos' || estadoCelda === filter) {
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

// MOSTRAR BUSCAR RECEPCION
function mostrarBuscarRecepcion(elemento) {
    const contenedor = elemento.closest('.container-tituloRecepciones');

    contenedor.querySelector('.vistaBotonBuscarRecepcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarRecepcion').style.display = 'block';
}
function ocultarBuscarRecepcion(elemento) {
    const contenedor = elemento.closest('.container-tituloRecepciones');

    contenedor.querySelector('.vistaBotonBuscarRecepcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarRecepcion').style.display = 'none';
}