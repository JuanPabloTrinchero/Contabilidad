// PAGINACION

function Paginacion(idTabla, idBtnPrev, idBtnNext, idNumPagina, idSelectCant, nombreFila) {
    const tabla = document.getElementById(idTabla);
    if (!tabla) return;

    const tbody = tabla.querySelector('tbody');
    const btnPrev = document.getElementById(idBtnPrev);
    const btnNext = document.getElementById(idBtnNext);
    const numPagina = document.getElementById(idNumPagina);
    const selectCant = document.getElementById(idSelectCant);

    let filasPorPagina = selectCant ? parseInt(selectCant.value) : 10;
    let paginaActual = 1;

    function mostrarPagina(pagina) {
        const filasVisibles = Array.from(tbody.querySelectorAll('tr.' + nombreFila + ':not(.filtrado-oculto)'));

        const totalPaginas = Math.ceil(filasVisibles.length / filasPorPagina);

        if (pagina < 1) pagina = 1;
        if (pagina > totalPaginas) pagina = totalPaginas;
        paginaActual = pagina;

        const todasLasFilas = tbody.querySelectorAll('tr.' + nombreFila);

        todasLasFilas.forEach(fila => fila.style.display = 'none');

        if (totalPaginas === 0) {
            if (numPagina) numPagina.textContent = "Página 0 de 0";
            if (btnPrev) btnPrev.disabled = true;
            if (btnNext) btnNext.disabled = true;
            return;
        }

        const inicio = (paginaActual - 1) * filasPorPagina;
        const fin = inicio + filasPorPagina;
        filasVisibles.slice(inicio, fin).forEach(fila => fila.style.display = '');

        if (numPagina) numPagina.textContent = `Página ${paginaActual} de ${totalPaginas}`;
        if (btnPrev) btnPrev.disabled = paginaActual === 1;
        if (btnNext) btnNext.disabled = paginaActual === totalPaginas;
    }

    if (selectCant) {
        selectCant.addEventListener('change', function () {
            filasPorPagina = parseInt(this.value);
            mostrarPagina(1);
        });
    }

    if (btnPrev) btnPrev.addEventListener('click', () => mostrarPagina(paginaActual - 1));
    if (btnNext) btnNext.addEventListener('click', () => mostrarPagina(paginaActual + 1));

    mostrarPagina(1);
}


