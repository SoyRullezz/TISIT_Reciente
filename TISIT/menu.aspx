<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="TISIT.alta_usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700,800,900" rel="stylesheet">
    <title>MENU PRINCIPAL</title>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
     <!--========== BOX ICONS ==========-->
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/boxicons@latest/css/boxicons.min.css">

    <!--========== CSS ==========-->
   <link rel="stylesheet" href="assets/css/styles.css">

    <script>
        function alertaExito() {
            Swal.fire(
                'Personal Agregado',
                'El trabajor ha sido agregado correctamente',
                'success'
            )
        }

        function alertaInfoCURP() {
            Swal.fire({
                title: 'CURP no valido',
                text: "Revise que el curp tenga un formato correcto",
                icon: 'warning',
            })
        }

        function alertaInfoRFC() {
            Swal.fire({
                title: 'RFC no valido',
                text: "Revise que el rfc tenga un formato correcto",
                icon: 'warning',
            })
        }

        function alertaInfoNSS() {
            Swal.fire({
                title: 'NSS no valido',
                text: "Revise que el nss tenga un formato correcto",
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
        .centrado{

        margin-left: auto; 

        margin-right: auto;

        }

        .usuario {
          position:absolute;
          top:20px;
          right:115px;
        }
    </style>
   
</head>
<body>
    <form id="frmAltaPersonal" data-parsley-validate runat="server">

        <!--========== HEADER ==========-->
        <header class="header">
            <div class="header__container">

                

                <img src="assets/img/usuario.png" alt="" class="header__img">
              
                <asp:Label ID="lbluser" runat="server" CssClass="usuario"></asp:Label>

                <a href="#" class="header__logo">CLINICA SANTA LUCIA</a>

                <div class="header__toggle">
                    <i class='bx bx-menu' id="header-toggle"></i>
                </div>
            </div>
        </header>

        <!--========== NAV ==========-->
        <div class="nav" id="navbar">
            <nav class="nav__container">
                <div>
                    <a href="#" class="nav__link nav__logo">
                        <i class='bx bxs-spreadsheet'></i>
                        &nbsp;
                        <span class="nav__logo-name">BIENVENID@ <asp:Label ID="lblnombre_bienvenida" runat="server" Text="Label"></asp:Label></span>
                    </a>

                    <div class="nav__list">
                        <div class="nav__items">
                            <h3 class="nav__subtitle">Bienvenido</h3>

                            <a href="Inicio.aspx" class="nav__link active">
                                <i class='bx bx-home nav__icon'></i>
                                <span class="nav__name">Inicio</span>
                            </a>

                            <div class="nav__dropdown">
                                <a href="#" class="nav__link">
                                    <i class='bx bx-user nav__icon'></i>
                                    <span class="nav__name">Mi Usuario</span>
                                    <i class='bx bx-chevron-down nav__icon nav__dropdown-icon'></i>
                                </a>

                                <div class="nav__dropdown-collapse">
                                    <div class="nav__dropdown-content">
                                        <a href="cambiar_contra.aspx" class="nav__dropdown-item">Cambiar Contraseña</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="nav__items">
                            <h3 class="nav__subtitle">Menu</h3>

                            <div class="nav__dropdown">
                                <a href="alta_usuarios.aspx" class="nav__link">
                                <i class='bx bx-task nav__icon'></i>
                                    <span class="nav__name"> Administración
                                    </span>
                                    <i class='bx bx-chevron-down nav__icon nav__dropdown-icon'></i>
                                </a>
                                <div class="nav__dropdown-collapse">
                                    <div class="nav__dropdown-content">
                                        <a href="alta_personal.aspx" class="nav__dropdown-item">PERSONAL</a>
                                        <a href="direccion_personal.aspx" class="nav__dropdown-item">DIRECCION PERSONAL</a>
                                        <a href="#" class="nav__dropdown-item">CONTRATO PERSONAL</a>
                                        <a href="#" class="nav__dropdown-item">PERMISOS</a>
                                        <a href="registro_pacientes.aspx" class="nav__dropdown-item">REGISTRO DE PACIENTES</a>
                                    </div>
                                </div>
                            </div>


                            <div class="nav__dropdown">
                                <a href="#" class="nav__link">
                                <i class='bx bxs-shopping-bag-alt nav__icon'></i>
                                    <span class="nav__name"> 
                                        Servicios
                                    </span>
                                    <i class='bx bx-chevron-down nav__icon nav__dropdown-icon'></i>
                                </a>

                                <div class="nav__dropdown-collapse">
                                    <div class="nav__dropdown-content">
                                        <a href="calendarioadmin.aspx" class="nav__dropdown-item">REGISTRO DE CITAS</a>
                                        <a href="#" class="nav__dropdown-item">servicio 2</a>
                                        <a href="#" class="nav__dropdown-item">servicio 3</a>
                                    </div>
                                </div>

                            </div>
                            

                            <a href="#" class="nav__link">
                                <i class='bx bx-compass nav__icon'></i>
                                <span class="nav__name">Más</span>
                            </a>
                            <a href="#" class="nav__link">
                                <i class='bx bx-bookmark nav__icon'></i>
                                <span class="nav__name">Más</span>
                            </a>
                        </div>
                    </div>
                </div>

                <a href="login.aspx" class="nav__link nav__logout">
                    <i class='bx bx-log-out nav__icon'></i>
                    <span class="nav__name">Cerrar Sesion</span>
                </a>
            </nav>
        </div>

        <!--========== CONTENTS ==========-->
        <main>

            <!-- Page content-->
            <div class="container">
                <br />
                <div class="row" >
                    <asp:Image ID="logo" runat="server" src="img/tisit_logo1.png" class="centrado"/>
                </div>
            </div>
        </main>


    </form>

     <script src="assets/js/main.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/popper.js"></script>
    <script src="js/bootstrap.min.js"></script>
  </body>
</html>
