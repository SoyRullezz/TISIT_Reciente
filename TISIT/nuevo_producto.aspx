<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevo_producto.aspx.cs" Inherits="TISIT.nuevo_producto" ClientIDMode="Static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Last-Modified" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate" />
    <meta http-equiv="Pragma" content="no-cache" />

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Nunito:wght@300;400;600;700;800&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="assets2/css/bootstrap.css" />

    <link rel="stylesheet" href="assets2/vendors/simple-datatables/style.css" />

    <link rel="stylesheet" href="assets2/vendors/perfect-scrollbar/perfect-scrollbar.css" />
    <link rel="stylesheet" href="assets2/vendors/bootstrap-icons/bootstrap-icons.css" />
    <link rel="stylesheet" href="assets2/css/app.css" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" integrity="sha512-9usAa10IRO0HhonpyAIVpjrylPvoDwiPUiKdWk5t3PyolY1cOd4DSE0Ga+ri4AuTroPR5aQvXU9xC6qOPnzFeg==" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.4/css/jquery.dataTables.min.css" />
    <link rel="shortcut icon" href="assets2/images/logo/MATI PNG.ico" type="image/x-icon" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>



    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script>
        function Exitoso() {
            swal("REGISTRO EXITOSO", "El producto fue registrado", "success");
        }

        function MostrarMensaje(mensaje) {
            //swal(mensaje);
            swal("ALERTA PRODUCTOS", mensaje, "info");
        }


    </script>
</head>
<body>
    <style>
        /*.logo img {
            min-width: 200px;
            min-height: 300px;
            position: center;
        }

        @media screen and (max-width: 736px) {
            .logo img {
                min-width: 100px;
                min-height: 200px;
                position: center;
            }
        }*/

        footer {
            position: fixed;
            right: 0px;
            bottom: 0px;
            margin-right: 1rem;
        }

        .scroll {
            overflow: scroll;
            height: 100%;
            width: 100%;
        }

            .scroll table {
                width: 100%;
            }
    </style>

    <form id="form1" runat="server">

        <div id="app">
            <div id="sidebar" class="active">
                <div class="sidebar-wrapper active">
                    <div class="sidebar-header">
                        <div class="d-flex justify-content-between">
                            <div class="logo">
                                <a href="inicio.html">
                                    <img src="img/tisit_logo1.png" alt="Logo" />
                                </a>
                            </div>
                            <div class="toggler">
                                <a href="#" class="sidebar-hide d-xl-none d-block"><i class="bi bi-x bi-middle"></i></a>
                            </div>
                        </div>
                    </div>
                    <div class="sidebar-menu">
                        <ul class="menu">
                            <%-- <li class="sidebar-title">Menu</li>--%>
                            <li class="sidebar-item active">
                                <a href="inicio.aspx" class='sidebar-link'>

                                    <span>Menu</span>
                                </a>
                            </li>
                            <li class="sidebar-item  has-sub" runat="server" id="adminlink">
                                <a href="#" class='sidebar-link'>
                                    <i style="font-size: 18px" class="bi bi-person-badge"></i>
                                    <span>Administracion</span>
                                </a>
                                <ul class="submenu">
                                    <li class="submenu-item " runat="server" id="usulink">
                                        <a href="alta_personal.aspx">Alta de empleados</a>
                                    </li>

                                    <li class="submenu-item " runat="server" id="direclink">
                                        <a href="direccion_personal.aspx">Direccion personal</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="doculink">
                                        <a href="documentacion_personal.aspx">Documentación personal</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="lablink">
                                        <a href="contrato_laboral.aspx">Contrato laboral</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="perlink">
                                        <a href="permisos.aspx">Permisos</a>
                                    </li>
                                </ul>
                            </li>
                            <li class="sidebar-item  has-sub" runat="server" id="faclink">
                                <a href="#" class='sidebar-link'>
                                    <i style="font-size: 18px" class="bi bi-file-earmark-spreadsheet-fill"></i>
                                    <span>Personal Médico<br />
                                        y Citas</span>
                                </a>
                                <ul class="submenu">
                                    <li class="submenu-item " runat="server" id="doclink">
                                        <a href="doctores.aspx">Doctores</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="verlink">
                                        <a href="registro_pacientes.aspx">Registro Pacientes</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="faclink1">
                                        <a href="cancelar_cita.aspx">Cancelar Citas</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="reporlink">
                                        <a href="calendarioadmin.aspx">Agendar Citas</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="callink">
                                        <a href="Calendario.aspx">Calendario de Citas</a>
                                    </li>
                                </ul>
                            </li>
                            <li class="sidebar-item  has-sub" runat="server" id="docpaclink">
                                <a href="#" class='sidebar-link'>
                                    <i class="bi bi-file-earmark-fill"></i>
                                    <span>Documentos<br />
                                        y Solicitudes</span>
                                </a>
                                <ul class="submenu">
                                    <li class="submenu-item " runat="server" id="sollink">
                                        <a href="estudios.aspx">Solicitud de Estudios</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="conslink">
                                        <a href="consentimiento.aspx">Solicitud de Ingreso y Consentimiento iLaser</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="lucialink">
                                        <a href="ingreso.aspx">Solicitud de Ingreso y Consentimiento Santa Lucía</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="inglink">
                                        <a href="corte_caja.aspx">Reporte de Ingresos</a>
                                    </li>
                                </ul>
                            </li>
                            <li class="sidebar-item  has-sub" runat="server" id="deslink">
                                <a href="#" class='sidebar-link'>
                                    <i class="bi bi-minecart-loaded"></i>
                                    <span>Productos</span>
                                </a>
                                <ul class="submenu">
                                    <li class="submenu-item " runat="server" id="nuevolink">
                                        <a href="productos_hospital.aspx">Inventario de Hospital</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="Li2">
                                        <a href="requisiciones.aspx">Requisiciones</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="Li1">
                                        <a href="requisicion_hospital.aspx">Requisición de Material</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="proclink">
                                        <a href="nuevo_producto.aspx">Productos Nuevos</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="consulink">
                                        <a href="ventas.aspx">Ventas</a>
                                    </li>
                                </ul>
                            </li>

                            <li class="sidebar-item  has-sub">
                                <a href="#" class='sidebar-link'>
                                    <i class="bi bi-person-fill"></i>
                                    <span>Usuario</span>
                                </a>
                                <ul class="submenu ">
                                    <li class="submenu-item ">
                                        <a href="cambiar_usuarios.aspx">Cambiar Usuario</a>
                                    </li>
                                    <li class="submenu-item ">
                                        <a href="cambiar_contra.aspx">Cambiar Contraseña</a>
                                    </li>
                                    <li class="submenu-item ">
                                        <asp:LinkButton ID="btncerrar" runat="server" OnClick="btncerrar_Click">Cerrar Sesión</asp:LinkButton>
                                    </li>
                                </ul>
                            </li>
                            <li class="sidebar-item">
                                <asp:Label ID="lbcorreo" runat="server" Text=""></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <button class="sidebar-toggler btn x"><i data-feather="x"></i></button>
                </div>
            </div>

            <div id="main">
                <header class="mb-3">
                    <a href="#" class="burger-btn d-block d-xl-none">
                        <i class="bi bi-justify fs-3"></i>
                    </a>
                </header>
                <div class="">
                    <h3>Nuevo Producto</h3>
                </div>
                <br />
                <%--PARTE DE LA PAGINA--%>
                <asp:TextBox CssClass="form-control" ID="producto" runat="server" Visible="false"></asp:TextBox>

                <div class="card">
                    <div class="card-header">
                        <p style="font-weight: bold">
                            Registro de producto<code></code><code><hr>
                            </code>
                        </p>
                    </div>
                    <div class="card-content">
                        <section class="container">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label class="form-label">Nombre</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox CssClass="form-control" ID="txbNombre" runat="server" placeholder="Ingrese el nombre del producto"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="producto" runat="server" ControlToValidate="txbNombre" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                <label class="form-label">Descripcion</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox Rows="5" TextMode="MultiLine" CssClass="form-control" ID="txbDescripcion" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="sdf" ValidationGroup="producto" runat="server" ControlToValidate="txbDescripcion" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group">
                                                <label class="form-label">Imagen</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:FileUpload CssClass="form-control" ID="FUImgProducto" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label class="form-label">Unidad de Medida</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="dropUnidad" runat="server" CssClass="form-select">
                                                    <%--                <asp:ListItem Value="0" Enabled="true" Selected="True">---Seleccione una Unidad de Medida---</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="producto" runat="server" ControlToValidate="dropUnidad" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group">
                                                <label class="form-label">Cantidad</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox TextMode="Number" CssClass="form-control" ID="txbCantidad" runat="server" placeholder="Ingrese la cantidad del producto"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="producto" runat="server" ControlToValidate="txbCantidad" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group">
                                                <label class="form-label">Precio</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox TextMode="Number" CssClass="form-control" ID="txbPrecio" runat="server" placeholder="Ingrese el precio del producto"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="producto" runat="server" ControlToValidate="txbPrecio" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group">
                                                <label class="form-label">Categoria</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="dropCategorias" runat="server" CssClass="form-select">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="producto" runat="server" ControlToValidate="dropCategorias" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group">
                                                <label class="form-label">Ubicación</label>

                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="dpUbicacion" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="producto" runat="server" ControlToValidate="dpUbicacion" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col" style="text-align: left">

                                    <asp:LinkButton ID="btnNuevaCategoria" runat="server" ValidationGroup="producto" CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click"><i style="font-size:20px" class="bi bi-save-fill"></i>&nbsp;  Registrar</asp:LinkButton>

                                </div>

                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <div class="box box-primary">
                                        <div class="card-header">
                                            <p style="font-weight: bold">
                                                Productos guardados<code></code><code><hr>
                                                </code>
                                            </p>

                                        </div>
                                        <div class="box-body table table-responsive">
                                            <table id="tbl_Productos" class="table table-responsive table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Id</th>
                                                        <th>Nombre</th>
                                                        <th>Descripcion</th>
                                                        <th>Precio</th>
                                                        <th>Categoria</th>
                                                        <th>ImgURL</th>
                                                        <th>Unidad</th>
                                                        <th>Cantidad</th>
                                                        <th>Fecha de Entrada</th>
                                                        <th>Fecha Max. de Salida</th>
                                                        <th>Ubicacion en inventario</th>
                                                        <th>Acciones</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbl_bodyProductos">
                                                    <!--Aqui se insertan  los datos de la BD-->
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>

                            </div>

                        </section>

                        <!--Modal para actualizar-->
                        <div class="modal fade" id="editarModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Editar Producto</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <div class="mb-3 d-d-none">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="edit_txbId"></asp:TextBox>
                                            </div>
                                            <div class="mb-3 d-none">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="edit_txbImg"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label for="edit_txbNombre" class="col-form-label">Nombre:</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="edit_txbNombre"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label for="edit_txbDescripcion" class="col-form-label">Descripcion:</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="edit_txbDescripcion"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label class="form-label">Unidad de Medida</label>
                                                <asp:DropDownList ID="edit_dropUnidad" runat="server" CssClass="form-select">
                                                    <%--                <asp:ListItem Value="0" Enabled="true" Selected="True">---Seleccione una Unidad de Medida---</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="producto" runat="server" ControlToValidate="edit_dropUnidad" ErrorMessage="Campo necesario" ForeColor="Red">Llena el campo</asp:RequiredFieldValidator>

                                            </div>
                                            <div class="mb-3">
                                                <label for="edit_txbCantidad" class="col-form-label">Cantidad:</label>
                                                <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" ID="edit_txbCantidad"></asp:TextBox>
                                            </div>

                                            <div class="mb-3">
                                                <label for="edit_txbPrecio" class="col-form-label">Precio:</label>
                                                <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" ID="edit_txbPrecio"></asp:TextBox>
                                            </div>

                                            <div class="mb-3">
                                                <label for="edit_dropCategorias" class="col-form-label">Categoria:</label>
                                                <asp:DropDownList ID="edit_dropCategorias" runat="server" CssClass="form-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="mb-3">
                                                <label for="edit_dropUbicacion" class="col-form-label">Ubicacion:</label>
                                                <asp:DropDownList ID="edit_dropUbicacion" runat="server" CssClass="form-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="mb-3 row">
                                                <div class="col text-center">
                                                    <label class="col-form-label">Codigo de Barras</label>
                                                    <asp:Image CssClass="img-fluid img-thumbnail" ID="edit_ImgCB" runat="server" />
                                                </div>
                                                <div class="col text-center">
                                                    <label class="col-form-label">Imagen del Producto</label>
                                                    <asp:Image CssClass="img-fluid img-thumbnail" ID="edit_ImgProducto" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                        <button id="btnActualizar" type="button" class="btn btn-primary">Editar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--Modal para mostrar vista detallada-->

                        <div class="modal fade" id="showmodal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="showTitleModal">Vista Detallada</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <div class="mb-3 d-none">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="show_txbID"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label for="show_txbNombre" class="col-form-label">Nombre:</label>
                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="show_txbNombre"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label for="show_txbDescripcion" class="col-form-label">Descripcion</label>
                                                <asp:TextBox TextMode="MultiLine" ReadOnly="true" runat="server" CssClass="form-control" ID="show_txbDescripcion"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label for="show_txbDescripcion" class="col-form-label">Fecha de Entrada</label>
                                                <asp:TextBox ReadOnly="true" runat="server" CssClass="form-control" ID="show_txbFE"></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <label for="show_txbDescripcion" class="col-form-label">Fecha de Maxima de Salida</label>
                                                <asp:TextBox ReadOnly="true" runat="server" CssClass="form-control" ID="show_txbFS"></asp:TextBox>
                                            </div>

                                            <div class="mb-3 row">
                                                <div class="col text-center">
                                                    <label class="col-form-label">Codigo de Barras</label>
                                                    <asp:Image CssClass="img-fluid img-thumbnail" ID="imgCB" runat="server" />
                                                </div>
                                                <div class="col text-center">
                                                    <label class="col-form-label">Imagen del Producto</label>
                                                    <asp:Image CssClass="img-fluid img-thumbnail" ID="imgProducto" runat="server" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--FIN PARTE DE LA PAGINA--%>
                <footer>
                    <div class="footer clearfix mb-0 text-muted">
                        <div class="float-start">
                            <br />
                            <p>2022 &copy; Todos los derechos reservados.</p>
                        </div>
                        <div class="float-end">
                            <p>
                                <img src="assets2/images/logo/mati_tenkui2.png" width="250px" />
                            </p>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
    </form>

    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>

    <script src="https://kit.fontawesome.com/4c35c9df44.js" crossorigin="anonymous"></script>
    }
  <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.4/js/jquery.dataTables.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jQuery-slimScroll/1.3.8/jquery.slimscroll.min.js"></script>



    <script src="assets2/js/main.js"></script>
    <script src="assets2/js/Productos.js" type="text/javascript"></script>
</body>
</html>
