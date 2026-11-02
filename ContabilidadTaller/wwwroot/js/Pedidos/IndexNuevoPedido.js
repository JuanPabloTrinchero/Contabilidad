// PRECIO TOTAL PIEZA NUEVA
const inputCantidad = document.getElementById('cantidad');
const inputPrecio = document.getElementById('precioUnitario');
const displayTotal = document.getElementById('precioTotal');

function calcularTotal() {
    const cant = parseFloat(inputCantidad.value) || 0;
    const precio = parseFloat(inputPrecio.value) || 0;
    const total = cant * precio;

    displayTotal.innerText = total.toFixed(2);
}

inputCantidad.addEventListener('input', calcularTotal);
inputPrecio.addEventListener('input', calcularTotal);

//-------------------------------------------------------------
// PRECIO TOTAL PIEZA EXISTENTE
const inputCantidad2 = document.getElementById('cantidad2');
const inputPrecio2 = document.getElementById('precioUnitario2');
const displayTotal2 = document.getElementById('precioTotal2');

function calcularTotal2() {
    const cant2 = parseFloat(inputCantidad2.value) || 0;
    const precio2 = parseFloat(inputPrecio2.value) || 0;
    const total2 = cant2 * precio2;

    displayTotal2.innerText = total2.toFixed(2);
}

inputCantidad2.addEventListener('input', calcularTotal2);
inputPrecio2.addEventListener('input', calcularTotal2);

//-------------------------------------------------------------
// PRECIO TOTAL CONJUNTO EXISTENTE
const inputCantidad3 = document.getElementById('cantidad3');
const inputPrecio3 = document.getElementById('precioUnitario3');
const displayTotal3 = document.getElementById('precioTotal3');

function calcularTotal3() {
    const cant3 = parseFloat(inputCantidad3.value) || 0;
    const precio3 = parseFloat(inputPrecio3.value) || 0;
    const total3 = cant3 * precio3;

    displayTotal3.innerText = total3.toFixed(2);
}

inputCantidad3.addEventListener('input', calcularTotal3);
inputPrecio3.addEventListener('input', calcularTotal3);

//-------------------------------------------------------------
// SELECCIONAR QUIEN HISO EN PEDIDO PIEZA NUEVA

document.querySelectorAll('.opcion-empleado').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id = this.getAttribute('data-id');
        const nombre = this.getAttribute('data-nombre');

        document.getElementById('btnEmpleado').innerText = nombre;

        if (!id || id === "") {
            document.getElementById('idEmpleadoSeleccionado').value = "";
        } else {
            document.getElementById('idEmpleadoSeleccionado').value = id;
        }
    });
});

// SELECCIONAR QUIEN HISO EN PEDIDO PIEZA EXISTENTE

document.querySelectorAll('.opcion-empleado2').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id2 = this.getAttribute('data-id2');
        const nombre2 = this.getAttribute('data-nombre2');

        document.getElementById('btnEmpleado2').innerText = nombre2;

        if (!id2 || id2 === "") {
            document.getElementById('idEmpleadoSeleccionado2').value = "";
        } else {
            document.getElementById('idEmpleadoSeleccionado2').value = id2;
        }
    });
});

// SELECCIONAR QUIEN HISO EN PEDIDO CONJUNTO EXISTENTE

document.querySelectorAll('.opcion-empleado3').forEach(item => {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        const id3 = this.getAttribute('data-id3');
        const nombre3 = this.getAttribute('data-nombre3');

        document.getElementById('btnEmpleado3').innerText = nombre3;

        if (!id3 || id3 === "") {
            document.getElementById('idEmpleadoSeleccionado3').value = "";
        } else {
            document.getElementById('idEmpleadoSeleccionado3').value = id3;
        }
    });
});


//-------------------------------------------------------------------
// PIEZA NUEVA, EXISTENTE O CONJUNTO
function mostrarPiezaNueva(elemento) {
    const contenedor = elemento.closest('.container-pedido');

    contenedor.querySelector('.vistaPiezaNueva').style.display = 'block';
    contenedor.querySelector('.vistaPiezasExistentes').style.display = 'none';
    contenedor.querySelector('.vistaConjuntosExistentes').style.display = 'none';

    contenedor.querySelector('#btnOp1').disabled = true;
    contenedor.querySelector('#btnOp2').disabled = false;
    contenedor.querySelector('#btnOp3').disabled = false;
    contenedor.querySelector('#btnOp4').disabled = false;
}
function mostrarPiezaExistente(elemento) {
    const contenedor = elemento.closest('.container-pedido');

    contenedor.querySelector('.vistaPiezaNueva').style.display = 'none';
    contenedor.querySelector('.vistaPiezasExistentes').style.display = 'block';
    contenedor.querySelector('.vistaConjuntosExistentes').style.display = 'none';

    contenedor.querySelector('#btnOp1').disabled = false;
    contenedor.querySelector('#btnOp2').disabled = true;
    contenedor.querySelector('#btnOp3').disabled = false;
    contenedor.querySelector('#btnOp4').disabled = false;
}
function mostrarConjuntoExistente(elemento) {
    const contenedor = elemento.closest('.container-pedido');

    contenedor.querySelector('.vistaPiezaNueva').style.display = 'none';
    contenedor.querySelector('.vistaPiezasExistentes').style.display = 'none';
    contenedor.querySelector('.vistaConjuntosExistentes').style.display = 'block';

    contenedor.querySelector('#btnOp1').disabled = false;
    contenedor.querySelector('#btnOp2').disabled = false;
    contenedor.querySelector('#btnOp3').disabled = true;
    contenedor.querySelector('#btnOp4').disabled = false;
}
function cancelarOpciones(elemento) {
    const contenedor = elemento.closest('.container-pedido');

    contenedor.querySelector('.vistaPiezaNueva').style.display = 'none';
    contenedor.querySelector('.vistaPiezasExistentes').style.display = 'none';
    contenedor.querySelector('.vistaConjuntosExistentes').style.display = 'none';

    contenedor.querySelector('#btnOp1').disabled = false;
    contenedor.querySelector('#btnOp2').disabled = false;
    contenedor.querySelector('#btnOp3').disabled = false;
    contenedor.querySelector('#btnOp4').disabled = true;
}


// AGREGAR NUEVA NOTA PIEZA EXISTENTE
function agregarNuevaNotaPiezaExistente() {
    const contenedor = document.getElementById('listaNotasPiezaExistente');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                        <textarea type="text" name="notas" class="form-control-sm" style="width: 50%; height: 100px" placeholder="Nueva nota..."></textarea>
                        <button type="button" class="btn btn-danger btn-eliminarNotaNueva btn-sm px-3 text-nowrap">Quitar fila</button>
                            `;

    fila.querySelector('.btn-eliminarNotaNueva').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}

// AGREGAR NUEVA NOTA PIEZA NUEVA
function agregarNuevaNotaPiezaNueva() {
    const contenedor = document.getElementById('listaNotasPiezaNueva');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                                    <textarea type="text" name="notas" class="form-control-sm" style="width: 50%; height: 100px" placeholder="Nueva nota..."></textarea>
                            <button type="button" class="btn btn-danger btn-eliminarNotaNueva btn-sm px-3 text-nowrap">Quitar fila</button>
                                `;

    fila.querySelector('.btn-eliminarNotaNueva').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}

// AGREGAR NUEVA NOTA CONJUNTO EXISTENTE
function agregarNuevaNotaConjuntoExistente() {
    const contenedor = document.getElementById('listaNotasConjuntoExistente');

    const fila = document.createElement('div');
    fila.className = 'nota-item d-flex align-items-center';

    fila.innerHTML = `
                            <textarea type="text" name="notas" class="form-control-sm" style="width: 50%; height: 100px" placeholder="Nueva nota..."></textarea>
                            <button type="button" class="btn btn-danger btn-eliminarNotaNueva btn-sm px-3 text-nowrap">Quitar fila</button>
                            `;

    fila.querySelector('.btn-eliminarNotaNueva').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}

// FILTRAR POR DESCRIPCION PIEZA
function filtrarTablaDescripcionPieza() {
    let input = document.getElementById("filtroDescripcionPieza");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFilaPieza");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcionPieza");
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

    const selectCant = document.getElementById('cantPiezas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRAR POR CODIGO PIEZA
function filtrarTablaCodigoPieza() {
    let input = document.getElementById("filtroCodigoPieza");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFilaPieza");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-codigoPieza");
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

    const selectCant = document.getElementById('cantPiezas');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaPiezas', 'prevPiezas', 'nextPiezas', 'numPiezas', 'cantPiezas', 'claseFilaPieza');

// AGREGAR NUEVO ARCHIVO PIEZA NUEVA
function agregarNuevoArchivo() {
    const contenedor = document.getElementById('listaArchivos');

    const fila = document.createElement('div');
    fila.className = 'archivo-item d-flex align-items-center';

    fila.innerHTML = `
                                <input type="file" id="archivoCargado" name="archivos" class="form-control-sm">
                                <button type="button" class="btn btn-warning btn-limpiar btn-sm px-3 text-nowrap">Limpiar archivo</button>
                                <button type="button" class="btn btn-danger btn-eliminar btn-sm px-3 text-nowrap">Quitar fila</button>
                            `;

    fila.querySelector('.btn-limpiar').addEventListener('click', function () {
        fila.querySelector('input[type="file"]').value = '';
    });

    fila.querySelector('.btn-eliminar').addEventListener('click', function () {
        fila.remove();
    });

    contenedor.appendChild(fila);
}


// HABILITAR BUTON GUARDAR SOLO CON CAMPOS REQUERIDOS
// PEDIDO PIEZA NUEVA
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formPedidoPiezaNueva");
    const btnGuardar = document.getElementById("btnGuardarPPN");
    const requiredFields = form.querySelectorAll("[required]");

    function validarFormulario() {
        let formValido = true;

        requiredFields.forEach(function (field) {
            if (field.value.trim() === "") {
                formValido = false;
            }
        });

        btnGuardar.disabled = !formValido;
    }

    form.addEventListener("input", validarFormulario);
    validarFormulario();
});

// PEDIDO PIEZA EXISTENTE
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formPedidoPiezaExistente");
    const btnGuardar = document.getElementById("btnGuardarPPE");
    const inputCantidad = document.getElementById("cantidad2");

    function validarFormulario() {
        const cantidadValida = inputCantidad.value.trim() !== "" && parseInt(inputCantidad.value) >= 0;

        const radioSeleccionado = form.querySelector('input[name="idPiezaExistente"]:checked') !== null;

        btnGuardar.disabled = !(cantidadValida && radioSeleccionado);
    }

    inputCantidad.addEventListener("input", validarFormulario);

    let radioPrevio = null;

    form.addEventListener("click", function (e) {
        if (e.target.name === "idPiezaExistente") {
            if (radioPrevio === e.target) {
                e.target.checked = false;
                radioPrevio = null;
            } else {
                radioPrevio = e.target;
            }

            validarFormulario();
        }
    });
});


// PEDIDO CONJUNTO
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formPedidoConjunto");
    const btnGuardar = document.getElementById("btnGuardarPC");
    const inputCantidad = document.getElementById("cantidad3");

    function validarFormulario() {
        const cantidadValida = inputCantidad.value.trim() !== "" && parseInt(inputCantidad.value) >= 0;

        const radioSeleccionado = form.querySelector('input[name="idConjuntoExistente"]:checked') !== null;

        btnGuardar.disabled = !(cantidadValida && radioSeleccionado);
    }

    inputCantidad.addEventListener("input", validarFormulario);

    let radioPrevio = null;

    form.addEventListener("click", function (e) {
        if (e.target.name === "idConjuntoExistente") {
            if (radioPrevio === e.target) {
                e.target.checked = false;
                radioPrevio = null;
            } else {
                radioPrevio = e.target;
            }

            validarFormulario();
        }
    });
});

// FILTRAR POR DESCRIPCION CONJUNTO
function filtrarTablaDescripcionConjunto() {
    let input = document.getElementById("filtroDescripcionConjunto");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFilaConjunto");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-descripcionConjunto");
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

    const selectCant = document.getElementById('cantConjuntos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

// FILTRAR POR CODIGO CONJUNTO
function filtrarTablaCodigoConjunto() {
    let input = document.getElementById("filtroCodigoConjunto");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".claseFilaConjunto");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-codigoConjunto");
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

    const selectCant = document.getElementById('cantConjuntos');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaConjuntos', 'prevConjuntos', 'nextConjuntos', 'numConjuntos', 'cantConjuntos', 'claseFilaConjunto');

// ALTERNAR BOTONES DE PREGUNTA
function activarBoton(boton) {
    boton.parentElement.querySelectorAll('.btn').forEach(b => b.disabled = false);
    boton.disabled = true;
}


// MOSTRAR BUSCAR DESCRIPCION PIEZA
function mostrarBuscarDescripcionPieza(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcionPieza');

    contenedor.querySelector('.vistaBotonBuscarDescripcionPieza').style.display = 'none';
    contenedor.querySelector('.vistaBuscarDescripcionPieza').style.display = 'block';
}
function ocultarBuscarDescripcionPieza(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcionPieza');

    contenedor.querySelector('.vistaBotonBuscarDescripcionPieza').style.display = 'block';
    contenedor.querySelector('.vistaBuscarDescripcionPieza').style.display = 'none';
}
// MOSTRAR BUSCAR CODIGO PIEZA
function mostrarBuscarCodigoPieza(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoPieza');

    contenedor.querySelector('.vistaBotonBuscarCodigoPieza').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigoPieza').style.display = 'block';
}
function ocultarBuscarCodigoPieza(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoPieza');

    contenedor.querySelector('.vistaBotonBuscarCodigoPieza').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigoPieza').style.display = 'none';
}

// MOSTRAR BUSCAR DESCRIPCION CONJUNTO
function mostrarBuscarDescripcionConjunto(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcionConjunto');

    contenedor.querySelector('.vistaBotonBuscarDescripcionConjunto').style.display = 'none';
    contenedor.querySelector('.vistaBuscarDescripcionConjunto').style.display = 'block';
}
function ocultarBuscarDescripcionConjunto(elemento) {
    const contenedor = elemento.closest('.container-tituloDescripcionConjunto');

    contenedor.querySelector('.vistaBotonBuscarDescripcionConjunto').style.display = 'block';
    contenedor.querySelector('.vistaBuscarDescripcionConjunto').style.display = 'none';
}
// MOSTRAR BUSCAR CODIGO CONJUNTO
function mostrarBuscarCodigoConjunto(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoConjunto');

    contenedor.querySelector('.vistaBotonBuscarCodigoConjunto').style.display = 'none';
    contenedor.querySelector('.vistaBuscarCodigoConjunto').style.display = 'block';
}
function ocultarBuscarCodigoConjunto(elemento) {
    const contenedor = elemento.closest('.container-tituloCodigoConjunto');

    contenedor.querySelector('.vistaBotonBuscarCodigoConjunto').style.display = 'block';
    contenedor.querySelector('.vistaBuscarCodigoConjunto').style.display = 'none';
}