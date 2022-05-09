<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="corte_caja.aspx.cs" Inherits="TISIT.corte_caja" ClientIDMode="Static" %>

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
        .logo img {
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
        }

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
                                        <a href="citas.aspx">Citas</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="reporlink">
                                        <a href="calendarioadmin.aspx">Agendar Citas</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="Li1">
                                        <a href="calendario.aspx">Calendario de Citas</a>
                                    </li>
                                </ul>
                            </li>
                            <%--<li class="sidebar-item  has-sub" runat="server" id="prodlink">
                            <a href="#" class='sidebar-link'>
                                <i class="bi bi-box-seam"></i>
                                <span>Entrada de Producto</span>
                            </a>
                            <ul class="submenu ">
                                
                                
                            </ul>
                        </li>--%>
                            <li class="sidebar-item  has-sub" runat="server" id="deslink">
                                <a href="#" class='sidebar-link'>
                                    <i class="bi bi-minecart-loaded"></i>
                                    <span>Productos</span>
                                </a>
                                <ul class="submenu">
                                    <li class="submenu-item " runat="server" id="nuevolink">
                                        <a href="#">Inventario</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="proclink">
                                        <a href="nuevo_producto.aspx">Productos Nuevos</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="consulink">
                                        <a href="#">Ventas</a>
                                    </li>
                                    <%--<li class="submenu-item " runat="server" id="invreclink">
                                        <a href="procesoFinalizado.aspx">Inventario reciclado</a>
                                    </li>--%>
                                </ul>
                            </li>
                            <%--<li class="sidebar-item  has-sub" runat="server" id="vendelink">
                                <a href="#" class='sidebar-link'>
                                    <i class="bi bi-file-earmark-person-fill"></i>
                                    <span>Reporte Vendedores</span>
                                </a>
                                <ul class="submenu ">
                                    <li class="submenu-item " runat="server" id="geolink">
                                        <a href="geolocalizacion.aspx">Geolocalización</a>
                                    </li>
                                    <li class="submenu-item " runat="server" id="llamlink">
                                        <a href="llamadas.aspx">Llamadas</a>
                                    </li>
                                </ul>
                            </li>--%>
                            <li class="sidebar-item  has-sub">
                                <a href="#" class='sidebar-link'>
                                    <i class="bi bi-person-fill"></i>
                                    <span>Usuario</span>
                                </a>
                                <ul class="submenu ">
                                    <li class="submenu-item ">
                                        <a href="cambiar_contra.aspx">Cambiar Contraseña</a>
                                    </li>
                                    <li class="submenu-item ">
                                        <asp:LinkButton ID="btncerrar" runat="server">Cerrar Sesión</asp:LinkButton>
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

                <%--PARTE DE LA PAGINA--%>
                <asp:TextBox CssClass="form-control" ID="producto" runat="server" Visible="false"></asp:TextBox>

                <div class="card">
                    <div class="card-header">
                        <p style="font-weight: bold">
                            Cortes de Caja
                        </p>
                    </div>
                    <div class="card-content">
                        <section class="container">
                            <div class="col">
                                <div class="box box-primary">
                                        <div class="box-body">
                                            <div class="row justify-content-between"> 
                                                <div class="form-group col text-center">
                                                
                                                <input class="form-check-input" type="radio" name="tipoBusqueda" id="rb-Antes" value="Antes" />                                         
                                                <label class="form-check-label">Antes del:</label>
            
                                                </div>
                                                <div class="form-group col text-center">
                                                
                                                 <input class="form-check-input" type="radio" name="tipoBusqueda" id="rb-Despues" value="Despues" />                                         
                                                <label class="form-check-label">Despues del:</label>                              

                                                </div>
                                                <div class="form-group col text-center">
                                                
                                                 <input class="form-check-input" type="radio" name="tipoBusqueda" id="rb-Entre"  value="Entre" checked="checked"/>                                         
                                                <label class="form-check-label">Entre el:</label>                              

                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="form-group col text-end">
                                                
                                                <label id="lb_FI" class="form-label">Fecha Inicial:</label>
                                                    <asp:TextBox ID="txb_FI" TextMode="Date" runat="server" class=""></asp:TextBox>                                       
            
                                                </div>
                                                 <div class="form-group col text-start">
                                                
                                                <label id="lb_FF" class="form-label">Fecha Final:</label>
                                                    <asp:TextBox ID="txb_FF" TextMode="Date" runat="server" class=""></asp:TextBox>                                       
            
                                                </div>
                                            </div>
                                             <div class="row justify-content-center">
                                                 <asp:Button CssClass="btn btn-primary" Width="200" ID="btn_BuscarReportes" runat="server" Text="Buscar Reportes" />
                                              
                                            </div>
                                        
                                        </div>
                                    </div>
                            </div>
                   
                            <div class="row">
                                <div class="col-12">
                                    <div class="box box-primary">
                                        <div class="card-header pb-0">
                                            <p class="mb-1" style="font-weight: bold">
                                                Reportes
                                                
                                            </p>

                                        </div>
                                        <div class="box-body table table-responsive">
                                            <table id="tbl_Productos" class="table table-responsive table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center">Fecha</th>
                                                        <th class="text-center">Monto</th>
                                                        <th class="text-center">Descargar Reporte</th>
                                                        
                                                        
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
    <script src="assets2/js/Corte_Caja.js"></script>
</body>
</html>