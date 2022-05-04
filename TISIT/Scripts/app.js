
document.addEventListener('DOMContentLoaded', function () {
    iniciarApp();
});

function iniciarApp() {
    habilitar();
}



function habilitar() {

    const input = document.querySelector('#v');
    const dia_extra = document.querySelector('#btn_pago_dia_extra');
    dia_extra.addEventListener('click', e => {
        if (input.classList.contains('ocultar')) {
            input.classList.remove('ocultar');
        } else {
            input.classList.add('ocultar');
        }
    });

    const input2 = document.querySelector('#v2');
    const otra_categoria = document.querySelector('#otra_categoria');
    otra_categoria.addEventListener('click', e => {
        if (input2.classList.contains('ocultar')) {
            input2.classList.remove('ocultar');
        } else {
            input2.classList.add('ocultar');
        }
    });


}
