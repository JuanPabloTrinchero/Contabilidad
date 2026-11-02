// VER ITEMS
function ocultarItems(elemento) {
    const contenedor = elemento.closest('.container-items');

    contenedor.querySelector('.verBoton').style.display = 'block';
    contenedor.querySelector('.verItems').style.display = 'none';
}

function mostrarItems(elemento) {
    const contenedor = elemento.closest('.container-items');

    contenedor.querySelector('.verBoton').style.display = 'none';
    contenedor.querySelector('.verItems').style.display = 'block';
}

// FILTRAR POR NOMBRE
function filtrarTabla() {
    let input = document.getElementById("filtroNumero");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-remito");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-numero");
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

    const selectCant = document.getElementById('cantRemitos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaRemitos', 'prevRemitos', 'nextRemitos', 'numRemitos', 'cantRemitos', 'fila-remito');

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
    let filas = document.querySelectorAll(".fila-remito");

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

// MOSTRAR BUSCAR POR NUMERO
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