var tabla1 = $("#tbl_Ingresos1").dataTable();

function sendDataAjax1() {
    $.ajax({
        type: "POST",
        url: "ingreso.aspx/listarIngresos",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            addRow1(arrObj.d);
            //console.log(arrObj.d);

            tabla1.clear()
            tabla1.draw();
            addRow1(arrObj.d)
        }

    })

}

function addRow1(data) {


    for (var i = 0; i < data.length; i++) {

        //var Unidad = data[i].Unidad;
        //var Cantidad = data[i].Cantidad;
        //var FechaEntrada = data[i].FechaEntrada;
        //var FechaSalida = data[i].FechaSalida;
        //var Ubicacion = data[i].Ubicacion;
        tabla1.fnAddData([
            data[i].id_ingreso,
            data[i].paciente,
            data[i].fecha,
            data[i].edad,
            data[i].sexo,
            data[i].medico,
            data[i].diagnostico,
            //Unidad,
            //Cantidad,
            //FechaEntrada,
            //FechaSalida,
            //Ubicacion,
            '<div class="text-center"><button value="Vista Detallada" class="btn btn-secondary btn-show1 close" data-bs-dismiss="modal" >SELECCIONAR</button>' +
            //'<button value="Eliminar" class="btn btn-danger btn-delete"><i class="fa-solid fa-trash-can"></i></button>&nbsp' +
            //'<button value="Imprimir_VD" class="btn btn-secondary btn-PDF"><i class="fa-solid fa-file-lines"></i></button>'+
            '</div > '
        ]);
    }

}

$(document).on('click', ".btn-show1", function (e) {
    e.preventDefault();
    var row = $(this).parent().parent();
    var data = tabla1.fnGetData(row);
    fillModalDataShow1(data)
})

//Cargar con los datos de la tabla los compos del modalshow
function fillModalDataShow1(modelData) {
    $("#txtpaciente1").val(modelData[1]);
    $("#txtfecha1").val(modelData[2]);
    $("#txtedad1").val(modelData[3]);
    $("#txtsexo1").val(modelData[4]);
    $("#txtmedico1").val(modelData[5]);
    $("#txtdiagnostico1").val(modelData[6]);

    $("txtpaciente1").text() = $("#txtpaciente1").val(modelData[1]);


    console.log($("#txtpaciente1").val());
}

//llamar a la funcion ajax al cargar el documento
sendDataAjax1();
