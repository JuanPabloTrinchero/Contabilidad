//Modificar Precio Unitario SIN USAR
function mostrarPrecioUnitario(elemento) {
    const contenedor = elemento.closest('.container-precio');

    contenedor.querySelector('.vistaPrecio').style.display = 'block';
    contenedor.querySelector('.vistaModificarPrecio').style.display = 'none';
}
function mostrarModificarPrecioUnitario(elemento) {
    const contenedor = elemento.closest('.container-precio');

    contenedor.querySelector('.vistaPrecio').style.display = 'none';
    contenedor.querySelector('.vistaModificarPrecio').style.display = 'block';
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

// FILTRAR POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFila");

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

    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRO POR EMPLEADO
function filtrarPorNombreEmpleado(event) {
    event.preventDefault();
    const nombreSeleccionado = event.currentTarget.getAttribute('data-nombre');

    const filas = document.querySelectorAll(".claseFila");

    filas.forEach(fila => {
        let celdaEmpleado = fila.querySelector(".col-empleado");

        if (celdaEmpleado) {
            let textoCelda = celdaEmpleado.textContent.trim();

            // Si es "(Sin filtro)" o el texto coincide exactamente, muestra la fila
            if (nombreSeleccionado === "(Sin filtro)" || textoCelda === nombreSeleccionado) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    // Actualiza el select de cantidad si existe
    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}


// FILTRO POR ESTADO
function filtrarTablaEstados(valorFiltro) {
    let filter = valorFiltro.toLowerCase();
    let filas = document.querySelectorAll(".claseFila");

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

    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRO POR SI ESTA PRESUPUESTADO
function filtrarTablaPresupuesto(valorFiltro) {
    let filter = valorFiltro.toLowerCase();
    let filas = document.querySelectorAll(".claseFila");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-presupuestado");

        if (celdaDescripcion) {
            let textoCelda = (celdaDescripcion.textContent || celdaDescripcion.innerText).trim().toLowerCase();

            if (filter === 'todos' || textoCelda === filter) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaPedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'claseFila');

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