let id = 1;
document.addEventListener('DOMContentLoaded', function () {
    iniciarApp();
});

function iniciarApp() {
 
}


function agregar_concepto() {
    const tr = document.createElement('TR');

    //1
    const td = document.createElement('TD');
    td.innerHTML = '<textarea class="form-control" style="font-size:15px; width:200px; max-height:80px"  id="concepto" name="concepto"></textarea>';
    //2
    const td2 = document.createElement('TD');
    td2.innerHTML = '<select class="select2-container--default" name="unidad_elemento" style="font-size:15px; color:gray; border-color: gray; height:30px" id="unidad_elemento" required><option value="otra_unidad" readonly selected>Seleccionar</option><option value="PAQ">PAQ</option><option value="JORNAL">JORNAL</option></select>';
    const texto = document.createElement('P');
    texto.name = 'otro_elemento';
    texto.id = 'otro_elemento';
    texto.textContent = "¿No está la unidad / elemento ?";
    texto.onclick = otro;

    td2.appendChild(texto);

    const div = document.createElement('DIV');
    div.classList.add('ocultar');
    div.id = 'v3';

    const label = document.createElement('LABEL');
    label.textContent = 'Escribe la unidad/elemento';
    label.style ='font-size:11px', 'color:gray';

    const input = document.createElement('INPUT');
    input.classList.add('form-control');
    input.style = 'font-size:12px';
    input.type = 'text';
    input.name = 'elemento_extra';
    input.id = 'elemento_extra';

    //Agregar todo al DIV
    div.appendChild(label);
    div.appendChild(input);

    td2.appendChild(div);

    //3
    const td3 = document.createElement('TD');
    td3.innerHTML = '<input class="form-control" style="font-size:15px" type="number" name="vol_cont" min="0" id="vol_cont" required>';
    //4
    const td4 = document.createElement('TD');
    td4.innerHTML = '<input class="form-control" style="font-size:15px" type="number" name="px_paquete" min="0" id="px_paquete" required>';
    //5
    const td5 = document.createElement('TD');
    td5.innerHTML = '<input class="form-control" style="font-size:15px" type="number" name="estimar" min="0" id="estimar" required>';

    //BTN
    const btn_eli = document.createElement('BUTTON');
    btn_eli.classList.add('btn', 'btn-danger');
    btn_eli.style = 'margin:20px 5px 0 10px';
    btn_eli.textContent = 'Quitar';

    tr.appendChild(td);
    tr.appendChild(td2);
    tr.appendChild(td3);
    tr.appendChild(td4);
    tr.appendChild(td5);
    tr.appendChild(btn_eli);

    document.querySelector('#padre').appendChild(tr);


}


function otro() {

    const input3 = document.querySelector('#v3');
   
        if (input3.classList.contains('ocultar')) {
            input3.classList.remove('ocultar');
        } else {
            input3.classList.add('ocultar');
        }
}


var campos_max = 10;   //max de 10 campos

var x = 0;
$('#add_field').click(function (e) {
    e.preventDefault();     //prevenir novos clicks
    if (x < campos_max) {
        $('#listas').append('<div>\
                                <input type="text" name="campo[]">\
                                <a href="#" class="remover_campo">Remover</a>\
                                </div>');
        x++;
    }
});
// Remover o div anterior
$('#listas').on("click", ".remover_campo", function (e) {
    e.preventDefault();
    $(this).parent('div').remove();
    x--;
});