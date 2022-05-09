<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="doctores.aspx.cs" Inherits="TISIT.doctores" %>

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
                text: 'POR FAVOR VERIFIQUE LA INFORMACIÃ“N',
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
                    <h3>Médicos</h3>
                </div>
                <br />
                <!-- Page content-->
                <div class="container">
                    <div class="card card-body" id="guardardatosdoctores" runat="server">
                        <div class="col-lg" style="display: flex; justify-content: flex-end">

                            <button type="button" class="btn btn-outline-warning block"
                                data-bs-toggle="modal" data-bs-target="#xlarge" runat="server" id="Button2">
                                MÉDICOS
                            </button>
                            &nbsp;&nbsp;&nbsp;
                            <button type="button" class="btn btn-outline-warning block"
                                data-bs-toggle="modal" data-bs-target="#xlarge1" runat="server" id="Button1">
                                MÉDICOS INACTIVOS
                            </button>
                        </div>
                        <center>
                            <asp:Label ID="Label1" runat="server" Text="REGISTRO DE DOCTORES"></asp:Label>
                        </center>
                        <br />
                        <div class="row gap-1" style="align-items: center; margin: 10px;">
                            <div class="col-lg" runat="server" id="completo" visible="false">
                                <label class="visually">NOMBRE</label>
                                &nbsp;<asp:TextBox ID="txtnombretotal" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" BackColor="White" ControlToValidate="txtnombretotal" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg" runat="server" id="paternoup" visible="false">
                                <label class="visually">APELLIDO PATERNO</label>
                                &nbsp;<asp:TextBox ID="txtpaternoupdate" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" BackColor="White" ControlToValidate="txtpaternoupdate" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg" runat="server" id="maternoup" visible="false">
                                <label class="visually">APELLIDO MATERNO</label>
                                &nbsp;<asp:TextBox ID="txtmaternoupdate" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" BackColor="White" ControlToValidate="txtmaternoupdate" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row gap-1" style="align-items: center; margin: 10px;">
                            <div class="col-lg" runat="server" id="nombre">
                                <label class="visually">NOMBRE(S) </label>
                                &nbsp;<asp:TextBox ID="txtnombredoctor" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" BackColor="White" ControlToValidate="txtnombredoctor" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg" runat="server" id="paterno">
                                <label class="visually">APELLIDO PATERNO</label>
                                &nbsp;<asp:TextBox ID="txtpaternodoctor" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="White" ControlToValidate="txtpaternodoctor" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg" runat="server" id="materno">
                                <label class="visually">APELLIDO MATERNO</label>
                                &nbsp;<asp:TextBox ID="txtmaternodoctor" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="White" ControlToValidate="txtmaternodoctor" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row gap-1" style="align-items: center; margin: 10px;">
                            <div class="col-lg">
                                <label class="visually">HOSPITAL</label>
                                &nbsp;<asp:TextBox ID="txthospital" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="White" ControlToValidate="txthospital" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg">
                                <label class="visually">ESPECIALISTA</label>
                                &nbsp;<asp:TextBox ID="txtespecialista" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" BackColor="White" ControlToValidate="txtespecialista" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row gap-1" style="align-items: center; margin: 10px;">
                            <div class="col-lg">
                                <asp:LinkButton ID="btnguardar" runat="server" CssClass="btn btn-outline-primary" ValidationGroup="Usuario" OnClick="btnguardar_Click"><span class="glyphicon glyphicon-save"> GUARDAR</span></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                        <%--<asp:LinkButton ID="btnconsultar" runat="server" CssClass="btn btn-outline-info" OnClick="btnconsultar_Click"><span class="glyphicon glyphicon-save"> CONSULTAR</span></asp:LinkButton>--%>
                                &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="btneditar" runat="server" CssClass="btn btn-outline-secondary" Visible="false" OnClick="btneditar_Click"><span class="glyphicon glyphicon-save"> EDITAR</span></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="btncancelaredit" runat="server" CssClass="btn btn-danger" Visible="false" OnClick="btncancelaredit_Click"><span class="glyphicon glyphicon-save"> CANCELAR EDICION</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <%--<div class="card card-body" id="consultadoctores" runat="server" visible="false">
                        <div class="col-lg">
                            <asp:Label ID="Label2" runat="server" Text="BUSCAR:  "></asp:Label>
                            <asp:TextBox ID="txtbuscar" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:LinkButton ID="btnbuscar" runat="server" CssClass="btn btn-primary" OnClick="btnbuscar_Click"><span class="glyphicon glyphicon-save"> BUSCAR</span></asp:LinkButton>
                            <asp:LinkButton ID="btncancelar" runat="server" CssClass="btn btn-danger" OnClick="btncancelar_Click"><span class="glyphicon glyphicon-save"> CANCELAR CONSULTA</span></asp:LinkButton>
                        </div>
                        <br />

                    </div>--%>

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
                                    <asp:GridView ID="griddoctores" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnSelectedIndexChanged="griddoctores_SelectedIndexChanged" OnRowDeleting="griddoctores_RowDeleting" DataKeyNames="id_doctor">
                                        <Columns>
                                            <asp:BoundField DataField="id_doctor" HeaderText="ID DOCTOR" />
                                            <asp:BoundField DataField="nombre_doctor" HeaderText="NOMBRE" />
                                            <asp:BoundField DataField="apaterno" HeaderText="APELLIDO PATERNO" />
                                            <asp:BoundField DataField="amaterno" HeaderText="APELLIDO MATERNO" />
                                            <asp:BoundField DataField="hospital" HeaderText="HOSPITAL" />
                                            <asp:BoundField DataField="especialista" HeaderText="ESPECIALISTA" />
                                            <asp:CommandField ButtonType="Button" SelectText="EDITAR" ShowSelectButton="True">
                                                <ControlStyle CssClass="btn btn-secondary" />
                                            </asp:CommandField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="Button1" runat="server" Text="DAR DE BAJA" OnClick="Button1_Click1" CssClass="btn btn-danger" />--%>
                                                    <asp:Button ID="btnbaja" CommandName="Delete" CssClass="btn btn-danger" runat="server" Text="DAR DE BAJA" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

                                    <asp:GridView ID="griddoctores1" runat="server" CssClass="mGrid" AutoGenerateColumns="False" OnSelectedIndexChanged="griddoctores1_SelectedIndexChanged" DataKeyNames="id_doctor">
                                        <Columns>
                                            <asp:BoundField DataField="id_doctor" HeaderText="ID DOCTOR" />
                                            <asp:BoundField DataField="nombre_doctor" HeaderText="NOMBRE" />
                                            <asp:BoundField DataField="apaterno" HeaderText="APELLIDO PATERNO" />
                                            <asp:BoundField DataField="amaterno" HeaderText="APELLIDO MATERNO" />
                                            <asp:BoundField DataField="hospital" HeaderText="HOSPITAL" />
                                            <asp:BoundField DataField="especialista" HeaderText="ESPECIALISTA" />
                                            <asp:CommandField ButtonType="Button" SelectText="DAR DE ALTA" ShowSelectButton="True">
                                                <ControlStyle CssClass="btn btn-success" />
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
            </div>
        </div>
        <!--==========  Boton inexistente para confirmacion delet ==========-->

    </form>

    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="assets2/vendors/simple-datatables/simple-datatables.js"></script>
    <script>
        // Simple Datatable
        let tablep = document.querySelector('#griddoctores');
        let dataTable = new simpleDatatables.DataTable(tablep);
    </script>
    <script>
        // Simple Datatable
        let tablep1 = document.querySelector('#griddoctores1');
        let dataTable1 = new simpleDatatables.DataTable(tablep1);
    </script>
    <script src="assets2/js/main.js"></script>
</body>
</html>
