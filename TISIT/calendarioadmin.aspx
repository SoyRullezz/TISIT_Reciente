<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendarioadmin.aspx.cs" Inherits="TISIT.calendarioadmin" %>

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
    <script type="text/javascript">
        function horarios(yyyy, mm, dd) {
            var dias = ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"];
            let d = new Date(`${mm}/${dd}/${yyyy}`)
            //document.getElementById('sabado').innerHTML = "Dia de la semana : " + dias[d.getUTCDay()];
            if (dias[d.getUTCDay()] == "Sab") {
                document.getElementById('sabado').style.display = 'block'
                /*document.getElementById('txtDia').innerText =dias[d.getUTCDay()]*/
            } else {
                document.getElementById('LunesVier').style.display = 'block'
                /*document.getElementById('txtDia').innerText = dias[d.getUTCDay()]*/
            }

        }
        function llenado(hora) {
            console.log(hora)
        }
        function noDatoCalendar() {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Elige una fecha antes por favor!',
            })
        }
        function noselectDoc() {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Elige a tu Doctor por favor!',
            })
        }
        function noDatoMolestia() {
            Swal.fire({
                icon: 'error',
                title: 'Llena el campo requerido',
                text: 'Llena el campo motivo!',

            })
        }
        function Exito() {
            Swal.fire({
                icon: 'success',
                title: 'Registro realizado',
                text: 'Su registro ha sido enviado!',
            })
        }
        function llenaCampos() {
            Swal.fire({
                icon: 'warning',
                title: 'No has seleccionado una horario',
                text: 'Selecciona una horario!',

            })
        }
        function NombrePaciente() {
            Swal.fire({
                icon: 'warning',
                title: 'No has escrito el nombre',
                text: 'Escribe un nombre!',

            })
        }
        function ApellidoPaciente() {
            Swal.fire({
                icon: 'warning',
                title: 'No has escrito el Apellido',
                text: 'Escribe el Apellido!',

            })
        }
        function Edad() {
            Swal.fire({
                icon: 'warning',
                title: 'No has escrito la Edad',
                text: 'Escribe la Edad!',

            })
        }
        function Correo() {
            Swal.fire({
                icon: 'warning',
                title: 'No has escrito un correo valido',
                text: 'Escribe un Correo Valido!',

            })
        }
        function Telefono() {
            Swal.fire({
                icon: 'error',
                title: 'No has escrito el Telefono',
                text: 'Escribe el Telefono!',

            })
        }
        function Algiosaliomal() {
            Swal.fire({
                icon: 'warning',
                title: 'Algo ha salido mal..',
                text: 'Algo salio Mal!',

            })
        }
    </script>
    <style>
        .btn {
            color: #17a2b8;
            border-color: #17a2b8;
        }

        .LunVier {
            display: none;
        }

        .sabado {
            display: none;
        }

        .contenedorCalendario {
            width: 545px;
            height: auto;
            padding: 23px;
            margin: 9px;
            background-color: white;
            border-radius: 15px;
        }

        .formulario {
            display: flex;
            width: auto;
            padding: 49px;
            margin: 15px;
            flex-direction: column;
            background-color: white;
            border-radius: 15px;
        }

        .contenedorglobal {
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: space-evenly;
        }

        .drodL {
            width: 341px;
        }

            .drodL:hover {
                color: #17a2b8;
                border-color: #17a2b8;
            }

        .Docs {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }

        body {
            background-color: #F9F6FD
        }

        .segbtn {
            display: flex;
            flex-direction: row;
        }

        .calendario {
            display: flex;
            flex-direction: column;
            align-items: center;
            background-color: #F9F6FD;
            border-radius: 5px;
        }

        .horarios {
            display: flex;
            justify-content: center;
        }

        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 28px 10px 1px;
            border: solid 1px #F9F6FD;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 7px;
                /*border: solid 1px #854a4a;*/
                color: #000000;
            }

            .mGrid th {
                padding: 4px 2px;
                color: #ffffff;
                background: #051097;
                border-left: solid 1px #ffffff;
                font-size: 0.9em;
            }

            .mGrid .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

            .mGrid a {
                color: #000000;
                text-decoration: none;
            }

                .mGrid a:hover {
                    color: #17a2b8;
                    text-decoration: none;
                }

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

        @media (max-width: 1200px) {
            .contenedorglobal {
                display: flex;
                flex-direction: column;
            }

            .contenedorCalendario {
                width: 92%;
            }

            .formulario {
                width: 92%;
            }

            @media (max-width:560px) {
                .drodL {
                    width: 250px;
                }
            }
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
                    <h3>Agendar Cita</h3>
                </div>
                <br />
                <!-- Page content-->
                <div class="container">
                    <div class="contenedorglobal">
                        <div class="contenedorCalendario">
                            <div>
                                <h1>Vamos agendar tu cita</h1>
                                <p>1. Primer paso selecciona el día que deseas tu cita recordando que los domingos no laboramos  </p>
                            </div>
                            <div class="calendario">
                                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" OnSelectionChanged="Calendar1_SelectionChanged" BorderWidth="1px" OnDayRender="Calendar1_DayRender">
                                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                    <TodayDayStyle BackColor="#CCCCCC" />
                                </asp:Calendar>
                            </div>
                            <div>
                                <asp:Label ID="Label8" runat="server" Text="Fecha seleccionada:"></asp:Label>
                                <asp:Label ID="txtCalendar" runat="server" Text=""></asp:Label>


                            </div>
                            <div>
                                <p>2. Segundo paso hay que seleccionar a un Doctor de tu preferencia y darle clic al boton de buscar</p>
                            </div>
                            <div class="Docs ">
                                <div class="segbtn">
                                    <asp:DropDownList ID="DropDownList1" CssClass=" drodL form-control" runat="server" DataTextField="Nombre_doctor" DataValueField="id_doctor" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataSourceID="SqlDataSource1"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TISITConnectionString2 %>" SelectCommand="select distinct id_doctor,concat(nombre_doctor, ' ', apaterno, ' ', amaterno) as 'Nombre_doctor' from doctores where activo = 1 order by Nombre_doctor;"></asp:SqlDataSource>
                                    <asp:Button ID="Button1" CssClass="btn  btn-outline-info" runat="server" Text="Buscar" OnClick="Button1_Click" />
                                </div>
                                <div>
                                    <asp:Label ID="Label23" runat="server" Text="Tu Doctor: "></asp:Label>
                                    <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="Instru">
                                <div>
                                    <p>3. Tercer paso vamos a ver la disponibilidad y selecciona el horario que mas se te acomode</p>

                                </div>
                            </div>
                            <div class="horarios">
                                <div class="entreSemana">
                                    <asp:Label ID="entreSemana" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="LunVier" id="LunesVier">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:BoundField DataField="hora" HeaderText="Horarios" />
                                            <asp:BoundField DataField="dis" HeaderText="Disponibilidad" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="fin">
                                    <asp:Label ID="fin" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="sabado" id="sabado">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <AlternatingRowStyle />
                                        <Columns>
                                            <asp:BoundField DataField="hora" HeaderText="Horarios" />
                                            <asp:BoundField DataField="dis" HeaderText="Disponibilidad" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle />
                                    </asp:GridView>
                                </div>
                                <div>
                                    <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                </div>


                            </div>
                        </div>
                        <div class="formulario">
                            <h1>Vamos Ingresar los datos necesarios</h1>
                            <p>4.Cuarto paso ingresa tu motivo de cita </p>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <asp:Label runat="server" CssClass="visually" Text="Datos del Doctor"></asp:Label>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col">
                                    <asp:Label runat="server" CssClass="visually" Text="Nombre"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtNombreDoctor" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label runat="server" CssClass="visually" Text="Apellido Paterno"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtApellidoDoctor" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label runat="server" CssClass="visually" Text="Apellido Materno"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtApellidoMaterno" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <asp:Label runat="server" CssClass="visually" Text="Datos del Paciente"></asp:Label>
                            </div>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col">
                                    <asp:Label runat="server" Text="Nombre"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtNpaciente" BorderWidth="2 px"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label runat="server" Text="Apellido Paterno"></asp:Label>
                                    <asp:TextBox runat="server" Width="190 px" ID="txtAPpaciente" BorderWidth="2 px"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label runat="server" Text="Apellidos Materno"></asp:Label>
                                    <asp:TextBox runat="server" Width="190 px" ID="txtAMpaciente" BorderWidth="2 px"></asp:TextBox>
                                </div>
                            </div>



                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col">
                                    <asp:Label runat="server" Text="Edad"></asp:Label>
                                    <asp:TextBox runat="server" ID="Edadpaciente" BorderWidth="2 px" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label runat="server" Text="Correo "></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCorreo" BorderWidth="2 px"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <asp:Label runat="server" Text="Telefono "></asp:Label>
                                    <asp:TextBox runat="server" ID="txttelefono" BorderWidth="2 px" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <div class="col">
                                    <asp:Label runat="server" Text="Fecha Elegida"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtfechaelegida" Enabled="false"></asp:TextBox>
                                </div>

                                <div class="col">
                                    <asp:Label runat="server" Text="Hora Elegida"></asp:Label>
                                    <asp:TextBox runat="server" ID="txthoralegida" Enabled="false"></asp:TextBox>
                                </div>

                                <div class="col">
                                    <asp:Label runat="server" Text="Tipo Cita"></asp:Label>
                                    <asp:DropDownList ID="DropDownList2" Width="190 px" BorderWidth="2 px" DataTextField="tipo_cita" DataValueField="id_tipo" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" DataSourceID="SqlDataSource2"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TISITConnectionString2 %>" SelectCommand="SELECT id_tipo, descripcion as tipo_cita FROM tipo_cita"></asp:SqlDataSource>
                                </div>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <asp:Label runat="server" Text="Motivo de la cita"></asp:Label>
                                <asp:TextBox runat="server" class="form-control labeltxt" ID="txtmotivo" TextMode="MultiLine" Rows="5" BorderWidth="2 px"></asp:TextBox>
                            </div>

                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <asp:Label runat="server" Text="Datos importantes referente a otros problemas que sienta"></asp:Label>
                                <asp:TextBox runat="server" class="form-control" ID="txtmensaje" Rows="2" BorderWidth="2 px"></asp:TextBox>
                            </div>
                            <%--boton para enviar la informacion recabada--%>
                            <div class="row gap-1" style="align-items: center; margin: 10px;">
                                <asp:Button runat="server" Text="Enviar Datos " ID="datasend" OnClick="datasend_Click" CssClass="btn btn-warning" />

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
