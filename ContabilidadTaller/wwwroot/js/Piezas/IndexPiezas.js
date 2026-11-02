// FILTRAR POR DESCRIPCION
function filtrarTablaDescripcion() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pieza");

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
    let filas = document.querySelectorAll(".fila-pieza");

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

// FILTRAR POR CONJUNTO
function filtrarTablaConjunto() {
    let input = document.getElementById("filtroConjunto");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pieza");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-conjunto");
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

Paginacion('tablaPiezas', 'prevPiezas', 'nextPiezas', 'numPiezas', 'cantPiezas', 'fila-pieza');

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

// MOSTRAR BUSCAR POR CODIGO
function mostrarBuscarConjunto(elemento) {
    const contenedor = elemento.closest('.container-tituloConjunto');

    contenedor.querySelector('.vistaBotonBuscarConjunto').style.display = 'none';
    contenedor.querySelector('.vistaBuscarConjunto').style.display = 'block';
}
function ocultarBuscarConjunto(elemento) {
    const contenedor = elemento.closest('.container-tituloConjunto');

    contenedor.querySelector('.vistaBotonBuscarConjunto').style.display = 'block';
    contenedor.querySelector('.vistaBuscarConjunto').style.display = 'none';
}