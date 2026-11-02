// ASIGNAR REMITO
function mostrarAsignarRemito(elemento) {
    const contenedor = elemento.closest('.container-asignarRemito');

    contenedor.querySelector('.vistaBotonAsignarRemito').style.display = 'none';
    contenedor.querySelector('.vistaAsignarRemito').style.display = 'block';
}
function ocultarAsignarRemito(elemento) {
    const contenedor = elemento.closest('.container-asignarRemito');

    contenedor.querySelector('.vistaBotonAsignarRemito').style.display = 'block';
    contenedor.querySelector('.vistaAsignarRemito').style.display = 'none';
}