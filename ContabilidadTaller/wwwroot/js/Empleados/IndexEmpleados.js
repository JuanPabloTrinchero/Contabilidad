// COPIAR EMAIL
function copiarEmail(email) {
    navigator.clipboard.writeText(email).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

// FILTRAR POR NOMBRE
function filtrarTabla() {
    let input = document.getElementById("filtroNombreApellido");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-empleado");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-nombreApellido");
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

    const selectCant = document.getElementById('cantEmpleados');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRO DESCRIPCION

function filtrarTablaEstados(valorFiltro) {
    let filter = valorFiltro.toLowerCase();
    let filas = document.querySelectorAll(".fila-empleado");

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
}

Paginacion('tablaEmpleados', 'prevEmpleados', 'nextEmpleados', 'numEmpleados', 'cantEmpleados', 'fila-empleado');


// BUSCAR POR mostrarBuscarNombre Y APELLIDO
function mostrarBuscarNombreApellido(elemento) {
    const contenedor = elemento.closest('.container-nombreApellido');

    contenedor.querySelector('.vistaBotonBuscar').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorNombreApellido').style.display = 'block';
}
function ocultarBuscarNombreApellido(elemento) {
    const contenedor = elemento.closest('.container-nombreApellido');

    contenedor.querySelector('.vistaBotonBuscar').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorNombreApellido').style.display = 'none';
}