// FILTRAR POR CODIGO
function filtrarTabla() {
    let input = document.getElementById("filtroCodigo");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-factura");

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

    const selectCant = document.getElementById('cantFacturas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaFacturas', 'prevFacturas', 'nextFacturas', 'numFacturas', 'cantFacturas', 'fila-factura');

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
    let filas = document.querySelectorAll(".fila-factura");

    const mensajePrevio = document.getElementById("fila-sin-datos");
    if (mensajePrevio) {
        mensajePrevio.remove();
    }

    let filasVisibles = 0;

    filas.forEach(fila => {
        let celdaEstado = fila.querySelector(".col-estado");

        if (celdaEstado) {
            let estadoCelda = (celdaEstado.getAttribute("data-estado") || "").trim().toLowerCase();

            if (filter === 'todos' || estadoCelda === filter) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
                filasVisibles++;
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    let mensaje = -'';
    switch (filter) {
        case 'impaga':
            mensaje = '- No hay Facturas Impagas -';
            break;
        case 'pagada':
            mensaje = '- No hay Facturas Pagadas -';
            break;
        case 'anulada':
            mensaje = '- No hay Facturas Anuladas -';
            break;
    }

    if (filasVisibles === 0 && filas.length > 0) {
        const padreTabla = filas[0].parentNode;

        const totalColumnas = filas[0].children.length;

        const filaVacia = document.createElement("tr");
        filaVacia.id = "fila-sin-datos";

        //filaVacia.innerHTML = `<td colspan="${totalColumnas}" style="text-align: center; padding: 20px;">No hay facturas ${filter}s</td>`;
        filaVacia.innerHTML = `<td colspan="${totalColumnas}" style="text-align: center; padding: 20px;">${mensaje}</td>`;

        padreTabla.appendChild(filaVacia);
    }

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