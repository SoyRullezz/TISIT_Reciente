<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contrato_laboral.aspx.cs" Inherits="TISIT.contrato_laboral" EnableEventValidation="false" %>

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

    <style>
        .intento {
            color: red;
            font-size: 1.2em;
            font-style: italic;
        }

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

        .btn-danger1 {
            display: none;
            background: none;
            color: inherit;
            border: none;
            padding: 0;
            font: inherit;
            cursor: pointer;
            outline: inherit;
        }
    </style>

</head>
<body>
    <form id="form1" data-parsley-validate runat="server">

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
                <%--PARTE DE LA PAGINA--%>

                <header class="mb-3">
                    <a href="#" class="burger-btn d-block d-xl-none">
                        <i class="bi bi-justify fs-3"></i>
                    </a>
                </header>
                <div class="container">
                    <h3>Contrato Laboral</h3>
                </div>
                <br />
                <%--<div class="card-header">
                    <h4 class="card-title">Contrato laboral</h4>
                    <p>
                        Regista y consulta el contrato laboral <code></code><code>
                            <hr>
                        </code>
                    </p>
                </div>--%>
                <br />
                <div class="container">
                    <asp:Panel runat="server" ID="Panel1">
                        <div class="collapse" id="contrato">
                            <div class="col">
                                <asp:Label ID="trab" runat="server" Text=""></asp:Label>
                            </div>
                            <br />
                            <br />
                            <asp:Button ID="btneditar_contrato" runat="server" Text="Consultar registros"></asp:Button>
                        </div>
                        <div class="card card-body">

                            <!--Extra Large Modal -->
                            <div class="modal fade text-left w-100" id="xlarge" tabindex="-1"
                                role="dialog" aria-labelledby="myModalLabel16" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                                    role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" id="myModalLabel16">Empleados</h4>
                                            <button type="button" class="close" data-bs-dismiss="modal"
                                                aria-label="Close">
                                                <i class="bi bi-x-circle"></i>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="7" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:BoundField DataField="id_empleado" HeaderText="Id" />
                                                    <asp:BoundField DataField="no_empleado" HeaderText="Correo" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Id" />
                                                    <asp:BoundField DataField="paterno" HeaderText="Correo" />
                                                    <asp:BoundField DataField="materno" HeaderText="Id" />
                                                    <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True">
                                                        <ControlStyle CssClass="btn btn-outline-secondary" />
                                                    </asp:CommandField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col" runat="server" id="no_contrato">
                                    <label class="visually">N° CONTRATO</label>
                                    <asp:TextBox ID="txtcontrato" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" BackColor="White" ControlToValidate="txtnumber" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col" runat="server" id="no_trabajador">
                                    <label class="visually">N° TRABAJADOR</label>
                                    <asp:TextBox ID="txtnumber" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" BackColor="White" ControlToValidate="txtnumber" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="col">
                                    <label class="visually">NOMBRE</label>
                                    <asp:TextBox ID="txtnames" runat="server" CssClass="form-control" TextMode="SingleLine" ReadOnly="true"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" BackColor="White" ControlToValidate="txtnames" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col">
                                    <label class="visually">APELLIDO PATERNO</label>
                                    <asp:TextBox ID="txtpaterno" runat="server" CssClass="form-control" TextMode="SingleLine" ReadOnly="true"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" BackColor="White" ControlToValidate="txtapellidoslaboral" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col">
                                    <label class="visually">APELLIDO MATERNO</label>
                                    <asp:TextBox ID="txtmaterno" runat="server" CssClass="form-control" TextMode="SingleLine" ReadOnly="true"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" BackColor="White" ControlToValidate="txtapellidoslaboral" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col">
                                    <br />
                                    <button type="button" class="btn btn-outline-info" data-bs-toggle="modal"
                                        data-bs-target="#xlarge">
                                        <i class="bi bi-search"></i>&nbsp;&nbsp;&nbsp;BUSCAR
                                    </button>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">ADMINISTRATIVO / HOSPITAL</label>
                                    <asp:DropDownList ID="dptipemple" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="administrativo">ADMINISTRATIVO</asp:ListItem>
                                        <asp:ListItem Value="campo">HOSPITAL</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">PUESTO / CARGO</label>
                                    <asp:DropDownList ID="dppuesto" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="dir">DIRECCIÓN</asp:ListItem>
                                        <asp:ListItem Value="admin">ADMINISTRACION</asp:ListItem>
                                        <asp:ListItem Value="med">MÉDICO</asp:ListItem>
                                        <asp:ListItem Value="enf">ENFEREMERÍA</asp:ListItem>
                                        <asp:ListItem Value="est">ESTUDIOS</asp:ListItem>
                                        <asp:ListItem Value="inv">INVENTARIO</asp:ListItem>
                                        <asp:ListItem Value="ven">VENTAS</asp:ListItem>
                                        <%--<asp:ListItem Value="plan">PLANIFICACION</asp:ListItem>
                                        <asp:ListItem Value="con">CONTABILIDAD</asp:ListItem>
                                        <asp:ListItem Value="fact">FACTURAS</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">TIPO DE CONTRATO</label>
                                    <asp:DropDownList ID="dptipocontra" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="eventual">EVENTUAL</asp:ListItem>
                                        <asp:ListItem Value="definitivo">DEFINITIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">TIEMPO</label>
                                    <asp:DropDownList ID="dptiempo" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="3 meses">3 MESES</asp:ListItem>
                                        <asp:ListItem Value="6 meses">6 MESES</asp:ListItem>
                                        <asp:ListItem Value="9 meses">9 MESES</asp:ListItem>
                                        <asp:ListItem Value="1 año">1 AÑO</asp:ListItem>
                                        <asp:ListItem Value="3 años">3 AÑOS</asp:ListItem>
                                        <asp:ListItem Value="6 años">6 AÑOS</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg">

                                    <br />
                                    <label class="visually">AREA</label>
                                    <asp:TextBox ID="txtarea" runat="server" CssClass="form-control" TextMode="SingleLine" onkeypress="javascript:return sololetras(event)"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" BackColor="White" ControlToValidate="txtarea" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg">
                                    <br />
                                    <label class="visually">FECHA DE INICIO</label>
                                    <asp:TextBox ID="txtfechainicio" runat="server" CssClass="form-control" TextMode="Date" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" BackColor="White" ControlToValidate="txtarea" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg">
                                    <br />
                                    <label class="visually">FECHA DE TERMINO</label>
                                    <asp:TextBox ID="txtfechaterm" runat="server" CssClass="form-control" TextMode="Date" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" BackColor="White" ControlToValidate="txtfechaterm" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-3">
                                    <br />
                                    <label class="visually">SUELDO</label>
                                    <asp:TextBox ID="txtsueldo" runat="server" CssClass="form-control" TextMode="SingleLine" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" BackColor="White" ControlToValidate="txtsueldo" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="contrato">*</asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-3">
                                    <label class="visually">PERIODO DE PAGO</label>
                                    <asp:DropDownList ID="dpperiodopago" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="semanal">SEMANAL</asp:ListItem>
                                        <asp:ListItem Value="quincenal">QUINCENAL</asp:ListItem>
                                        <asp:ListItem Value="mensual">MENSUAL</asp:ListItem>
                                        <asp:ListItem Value="honorarios">HONORARIOS</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <br />
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <asp:LinkButton ID="btnsave" runat="server" CssClass="btn btn-outline-success" ValidationGroup="contrato" Width="200px" OnClick="btnsave_Click1"><i style="font-size:20px" class="bi bi-person-check-fill"></i>&nbsp; GUARDAR</span></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btneditar" runat="server" CssClass="btn btn-outline-info" OnClick="btneditar_Click"><i class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;&nbsp;&nbsp;EDITAR</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btncancelar" runat="server" CssClass="btn btn-outline-danger" OnClick="btncancelar_Click"><i class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;&nbsp;&nbsp;CANCELAR EDICIÓN</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <button type="button" class="btn btn-outline-info" data-bs-toggle="modal"
                                        data-bs-target="#xlarge1">
                                        <i class="bi bi-search"></i>&nbsp;&nbsp;&nbsp;MOSTRAR CONTRATOS
                                    </button>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>


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
                                    <asp:GridView ID="gridcontra" runat="server" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="id_contrato" Width="100%" OnSelectedIndexChanged="gridcontra_SelectedIndexChanged1">
                                        <Columns>
                                            <asp:BoundField DataField="id_contrato" HeaderText="N° Contrato" ReadOnly="False" />
                                            <asp:BoundField DataField="no_empleado" HeaderText="N° Empleado" ReadOnly="True" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="apaterno" HeaderText="Apellido Paterno" />
                                            <asp:BoundField DataField="amaterno" HeaderText="Apellido Materno" />
                                            <asp:BoundField DataField="tipo_empleado" HeaderText="Tipo Empleo" />
                                            <asp:BoundField DataField="tipo_contrato" HeaderText="Tipo Contrato" />
                                            <asp:BoundField DataField="tiempo" HeaderText="Tiempo" />
                                            <asp:BoundField DataField="puesto" HeaderText="Puesto" />
                                            <asp:BoundField DataField="area" HeaderText="Área" />
                                            <asp:BoundField DataField="f_inicio" HeaderText="Fecha de Inicio" ReadOnly="False" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="True" ConvertEmptyStringToNull="True" />
                                            <asp:BoundField DataField="f_termino" HeaderText="Fecha de Termino" ReadOnly="False" DataFormatString="{0:dd/MM/yyyy}" ApplyFormatInEditMode="True" ConvertEmptyStringToNull="True" />
                                            <asp:BoundField DataField="sueldo" HeaderText="Sueldo" />
                                            <asp:BoundField DataField="periodo_pago" HeaderText="Periodo de Pago" />
                                            <asp:CommandField ButtonType="Button" SelectText="EDITAR" ShowSelectButton="True">
                                                <ControlStyle CssClass="btn btn-outline-secondary" Width="150px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnexcel" runat="server" CssClass="btn btn-success" OnClick="btnexcel_Click"><i class="far fa-file-excel"></i>&nbsp;&nbsp;&nbsp;&nbsp;EXPORTAR A EXCEL</asp:LinkButton>
                                    <asp:LinkButton ID="btnpdf" runat="server" CssClass="btn btn-danger" OnClick="btnpdf_Click"><i class="far fa-file-pdf"></i>&nbsp;&nbsp;&nbsp;&nbsp;EXPORTAR A PDF</asp:LinkButton>
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
                                <img src="assets/images/logo/mati_tenkui2.png" width="250px" />
                            </p>
                        </div>
                    </div>
                </footer>
            </div>
        </div>



        <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
        <script src="assets2/js/bootstrap.bundle.min.js"></script>
        <script src="assets2/js/permisos.js"></script>
        <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <script src="assets2/vendors/simple-datatables/simple-datatables.js"></script>
        <script>
            function alertaExito() {
                Swal.fire(
                    'ÉXITO',
                    'La información se guardó con éxito',
                    'success'
                )
            }

            function alertaError() {
                Swal.fire(
                    'ERROR',
                    'No se pudieron guardar los permisos',
                    'error'
                )
            }

            function alertaCoincidencia() {
                Swal.fire(
                    'ALERTA',
                    'Las contraseñas no coinciden',
                    'warning'
                )
            }
        </script>

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
    </form>
</body>
</html>
