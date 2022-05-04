
var tabla = $("#tbl_Citas").dataTable();

function sendDataAjax() {
    $.ajax({
        type: "POST",
        url: "ingreso.aspx/listarCitas",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            addRow(arrObj.d);
            //console.log(arrObj.d);

            tabla.clear()
            tabla.draw();
            addRow(arrObj.d)
        }

    })

}

function addRow(data) {


    for (var i = 0; i < data.length; i++) {

        //var Unidad = data[i].Unidad;
        //var Cantidad = data[i].Cantidad;
        //var FechaEntrada = data[i].FechaEntrada;
        //var FechaSalida = data[i].FechaSalida;
        //var Ubicacion = data[i].Ubicacion;
        tabla.fnAddData([
            data[i].id_llegada,
            data[i].nombre_paciente,
            data[i].nombre_doctor,
            data[i].tipo_cita,
            data[i].fecha_ingreso,
            data[i].hora_ingreso,
            //Unidad,
            //Cantidad,
            //FechaEntrada,
            //FechaSalida,
            //Ubicacion,
            '<div class="text-center"><button value="Vista Detallada" class="btn btn-secondary btn-show close" data-bs-dismiss="modal" >SELECCIONAR</button>' +
            //'<button value="Eliminar" class="btn btn-danger btn-delete"><i class="fa-solid fa-trash-can"></i></button>&nbsp' +
            //'<button value="Imprimir_VD" class="btn btn-secondary btn-PDF"><i class="fa-solid fa-file-lines"></i></button>'+
            '</div > '
        ]);
    }

}

//Evento click en el boton mostrar
$(document).on('click', ".btn-show", function (e) {
    e.preventDefault();
    var row = $(this).parent().parent();
    var data = tabla.fnGetData(row);
    fillModalDataShow(data)
})

//Cargar con los datos de la tabla los compos del modalshow
function fillModalDataShow(modelData) {
    $("#txtpaciente").val(modelData[1]);
    //$("#show_txbNombre").val(modelData[1]);

}

//llamar a la funcion ajax al cargar el documento
sendDataAjax();
