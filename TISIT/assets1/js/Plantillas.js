
function addRowDT_plantilla(data) {
    var tabla_plantilla = $("#tablePlantilla").DataTable();
    moment.locale('es');
    var cod = "A-";
    var button = "<button class='btn btn-info btn-checkList' style='font-size:12px'><i style='font-size:12px' class='bi bi-card-checklist'></i><br />Check-List</button>";


    for (var i = 0; i < data.length; i++) {
        tabla_plantilla.row.add([
            button,
            data[i].id,
            data[i].cliente,
            data[i].direccion_cliente,
            cod + data[i].n_remision ,
            moment(data[i].fecha_remision).format('DD/MM/YYYY'),
            data[i].carga,
            data[i].n_tolva,
            moment(data[i].hora_llegada).format('HH:mm:ss'),
            moment(data[i].hora_inicioFC).format('HH:mm:ss'),
            moment(data[i].hora_terminoFC).format('HH:mm:ss'),
            moment(data[i].hora_salida).format('HH:mm:ss'),
            data[i].operador_pqsl,
            data[i].trasvase,
            data[i].tipo_unidad,
            data[i].sellos,
            data[i].ticket_peso + "kg",
            data[i].peso_bruto +"kg",
            data[i].peso_tara + "kg",
            data[i].peso_neto + "kg",
            data[i].carta_porte,
            data[i].fletera, 
            data[i].operador,
            data[i].unidad,
            data[i].placas
           
        ]).draw();
    }

}


$(document).on('click', '.btn-checkList', function myfunction(e) {
    e.preventDefault();
    pdfCheckList();
});

function pdfCheckList() {
    $.ajax({
        type: "POST",
        url: "ver_plantillas.aspx/PdfCheckList",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("error");
        },
        success: function (data) {
       
        }
    })
}

function sendDataAjax_plantilla() {
    $.ajax({
        type: "POST",
        url: "ver_plantillas.aspx/ListarPlantillas",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("error");
        },
        success: function (data) {
            console.log(data.d);
            addRowDT_plantilla(data.d);
        }
    })
}




//LLAMAR FUNCION AJAX AL CARGAR EL DOCUMENTO

    sendDataAjax_plantilla();
