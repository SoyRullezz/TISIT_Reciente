<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="direccion_personal.aspx.cs" Inherits="TISIT.direccion_personal" %>

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
                'Personal Agregado',
                'El trabajor ha sido agregado correctamente',
                'success'
            )
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

        function alertaguardado() {
            Swal.fire({
                title: 'DATOS GUARDADOS CORRECTAMENTE',
                text: "Los datos guardados fueron almacenados correctamente",
                icon: 'success',
            })
        }

        function errorguardado() {
            Swal.fire({
                title: 'ERROR',
                text: "Hubo un error al guardar los datos por favor, verifique",
                icon: 'error',
            })
        }

        function BAJA() {
            Swal.fire({
                title: 'ESTE USUARIO ESTA DADO DE BAJA',
                text: "El usuario que busco está dado de baja por favor verifique",
                icon: 'warning',
            })
        }

        function alertaActualizado() {
            Swal.fire(
                'Personal actualizado',
                'Datos actualizados correctamente',
                'success'
            )
        }
    </script>

    <style>
        .boton {
            display: block;
            cursor: pointer;
            width: 40px;
            height: 40px;
            position: absolute;
            top: 60px;
            right: 220px;
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

            <!-- Page content-->
            <div id="main">
                <header class="mb-3">
                    <a href="#" class="burger-btn d-block d-xl-none">
                        <i class="bi bi-justify fs-3"></i>
                    </a>
                </header>
                <div class="container">
                    <h3>Dirección Personal</h3>
                </div>
                <br />
                <div class="container">
                    <%--<div class="card card-body">
                    </div>
                    <br />--%>

                    <div class="row">

                        <div class="card card-body" id="consultaempleados" runat="server" visible="false">
                            <div class="col-lg" style="display: flex; justify-content: flex-end">
                            </div>
                            <br />
                            <div class="col-lg">
                                <asp:Label ID="Label1" runat="server" Text="BUSCAR:  "></asp:Label>
                                <asp:TextBox ID="txtbuscar" runat="server" CssClass="form-control"></asp:TextBox>
                                <br />
                                <asp:LinkButton ID="btnbuscarempleado" runat="server" CssClass="btn btn-primary" OnClick="btnbuscarempleado_Click"><span class="glyphicon glyphicon-save"> BUSCAR</span></asp:LinkButton>
                            </div>
                            <br />
                        </div>

                        <div class="card card-body" id="agregardireccion" runat="server" visible="true">
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col">
                                    <label class="visually">N° TRABAJADOR</label>
                                    <asp:TextBox ID="txtnouser" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" BackColor="White" ControlToValidate="txtnouser" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label class="visually">NOMBRE(S)</label>
                                    <asp:TextBox ID="txtnombre" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" BackColor="White" ControlToValidate="txtnombre" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label class="visually">APELLIDO PATERNO</label>
                                    <asp:TextBox ID="txtpaterno" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="White" ControlToValidate="txtpaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>

                                <div class="col">
                                    <label class="visually">APELLIDO MATERNO</label>
                                    <asp:TextBox ID="txtmaterno" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="White" ControlToValidate="txtmaterno" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>

                                <div class="col">
                                    <%--<div class="boton">--%>
                                    <button type="button" class="btn btn-outline-info block"
                                        data-bs-toggle="modal" data-bs-target="#xlarge" runat="server" id="Button1">
                                        BUSCAR EMPLEADO
                                    </button>
                                    <%--</div>--%>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">CALLE</label>
                                    <asp:TextBox ID="txtdire" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" BackColor="White" ControlToValidate="txtdire" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-auto">
                                    <label class="visually">N° INTERIOR</label>
                                    <asp:TextBox ID="txtinterior" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" BackColor="White" ControlToValidate="txtinterior" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-auto">
                                    <label class="visually">N° EXTERIOR</label>
                                    <asp:TextBox ID="txtexterior" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" BackColor="White" ControlToValidate="txtexterior" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">COLONIA</label>
                                    <asp:TextBox ID="txtcolonia" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" BackColor="White" ControlToValidate="txtcolonia" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">MUNICIPIO / ALCALDIA</label>
                                    <asp:TextBox ID="txtmunicipio" runat="server" CssClass="form-control" onkeypress="javascript:return sololetras(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" BackColor="White" ControlToValidate="txtmunicipio" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">ENTIDAD</label>
                                    <asp:TextBox ID="txtentidad" runat="server" CssClass="form-control" onkeypress="javascript:return sololetras(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" BackColor="White" ControlToValidate="txtentidad" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <label class="visually">CODIGO POSTAL</label>
                                    <asp:TextBox ID="txtcodigo" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" BackColor="White" ControlToValidate="txtcodigo" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">TEL CASA</label>
                                    <asp:TextBox ID="txttelefono" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)" TextMode="Phone"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" BackColor="White" ControlToValidate="txttelefono" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg">
                                    <label class="visually">CELULAR</label>
                                    <asp:TextBox ID="txtcelular" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)" TextMode="Phone"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" BackColor="White" ControlToValidate="txtcelular" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-8">
                                    <asp:Label ID="lblcomprobante" runat="server" Text="COMPROBANTE DE DOMICILIO"></asp:Label>
                                    <asp:FileUpload ID="FileUpload1" Font-Size="Small" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="White" ControlToValidate="FileUpload1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="dir">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg">
                                    <asp:LinkButton ID="btnguardar" runat="server" CssClass="btn btn-outline-primary" ValidationGroup="Usuario" OnClick="btnguardar_Click"><span class="glyphicon glyphicon-save"> GUARDAR</span></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                      <asp:LinkButton ID="btneditar" runat="server" CssClass="btn btn-outline-secondary" Visible="false" OnClick="btneditar_Click"><span class="glyphicon glyphicon-save"> EDITAR</span></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                      <button type="button" class="btn btn-outline-warning block"
                                          data-bs-toggle="modal" data-bs-target="#xlarge1" runat="server" id="Button3">
                                          CONSULTA
                                      </button>
                                    &nbsp;&nbsp;&nbsp;
                                      <asp:LinkButton ID="btncancelaredit" runat="server" CssClass="btn btn-outline-danger" Visible="false" OnClick="btncancelaredit_Click"><span class="glyphicon glyphicon-save"> CANCELAR EDICION</span></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                      <asp:LinkButton ID="btneditardocu" runat="server" CssClass="btn btn-outline-info" Visible="false" OnClick="btneditardocu_Click"><span class="glyphicon glyphicon-save"> EDITAR COMPROBANTE</span></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                      <asp:LinkButton ID="btncancelardocu" runat="server" CssClass="btn btn-outline-danger" Visible="false" OnClick="btncancelardocu_Click"><span class="glyphicon glyphicon-save"> CANCELAR EDIT COMPROBANTE</span></asp:LinkButton>
                                </div>
                            </div>
                        </div>

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
                            <i class="bi bi-x-circle"></i>
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
                        <asp:GridView ID="gridir" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnSelectedIndexChanged="gridir_SelectedIndexChanged" DataKeyNames="no_empleado">
                            <Columns>

                                <asp:BoundField DataField="no_empleado" HeaderText="# Empleado" ReadOnly="True" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="paterno" HeaderText="Paterno" />
                                <asp:BoundField DataField="materno" HeaderText="Materno" />
                                <asp:BoundField DataField="calle" HeaderText="Calle" />
                                <asp:BoundField DataField="colonia" HeaderText="Colonia" />
                                <asp:BoundField DataField="no_int" HeaderText="Interior" />
                                <asp:BoundField DataField="no_ext" HeaderText="Exterior" />
                                <asp:BoundField DataField="municipio" HeaderText="Municipio" />
                                <asp:BoundField DataField="entidad" HeaderText="Entidad" />
                                <asp:BoundField DataField="codigo_postal" HeaderText="Codigo" />
                                <asp:BoundField DataField="tel_casa" HeaderText="Telefono" />
                                <asp:BoundField DataField="celular" HeaderText="Celular" />
                                <asp:TemplateField HeaderText="C. Domicilio">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click" Text='<%# Eval("FileName1") %>' CssClass="btn btn-link"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" SelectText="EDITAR" ShowSelectButton="True">
                                    <ControlStyle CssClass="btn btn-outline-secondary" />
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
    </form>

    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="assets2/vendors/simple-datatables/simple-datatables.js"></script>
    <script>
        // Simple Datatable
        let tablep = document.querySelector('#gridempleados');
        let dataTable = new simpleDatatables.DataTable(tablep);
    </script>
    <script>
        // Simple Datatable
        let tablep1 = document.querySelector('#gridempleados2');
        let dataTable1 = new simpleDatatables.DataTable(tablep1);
    </script>
    <script src="assets2/js/main.js"></script>
</body>
</html>

