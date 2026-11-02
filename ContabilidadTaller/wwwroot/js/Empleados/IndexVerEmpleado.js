// MODIFICAR NOMBRES Y/O APELLIDO
function mostrarModificarNombre() {
    document.querySelector('.vistaNombreApellido').style.display = 'none';

    document.querySelector('.vistaModificarNombres').style.display = 'block';
    document.querySelector('.vistaModificarApellido').style.display = 'none';

    const boton1 = document.querySelector('#botonModifNombre');
    boton1.disabled = true;
    const boton2 = document.querySelector('#botonModifApellido');
    boton2.disabled = false;
}
function mostrarModificarApellido(elemento) {
    document.querySelector('.vistaNombreApellido').style.display = 'none';

    document.querySelector('.vistaModificarNombres').style.display = 'none';
    document.querySelector('.vistaModificarApellido').style.display = 'block';

    const boton1 = document.querySelector('#botonModifNombre');
    boton1.disabled = false;
    const boton2 = document.querySelector('#botonModifApellido');
    boton2.disabled = true;
}
function ocultarModificarNombreApellido(elemento) {
    document.querySelector('.vistaNombreApellido').style.display = 'block';

    document.querySelector('.vistaModificarNombres').style.display = 'none';
    document.querySelector('.vistaModificarApellido').style.display = 'none';

    const boton1 = document.querySelector('#botonModifNombre');
    boton1.disabled = false;
    const boton2 = document.querySelector('#botonModifApellido');
    boton2.disabled = false;
}

// AGREGAR TELEFONO
function mostrarAgregarTelefono(elemento) {
    const contenedor = elemento.closest('.container-telefonos');

    contenedor.querySelector('.vistaBotonAgregarTelefono').style.display = 'none';
    contenedor.querySelector('.vistaAgregarTelefono').style.display = 'block';
}
function ocultarAgregarTelefono(elemento) {
    const contenedor = elemento.closest('.container-telefonos');

    contenedor.querySelector('.vistaBotonAgregarTelefono').style.display = 'block';
    contenedor.querySelector('.vistaAgregarTelefono').style.display = 'none';
}

// MODIFICAR TELEFONO
function mostrarModificarTelefono() {
    const contenedor = document.querySelector('.container-telefono');

    contenedor.querySelector('.vistaTelefono').style.display = 'none';
    contenedor.querySelector('.vistaModificarTelefono').style.display = 'block';
}
function ocultarModificarTelefono() {
    const contenedor = document.querySelector('.container-telefono');

    contenedor.querySelector('.vistaTelefono').style.display = 'block';
    contenedor.querySelector('.vistaModificarTelefono').style.display = 'none';
}

// AGREGAR EMAIL
function mostrarAgregarEmail(elemento) {
    const contenedor = elemento.closest('.container-emails');

    contenedor.querySelector('.vistaBotonAgregarEmail').style.display = 'none';
    contenedor.querySelector('.vistaAgregarEmail').style.display = 'block';
}
function ocultarAgregarEmail(elemento) {
    const contenedor = elemento.closest('.container-emails');

    contenedor.querySelector('.vistaBotonAgregarEmail').style.display = 'block';
    contenedor.querySelector('.vistaAgregarEmail').style.display = 'none';
}

// MODIFICAR EMAIL
function mostrarModificarEmail() {
    const contenedor = document.querySelector('.container-email');

    contenedor.querySelector('.vistaEmail').style.display = 'none';
    contenedor.querySelector('.vistaModificarEmail').style.display = 'block';
}
function ocultarModificarEmail() {
    const contenedor = document.querySelector('.container-email');

    contenedor.querySelector('.vistaEmail').style.display = 'block';
    contenedor.querySelector('.vistaModificarEmail').style.display = 'none';
}

// VER/OCULTAR PEDIDOS
function mostrarPedidos() {
    document.querySelector('.vistaBotonVerPedidos').style.display = 'none';
    document.querySelector('.vistaVerPedidos').style.display = 'block';
}
function ocultarPedidos() {
    document.querySelector('.vistaBotonVerPedidos').style.display = 'block';
    document.querySelector('.vistaVerPedidos').style.display = 'none';
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

Paginacion('tablaPedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'claseFila');

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

// MOSTRAR BUSCAR DESCRIPCION
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

// COPIAR EMAIL
function copiarEmail(email) {
    navigator.clipboard.writeText(email).then(() => { })
        .catch(err => {
            console.error('Error al copiar: ', err);
        });
}

// AGREGAR NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'none';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'block';
}
function ocultarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-notas');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'block';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'none';
}

// MODIFICAR NOTA
function mostrarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaNota').style.display = 'none';
    contenedor.querySelector('.vistaModificarNota').style.display = 'block';
}
function ocultarModificarNota(elemento) {
    const contenedor = elemento.closest('.container-nota');

    contenedor.querySelector('.vistaNota').style.display = 'block';
    contenedor.querySelector('.vistaModificarNota').style.display = 'none';
}



// SELECCIONAR SOCIO
document.querySelectorAll('.opcion-socio').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id = this.getAttribute('data-id');
        const nombre = this.getAttribute('data-nombre');

        document.getElementById('btnSocio').innerText = nombre;

        document.getElementById('idSocioSeleccionado').value = id;
    });
});

// VER/OCULTAR CONTRASEñA
function togglePassword() {
    var input = document.getElementById("passSocio");
    // Si es password lo pasa a text, y viceversa
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}


// MOSTRAR BORRAR EMPLEADO
function mostrarBorrarEmpleado() {
    document.querySelector('.container-borrarEmpleado').style.display = 'block';

    const boton = document.querySelector('#botonBorrarEmpleado');
    boton.disabled = true;
    boton.style.color = 'grey';
}
function ocultarBorrarEmpleado() {
    document.querySelector('.container-borrarEmpleado').style.display = 'none';

    const boton = document.querySelector('#botonBorrarEmpleado');
    boton.disabled = false;
    boton.style.color = 'white';
}