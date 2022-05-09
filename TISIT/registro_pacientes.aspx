<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registro_pacientes.aspx.cs" Inherits="TISIT.registro_pacientes" %>

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
    <%--<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/boxicons@latest/css/boxicons.min.css">--%>


    <!--========== CSS ==========-->
    <%--<link rel="stylesheet" href="assets/css/styles.css">--%>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="_framework/blazor.webassembly.js"></script>

    <script>
        function alertaExito() {
            Swal.fire(
                'REGISTRADO EXITOSAMENTE',
                'El registro del paciente ha sido registrado con exito',
                'success'
            )
        }

        function alertaerror() {
            Swal.fire({
                title: 'ERROR AL REGISTRARSE',
                text: "Hubo un error por favor verifique",
                icon: 'error',
            })
        }

        function alertaInfo() {
            Swal.fire(
                'POR FAVOR SELECCIONE UN DOCTOR',
                'Por favor elige un doctor',
                'info'
            )
        }
    </script>

    <style>
        .ok {
            display: none;
        }

        .activate_ok {
            background-color: #4B778D !important;
        }

        .desactivate_ok {
            display: none;
        }

        .bloq:hover {
            cursor: no-drop;
        }

        .centrado {
            margin-left: auto;
            margin-right: auto;
        }

        .usuario {
            position: absolute;
            top: 20px;
            right: 115px;
        }
    </style>

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
            <!--========== CONTENTS ==========-->
            <div id="main">
                <header class="mb-3">
                    <a href="#" class="burger-btn d-block d-xl-none">
                        <i class="bi bi-justify fs-3"></i>
                    </a>
                </header>
                <div class="container">
                    <h3>Registro de Pacientes</h3>
                </div>
                <br />
                <!-- Page content-->
                <div class="container">
                    <br />
                    <div class="row">
                        <div class="card card-body" id="consultaempleados" runat="server">
                            <div class="col-lg" style="display: flex; justify-content: flex-end">
                                <button type="button" class="btn btn-outline-warning block"
                                    data-bs-toggle="modal" data-bs-target="#xlarge1" runat="server" id="Button1">
                                    AGENDA DE CITAS
                                </button>
                                &nbsp;&nbsp;
                                <button type="button" class="btn btn-outline-warning block"
                                    data-bs-toggle="modal" data-bs-target="#xlarge" runat="server" id="Button2">
                                    REGISTRO DE PACIENTES
                                </button>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">N° CITA</label>
                                    <asp:TextBox ID="txtcita" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="White" ControlToValidate="txtcita" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">NOMBRE(S)</label>
                                    <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" BackColor="White" ControlToValidate="txtnombre" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">APELLIDO PATERNO</label>
                                    <asp:TextBox ID="txtpaterno" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="White" ControlToValidate="txtpaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">APELLIDO MATERNO</label>
                                    <asp:TextBox ID="txtmaterno" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="White" ControlToValidate="txtmaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-lg" style="align-items: center; margin: 10px;">

                                <div class="col-lg">
                                    <label class="visually">NOMBRE DEL DOCTOR</label>
                                    <%--<asp:DropDownList ID="dpdoctor" CssClass="form-control" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre_doctor" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataValueField="id_doctor">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TISITConnectionString %>" SelectCommand="SELECT id_doctor,concat(nombre_doctor, ' ', apaterno, ' ', amaterno) as nombre_doctor FROM [doctores]"></asp:SqlDataSource>--%>
                                    <%--<label class="visually">APELLIDO MATERNO</label>--%>
                                    <asp:TextBox ID="txtdoctor" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="White" ControlToValidate="txtmaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">TIPO DE CITA</label>
                                    <asp:TextBox ID="txttipo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">HORA DE LA CITA</label>
                                    <asp:TextBox ID="txthora" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <asp:Button ID="btnregistrar" CssClass="btn btn-outline-info" runat="server" Text="REGISTRAR" OnClick="btnregistrar_Click" ValidationGroup="dir" />
                                </div>
                                <%--<div class="col-10">
                                    <asp:Button ID="btnconsul" CssClass="btn btn-outline-secondary" runat="server" Text="CONSULTAR" OnClick="btnconsul_Click" />
                                </div>--%>
                            </div>
                        </div>
                        <%--<div class="card card-body" id="divconsul" runat="server" visible="false">
                            <div class="col-lg">
                                <asp:Label ID="Label2" runat="server" Text="BUSCAR:  "></asp:Label>
                                <asp:TextBox ID="txtbusdir" runat="server" CssClass="form-control"></asp:TextBox>
                                <br />
                                <asp:LinkButton ID="btnbuscardir" runat="server" CssClass="btn btn-primary" OnClick="btnbuscardir_Click"><span class="glyphicon glyphicon-save"> BUSCAR</span></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="btncanelardir" runat="server" CssClass="btn btn-danger" OnClick="btncanelardir_Click"><span class="glyphicon glyphicon-save"> CANCELAR CONSULTA</span></asp:LinkButton>
                                
                            </div>
                        </div>--%>
                    </div>
                </div>


                <div class="modal fade text-left w-100" id="xlarge" tabindex="-1"
                    role="dialog" aria-labelledby="myModalLabel16" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                        role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="myModalLabel16">Usuarios</h4>
                                <button type="button" class="close" data-bs-dismiss="modal"
                                    aria-label="Close">
                                    <i class="bi bi-x-circle"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gridregistro" runat="server" CssClass="mGrid" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="id_llegada" HeaderText="ID LLEGADA" />
                                        <asp:BoundField DataField="nombre_paciente" HeaderText="NOMBRE PACIENTE" />
                                        <asp:BoundField DataField="nombre_doctor" HeaderText="NOMBRE DOCTOR" />
                                        <asp:BoundField DataField="tipo_cita" HeaderText="TIPO DE CITA" />
                                        <asp:BoundField DataField="fecha_ingreso" HeaderText="FECHA INGRESO" />
                                        <asp:BoundField DataField="hora_ingreso" HeaderText="HORA INGRESO" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal fade text-left w-100" id="xlarge1" tabindex="-1"
                    role="dialog" aria-labelledby="myModalLabel17" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                        role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="myModalLabel17">Usuarios</h4>
                                <button type="button" class="close" data-bs-dismiss="modal"
                                    aria-label="Close">
                                    <i class="bi bi-x-circle"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gridpacientes" runat="server" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="id_cita" OnSelectedIndexChanged="gridpacientes_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="id_cita" HeaderText="N° DE CITA" />
                                        <asp:BoundField DataField="nombreCliente" HeaderText="NOMBRE" />
                                        <asp:BoundField DataField="apellidoCliente" HeaderText="APELLIDO PATERNO" />
                                        <asp:BoundField DataField="apellidoMCliente" HeaderText="APELLIDO MATERNO" />
                                        <asp:BoundField DataField="nombre_doctor" HeaderText="MEDICO" />
                                        <asp:BoundField DataField="tipo_cita" HeaderText="TIPO DE CITA" />
                                        <asp:BoundField DataField="hora" HeaderText="HORA DE LA CITA" />
                                        <asp:CommandField ButtonType="Button" SelectText="SELECCIONAR" ShowSelectButton="True">
                                            <ControlStyle CssClass="btn btn-secondary" />
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                                <br />

                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
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
        let tablep = document.querySelector('#gridregistro');
        let dataTable = new simpleDatatables.DataTable(tablep);
    </script>
    <script>
        // Simple Datatable
        let tablep1 = document.querySelector('#gridpacientes');
        let dataTable1 = new simpleDatatables.DataTable(tablep1);
    </script>
    <script src="assets2/js/main.js"></script>
</body>
</html>
