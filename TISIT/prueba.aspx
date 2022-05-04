<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="TISIT.prueba" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
        <div>
            <label class="visually">NOMBRE DEL PACIENTE</label>
            &nbsp;<asp:TextBox ID="txtpaciente" runat="server" CssClass="form-control" ReadOnly="true" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
        </div>
        <div class="card card-body">
            <div class="box-body table table-responsive mGrid">
                <div class="box-body table table-responsive">
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

                </div>

            </div>
        </div>

        <div>
            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
        </div>
    </form>
    <script src="assets2/vendors/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="assets2/js/bootstrap.bundle.min.js"></script>
    <script src="assets2/js/permisos.js"></script>

    <script src="https://kit.fontawesome.com/4c35c9df44.js" crossorigin="anonymous"></script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.4/js/jquery.dataTables.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jQuery-slimScroll/1.3.8/jquery.slimscroll.min.js"></script>



    <script src="assets2/js/main.js"></script>

    <script src="assets2/js/pages/Citas.js" type="text/javascript"></script>

</body>
</html>
