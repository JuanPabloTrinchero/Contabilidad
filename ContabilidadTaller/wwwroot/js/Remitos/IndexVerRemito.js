// AGREGAR ITEMS 
function mostrarAgregarItems(elemento) {
    const contenedor = elemento.closest('.container-agregarItems');

    contenedor.querySelector('.vistaBotonAgregarItems').style.display = 'none';
    contenedor.querySelector('.vistaAgregarItems').style.display = 'block';
}
function ocultarAgregarItems(elemento) {
    const contenedor = elemento.closest('.container-agregarItems');

    contenedor.querySelector('.vistaBotonAgregarItems').style.display = 'block';
    contenedor.querySelector('.vistaAgregarItems').style.display = 'none';
}

// MODIFICAR NUMERO
function mostrarModificarNumero() {
    const contenedor = document.querySelector('.container-numero');

    contenedor.querySelector('.vistaNumero').style.display = 'none';
    contenedor.querySelector('.vistaModificarNumero').style.display = 'block';
}
function ocultarModificarNumero() {
    const contenedor = document.querySelector('.container-numero');

    contenedor.querySelector('.vistaNumero').style.display = 'block';
    contenedor.querySelector('.vistaModificarNumero').style.display = 'none';
}

// MODIFICAR FECHA
function mostrarModificarFecha() {
    const contenedor = document.querySelector('.container-fecha');

    contenedor.querySelector('.vistaFecha').style.display = 'none';
    contenedor.querySelector('.vistaModificarFecha').style.display = 'block';
}
function ocultarModificarFecha() {
    const contenedor = document.querySelector('.container-fecha');

    contenedor.querySelector('.vistaFecha').style.display = 'block';
    contenedor.querySelector('.vistaModificarFecha').style.display = 'none';
}

// AGREGAR NOTA
function mostrarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-agregarNota');

    contenedor.querySelector('.vistaBotonAgregarNota').style.display = 'none';
    contenedor.querySelector('.vistaAgregarNota').style.display = 'block';
}
function ocultarAgregarNota(elemento) {
    const contenedor = elemento.closest('.container-agregarNota');

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

// BORRAR REMITO
function mostrarBorrarRemito() {
    const contenedor = document.querySelector('.container-borrar').style.display = 'block';
}
function ocultarBorrarRemito(elemento) {
    const contenedor = document.querySelector('.container-borrar').style.display = 'none';
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

// FILTRAR POR DESCRIPCION
function filtrarTabla() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();

    let filas = document.querySelectorAll(".fila-pedido");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcion");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.style.display = "";
            } else {
                fila.style.display = "none";
            }
        }
    });
    const selectCant = document.getElementById('cantPedidos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tabla-pedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'fila-pedido');

// ENTREGADO A
document.querySelectorAll('.opcion-empleado').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id = this.getAttribute('data-id');
        const nombre = this.getAttribute('data-nombre');

        const botonDropdown = document.getElementById('btnEmpleado');
        botonDropdown.innerText = nombre;

        const inputOculto = document.getElementById('idEmpleadoSeleccionado');
        const botonGuardar = document.getElementById('btnGuardar');

        inputOculto.value = id ? id : "";
        botonGuardar.removeAttribute('disabled');
    });
});

// CAMBIAR ENTREGADO A
document.querySelectorAll('.opcion-empleado2').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id2 = this.getAttribute('data-id2');
        const nombre2 = this.getAttribute('data-nombre2');

        const botonDropdown2 = document.getElementById('btnEmpleado2');
        botonDropdown2.innerText = nombre2;

        const inputOculto2 = document.getElementById('idEmpleadoSeleccionado2');
        const botonGuardar2 = document.getElementById('btnGuardar2');

        inputOculto2.value = id2 ? id2 : "";
        botonGuardar2.removeAttribute('disabled');

    });
});

// CAMBIAR ENTREGADO
function mostrarCambioEntrega() {
    const contenedor = document.querySelector('.container-cambioEntregado').style.display = 'block';
}
function ocultarCambioEntrega() {
    const contenedor = document.querySelector('.container-cambioEntregado').style.display = 'none';
}

// SELECCIONAR PEDIDO O ENTREGAS
function seleccionarTodos(origen) {
    const fila = origen.closest('.fila-pedido');

    const destinos = fila.querySelectorAll('.destino-entrega');

    destinos.forEach(destino => {
        destino.checked = origen.checked;
    });
}

function verificarDestinos(destinoClickeado) {
    const fila = destinoClickeado.closest('.fila-pedido');
    if (!fila) return;

    const origen = fila.querySelector('.origen-pedido');
    const destinos = fila.querySelectorAll('.destino-entrega');

    const algunoMarcado = Array.from(destinos).some(destino => destino.checked);

    if (origen && !origen.disabled) {
        origen.checked = algunoMarcado;
    }
}

// ENVIAR DATOS
document.getElementById('formAgregarPedidoEntregas').addEventListener('submit', function (e) {

    let todosLosInputs = this.querySelectorAll('input[name^="items"]');
    todosLosInputs.forEach(input => {

        if (!input.dataset.originalName) {
            input.dataset.originalName = input.name;
        }

        input.removeAttribute('name');
    });

    let indexPedido = 0;

    let filasPedidosMarcadas = document.querySelectorAll('.fila-pedido:has(.origen-pedido:checked)');

    filasPedidosMarcadas.forEach(fila => {

        let checkboxPedido = fila.querySelector('.origen-pedido');
        checkboxPedido.name = `items[${indexPedido}].IdPedido`;

        let indexEntrega = 0;

        let entregasMarcadas = fila.querySelectorAll('.destino-entrega:checked');

        entregasMarcadas.forEach(checkboxRecep => {

            checkboxRecep.name = `items[${indexPedido}].Entregas[${indexEntrega}].idEntrega`;

            indexEntrega++;
        });

        if (indexEntrega > 0) {
            indexPedido++;
        } else {
            checkboxOC.removeAttribute('name');
        }
    });
});

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formAgregarPedidoEntregas");
    const btnGuardar = document.getElementById("btnGuardarAgregarItems");

    function validarFormulario() {
        const origenMarcado = form.querySelectorAll('.origen-pedido:checked').length > 0;
        const destinoMarcado = form.querySelectorAll('.destino-entrega:checked').length > 0;

        const pedidoSeleccionado = destinoMarcado;

        btnGuardar.disabled = !(pedidoSeleccionado);
    }

    form.addEventListener("change", function (e) {
        if (e.target.classList.contains("origen-pedido") || e.target.classList.contains("destino-entrega")) {
            validarFormulario();
        }
    });

    validarFormulario();
});

// AGREGAR ARCHIVO
function mostrarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'block';
}
function ocultarAgregarArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonAgregarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaAgregarArchivo').style.display = 'none';
}

// LIMPIAR ARCHIVO A CARGAR
document.getElementById('btnLimpiarArchivo').addEventListener('click', function () {
    document.getElementById('cargarArchivo').value = '';
});

// BUSCAR POR DEASCRIPCION
function mostrarBuscarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-descripcion');

    contenedor.querySelector('.vistaBotonBuscarPorDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPorDescripcion').style.display = 'block';
}
function ocultarBuscarDescripcion(elemento) {
    const contenedor = elemento.closest('.container-descripcion');

    contenedor.querySelector('.vistaBotonBuscarPorDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPorDescripcion').style.display = 'none';
}
