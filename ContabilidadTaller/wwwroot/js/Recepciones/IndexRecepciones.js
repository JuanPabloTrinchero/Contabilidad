// FILTRAR POR NUMERO DE RECEPCION
function filtrarTabla() {
    let input = document.getElementById("filtroNumeroRecepcion");
    let filter = input.value.toLowerCase();
    let filas = document.querySelectorAll(".fila-recepcion");

    filas.forEach(fila => {
        let celdaDescripcion = fila.querySelector(".col-numeroRecepcion");
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

    const selectCant = document.getElementById('cantRecepciones');
    if (selectCant) {
        selectCant.dispatchEvent(new Event('change'));
    }
}

Paginacion('tablaRecepciones', 'prevRecepciones', 'nextRecepciones', 'numRecepciones', 'cantRecepciones', 'fila-recepcion');
