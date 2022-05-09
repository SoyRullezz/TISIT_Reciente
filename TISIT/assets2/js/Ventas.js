var tabla = $("#tbl_Productos").DataTable({
    paging: false,
    "searching": false,
    "info": false
});
var totalAjax
var sellList = { Id_Venta: 0, items: [] }
var sellListFormat = []

function registrarDetalle_Venta(Id) {


    var items = sellList.items
    items.forEach(i => {
        var item = {}
        item.Id_Venta = Id
        item.Precio_Unitario = i.Precio
        item.Cantidad = i.UnidadesV
        item.Id_Producto = i.Id
        item.Subtotal = i.Subtotal
        sellListFormat.push(item)
    })

    var obj = { sellListFormat }



    $.ajax({
        type: "POST",
        url: "ventas.aspx/registrarDetalle_Ventas",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Venta registrada correctamente',
                showConfirmButton: false,
                timer: 1500
            })
            sellList.items = []
            sellListFormat = []
            $("#lb_Total").text(`$ ${0}`)
            totalAjax = 0
            tabla.clear()
            tabla.draw();
            $("#txbCB").focus()
        }

    })
}

function registrarVenta() {
    var date = new Date(Date.now())
    obj = { venta: { Total: totalAjax, Fecha: date.toLocaleDateString(), Hora: date.toLocaleTimeString() } }
    //console.log(obj)
    //return
    $.ajax({
        type: "POST",
        url: "ventas.aspx/registrarVenta",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            registrarDetalle_Venta(arrObj.d.Id)
        }

    })
}

$("#btnVender").on('click', (e) => {
    e.preventDefault();
    if (sellList.items.length === 0) {
        Swal.fire({
            icon: 'error',
            title: 'Lista de productos vacia',
            text: 'No puedes registrar una venta sin productos',

        })
        return
    }
    registrarVenta()


})

function addProductAjax() {
    var obj = { Id: $("#txbCB").val() }
    $.ajax({
        type: "POST",
        url: "ventas.aspx/addProduct",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            addProduct(arrObj.d)
            tabla.clear()
            tabla.draw();
            addRow();
        }

    })

}

function addRow() {
    var total = 0


    for (var i = 0; i < sellList.items.length; i++) {


        tabla.row.add([
            sellList.items[i].Id,
            sellList.items[i].Nombre,
            sellList.items[i].Precio,
            sellList.items[i].UnidadesV,
            sellList.items[i].Subtotal,
            '<div class="text-center">' +
            '<button value="Eliminar" class="btn btn-danger btn-delete"><i class="fa-solid fa-trash-can"></i></button>&nbsp' +
            '</div > '
        ]).draw(false);


    }

    var subTotal = tabla.column(4).data()


    for (let i = 0; i < subTotal.length; i++) {
        total = total + subTotal[i]
    }

    totalAjax = total
    $("#lb_Total").text(`$ ${total}`)

}
$(document).on('click', '.btn-delete', function (e) {
    e.preventDefault()
    var row = $(this).parent().parent();
    var rowInt = row[0]._DT_CellIndex.row;

    var data = tabla.row(rowInt).data()

    var id = data[0];
    sellList.items = sellList.items.filter(item => (item.Id !== id))
    tabla.clear();
    tabla.draw()
    addRow();
})

$("#txbCB").keypress((e) => {
    if (e.keyCode === 13) {
        e.preventDefault()
        addProductAjax()
        $("#txbCB").val('')
        $("#txbCB").focus()

    }
})

function addProduct(Product) {
    var producto = {}
    producto.Id = Product.Id
    producto.Nombre = Product.Nombre
    producto.Precio = Product.Precio

    for (let i = 0; i < sellList.items.length; i++) {

        if (Product.Id === sellList.items[i].Id) {
            sellList.items[i].UnidadesV = sellList.items[i].UnidadesV + 1
            sellList.items[i].Subtotal = sellList.items[i].UnidadesV * sellList.items[i].Precio
            return
        }
    }
    producto.UnidadesV = 1
    producto.Subtotal = producto.Precio * producto.UnidadesV
    sellList.items.push(producto)
}

