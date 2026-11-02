function copiarCuit(cuit) {
    navigator.clipboard.writeText(cuit).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

function mostrarDireccion(elemento) {
    const contenedor = elemento.closest('.container-direccion');

    contenedor.querySelector('.VistaDireccion').style.display = 'block';
    contenedor.querySelector('.VistaModificarDireccion').style.display = 'none';
}
function mostrarModificarDireccion(elemento) {
    const contenedor = elemento.closest('.container-direccion');

    contenedor.querySelector('.VistaDireccion').style.display = 'none';
    contenedor.querySelector('.VistaModificarDireccion').style.display = 'block';
}


function mostrarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.VistaNota').style.display = 'none';
    contenedor.querySelector('.VistaModificarNota').style.display = 'block';
}
function mostrarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.VistaNota').style.display = 'none';
    contenedor.querySelector('.VistaModificarNota').style.display = 'block';
}

// FILTRAR POR NOMBRE
function filtrarTabla() {
    let input = document.getElementById("filtroNombreEmpresa");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-empresa");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-nombreEmpresa");
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

    const selectCant = document.getElementById('cantEmpresas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaEmpresas', 'prevEmpresas', 'nextEmpresas', 'numEmpresas', 'cantEmpresas', 'fila-empresa');

// BUSCAR POR NOMBRE
function mostrarBuscarNombre(elemento) {
    const contenedor = elemento.closest('.container-nombre');

    contenedor.querySelector('.vistaBotonBuscarPorNombre').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorNombre').style.display = 'block';
}
function ocultarBuscarNombre(elemento) {
    const contenedor = elemento.closest('.container-nombre');

    contenedor.querySelector('.vistaBotonBuscarPorNombre').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorNombre').style.display = 'none';
}