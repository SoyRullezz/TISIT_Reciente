var tabla = $("#tbl_Productos").DataTable({
    paging: true,
    "searching": true,
    "info": false
});

var rbGroupTipoBusqueda = $("input[name=tipoBusqueda]");
var FI = $("#txb_FI");
var FF = $("#txb_FF");
var FIL = $("#lb_FI");
var FFL = $("#lb_FF");

var reportes;


const buscarReportes = (tipoBusqueda) => {

    var obj = { FF: FF.val(), FI: FI.val(), tipo: tipoBusqueda }
    $.ajax({
        type: "POST",
        url: "corte_caja.aspx/buscarReportes",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {


            if (arrObj.d.length === 0) {
                Swal.fire({
                    icon: 'error',
                    title: 'Sin Registros',
                    text: 'No se encontraron registros de venta con las fechas especificadas',

                })
                tabla.clear()
                tabla.draw();
                return
            }
            tabla.clear()
            tabla.draw();
            addRow(arrObj.d)
            reportes = arrObj.d

        }

    })
}


$("#btn_BuscarReportes").on('click', (e) => {
    e.preventDefault()

    var checkedValue = rbGroupTipoBusqueda.filter(":checked").val();

    buscarReportes(checkedValue)




})

rbGroupTipoBusqueda.change(() => {
    switch (rbGroupTipoBusqueda.filter(":checked").val()) {
        case 'Despues':
            FF.attr('class', 'd-none')
            FFL.attr('class', 'd-none')
            FIL.text('Fecha:')
            break;
        case 'Antes':
            FF.attr('class', 'd-none')
            FFL.attr('class', 'd-none')
            FIL.text('Fecha:')
            break;
        case 'Entre':
            FF.attr('class', 'form-label')
            FFL.attr('class', 'form-label')
            FIL.text('Fecha Inicial:')
            break;

        default:
            ;
    }
})

function addRow(listaReportes) {



    for (var i = 0; i < listaReportes.length; i++) {


        tabla.row.add([
            listaReportes[i].Fecha,
            listaReportes[i].Total,
            '<div class="text-center">' +
            '<button value="Descargar" class="btn btn-danger btn-descargar"><i class="fa-solid fa-file-arrow-down"></i></button>&nbsp' +
            '</div > '
        ]).draw(false);


    }
}

$(document).on('click', '.btn-descargar', function (e) {
    e.preventDefault()
    $.ajax({
        type: "POST",
        url: "corte_caja.aspx/imprimirPDF",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function () {

        }

    })
})

