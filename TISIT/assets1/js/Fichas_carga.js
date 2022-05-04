

function addRowDT_fichas(data) {
    var tabla_fichas = $("#tableFichasCarga").DataTable();

    moment.locale('es');
    for (var i = 0; i < data.length; i++) {
        tabla_fichas.row.add([
            data[i].id,
             data[i].folio,
            data[i].operador,
            moment(data[i].fecha_llegada).format('DD/MM/YYYY'),
            moment(data[i].fecha_salida).format('DD/MM/YYYY'),
            data[i].placas,
            data[i].carta_porte,
            data[i].carro_ferrocarril
        ]).draw();
    }

}


function sendDataAjax_fichas() {
    $.ajax({
        type:"POST",
        url: "nueva_ficha.aspx/ListarFichas",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("error");
        },
        success: function (data) {
            console.log(data.d);
            addRowDT_fichas(data.d); 
        }
    })
}




//LLAMAR FUNCION AJAX AL CARGAR EL DOCUMENTO
sendDataAjax_fichas();