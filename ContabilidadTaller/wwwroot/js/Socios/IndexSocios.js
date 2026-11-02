// BUSCAR POR NOMBRE APELLIDO
function mostrarBuscarPorNombreApellido(elemento) {
    const contenedor = elemento.closest('.container-tituloNombreApellido');

    contenedor.querySelector('.vistaBotonBuscarNombreApellido').style.display = 'none';
    contenedor.querySelector('.vistaBuscarNombreApellido').style.display = 'block';
}
function ocultarBuscarPorNombreApellido(elemento) {
    const contenedor = elemento.closest('.container-tituloNombreApellido');

    contenedor.querySelector('.vistaBotonBuscarNombreApellido').style.display = 'block';
    contenedor.querySelector('.vistaBuscarNombreApellido').style.display = 'none';
}

// FILTRAR POR NOMBRE
function filtrarTabla() {
    let input = document.getElementById("filtroNombreSocio");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-socio");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-nombreSocio");
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

    const selectCant = document.getElementById('cantSocios');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaSocios', 'prevSocios', 'nextSocios', 'numSocios', 'cantSocios', 'fila-socio');

// COPIAR CUIT
function copiarCuit(cuit) {
    navigator.clipboard.writeText(cuit).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

// COPIAR EMAIL
function copiarEmail(cuit) {
    navigator.clipboard.writeText(cuit).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}