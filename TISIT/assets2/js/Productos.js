
var tabla = $("#tbl_Productos").dataTable();
tabla.fnSetColumnVis(0, false);
tabla.fnSetColumnVis(2, false);
tabla.fnSetColumnVis(8, false);
tabla.fnSetColumnVis(9, false);



tabla.fnSetColumnVis(5, false);

function sendDataAjax() {
    $.ajax({
        type: "POST",
        url: "nuevo_producto.aspx/listarProductos",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            addRow(arrObj.d);
        }

    })

}



function printDataAjax(pdfData) {
    var obj = { Id: pdfData[0], Nombre: pdfData[1], Descripcion: pdfData[2], Precio: pdfData[3], ImgURL: pdfData[5], CategoriaId: pdfData[4] }

    $.ajax({
        type: "POST",
        url: "nuevo_producto.aspx/imprimirPDF",
        data: JSON.stringify(obj),
        datat: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (response) {
            if (response) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Documento Creado Correctamente',
                    showConfirmButton: false,
                    timer: 1500

                });
            }
        }

    })

}



function updateDataAjax() {

    //var fileUpload = $("#edit_FUImgProducto").get(0);
    //var file = fileUpload.files;
    //var ImgUrl;

    //if (file.length > 0) {
    //    var fileUploadName = file[0].name;
    //    var ext = fileUploadName.split('.').pop();
    //    var nombre = createGuid();
    //    ImgUrl = nombre + '.' + ext
    //} else {
    //    ImgUrl = $("#edit_txbImg").val()
    //}


    const updateProducto = { objetoProducto: { Id: $("#edit_txbId").val(), Nombre: $("#edit_txbNombre").val(), Descripcion: $("#edit_txbDescripcion").val(), ImgURL: '', Categoria: $("#edit_dropCategorias").val(), Cantidad: $("#edit_txbCantidad").val(), Unidad: $("#edit_dropUnidad").val(), Ubicacion: $("#edit_dropUbicacion").val(), FechaEntrada: '', FechaSalida: '', Precio: $("#edit_txbPrecio").val() } }



    $.ajax({
        type: "POST",
        url: "nuevo_producto.aspx/actualizarProducto",
        data: JSON.stringify(updateProducto),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {


            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (hasfile) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Producto Actualizado Correctamente',
                showConfirmButton: false,
                timer: 1500

            });
            tabla.fnClearTable();
            tabla.fnDraw();
            sendDataAjax();

        }

    })

}

function deleteDataAjax(Id) {

    //var obj = JSON.stringify({ id: JSON.stringify($("edit_txbId").val()), nombre: JSON.stringify($("edit_txbNombre").val()), orden: JSON.stringify($("edit_txbOrden").val()) });


    var obj = { id: Id };

    $.ajax({
        type: "POST",
        url: "nuevo_producto.aspx/eliminarCategoria",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (response) {
            if (response) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Categoria Eliminada Correctamente',
                    showConfirmButton: false,
                    timer: 1500
                });
                tabla.fnClearTable();
                tabla.fnDraw();
                sendDataAjax();
            }
        }

    })

}


function addRow(data) {


    for (var i = 0; i < data.length; i++) {

        var Unidad = data[i].Unidad;
        var Cantidad = data[i].Cantidad;
        var FechaEntrada = data[i].FechaEntrada;
        var FechaSalida = data[i].FechaSalida;
        var Ubicacion = data[i].Ubicacion;
        tabla.fnAddData([
            data[i].Id,
            data[i].Nombre,
            data[i].Descripcion,
            data[i].Precio,
            data[i].Categoria,
            data[i].ImgURL,
            Unidad,
            Cantidad,
            FechaEntrada,
            FechaSalida,
            Ubicacion,
            '<div class="text-center"><button value="Vista Detallada" class="btn btn-info btn-show" data-bs-toggle="modal" data-bs-target="#showmodal"><i class="fa-solid fa-eye"></i></button>&nbsp' +
            '<button value = "Actualizar" class= "btn btn-primary btn-edit" data-bs-toggle="modal" data-bs-target="#editarModal"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp' +
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

//Evento click en el boton imprimirPDF
$(document).on('click', ".btn-PDF", function (e) {
    e.preventDefault();
    var row = $(this).parent().parent();
    var data = tabla.fnGetData(row);
    printDataAjax(data);

})


//Evento click en el boton actualizar
$(document).on('click', '.btn-edit', function (e) {
    e.preventDefault();

    var row = $(this).parent().parent();

    var data = tabla.fnGetData(row);
    fillModalEditData(data);


});

//Evento click en el boton eliminar
$(document).on('click', '.btn-delete', function (e) {
    e.preventDefault();
    Swal.fire({
        title: 'Estas seguro de eliminar esta categoria?',
        text: "Se eliminaran los datos permanentemente",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminala'
    }).then((result) => {
        if (result.isConfirmed) {
            var row = $(this).parent().parent();
            var data = tabla.fnGetData(row);

            deleteDataAjax(data[0]);

        }
    })




});

//Cargar con los datos de la tabla los compos del modalshow
function fillModalDataShow(modelData) {
    $("#show_txbID").val(modelData[0]);
    $("#show_txbNombre").val(modelData[1]);
    $("#show_txbDescripcion").val(modelData[2]);
    $("#show_txbFE").val(modelData[8]);
    $("#show_txbFS").val(modelData[9]);

    $("#imgProducto").attr('src', "\\Imagenes\\productos\\" + modelData[5]);
    $("#imgCB").attr('src', "\\Imagenes\\productos\\imgCodigos\\" + modelData[0] + ".jpg");
}

//Cargar con los datos de la tabla los campos del modaledit
function fillModalEditData(modelData) {
    $("#edit_txbId").val(modelData[0]);
    $("#edit_txbNombre").val(modelData[1]);
    $("#edit_txbDescripcion").val(modelData[2]);
    $("#edit_txbPrecio").val(modelData[3]);
    $("#edit_dropCategorias").val(modelData[4]);

    $("#edit_ImgProducto").attr('src', "\\Imagenes\\productos\\" + modelData[5]);
    $("#edit_ImgCB").attr('src', "\\Imagenes\\productos\\imgCodigos\\" + modelData[0] + ".jpg");
    $("#edit_txbCantidad").val(modelData[7]);
    $("#edit_dropUnidad").val(modelData[6]);
    $("#edit_dropUbicacion").val(modelData[10]);



}

//Enviar los datos del modal al servidor
$(document).on('click', '#btnActualizar', function (e) {
    e.preventDefault();


    updateDataAjax();
});

function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

//llamar a la funcion ajax al cargar el documento
sendDataAjax();
