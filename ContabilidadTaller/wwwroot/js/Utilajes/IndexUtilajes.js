// BUSCAR POR DESCRIPCION
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

// BUSCAR POR DESCRIPCION
function mostrarBuscarPieza(elemento) {
    const contenedor = elemento.closest('.container-tituloPieza');

    contenedor.querySelector('.vistaBotonBuscarPieza').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPieza').style.display = 'block';
}
function ocultarBuscarPieza(elemento) {
    const contenedor = elemento.closest('.container-tituloPieza');

    contenedor.querySelector('.vistaBotonBuscarPieza').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPieza').style.display = 'none';
}

// FILTRAR POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-utilaje");

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

    const selectCant = document.getElementById('cantUtilaje');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRAR POR PIEZA
function filtrarTablaPieza() {
    let input = document.getElementById("filtroPieza");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-utilaje");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-pieza");
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

    const selectCant = document.getElementById('cantUtilaje');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaUtilajes', 'prevUtilaje', 'nextUtilaje', 'numUtilaje', 'cantUtilaje', 'fila-utilaje');