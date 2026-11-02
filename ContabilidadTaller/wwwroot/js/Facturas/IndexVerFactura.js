// MODIFICAR CODIGO
function mostrarModificarCodigo() {
    const contenedor = document.querySelector('.container-codigo');

    contenedor.querySelector('.vistaCodigo').style.display = 'none';
    contenedor.querySelector('.vistaModificarCodigo').style.display = 'block';
}
function ocultarModificarCodigo() {
    const contenedor = document.querySelector('.container-codigo');

    contenedor.querySelector('.vistaCodigo').style.display = 'block';
    contenedor.querySelector('.vistaModificarCodigo').style.display = 'none';
}
// -----------------------------------------------------------------

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
// -----------------------------------------------------------------
// MODIFICAR DESCRIPCION
function mostrarModificarDescripcion() {
    const contenedor = document.querySelector('.container-descripcion');

    contenedor.querySelector('.vistaBotonModificarDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'block';
}
function ocultarModificarDescripcion() {
    const contenedor = document.querySelector('.container-descripcion');

    contenedor.querySelector('.vistaBotonModificarDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaModificarDescripcion').style.display = 'none';
}

// MODIFICAR MONTO
function mostrarModificarMonto() {
    const contenedor = document.querySelector('.container-monto');

    contenedor.querySelector('.vistaBotonModificarMonto').style.display = 'none';
    contenedor.querySelector('.vistaModificarMonto').style.display = 'block';
}
function ocultarModificarMonto() {
    const contenedor = document.querySelector('.container-monto');

    contenedor.querySelector('.vistaBotonModificarMonto').style.display = 'block';
    contenedor.querySelector('.vistaModificarMonto').style.display = 'none';
}

// -----------------------------------------------------------------

// AGREGAR PEDIDOS
function mostrarAgregarPedidos(elemento) {
    const contenedor = elemento.closest('.container-agregarPedidos');

    contenedor.querySelector('.vistaBotonAgregarPedidos').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPedidos').style.display = 'block';
}
function ocultarAgregarPedidos(elemento) {
    const contenedor = elemento.closest('.container-agregarPedidos');

    contenedor.querySelector('.vistaBotonAgregarPedidos').style.display = 'block';
    contenedor.querySelector('.vistaAgregarPedidos').style.display = 'none';
}
// -----------------------------------------------------------------

// AGREGAR PRESUPUESTOS
function mostrarAgregarPresupuestos(elemento) {
    const contenedor = elemento.closest('.container-agregraPresupuestos');

    contenedor.querySelector('.vistaBotonAgregarPresupuestos').style.display = 'none';
    contenedor.querySelector('.vistaAgregarPresupuestos').style.display = 'block';
}
function ocultarAgregarPresupuestos(elemento) {
    const contenedor = elemento.closest('.container-agregraPresupuestos');

    contenedor.querySelector('.vistaBotonAgregarPresupuestos').style.display = 'block';
    contenedor.querySelector('.vistaAgregarPresupuestos').style.display = 'none';
}
// -----------------------------------------------------------------

// AGREGAR OCS
function mostrarAgregarOCsConRecepciones(elemento) {
    const contenedor = elemento.closest('.container-agregarOCs');

    contenedor.querySelector('.vistaBotonesAgregarOCs').style.display = 'none';
    contenedor.querySelector('.vistaAgregarOCsConRecepciones').style.display = 'block';
    contenedor.querySelector('.vistaAgregarOCsSinRecepciones').style.display = 'none';
}
function mostrarAgregarOCsSinRecepciones(elemento) {
    const contenedor = elemento.closest('.container-agregarOCs');

    contenedor.querySelector('.vistaBotonesAgregarOCs').style.display = 'none';
    contenedor.querySelector('.vistaAgregarOCsConRecepciones').style.display = 'none';
    contenedor.querySelector('.vistaAgregarOCsSinRecepciones').style.display = 'block';
}
function mostrarBotonesAgregarOCs(elemento) {
    const contenedor = elemento.closest('.container-agregarOCs');

    contenedor.querySelector('.vistaBotonesAgregarOCs').style.display = 'block';
    contenedor.querySelector('.vistaAgregarOCsConRecepciones').style.display = 'none';
    contenedor.querySelector('.vistaAgregarOCsSinRecepciones').style.display = 'none';
}
// -----------------------------------------------------------------

// VALIDAR FORMULARIO AGREGAR OC SIN RECEPCIONES
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formAgregarOCSinRecep");
    const btnGuardar = document.getElementById("btnGuardar1");

    function validarFormulario() {
        const checkboxes = form.querySelectorAll('input[name="items"]:checked');
        const ocSeleccionado = checkboxes.length > 0;

        btnGuardar.disabled = !(ocSeleccionado);
    }

    form.addEventListener("change", function (e) {
        if (e.target.name === "items") {
            validarFormulario();
        }
    });

    validarFormulario();
});
// -----------------------------------------------------------------

// VALIDAR FORMULARIO AGREGAR OC CON RECEPCIONES
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formAgregarOCConRecep");
    const btnGuardar = document.getElementById("btnGuardar2");

    function validarFormulario() {
        const origenMarcado = form.querySelectorAll('.origen-oc:checked').length > 0;
        const destinoMarcado = form.querySelectorAll('.destino-recepcion:checked').length > 0;

        const itemsSeleccionados = origenMarcado || destinoMarcado;

        btnGuardar.disabled = !(itemsSeleccionados);
    }

    form.addEventListener("change", function (e) {
        if (e.target.classList.contains("origen-oc") || e.target.classList.contains("destino-recepcion")) {
            validarFormulario();
        }
    });

    validarFormulario();
});
// -----------------------------------------------------------------

// SELECCIONAR OC O RECEPCIONES
function seleccionarTodos(origen) {
    const fila = origen.closest('.fila-occrAgregar');

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
    const fila = destinoClickeado.closest('.fila-occrAgregar');
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
// -----------------------------------------------------------------

// ENVIAR DATOS
document.getElementById('formAgregarOCConRecep').addEventListener('submit', function (e) {

    let todosLosInputs = this.querySelectorAll('input[name^="OCsRecepciones"]');
    todosLosInputs.forEach(input => {

        if (!input.dataset.originalName) {
            input.dataset.originalName = input.name;
        }

        input.removeAttribute('name');
    });

    let indexOC = 0;

    let filasOcMarcadas = document.querySelectorAll('.fila-occrAgregar:has(.origen-oc:checked)');

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

        if (indexRecep > 0) {
            indexOC++;
        } else {
            checkboxOC.removeAttribute('name');
        }
    });
});

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

// MOSTRAR BUSCAR PEDIDO AGREGAR POR DESCRIPCION
function mostrarBuscarPedidosAgregarDescricpion(elemento) {
    const contenedor = elemento.closest('.container-tituloPedidoDescripcion');

    contenedor.querySelector('.vistaBotonBuscarPedidoAgregarDescripcion').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPedidoAgregarDescripcion').style.display = 'block';
}
function ocultarBuscarPedidosAgregarDescricpion(elemento) {
    const contenedor = elemento.closest('.container-tituloPedidoDescripcion');

    contenedor.querySelector('.vistaBotonBuscarPedidoAgregarDescripcion').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPedidoAgregarDescripcion').style.display = 'none';
}

// FILTRAR POR DESCRIPCION PEDIDO A AGREGAR
function filtrarTablaDescripcionPedido() {
    let input = document.getElementById("filtroDescripcionPedido");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-pedidoAgregar");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcionPedidoAgregar");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
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

Paginacion('tablaAgregarPedidos', 'prevPedidos', 'nextPedidos', 'numPedidos', 'cantPedidos', 'fila-pedidoAgregar');

// MOSTRAR BUSCAR PRESUPUESTO AGREGAR POR NUMERO
function mostrarBuscarPresupuestoAgregarNumero(elemento) {
    const contenedor = elemento.closest('.container-tituloNumeroPresupuesto');

    contenedor.querySelector('.vistaBotonBuscarPresupuestoAgregarNumero').style.display = 'none';
    contenedor.querySelector('.vistaBuscarPresupuestoAgregarNumero').style.display = 'block';
}
function ocultarBuscarPresupuestoAgregarNumero(elemento) {
    const contenedor = elemento.closest('.container-tituloNumeroPresupuesto');

    contenedor.querySelector('.vistaBotonBuscarPresupuestoAgregarNumero').style.display = 'block';
    contenedor.querySelector('.vistaBuscarPresupuestoAgregarNumero').style.display = 'none';
}

// FILTRAR POR NUMERO PRESUPUESTO A AGREGAR
function filtrarTablaNumeroPresupuesto() {
    let input = document.getElementById("filtroNumeroPresupuesto");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-presupuestoAgregar");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-numeroPresupuestoAgregar");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
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

Paginacion('tablaAgregarPresupuestos', 'prevPresupuestos', 'nextPresupuestos', 'numPresupuestos', 'cantPresupuestos', 'fila-presupuestoAgregar');

// MOSTRAR BUSCAR CODIGO OC SIN RECEPCIONES
function mostrarBuscarCodigoOCsRecep(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoOCsRecep');

    contenedor.querySelector('.vistaBotonBuscarCodigoOCsRecep').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigoOCsRecep').style.display = 'block';
}
function ocultarBuscarCodigoOCsRecep(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoOCsRecep');

    contenedor.querySelector('.vistaBotonBuscarCodigoOCsRecep').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigoOCsRecep').style.display = 'none';
}

// FILTRAR POR CODIGO OC SIN RECEPCIONES A AGREGAR
function filtrarTablaACodigoOCsr() {
    let input = document.getElementById("filtroCodigoOCsr");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-ocsrAgregar");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-codigoOCsrAgregar");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    const selectCant = document.getElementById('cantOCsrs');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaAgregarOCsRecep', 'prevOCsr', 'nextOCsr', 'numOCsr', 'cantOCsr', 'fila-ocsrAgregar');

// MOSTRAR BUSCAR CODIGO OC CON RECEPCIONES
function mostrarBuscarCodigoOCcRecep(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoOCcRecep');

    contenedor.querySelector('.vistaBotonBuscarCodigoOCcRecep').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigoOCcRecep').style.display = 'block';
}
function ocultarBuscarCodigoOCcRecep(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoOCcRecep');

    contenedor.querySelector('.vistaBotonBuscarCodigoOCcRecep').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigoOCcRecep').style.display = 'none';
}

// FILTRAR POR CODIGO OC CON RECEPCIONES A AGREGAR
function filtrarTablaACodigoOCcr() {
    let input = document.getElementById("filtroCodigoOCcr");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-occrAgregar");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-codigoOCcrAgregar");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    const selectCant = document.getElementById('cantOCcr');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaAgregarOCcRecep', 'prevOCcr', 'nextOCcr', 'numOCcr', 'cantOCcr', 'fila-occrAgregar');

// MOSTRAR BUSCAR RECEPCIONES
function mostrarBuscarRecepciones(elemento) {
    const contenedor = elemento.closest('.container-tituloRecepciones');

    contenedor.querySelector('.vistaBotonBuscarRecepciones').style.display = 'none';
    contenedor.querySelector('.vistaBuscarRecepciones').style.display = 'block';
}
function ocultarBuscarRecepciones(elemento) {
    const contenedor = elemento.closest('.container-tituloRecepciones');

    contenedor.querySelector('.vistaBotonBuscarRecepciones').style.display = 'block';
    contenedor.querySelector('.vistaBuscarRecepciones').style.display = 'none';
}

function filtrarTablaRecepciones() {
    let input = document.getElementById("filtroRecepciones");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-occrAgregar");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-recepciones");
        if (celdaDescripcion) {
            let texto = celdaDescripcion.textContent || celdaDescripcion.innerText;

            if (texto.toLowerCase().indexOf(filter) > -1) {
                fila.classList.remove('filtrado-oculto');
                fila.style.display = "";
            } else {
                fila.classList.add('filtrado-oculto');
                fila.style.display = "none";
            }
        }
    });

    const selectCant = document.getElementById('cantRecep');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// BORRAR FACTURA
function mostrarBorrarFactura() {
    const contenedor = document.querySelector('.container-borrar').style.display = 'block';
}
function ocultarBorrarFactura() {
    const contenedor = document.querySelector('.container-borrar').style.display = 'none';
}

// DESMARCAR INPUT RADIO
function ToggleRadio(radio) {
    if (radio.getAttribute('data-checked') === 'true') {
        radio.checked = false;
        radio.removeAttribute('data-checked');
    } else {
        document.querySelectorAll(`input[name="${radio.name}"]`).forEach(r => {
            r.removeAttribute('data-checked');
        });
        radio.checked = true;
        radio.setAttribute('data-checked', 'true');
    }
}


// VER/OCULTAR CONTRASEñA
function togglePassword() {
    var input = document.getElementById("passSocio");
    const icono = document.getElementById('iconoOjo');

    if (input.type === "password") {
        input.type = "text";

        icono.classList.remove('bi-eye');
        icono.classList.add('bi-eye-slash');
    } else {
        input.type = "password";

        icono.classList.remove('bi-eye-slash');
        icono.classList.add('bi-eye');
    }
}

// LIMPIAR ARCHIVO A CARGAR
document.getElementById('btnLimpiarArchivo').addEventListener('click', function () {
    document.getElementById('cargarArchivo').value = '';
});