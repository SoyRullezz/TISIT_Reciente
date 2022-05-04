<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="TISIT.ingreso" %>

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
    <script>
        $('#xlarge1').modal({ backdrop: 'static', keyboard: false })
    </script>
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
                    <div class="card card-body">
                        <div class="row gap-1" style="align-items: center; margin: 10px;">
                            <div class="col-lg" style="display: flex; justify-content: center; align-items: center;">
                                <button type="button" class="btn btn-outline-warning block" style="width: 280px"
                                    data-bs-toggle="modal" data-bs-target="#xlarge1" data-backdrop="static" data-keyboard="false" role="dialog" runat="server" id="Button1">
                                    HOJA DE INGRESO SANTA LUCIA
                                </button>
                            </div>
                            <div class="col-lg" style="display: flex; justify-content: center; align-items: center;">
                                <button type="button" class="btn btn-outline-warning block" style="width: 280px"
                                    data-bs-toggle="modal" data-bs-target="#xlarge2" runat="server" id="Button3">
                                    HOJA DE PACIENTE QUIRÚRGICO
                                </button>
                            </div>
                            <div class="col-lg" style="display: flex; justify-content: center; align-items: center;">
                                <button type="button" style="align-items: center; width: 280px" class="btn btn-outline-warning block"
                                    data-bs-toggle="modal" data-bs-target="#xlarge3" runat="server" id="Button4">
                                    ANESTESIA Y RECUPERACION
                                </button>
                            </div>
                            <%--</div>--%>
                        </div>
                    </div>


                    <%--<div class="card card-body">
                    </div>--%>
                </div>
            </div>

            <div class="modal fade text-left w-100" id="xlarge1" tabindex="-1"
                role="dialog" aria-labelledby="myModalLabel17" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-full"
                    role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel17">Hoja De Ingreso</h4>
                            <button type="button" class="close" data-bs-dismiss="modal"
                                aria-label="Close">
                                <i class="bi bi-x-circle"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="upAlgo" RenderMode="Block">
                                <ContentTemplate>--%>
                            <div class="card card-body">
                                <div class="col-lg" style="display: flex; justify-content: flex-end">
                                    <button type="button" class="btn btn-outline-info block"
                                        data-bs-toggle="modal" data-bs-target="#xlarge" runat="server" id="Button2">
                                        BUSCAR PACIENTE
                                    </button>
                                </div>

                                <center>
                                    <asp:Label ID="Label1" runat="server" Text="HOJA DE INGRESO"></asp:Label>
                                </center>
                                <br />
                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div1">
                                        <label class="visually">NOMBRE DEL PACIENTE</label>
                                        &nbsp;<asp:TextBox ID="txtpaciente" runat="server" CssClass="form-control" ReadOnly="true" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="White" ControlToValidate="txtpaciente" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="completo">
                                        <label class="visually">FECHA NACIMIENTO</label>
                                        &nbsp;<asp:TextBox ID="txtfecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" BackColor="White" ControlToValidate="txtfecha" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="paternoup">
                                        <label class="visually">EDAD</label>
                                        &nbsp;<asp:TextBox ID="txtedad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" BackColor="White" ControlToValidate="txtedad" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div2">
                                        <label class="visually">SEXO</label>
                                        <asp:DropDownList ID="ddsexo" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="FEMENINO">FEMENINO</asp:ListItem>
                                            <asp:ListItem Value="MASCULINO">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="OTRO">OTRO</asp:ListItem>
                                            <asp:ListItem Value="PREFIERO NO DECIRLO">PREFIERO NO DECIRLO</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<%--<asp:TextBox ID="txtsexo" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="White" ControlToValidate="txtsexo" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div3">
                                        <label class="visually">PESO</label>
                                        &nbsp;<asp:TextBox ID="txtpeso" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="White" ControlToValidate="txtpeso" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div4">
                                        <label class="visually">TALLA</label>
                                        &nbsp;<asp:TextBox ID="txttalla" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" BackColor="White" ControlToValidate="txttalla" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div5">
                                        <label class="visually">DOMICILIO</label>
                                        &nbsp;<asp:TextBox ID="txtdomicilio" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" BackColor="White" ControlToValidate="txtdomicilio" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div6">
                                        <label class="visually">C.P.</label>
                                        &nbsp;<asp:TextBox ID="txtcp" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" BackColor="White" ControlToValidate="txtcp" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div11">
                                        <label class="visually">OCUPACION</label>
                                        &nbsp;<asp:TextBox ID="txtocupacion" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="White" ControlToValidate="txtocupacion" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div7">
                                        <label class="visually">TELEFONO PACIENTE</label>
                                        &nbsp;<asp:TextBox ID="txttel" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" BackColor="White" ControlToValidate="txttel" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div8">
                                        <label class="visually">TELEFONO FAMILIAR</label>
                                        &nbsp;<asp:TextBox ID="txtfamiliar" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" BackColor="White" ControlToValidate="txtfamiliar" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div18">
                                        <label class="visually">REPRESENTANTE LEGAL</label>
                                        &nbsp;<asp:TextBox ID="txtrepresentante" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" BackColor="White" ControlToValidate="txtrepresentante" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div19">
                                        <label class="visually">PARENTESCO</label>
                                        <asp:DropDownList ID="ddparentesco" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="FAMILIAR">FAMILIAR</asp:ListItem>
                                            <asp:ListItem Value="REPRESENTANTE LEGAL">REPRESENTANTE LEGAL</asp:ListItem>
                                            <asp:ListItem Value="OTRO">OTRO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" BackColor="White" ControlToValidate="ddparentesco" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div9">
                                        <label class="visually">MÉDICO</label>
                                        <%--&nbsp;<asp:TextBox ID="txtmedico" runat="server" CssClass="form-control" ReadOnly="true" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        --%>
                                        <asp:DropDownList ID="DropDownList1" CssClass=" drodL form-control" runat="server" DataTextField="Nombre_doctor" DataValueField="id_doctor" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataSourceID="SqlDataSource1"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TISITConnectionString2 %>" SelectCommand="select distinct id_doctor,concat(nombre_doctor, ' ', apaterno, ' ', amaterno) as 'Nombre_doctor' from doctores where activo = 1 order by Nombre_doctor;"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" BackColor="White" ControlToValidate="DropDownList1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div16">
                                        <label class="visually">FECHA DE ENTRADA</label>
                                        &nbsp;<asp:TextBox ID="txtfechaentrada" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" BackColor="White" ControlToValidate="txtfechaentrada" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div10">
                                        <label class="visually">HORA ENTRADA</label>
                                        &nbsp;<asp:TextBox ID="txthoraentrada" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" BackColor="White" ControlToValidate="txthoraentrada" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div13">
                                        <label class="visually">DX INGRESO</label>
                                        &nbsp;<asp:TextBox ID="txtdxingreso" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" BackColor="White" ControlToValidate="txtdxingreso" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div15">
                                        <label class="visually">ESTUDIOS REALIZADOS</label>
                                        &nbsp;<asp:TextBox ID="txtestudios" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" BackColor="White" ControlToValidate="txtestudios" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div17">
                                        <label class="visually">DIAGNOSTICO</label>
                                        &nbsp;<asp:TextBox ID="txtdiagnostico" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" BackColor="White" ControlToValidate="txtdiagnostico" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div22">
                                        <label class="visually">PROCEDIMIENTO</label>
                                        &nbsp;<asp:TextBox ID="txtprocedimiento" runat="server" TextMode="MultiLine" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" BackColor="White" ControlToValidate="txtprocedimiento" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div20">
                                        <label class="visually">PROBABLES BENEFICIOS</label>
                                        &nbsp;<asp:TextBox ID="txtbeneficios" runat="server" TextMode="MultiLine" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" BackColor="White" ControlToValidate="txtbeneficios" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div21">
                                        <label class="visually">PRONÓSTICO</label>
                                        &nbsp;<asp:TextBox ID="txtpronostico" runat="server" TextMode="MultiLine" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" BackColor="White" ControlToValidate="txtpronostico" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div50">
                                        <label class="visually">RIESGOS Y COMPLICACIONES</label>
                                        &nbsp;<asp:TextBox ID="txtcomplicaciones" runat="server" TextMode="MultiLine" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" BackColor="White" ControlToValidate="txtcomplicaciones" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg" runat="server" id="Div12">
                                        <label class="visually">ALERGIAS</label>
                                        &nbsp;<asp:TextBox ID="txtalergias" runat="server" TextMode="MultiLine" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" BackColor="White" ControlToValidate="txtalergias" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col-lg" runat="server" id="Div14">
                                        <label class="visually">OBSERVACIONES</label>
                                        &nbsp;<asp:TextBox ID="txtobservaciones" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" BackColor="White" ControlToValidate="txtobservaciones" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row gap-1" style="align-items: center; margin: 10px;">
                                    <div class="col">
                                        <asp:Button ID="btnpdf" CssClass="btn btn-outline-warning" runat="server" Text="INGRESAR PACIENTE" OnClick="btnpdf_Click" />
                                        <asp:Button ID="btnempty" CssClass="btn btn-outline-warning" runat="server" Text="LIMPIAR" OnClick="btnempty_Click" />
                                    </div>
                                </div>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                            </div>


                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade text-left w-100" id="xlarge" tabindex="-1"
                role="dialog" aria-labelledby="myModalLabel16" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                    role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-dark white">
                            <h4 class="modal-title" style="color: white" id="myModalLabel16">Pacientes</h4>
                            <button type="button" class="close" data-bs-dismiss="modal" style="color: white;"
                                aria-label="Close">
                                <i class="bi bi-x-circle"></i>
                            </button>
                        </div>
                        <asp:GridView ID="gridregistro" runat="server" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="id_llegada" OnSelectedIndexChanged="gridregistro_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="id_llegada" HeaderText="ID LLEGADA" />
                                <asp:BoundField DataField="nombre_paciente" HeaderText="NOMBRE PACIENTE" />
                                <asp:BoundField DataField="nombre_doctor" HeaderText="NOMBRE DOCTOR" />
                                <asp:BoundField DataField="tipo_cita" HeaderText="TIPO DE CITA" />
                                <asp:BoundField DataField="fecha_ingreso" HeaderText="FECHA INGRESO" />
                                <asp:BoundField DataField="hora_ingreso" HeaderText="HORA INGRESO" />
                                <asp:CommandField ButtonType="Button" SelectText="SELECCIONAR" ShowSelectButton="True" ItemStyle-CssClass="btn-seleccionar">
                                    <ControlStyle CssClass="btn btn-secondary" />

                                    <ItemStyle CssClass="btn-seleccionar"></ItemStyle>
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                        <%--<div class="box-body table table-responsive">
                            <table id="tbl_Citas" class="table table-responsive table-bordered">
                                <thead>
                                    <tr>
                                        <th>ID LLEGADA</th>
                                        <th>NOMBRE PACIENTE</th>
                                        <th>NOMBRE DOCTOR</th>
                                        <th>TIPO DE CITA</th>
                                        <th>FECHA INGRESO</th>
                                        <th>HORA INGRESO</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody id="tbl_bodyCitas">
                                    <!--Aqui se insertan  los datos de la BD-->
                                </tbody>
                            </table>

                        </div>--%>

                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>

            <div class="modal fade text-left w-100" id="xlarge2" tabindex="-1"
                role="dialog" aria-labelledby="myModalLabel150" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-full"
                    role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel150">Hoja De Paciente Quirúrgico</h4>
                            <button type="button" class="close" data-bs-dismiss="modal"
                                aria-label="Close">
                                <i class="bi bi-x-circle"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="col-lg" style="display: flex; justify-content: flex-end">
                                <button type="button" class="btn btn-outline-info block"
                                    data-bs-toggle="modal" data-bs-target="#xlargeHQ" runat="server" id="Button5">
                                    PACIENTES INGRESADOS
                                </button>
                            </div>
                            <center>
                                <asp:Label ID="Label4" runat="server" Text="HOJA DE PACIENTE QUIRÚRGICO"></asp:Label>
                            </center>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div23">
                                    <label class="visually">NOMBRE DEL PACIENTE</label>
                                    &nbsp;<asp:TextBox ID="txtpaciente1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" BackColor="White" ControlToValidate="txtpaciente1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div24">
                                    <label class="visually">FECHA NACIMIENTO</label>
                                    &nbsp;<asp:TextBox ID="txtfecha1" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" BackColor="White" ControlToValidate="txtfecha1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div25">
                                    <label class="visually">EDAD</label>
                                    &nbsp;<asp:TextBox ID="txtedad1" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" BackColor="White" ControlToValidate="txtedad1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div26">
                                    <label class="visually">SEXO</label>
                                    <asp:TextBox ID="txtsexo1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" BackColor="White" ControlToValidate="txtsexo1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div36">
                                    <label class="visually">MÉDICO</label>
                                    &nbsp;<asp:TextBox ID="txtmedico1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" BackColor="White" ControlToValidate="txtmedico1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div29">
                                    <label class="visually">ENFERMERA</label>
                                    &nbsp;<asp:TextBox ID="txtenfermera1" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" BackColor="White" ControlToValidate="txtenfermera1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div41">
                                    <label class="visually">DIAGNOSTICO</label>
                                    &nbsp;<asp:TextBox ID="txtdiagnostico1" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" BackColor="White" ControlToValidate="txtdiagnostico1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div30">
                                    <asp:Button ID="btnlimpiar" CssClass="btn btn-outline-warning" runat="server" Text="GENERAR HOJA QUIRURGICA" OnClick="btnlimpiar_Click" />
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade text-left w-100" id="xlargeHQ" tabindex="-1"
                role="dialog" aria-labelledby="myModalLabel151" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                    role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-dark white">
                            <h4 class="modal-title" style="color: white" id="myModalLabel151">Pacientes Ingresado</h4>
                            <button type="button" class="close" data-bs-dismiss="modal" style="color: white;"
                                aria-label="Close">
                                <i class="bi bi-x-circle"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="gridingresos" runat="server" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="id_ingreso" OnSelectedIndexChanged="gridingresos_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="id_ingreso" HeaderText="ID LLEGADA" />
                                    <asp:BoundField DataField="paciente" HeaderText="NOMBRE PACIENTE" />
                                    <asp:BoundField DataField="fecha" HeaderText="FECHA DE NACIMIENTO" />
                                    <asp:BoundField DataField="edad" HeaderText="EDAD" />
                                    <asp:BoundField DataField="sexo" HeaderText="SEXO" />
                                    <asp:BoundField DataField="medico" HeaderText="MEDICO" />
                                    <asp:BoundField DataField="diagnostico" HeaderText="DIAGNOSTICO" />
                                    <asp:CommandField ButtonType="Button" SelectText="SELECCIONAR" ShowSelectButton="True">
                                        <ControlStyle CssClass="btn btn-secondary" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>

                            <%--<div class="box-body table table-responsive">
                                <table id="tbl_Ingresos1" class="table table-responsive table-bordered">
                                    <thead>
                                        <tr>
                                            <th>ID INGRESO</th>
                                            <th>NOMBRE PACIENTE</th>
                                            <th>FECHA</th>
                                            <th>EDAD</th>
                                            <th>SEXO</th>
                                            <th>MEDICO</th>
                                            <th>DIAGNÓSTICO</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbl_bodyIngresos1">
                                        <!--Aqui se insertan  los datos de la BD-->
                                    </tbody>
                                </table>

                            </div>--%>

                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade text-left w-100" id="xlarge3" tabindex="-1"
                role="dialog" aria-labelledby="myModalLabel152" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-full"
                    role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel152">Registro de Anestesia y Recuperación</h4>
                            <button type="button" class="close" data-bs-dismiss="modal"
                                aria-label="Close">
                                <i class="bi bi-x-circle"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="col-lg" style="display: flex; justify-content: flex-end">
                                <button type="button" class="btn btn-outline-info block"
                                    data-bs-toggle="modal" data-bs-target="#xlargeOP" runat="server" id="Button6">
                                    PACIENTES INGRESADOS
                                </button>
                            </div>
                            <center>
                                <asp:Label ID="Label2" runat="server" Text="DATOS DEL PACIENTE"></asp:Label>
                            </center>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div27">
                                    <label class="visually">NOMBRE DEL PACIENTE</label>
                                    &nbsp;<asp:TextBox ID="txtpaciente2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" BackColor="White" ControlToValidate="txtpaciente2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div28">
                                    <label class="visually">FECHA NACIMIENTO</label>
                                    &nbsp;<asp:TextBox ID="txtfecha2" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" BackColor="White" ControlToValidate="txtfecha2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div31">
                                    <label class="visually">EDAD</label>
                                    &nbsp;<asp:TextBox ID="txtedad2" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" BackColor="White" ControlToValidate="txtedad2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>

                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div32">
                                    <label class="visually">SEXO</label>
                                    <asp:TextBox ID="txtsexo2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" BackColor="White" ControlToValidate="txtsexo2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div38">
                                    <label class="visually">PESO</label>
                                    &nbsp;<asp:TextBox ID="txtpeso2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" BackColor="White" ControlToValidate="txtpeso2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div39">
                                    <label class="visually">TALLA</label>
                                    &nbsp;<asp:TextBox ID="txttalla2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" BackColor="White" ControlToValidate="txttalla2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div44">
                                    <label class="visually">CAMA</label>
                                    &nbsp;<asp:TextBox ID="txtcama2" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" BackColor="White" ControlToValidate="txtcama2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div34">
                                    <label class="visually">REPRESENTANTE LEGAL</label>
                                    &nbsp;<asp:TextBox ID="txtrepresentante2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" BackColor="White" ControlToValidate="txtrepresentante2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div33">
                                    <label class="visually">MÉDICO</label>
                                    &nbsp;<asp:TextBox ID="txtmedico2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" BackColor="White" ControlToValidate="txtmedico2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div35">
                                    <label class="visually">DIAGNOSTICO</label>
                                    &nbsp;<asp:TextBox ID="txtdiagnostico2" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" BackColor="White" ControlToValidate="txtdiagnostico2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div40">
                                    <label class="visually">PROCEDIMIENTO</label>
                                    &nbsp;<asp:TextBox ID="txtprocedimiento2" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" BackColor="White" ControlToValidate="txtprocedimiento2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div42">
                                    <label class="visually">ALERGIAS</label>
                                    &nbsp;<asp:TextBox ID="txtalergias2" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" BackColor="White" ControlToValidate="txtalergias2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <br />
                            <center>
                                <asp:Label ID="Label3" runat="server" Text="PERSONAL MÉDICO Y DE ENFERMERÍA"></asp:Label>
                            </center>
                            <br />
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div43">
                                    <label class="visually">ANESTESIÓLOGO</label>
                                    &nbsp;<asp:TextBox ID="txtanestesiologo2" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" BackColor="White" ControlToValidate="txtanestesiologo2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div45">
                                    <label class="visually">MEDICO AYUDANTE 1</label>
                                    &nbsp;<asp:TextBox ID="txtayudante1" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" BackColor="White" ControlToValidate="txtayudante1" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div46">
                                    <label class="visually">MEDICO AYUDANTE 2</label>
                                    &nbsp;<asp:TextBox ID="txtayudante2" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" BackColor="White" ControlToValidate="txtayudante2" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div47">
                                    <label class="visually">INSTRUMENTISTA</label>
                                    &nbsp;<asp:TextBox ID="txtinstrumentista" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" BackColor="White" ControlToValidate="txtinstrumentista" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div48">
                                    <label class="visually">ENFERMERA CIRCULANTE</label>
                                    &nbsp;<asp:TextBox ID="txtcirculante" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" BackColor="White" ControlToValidate="txtcirculante" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg" runat="server" id="Div49">
                                    <label class="visually">ENFERMERA INSTRUMENTISTA</label>
                                    &nbsp;<asp:TextBox ID="txteinstrumentista" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" BackColor="White" ControlToValidate="txteinstrumentista" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col-lg" runat="server" id="Div37">
                                    <asp:Button ID="btnpdf1" CssClass="btn btn-outline-warning" runat="server" Text="GENERAR REGISTRO" OnClick="btnpdf1_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade text-left w-100" id="xlargeOP" tabindex="-1"
                role="dialog" aria-labelledby="myModalLabel153" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
                    role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-dark white">
                            <h4 class="modal-title" style="color: white" id="myModalLabel153">Pacientes Ingresado</h4>
                            <button type="button" class="close" data-bs-dismiss="modal" style="color: white;"
                                aria-label="Close">
                                <i class="bi bi-x-circle"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="gridingresos1" runat="server" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="id_ingreso" OnSelectedIndexChanged="gridingresos1_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="id_ingreso" HeaderText="ID LLEGADA" />
                                    <asp:BoundField DataField="paciente" HeaderText="NOMBRE PACIENTE" />
                                    <asp:BoundField DataField="fecha" HeaderText="FECHA DE NACIMIENTO" />
                                    <asp:BoundField DataField="edad" HeaderText="EDAD" />
                                    <asp:BoundField DataField="sexo" HeaderText="SEXO" />
                                    <asp:BoundField DataField="peso" HeaderText="PESO" />
                                    <asp:BoundField DataField="talla" HeaderText="TALLA" />
                                    <asp:BoundField DataField="representante" HeaderText="REPRESENTANTE LEGAL" />
                                    <asp:BoundField DataField="medico" HeaderText="MEDICO" />
                                    <asp:BoundField DataField="diagnostico" HeaderText="DIAGNOSTICO" />
                                    <asp:BoundField DataField="procedimiento" HeaderText="PROCEDIMIENTO" />
                                    <asp:BoundField DataField="alergias" HeaderText="ALERGIAS" />
                                    <asp:CommandField ButtonType="Button" SelectText="SELECCIONAR" ShowSelectButton="True">
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

        </div>
        <!--==========  Boton inexistente para confirmacion delet ==========-->

    </form>

    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>

    <script src="https://kit.fontawesome.com/4c35c9df44.js" crossorigin="anonymous"></script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.4/js/jquery.dataTables.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jQuery-slimScroll/1.3.8/jquery.slimscroll.min.js"></script>
    <script>
        // Simple Datatable
        let tablep = document.querySelector('#gridregistro');
        let dataTable = new simpleDatatables.DataTable(tablep);
    </script>
    <script>
        // Simple Datatable
        let tablep1 = document.querySelector('#gridingresos');
        let dataTable1 = new simpleDatatables.DataTable(tablep1);
    </script>

    <script src="assets2/js/pages/Citas.js" type="text/javascript"></script>
    <script src="assets2/js/pages/Ingresos.js" type="text/javascript"></script>
    <script src="assets2/js/main.js"></script>
</body>
</html>
