0<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="documentacion_personal.aspx.cs" Inherits="TISIT.documentacion_personal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Nunito:wght@300;400;600;700;800&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="assets2/css/bootstrap.css">

    <link rel="stylesheet" href="assets2/vendors/simple-datatables/style.css">

    <link rel="stylesheet" href="assets2/vendors/perfect-scrollbar/perfect-scrollbar.css">
    <link rel="stylesheet" href="assets2/vendors/bootstrap-icons/bootstrap-icons.css">
    <link rel="stylesheet" href="assets2/css/app.css">
    <link rel="shortcut icon" href="assets2/images/favicon.svg" type="image/x-icon">

    <!--========== CSS ==========-->
    <link rel="stylesheet" href="assets/css/styles.css">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="_framework/blazor.webassembly.js"></script>
    <script>
        function alertaExito() {
            Swal.fire(
                'INFORMACION GUARDADA',
                'Los datos fueron almacenados correctamente',
                'success'
            )
        }
        function errorguardado() {
            Swal.fire({
                title: 'ERROR',
                text: "Hubo un error al guardar los datos por favor, verifique",
                icon: 'error',
            })
        }

        function alertaExitoActualizar() {
            Swal.fire(
                'Modificación realizada correctamente',
                'La modificación se ha realizado con exito',
                'success'
            )
        }

        function erroractualizado() {
            Swal.fire({
                title: 'ERROR',
                text: "Hubo un error al actualizar los datos por favor, verifique",
                icon: 'error',
            })
        }
    </script>

</head>
<body>
    <form id="frmAltaPersonal" data-parsley-validate runat="server">

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
                                        <a href="#">Reporte de Ingresos</a>
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
                                        <a href="#">Ventas</a>
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

            <!--========== CONTENTS ==========-->
            <div id="main">
                <header class="mb-3">
                    <a href="#" class="burger-btn d-block d-xl-none">
                        <i class="bi bi-justify fs-3"></i>
                    </a>
                </header>
                <div class="container">
                    <h3>Documentación Personal</h3>
                </div>
                <br />
                <!-- Page content-->
                <div class="container">
                    <br />
                    <div class="row">

                        <div class="card card-body" runat="server" id="agregardocumentacion">
                            <div class="col-lg" style="display: flex; justify-content: flex-end">
                                <button type="button" class="btn btn-outline-warning block"
                                    data-bs-toggle="modal" data-bs-target="#xlarge1" runat="server" id="Button1">
                                    CONSULTAR
                                </button>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually" id="lblno">N° TRABAJADOR</label>
                                    <asp:TextBox ID="txtnumberdocu" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" BackColor="White" ControlToValidate="txtnumberdocu" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually" id="lblnombre">NOMBRE</label>
                                    <asp:TextBox ID="txtnombredocu" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" BackColor="White" ControlToValidate="txtnombredocu" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">APELLIDO PATERNO</label>
                                    <asp:TextBox ID="txtpaterno" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" BackColor="White" ControlToValidate="txtpaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">APELLIDO MATERNO</label>
                                    <asp:TextBox ID="txtmaterno" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" BackColor="White" ControlToValidate="txtmaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <button type="button" class="btn btn-outline-info block"
                                        data-bs-toggle="modal" data-bs-target="#xlarge" runat="server" id="Button2">
                                        BUSCAR
                                    </button>
                                </div>
                                <%--<div class="col-lg">
                                    
                                </div>--%>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually" id="lblcorreopersonal">CORREO ELECTRONICO PERSONAL</label>
                                    <asp:TextBox ID="txtcorreopersonal" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" BackColor="White" ControlToValidate="txtcorreopersonal" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually" id="lblcorreoinst">CORREO ELECTRONICO INSTITUCIONAL</label>
                                    <asp:TextBox ID="txtcorreoinstitucional" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" BackColor="White" ControlToValidate="txtcorreoinstitucional" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;" runat="server" id="acta">
                                <div class="col-6">
                                    <label class="visually" id="lblacta" runat="server">ACTA DE NACIMIENTO</label>
                                    <asp:FileUpload ID="fileacta" Font-Size="Small" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="White" ControlToValidate="fileacta" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;" runat="server" id="ine">
                                <div class="col-6">
                                    <label class="visually" id="lbline" runat="server">IDENTIFICACIÓN (INE)</label>
                                    <asp:FileUpload ID="fileine" Font-Size="Small" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="White" ControlToValidate="fileine" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;" runat="server" id="comprobante">
                                <div class="col-6">
                                    <label class="visually" id="lblcomp" runat="server">COMPROBANTE DE ESTUDIOS</label>
                                    <asp:FileUpload ID="filecomprobante" Font-Size="Small" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="White" ControlToValidate="filecomprobante" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="doc_admin">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;" runat="server">
                                <div class="col-lg">
                                    <asp:LinkButton ID="btnguardardocu" runat="server" CssClass="btn btn-outline-success" ValidationGroup="doc_admin" OnClick="btnguardardocu_Click"><span class="glyphicon glyphicon-save"> GUARDAR</span></asp:LinkButton>


                                    &nbsp;&nbsp;
                                        <asp:LinkButton ID="btneditar" runat="server" CssClass="btn btn-outline-success" OnClick="btneditar_Click" Visible="false"><span class="glyphicon glyphicon-save"> EDITAR</span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                        <asp:LinkButton ID="btncancel" runat="server" CssClass="btn btn-outline-danger" Visible="false" OnClick="btncancel_Click"><span class="glyphicon glyphicon-save"> CANCELAR EDICION</span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnacta" runat="server" CssClass="btn btn-outline-secondary" Visible="false" OnClick="btnacta_Click"><span class="glyphicon glyphicon-save"> EDITAR ACTA</span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnine" runat="server" CssClass="btn btn-outline-secondary" Visible="false" OnClick="btnine_Click"><span class="glyphicon glyphicon-save"> EDITAR INE</span></asp:LinkButton>
                                    &nbsp;&nbsp;
                                        <asp:LinkButton ID="btncomprobante" runat="server" CssClass="btn btn-outline-secondary" Visible="false" OnClick="btncomprobante_Click"><span class="glyphicon glyphicon-save"> EDITAR COMPROBANTE</span></asp:LinkButton><br />
                                    &nbsp;&nbsp;
                                    <%--                                        <asp:LinkButton ID="btncancelar" runat="server" CssClass="btn btn-outline-secondary" Visible="false" ><span class="glyphicon glyphicon-save"> CANCELAR EDICION </span></asp:LinkButton><br />--%>
                                    <br />
                                    <asp:LinkButton ID="btncancelaredit" runat="server" CssClass="btn btn-outline-danger" Visible="false" OnClick="btncancelaredit_Click"><span class="glyphicon glyphicon-save"> CANCELAR EDICION DE DOCUMENTOS </span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <%--<div class="card card-body" id="consultadocu" runat="server" visible="false">
                            <div class="row" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">Nombre del empleado</label>
                                    <asp:TextBox ID="txtnombreemple" runat="server" CssClass="form-control"></asp:TextBox>
                                    <br />
                                    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info" OnClick="btnBuscar_Click"><i class="glyphicon glyphicon-search"></i>BUSCAR</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btncancelardocu" runat="server" CssClass="btn btn-danger" OnClick="btncancelardocu_Click"><span class="glyphicon glyphicon-save"> CANCELAR BUSQUEDA</span></asp:LinkButton>
                                </div>

                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>

        </div>

        <!--Extra Large Modal -->
        <div class="modal fade text-left w-100" id="xlarge" tabindex="-1"
            role="dialog" aria-labelledby="myModalLabel16" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel16">Usuarios</h4>
                        <button type="button" class="close" data-bs-dismiss="modal"
                            aria-label="Close">
                            <i data-feather="x"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gridempleados" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnSelectedIndexChanged="gridempleados_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="id_empleado" HeaderText="ID EMPLEADO" />
                                <asp:BoundField DataField="no_empleado" HeaderText="NO. EMPLEADO" />
                                <asp:BoundField DataField="nombre" HeaderText="NOMBRE" />
                                <asp:BoundField DataField="paterno" HeaderText="A. PATERNO" />
                                <asp:BoundField DataField="materno" HeaderText="A. MATERNO" />
                                <asp:BoundField DataField="fecha_nacimiento" HeaderText="FECHA NACIMIENTO" />
                                <asp:BoundField DataField="curp" HeaderText="CURP" />
                                <asp:BoundField DataField="rfc" HeaderText="R.F.C" />
                                <asp:BoundField DataField="nss" HeaderText="NSS" />
                                <asp:CommandField ButtonType="Button" ShowSelectButton="True">
                                    <ControlStyle CssClass="btn btn-secondary" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>

        <!--Extra Large Modal -->
        <div class="modal fade text-left w-100" id="xlarge1" tabindex="-1"
            role="dialog" aria-labelledby="myModalLabel17" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel17">Usuarios</h4>
                        <button type="button" class="close" data-bs-dismiss="modal"
                            aria-label="Close">
                            <i data-feather="x"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gridocu" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnSelectedIndexChanged="gridocu_SelectedIndexChanged" DataKeyNames="no_empleado">
                            <Columns>
                                <asp:BoundField DataField="no_empleado" HeaderText="N° DE EMPLEADO" ReadOnly="True" />
                                <asp:BoundField DataField="nombre" HeaderText="NOMBRE" />
                                <asp:BoundField DataField="paterno" HeaderText="A. PATERNO" />
                                <asp:BoundField DataField="materno" HeaderText="A. MATERNO" />
                                <asp:BoundField DataField="correo_personal" HeaderText="CORREO PERSONAL" />
                                <asp:BoundField DataField="correo_institucional" HeaderText="CORREO INSTITUCIONAL" />
                                <asp:TemplateField HeaderText="Acta de Nacimiento">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click" Text='<%# Eval("FileName1") %>' CssClass="btn btn-link"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Identificacion (INE)">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton14_Click" Text='<%# Eval("FileName2") %>' CssClass="btn btn-link"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comprobante de Estudios">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click1" Text='<%# Eval("FileName3") %>' CssClass="btn btn-link"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" SelectText="EDITAR" ShowSelectButton="True">
                                    <ControlStyle CssClass="btn btn-outline-secondary" />
                                </asp:CommandField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:LinkButton ID="LinkButton10" runat="server" Text='<%# Eval("FileName1") %>' CssClass="btn btn-link"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton11" runat="server" Text='<%# Eval("FileName2") %>' CssClass="btn btn-link"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton12" runat="server" Text='<%# Eval("FileName3") %>' CssClass="btn btn-link"></asp:LinkButton>
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="assets2/vendors/simple-datatables/simple-datatables.js"></script>
    <script>
        // Simple Datatable
        let tablep = document.querySelector('#gridcontra');
        let dataTable = new simpleDatatables.DataTable(tablep);
    </script>

    <script>
        let table1 = document.querySelector('#GridView1');
        let dataTable1 = new simpleDatatables.DataTable(table1);
    </script>
    <script src="assets2/js/main.js"></script>
</body>
</html>

