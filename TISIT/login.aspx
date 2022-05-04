<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TISIT.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<link rel="stylesheet" href="assets2/css/bootstrap.css">--%>

    <link rel="stylesheet" href="assets2/vendors/simple-datatables/style.css">

    <link rel="stylesheet" href="assets2/vendors/perfect-scrollbar/perfect-scrollbar.css">
    <link rel="stylesheet" href="assets2/vendors/bootstrap-icons/bootstrap-icons.css">
    <link rel="stylesheet" href="assets2/css/app.css">
    <link rel="shortcut icon" href="assets2/images/favicon.svg" type="image/x-icon">
    <title>LOGIN</title>

    <link rel="stylesheet" href="Styles/styleNew.css">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <%--<script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>--%>
    <script src="_framework/blazor.webassembly.js"></script>
    <script>
        function alertaExito() {
            Swal.fire(
                'DATOS INSERTADOS',
                'Los datos han sido guardados correctamente',
                'success'
            )
        }
        function alertaCamposVacios() {
            Swal.fire(
                'USUARIO O CONTRASEÑA ESTAN VACÍOS',
                '',
                'error',
            )
        }

        function alertaError() {
            Swal.fire(
                'EL USUARIO O LA CONTRASEÑA NO COINCIDEN',
                '',
                'error',
            )
        }

        //function alertaError() {
        //    Swal.fire({
        //        icon: 'error',
        //        title: 'USUARIO INVALIDO',
        //        text: 'LOS DATOS ESTAN INCORRECTOS',
        //    })
        //}
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <aside class="profile-card">
                <header>
                    <!-- here’s the avatar -->

                    <%--<img src="img/icons8-doctor-60.png" class="hoverZoomLink">--%>
                    <img src="img/tisit_logo1.png" class="hoverZoomLink">
                </header>

                <!-- bit of a bio; who are you? -->
                <div class="profile-bio">

                    <asp:TextBox CssClass="text" ID="txtcorreo" runat="server" placeholder="Usuario"></asp:TextBox>
                    <br />
                    <asp:TextBox CssClass="text" ID="txtcontra" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                    <br />
                    <div class="btn">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button CssClass=" button" ID="BTNI" runat="server" Text="Ingresar" OnClick="Button1_Click" onSubmit="return false" />
                                <asp:Button CssClass=" button2" ID="Button2" runat="server" Text="¿Olvidaste tu contraseña?" />
                                <div id="demo">
                                    <asp:Label CssClass="txt" ID="Label1" runat="server" Text=""></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <img src="img/tenkui.png" class="hoverZoomLink">
                    </div>
                </div>

            </aside>
        </div>
    </form>
    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>
    <%--<script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>--%>
    <script src="assets2/vendors/simple-datatables/simple-datatables.js"></script>
</body>
</html>
