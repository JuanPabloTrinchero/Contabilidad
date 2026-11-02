// OPCIONES
function mostrarTodasOpciones(elemento) {
    const contenedor = elemento.closest('.container-opciones');

    contenedor.querySelector('.vistaMontoManual').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPedidos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPresupuestos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarOrdenesDeCompra').style.display = 'none';

    contenedor.querySelector('#btnMontoManual').disabled = false;
    contenedor.querySelector('#btnPedidos').disabled = false;
    contenedor.querySelector('#btnPresupuestos').disabled = false;
    contenedor.querySelector('#btnODCs').disabled = false;

    contenedor.querySelector('#btnCancelar').disabled = true;

    const formularioManual = document.getElementById('formFacturaManual');
    const formularioPedidos = document.getElementById('formFacturaPedidos');
    const formularioPresupuestos = document.getElementById('formFacturaPresupuestos');
    const formularioOC = document.getElementById('formFacturaOC');

    formularioManual.reset();
    formularioPedidos.reset();
    formularioPresupuestos.reset();
    formularioOC.reset();
}

function mostrarAgregarMontoManual(elemento) {
    const contenedor = elemento.closest('.container-opciones');

    contenedor.querySelector('.vistaMontoManual').style.display = 'block';
    contenedor.querySelector('.vistaAgrerarPedidos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPresupuestos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarOrdenesDeCompra').style.display = 'none';

    contenedor.querySelector('#btnMontoManual').disabled = true;
    contenedor.querySelector('#btnPedidos').disabled = false;
    contenedor.querySelector('#btnPresupuestos').disabled = false;
    contenedor.querySelector('#btnODCs').disabled = false;

    contenedor.querySelector('#btnCancelar').disabled = false;

    const formularioPedidos = document.getElementById('formFacturaPedidos');
    const formularioPresupuestos = document.getElementById('formFacturaPresupuestos');
    const formularioOC = document.getElementById('formFacturaOC');

    formularioPedidos.reset();
    formularioPresupuestos.reset();
    formularioOC.reset();
}

function mostrarAgregarPedidos(elemento) {
    const contenedor = elemento.closest('.container-opciones');

    contenedor.querySelector('.vistaMontoManual').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPedidos').style.display = 'block';
    contenedor.querySelector('.vistaAgrerarPresupuestos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarOrdenesDeCompra').style.display = 'none';

    contenedor.querySelector('#btnMontoManual').disabled = false;
    contenedor.querySelector('#btnPedidos').disabled = true;
    contenedor.querySelector('#btnPresupuestos').disabled = false;
    contenedor.querySelector('#btnODCs').disabled = false;

    contenedor.querySelector('#btnCancelar').disabled = false;

    const formularioManual = document.getElementById('formFacturaManual');
    const formularioPresupuestos = document.getElementById('formFacturaPresupuestos');
    const formularioOC = document.getElementById('formFacturaOC');

    formularioManual.reset();
    formularioPresupuestos.reset();
    formularioOC.reset();
}

function mostrarAgregarPresupuestos(elemento) {
    const contenedor = elemento.closest('.container-opciones');

    contenedor.querySelector('.vistaMontoManual').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPedidos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPresupuestos').style.display = 'block';
    contenedor.querySelector('.vistaAgrerarOrdenesDeCompra').style.display = 'none';

    contenedor.querySelector('#btnMontoManual').disabled = false;
    contenedor.querySelector('#btnPedidos').disabled = false;
    contenedor.querySelector('#btnPresupuestos').disabled = true;
    contenedor.querySelector('#btnODCs').disabled = false;

    contenedor.querySelector('#btnCancelar').disabled = false;

    const formularioManual = document.getElementById('formFacturaManual');
    const formularioPedidos = document.getElementById('formFacturaPedidos');
    const formularioOC = document.getElementById('formFacturaOC');

    formularioManual.reset();
    formularioPedidos.reset();
    formularioOC.reset();
}

function mostrarAgregarOrdenesDeCompra(elemento) {
    const contenedor = elemento.closest('.container-opciones');

    contenedor.querySelector('.vistaMontoManual').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPedidos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarPresupuestos').style.display = 'none';
    contenedor.querySelector('.vistaAgrerarOrdenesDeCompra').style.display = 'block';

    contenedor.querySelector('#btnMontoManual').disabled = false;
    contenedor.querySelector('#btnPedidos').disabled = false;
    contenedor.querySelector('#btnPresupuestos').disabled = false;
    contenedor.querySelector('#btnODCs').disabled = true;

    contenedor.querySelector('#btnCancelar').disabled = false;

    const formularioManual = document.getElementById('formFacturaManual');
    const formularioPedidos = document.getElementById('formFacturaPedidos');
    const formularioPresupuestos = document.getElementById('formFacturaPresupuestos');

    formularioManual.reset();
    formularioPedidos.reset();
    formularioPresupuestos.reset();
}

// MOSTRAR PEDIDOS DE PRESUPUESTO
function mostrarPedidosDePresupuesto(elemento) {
    const contenedor = elemento.closest('.container-pedidosPresupuesto');

    contenedor.querySelector('.vistaBotonVerPedidosPresupuesto').style.display = 'none';
    contenedor.querySelector('.vistaPedidosPresupuesto').style.display = 'block';
}
function ocultarPedidosDePresupuesto(elemento) {
    const contenedor = elemento.closest('.container-pedidosPresupuesto');

    contenedor.querySelector('.vistaBotonVerPedidosPresupuesto').style.display = 'block';
    contenedor.querySelector('.vistaPedidosPresupuesto').style.display = 'none';
}

// AGREGAR ARCHIVO?
function mostrarCargarArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonCargarArchivo').style.display = 'none';
    contenedor.querySelector('.vistaCargarArchivo').style.display = 'block';
}
function ocultarCargarArchivo(elemento) {
    const contenedor = elemento.closest('.container-archivo');

    contenedor.querySelector('.vistaBotonCargarArchivo').style.display = 'block';
    contenedor.querySelector('.vistaCargarArchivo').style.display = 'none';
}

// FILTRAR PEDIDOS POR DESCRIPCION
function filtrarTablaPedidos() {
    let input = document.getElementById("filtroDescripcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pedido");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcionPedido");
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

Paginacion('tablaPedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'fila-pedido');

// FILTRAR PRESUPUESTOS POR NUMERO
function filtrarTablaPresupuestos() {
    let input = document.getElementById("filtroNumero");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-presupuesto");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-numero");
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

    const selectCant = document.getElementById('cantPresupuestos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaPresupuestos', 'prevPresupuestos', 'nextPresupuestos', 'numPresupuestos', 'cantPresupuestos', 'fila-presupuesto');

// FILTRAR OC POR CODIGO
function filtrarTablaOcs() {
    let input = document.getElementById("filtroCodigo");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-oc");

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

    const selectCant = document.getElementById('cantOCs');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaOcs', 'prevOCs', 'nextOCs', 'numOCs', 'cantOCs', 'fila-oc');

// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
// MONTO MANUAL
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formFacturaManual");
    const btnGuardar = document.getElementById("btnGuardar");

    const inputFecha = document.querySelector('input[id="fecha"]');
    const inputPuesto = document.querySelector('input[id="puesto"]');
    const inputNumero = document.querySelector('input[id="numero"]');
    const inputDescripcion = document.querySelector('input[id="descripcion"]');
    const inputMonto = document.querySelector('input[id="monto"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const puestoValido = inputPuesto && inputPuesto.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";
        const descripcionVAalido = inputDescripcion && inputDescripcion.value.trim() !== "";
        const montoValido = inputMonto && inputMonto.value.trim() !== "";

        btnGuardar.disabled = !(fechaValida && puestoValido && numeroValido && descripcionVAalido && montoValido);
    }
    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }
    if (inputPuesto) {
        inputPuesto.addEventListener("input", validarFormulario);
    }
    if (inputNumero) {
        inputNumero.addEventListener("input", validarFormulario);
    }
    if (inputDescripcion) {
        inputDescripcion.addEventListener("input", validarFormulario);
    }
    if (inputMonto) {
        inputMonto.addEventListener("input", validarFormulario);
    }
    validarFormulario();
});

// FACTURA DE PEDIDOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formFacturaPedidos");
    const btnGuardar = document.getElementById("btnGuardar2");

    const inputFecha = document.querySelector('input[id="fecha2"]');
    const inputPuesto = document.querySelector('input[id="puesto2"]');
    const inputNumero = document.querySelector('input[id="numero2"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const puestoValido = inputPuesto && inputPuesto.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";

        const checkboxes = form.querySelectorAll('input[id="itemsPedidos"]:checked');
        const pedidoSeleccionado = checkboxes.length > 0;

        btnGuardar.disabled = !(fechaValida && puestoValido && numeroValido && pedidoSeleccionado);
    }
    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }
    if (inputPuesto) {
        inputPuesto.addEventListener("input", validarFormulario);
    }
    if (inputNumero) {
        inputNumero.addEventListener("input", validarFormulario);
    }

    form.addEventListener("change", function (e) {
        if (e.target.id === "itemsPedidos") {
            validarFormulario();
        }
    });

    validarFormulario();
});

// FACTURA DE PRESUPUESTOS
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formFacturaPresupuestos");
    const btnGuardar = document.getElementById("btnGuardar3");

    const inputFecha = document.querySelector('input[id="fecha3"]');
    const inputPuesto = document.querySelector('input[id="puesto3"]');
    const inputNumero = document.querySelector('input[id="numero3"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const puestoValido = inputPuesto && inputPuesto.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";

        const checkboxes = form.querySelectorAll('input[id="itemsPresupuestos"]:checked');
        const pedidoSeleccionado = checkboxes.length > 0;

        btnGuardar.disabled = !(fechaValida && puestoValido && numeroValido && pedidoSeleccionado);
    }
    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }
    if (inputPuesto) {
        inputPuesto.addEventListener("input", validarFormulario);
    }
    if (inputNumero) {
        inputNumero.addEventListener("input", validarFormulario);
    }

    form.addEventListener("change", function (e) {
        if (e.target.id === "itemsPresupuestos") {
            validarFormulario();
        }
    });

    validarFormulario();
});

// FACTURAS DE OC
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formFacturaOC");
    const btnGuardar = document.getElementById("btnGuardar4");

    const inputFecha = document.querySelector('input[id="fecha4"]');
    const inputPuesto = document.querySelector('input[id="puesto4"]');
    const inputNumero = document.querySelector('input[id="numero4"]');

    function validarFormulario() {
        const fechaValida = inputFecha && inputFecha.value.trim() !== "";
        const puestoValido = inputPuesto && inputPuesto.value.trim() !== "";
        const numeroValido = inputNumero && inputNumero.value.trim() !== "";

        const origenMarcado = form.querySelectorAll('.origen-oc:checked').length > 0;
        const destinoMarcado = form.querySelectorAll('.destino-recepcion:checked').length > 0;

        const pedidoSeleccionado = origenMarcado || destinoMarcado;

        btnGuardar.disabled = !(fechaValida && puestoValido && numeroValido && pedidoSeleccionado);
    }

    if (inputFecha) {
        inputFecha.addEventListener("change", validarFormulario);
    }
    if (inputPuesto) {
        inputPuesto.addEventListener("input", validarFormulario);
    }
    if (inputNumero) {
        inputNumero.addEventListener("input", validarFormulario);
    }

    form.addEventListener("change", function (e) {
        if (e.target.classList.contains("origen-oc") || e.target.classList.contains("destino-recepcion")) {
            validarFormulario();
        }
    });

    validarFormulario();
});

// VER ITEMS EN OC
function mostrarItemsEnOC(elemento) {
    const contenedor = elemento.closest('.container-itemsOC');

    contenedor.querySelector('.vistaBotonVerItemsEnOC').style.display = 'none';
    contenedor.querySelector('.vistaVerItemsEnOC').style.display = 'block';
}
function ocultarItemsEnOC(elemento) {
    const contenedor = elemento.closest('.container-itemsOC');

    contenedor.querySelector('.vistaBotonVerItemsEnOC').style.display = 'block';
    contenedor.querySelector('.vistaVerItemsEnOC').style.display = 'none';
}

// SELECCIONAR OC O RECEPCIONES
function seleccionarTodos(origen) {
    const fila = origen.closest('.fila-oc');

    const destinos = fila.querySelectorAll('.destino-recepcion');
    const inputs = fila.querySelectorAll('.monto-recepcion-input');

    destinos.forEach(destino => {
        destino.checked = origen.checked;
    });

    inputs.forEach(input => {
        if (origen.checked == true) {
            input.style.display = 'block';
        } else {
            input.style.display = 'none';
        }

    });
}

function verificarDestinos(destinoClickeado) {
    const fila = destinoClickeado.closest('.fila-oc');
    if (!fila) return;

    const origen = fila.querySelector('.origen-oc');
    const destinos = fila.querySelectorAll('.destino-recepcion');

    const algunoMarcado = Array.from(destinos).some(destino => destino.checked);

    if (origen && !origen.disabled) {
        origen.checked = algunoMarcado;
    }

    const parrafoPadre = destinoClickeado.closest('p');
    const contenedorMonto = parrafoPadre ? parrafoPadre.nextElementSibling : null;

    if (contenedorMonto && contenedorMonto.classList.contains('monto-recepcion-input')) {
        const inputTexto = contenedorMonto.querySelector('input[type="text"]');

        if (destinoClickeado.checked) {
            contenedorMonto.style.display = 'block';
            if (inputTexto) inputTexto.disabled = false;
        } else {
            contenedorMonto.style.display = 'none';
            if (inputTexto) inputTexto.disabled = true;
        }
    }
}

// ENVIAR DATOS
document.getElementById('formFacturaOC').addEventListener('submit', function (e) {

    let todosLosInputs = this.querySelectorAll('input[name^="OCsRecepciones"]');
    todosLosInputs.forEach(input => {

        if (!input.dataset.originalName) {
            input.dataset.originalName = input.name;
        }

        input.removeAttribute('name');
    });

    let indexOC = 0;

    let filasOcMarcadas = document.querySelectorAll('.fila-oc:has(.origen-oc:checked)');

    filasOcMarcadas.forEach(fila => {

        let checkboxOC = fila.querySelector('.origen-oc');
        checkboxOC.name = `OCsRecepciones[${indexOC}].idOC`;

        let indexRecep = 0;

        let recepcionesMarcadas = fila.querySelectorAll('.destino-recepcion:checked');

        recepcionesMarcadas.forEach(checkboxRecep => {

            checkboxRecep.name = `OCsRecepciones[${indexOC}].Recepciones[${indexRecep}].IdRecepcion`;

            let divMonto = checkboxRecep.closest('p').nextElementSibling;
            let inputMonto = divMonto ? divMonto.querySelector('input[type="number"]') : null;

            if (inputMonto) {
                inputMonto.name = `OCsRecepciones[${indexOC}].Recepciones[${indexRecep}].Monto`;
            }

            indexRecep++;
        });

        indexOC++;

        /*
        if (indexRecep > 0) {
            indexOC++;
        } else {
            checkboxOC.removeAttribute('name');
        }
        */
    });
});


// LIMPIAR ARCHIVO SELECCIONADO
function limpiarArchivoSeleccionado(elemento) {
    const contenedor = elemento.closest('.container-archivo');
    const vista = contenedor.querySelector('.vistaCargarArchivo');

    const input = vista.querySelector('input[type="file"]');
    input.value = '';
}

// MOSTRAR BUSCAR PEDIDO POR DESCRIPCION
function mostrarBuscarPedidosDescricpion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcionPedido');

    contenedor.querySelector('.vistaBotonBuscarDescripcionPedido').style.display = 'none';
    contenedor.querySelector('.vistaBuscarDescripcionPedido').style.display = 'block';
}
function ocultarBuscarPedidosDescricpion(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcionPedido');

    contenedor.querySelector('.vistaBotonBuscarDescripcionPedido').style.display = 'block';
    contenedor.querySelector('.vistaBuscarDescripcionPedido').style.display = 'none';
}

// MOSTRAR BUSCAR PRESUPUESTO POR NUMERO
function mostrarBuscarNumeroPresupuesto(elemento) {
    const contenedor = elemento.closest('.container-tituloNumeroPresupuesto');

    contenedor.querySelector('.vistaBotonBuscarNumeroPresupuesto').style.display = 'none';
    contenedor.querySelector('.vistaBuscarNumeroPresupuesto').style.display = 'block';
}
function ocultarBuscarNumeroPresupuesto(elemento) {
    const contenedor = elemento.closest('.container-tituloNumeroPresupuesto');

    contenedor.querySelector('.vistaBotonBuscarNumeroPresupuesto').style.display = 'block';
    contenedor.querySelector('.vistaBuscarNumeroPresupuesto').style.display = 'none';
}

// MOSTRAR BUSCAR OC POR CODIGO
function mostrarBuscarCodigoOC(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoOC');

    contenedor.querySelector('.vistaBotonBuscarCodigoOC').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigoOC').style.display = 'block';
}
function ocultarBuscarCodigoOC(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoOC');

    contenedor.querySelector('.vistaBotonBuscarCodigoOC').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigoOC').style.display = 'none';
}
