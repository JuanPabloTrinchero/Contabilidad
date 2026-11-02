// MODIFICAR DESCRIPCION
function mostrarModificarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-descripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'block';
}
function ocultarModificarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-descripcion');

    contenedor.querySelector('.vistaDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'none';
}

// MODIFICAR PRECIO
function mostrarModificarPrecio(elemento) {
    const contenedor = elemento.closest('.container-precio');

    contenedor.querySelector('.vistaPrecio').style.display = 'none';
    contenedor.querySelector('.vistaModificarPrecio').style.display = 'block';
}
function ocultarModificarPrecio(elemento) {
    const contenedor = elemento.closest('.container-precio');

    contenedor.querySelector('.vistaPrecio').style.display = 'block';
    contenedor.querySelector('.vistaModificarPrecio').style.display = 'none';
}