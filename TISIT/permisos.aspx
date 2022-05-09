<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="permisos.aspx.cs" Inherits="TISIT.permisos" %>

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
        function errorBorrar(id) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'NO SE PUDO DAR DE BAJA CORRECTAMENTE CON EL ID: ' + id
            })
        }
        function correvtoborrado(id) {
            Swal.fire({
                icon: 'success',
                title: 'SE DIO DE BAJA CORRECTAMENTE CON EL ID:' + id,
                showConfirmButton: false,
                timer: 1500
            })
        }
        function alertaUsuario(username) {
            Swal.fire({
                icon: 'success',
                title: 'Los datos han sido guardados correctamente. Tu usuario es: ' + username,
                showConfirmButton: false,
                timer: 1500
            })
        }
        function confirmDelete() {
            Swal.fire({
                title: '¿DAR DE BAJA?',
                text: "ESTE USUARIO SERA DADO DE BAJA",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'DAR DE BAJA',

            }).then(async (result) => {
                if (result.isConfirmed) {
                    await document.getElementById('gridempleados_btnbaja_0').click();

                }
            })
        }

        function alertaExito() {
            Swal.fire(
                'DATOS INSERTADOS',
                'Los datos han sido guardados correctamente',
                'success'
            )
        }
        function alertaExitoEditar() {
            Swal.fire(
                'DATOS ACTUALIZADOS',
                'Los datos han sido actualizados correctamente',
                'success'
            )
        }

        function alertaExitoDarDeBaja() {
            Swal.fire(
                'BAJA EXITOSA',
                'El usuario ha sido dado de baja exitosamente',
                'success'
            )
        }

        function alertaError() {
            Swal.fire({
                icon: 'error',
                title: 'USUARIO INVALIDO',
                text: 'LOS DATOS ESTAN INCORRECTOS',
            })
        }

        function ErrorEmpleado() {
            Swal.fire({
                icon: 'error',
                title: 'USUARIO INVALIDO',
                text: 'EL NÃšMERO DE EMPLEADO YA SE ENCUENTRA REGISTRADO',
            })
        }

        function ErrorID() {
            Swal.fire({
                icon: 'error',
                title: 'USUARIO INVALIDO',
                text: 'EL ID PERSONAL YA SE ENCUENTRA REGISTRADO, INTENTE DE NUEVO',
            })
        }

        function ErrorActualizar() {
            Swal.fire({
                icon: 'error',
                title: 'ERROR AL ACTUALIZAR',
                text: 'NO SE PUDO ACTUALIZAR, VERIFIQUE',
            })
        }
        function ErrorDarDeBaja() {
            Swal.fire({
                icon: 'error',
                title: 'ERROR AL DAR DE BAJA',
                text: 'POR FAVOR VERIFIQUE LA INFORMACIÓN',
            })
        }
        function ConfirmDelete(ev) {
            if (object.status) { return true; };
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!',
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve()
                        }, 2000)
                    })
                    object.status = true;
                    object.ele = ev;
                    object.ele.click();
                }
            }).then(function () {
            })
            return false;

        };
    </script>
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
    <form id="form1" runat="server">
        <div class="app">
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
                <%--<div class="container">
                    <h3>Permisos</h3>
                </div>
                <br />--%>
                <!-- Page content-->
                <div class="container">
                    <div class="page-heading">
                        <div class="page-title">
                            <div class="row">
                                <div class="col-12 col-md-6 order-md-1 order-last">
                                    <h3>Permisos</h3>
                                    <asp:Label ID="lbpuesto" runat="server" Text=""></asp:Label>
                                    <%--<p class="text-subtitle text-muted">Permisos</p>--%>
                                </div>

                                <div class="col-12 col-md-6 order-md-2 order-first">
                                    <nav aria-label="breadcrumb" class="breadcrumb-header float-start float-lg-end">
                                    </nav>
                                </div>
                            </div>
                        </div>
                        <section class="section">
                            <div class="card">
                                <div class="card-body">

                                    <div class="row gap-1" style="align-items: center; margin: 10px;">
                                        <div class="col-lg">
                                            <asp:TextBox ID="txtempleado" CssClass="form-control" ReadOnly="true" runat="server" placeholder="N° Empleado"></asp:TextBox>

                                        </div>
                                        <div class="col-lg">
                                            <asp:TextBox ID="txtbuscar" CssClass="form-control" ReadOnly="true" runat="server" placeholder="Usuario"></asp:TextBox>

                                        </div>
                                        <div class="col-lg">
                                            <!-- Button trigger for BorderLess Modal -->
                                            <button type="button" class="btn btn-outline-info block"
                                                data-bs-toggle="modal" data-bs-target="#xlarge" runat="server" id="btnbuscar">
                                                Buscar
                                            </button>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <asp:Panel ID="pnlpermisos" runat="server">
                                <div class="card">
                                    <div class="card-header">
                                        Permisos
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-striped" id="table1">
                                            <thead>
                                                <tr>
                                                    <th>Módulo</th>
                                                    <th>Escritura</th>
                                                    <%--<th>Lectura</th>--%>
                                                    <th>Bloquear</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Administración</td>
                                                    <td>
                                                        <asp:RadioButton ID="admin" runat="server" GroupName="admin" />
                                                    </td>
                                                    <%--<td>
                                                        <asp:RadioButton ID="adminlect" runat="server" GroupName="admin" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:RadioButton ID="adminbloq" runat="server" Checked="true" GroupName="admin" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Personal Médico y Citas</td>
                                                    <td>
                                                        <asp:RadioButton ID="med" runat="server" GroupName="fac" />
                                                    </td>
                                                    <%--<td>
                                                        <asp:RadioButton ID="medlect" runat="server" GroupName="fac" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:RadioButton ID="medbloq" runat="server" Checked="true" GroupName="fac" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Documentos y Solicitudes</td>
                                                    <td>
                                                        <asp:RadioButton ID="docpac" runat="server" GroupName="rv" />
                                                    </td>
                                                    <%--<td>
                                                        <asp:RadioButton ID="docpaclect" runat="server" GroupName="rv" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:RadioButton ID="docpacbloq" runat="server" Checked="true" GroupName="rv" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Productos</td>
                                                    <td>
                                                        <asp:RadioButton ID="prod" runat="server" GroupName="dp" />
                                                    </td>
                                                    <%--<td>
                                                        <asp:RadioButton ID="prodlect" runat="server" GroupName="dp" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:RadioButton ID="prodbloq" runat="server" Checked="true" GroupName="dp" />
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                        <div class="float-start">
                                        </div>
                                        <div class="float-end">
                                            <asp:LinkButton ID="btnguardar" CssClass="btn btn-warning" runat="server" OnClick="btnguardar_Click">Guardar</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>


                            <asp:Panel ID="pnladmin" runat="server">
                                <div class="card">
                                    <div class="card-header">
                                        Permisos Administración
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-striped" id="table2">
                                            <thead>
                                                <tr>
                                                    <th>Módulo</th>
                                                    <th>Permiso</th>
                                                    <th>Bloquear</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Alta de Empleados</td>
                                                    <td>
                                                        <asp:RadioButton ID="user" runat="server" GroupName="usu" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="userbloq" runat="server" Checked="true" GroupName="usu" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Dirección</td>
                                                    <td>
                                                        <asp:RadioButton ID="dir" runat="server" GroupName="dir" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="dirbloq" runat="server" Checked="true" GroupName="dir" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Documentación</td>
                                                    <td>
                                                        <asp:RadioButton ID="doc" runat="server" GroupName="doc" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="docbloq" runat="server" Checked="true" GroupName="doc" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Contrato Laboral</td>
                                                    <td>
                                                        <asp:RadioButton ID="lab" runat="server" GroupName="lab" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="labbloq" runat="server" Checked="true" GroupName="lab" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Permisos</td>
                                                    <td>
                                                        <asp:RadioButton ID="per" runat="server" GroupName="perm" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="perbloq" runat="server" Checked="true" GroupName="perm" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlmed" runat="server">
                                <div class="card">
                                    <div class="card-header">
                                        Permisos Personal Médico y Citas
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-striped" id="table3">
                                            <thead>
                                                <tr>
                                                    <th>Módulo</th>
                                                    <th>Permiso</th>
                                                    <th>Bloquear</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Doctores</td>
                                                    <td>
                                                        <asp:RadioButton ID="docto" runat="server" GroupName="plant" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="doctobloq" runat="server" Checked="true" GroupName="plant" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Registro Pacientes</td>
                                                    <td>
                                                        <asp:RadioButton ID="pac" runat="server" GroupName="ver" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="pacbloq" runat="server" Checked="true" GroupName="ver" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Citas</td>
                                                    <td>
                                                        <asp:RadioButton ID="cit" runat="server" GroupName="rep" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="citbloq" runat="server" Checked="true" GroupName="rep" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Agendar Citas</td>
                                                    <td>
                                                        <asp:RadioButton ID="ag" runat="server" GroupName="fact1" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="agbloq" runat="server" Checked="true" GroupName="fact1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Calendario de Citas</td>
                                                    <td>
                                                        <asp:RadioButton ID="cal" runat="server" GroupName="cal" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="calbloq" runat="server" Checked="true" GroupName="cal" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnldoc" runat="server">
                                <div class="card">
                                    <div class="card-body">
                                        <table class="table table-striped" id="table6">
                                            <thead>
                                                <tr>
                                                    <th>Módulo</th>
                                                    <th>Permiso</th>
                                                    <th>Bloquear</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Solicitud de Estudios</td>
                                                    <td>
                                                        <asp:RadioButton ID="sol" runat="server" GroupName="sol" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="solbloq" runat="server" Checked="true" GroupName="sol" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Solicitud de Ingreso y Consentimiento</td>
                                                    <td>
                                                        <asp:RadioButton ID="cons" runat="server" GroupName="cons" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="consbloq" runat="server" Checked="true" GroupName="cons" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Reporte de Ingresos</td>
                                                    <td>
                                                        <asp:RadioButton ID="ing" runat="server" GroupName="ing" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="ingbloq" runat="server" Checked="true" GroupName="ing" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlprod" runat="server">
                                <div class="card">
                                    <div class="card-header">
                                        Permisos Productos
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-striped" id="table5">
                                            <thead>
                                                <tr>
                                                    <th>Módulo</th>
                                                    <th>Permiso</th>
                                                    <th>Bloquear</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Productos</td>
                                                    <td>
                                                        <asp:RadioButton ID="inv" runat="server" GroupName="prod" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="invbloq" runat="server" Checked="true" GroupName="prod" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Nuevo Producto</td>
                                                    <td>
                                                        <asp:RadioButton ID="nprod" runat="server" GroupName="proc" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="nprodbloq" runat="server" Checked="true" GroupName="proc" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Ventas</td>
                                                    <td>
                                                        <asp:RadioButton ID="ven" runat="server" GroupName="conproc" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="venbloq" runat="server" Checked="true" GroupName="conproc" />
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>

                                    </div>

                                </div>
                            </asp:Panel>

                            <div class="card" id="cdespeciales" runat="server">
                                <div class="card-body">
                                    <div class="float-start">
                                    </div>
                                    <div class="float-end">
                                        <asp:LinkButton ID="btnespeciales" CssClass="btn btn-warning" runat="server" OnClick="btnespeciales_Click">Guardar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                        </section>
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
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="7" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="no_empleado" HeaderText="N° Empleado" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="puesto" HeaderText="Puesto" />
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
