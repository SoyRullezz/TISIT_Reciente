using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using CapaLogicaNegocio;
using System.IO;
using BarcodeLib;
using System.Drawing;
using Image = System.Drawing.Image;

using iText;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.IO.Font;
using System.Configuration;
using System.Data.SqlClient;

namespace TISIT
{
    public partial class corte_caja : System.Web.UI.Page
    {
        public static System.Diagnostics.Process process = new System.Diagnostics.Process();

        protected void Page_Load(object sender, EventArgs e)
        {



        }

        [WebMethod]
        public static void imprimirPDF()
        {
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = "20220501" + ".pdf";

            using (PdfWriter pdfWriter = new PdfWriter(fullpath + "\\Cortes de Caja\\" + filename))
            using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
            using (Document document = new Document(pdfDocument))
            {
                document.SetMargins(75, 35, 70, 35);
                document.Add(new Paragraph("iLaser Ciruguia Refractiva").SetFontSize(25).SetTextAlignment(TextAlignment.CENTER));
                document.Add(new Paragraph("REPORTE DE INGRESOS").SetFontSize(25).SetTextAlignment(TextAlignment.CENTER));


                iText.Layout.Style styleCell = new iText.Layout.Style().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER);
                iText.Layout.Element.Table tabla = new iText.Layout.Element.Table(4).UseAllAvailableWidth();
                iText.Layout.Element.Cell celda = new iText.Layout.Element.Cell().Add(new Paragraph("DOCTOR").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("PASIENTE").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("CONCEPTO").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("TOTAL").SetFontSize(15));
                tabla.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Jose Mendoza Cabrera"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Alan Perez Martines"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("CONSULTA"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$500.00 MX"));
                tabla.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Alan Ituriel Gonzalez"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Judith Velazquez Velazco"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("ESTUDIOS"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$1000.00 MX"));
                tabla.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Javier Gonzalez Macias"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Edgar Gonzalez Velazquez"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("CIRUGIA"));
                tabla.AddCell(celda);
                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$10000.00 MX"));
                tabla.AddCell(celda);

                document.Add(tabla);

                iText.Layout.Element.Table tablaProductos = new iText.Layout.Element.Table(4).UseAllAvailableWidth();

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("GOTAS").SetFontSize(15));
                tablaProductos.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Precio Unitario").SetFontSize(15));
                tablaProductos.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Unidades").SetFontSize(15));
                tablaProductos.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Total").SetFontSize(15));
                tablaProductos.AddHeaderCell(celda.AddStyle(styleCell));



                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Systane"));
                tablaProductos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$200.00"));
                tablaProductos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("5"));
                tablaProductos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$1000"));
                tablaProductos.AddCell(celda);


                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Netex"));
                tablaProductos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$100.00"));
                tablaProductos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("5"));
                tablaProductos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$500"));
                tablaProductos.AddCell(celda);

                document.Add(tablaProductos);


                iText.Layout.Element.Table tablaTotalIngresos = new iText.Layout.Element.Table(2).UseAllAvailableWidth();

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("TOTAL"));
                tablaTotalIngresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$13000.00"));
                tablaTotalIngresos.AddCell(celda);

                document.Add(tablaTotalIngresos);

                document.Add(new Paragraph("EGRESOS").SetFontSize(25).SetTextAlignment(TextAlignment.CENTER));


                iText.Layout.Element.Table tablaEgresos = new iText.Layout.Element.Table(3).UseAllAvailableWidth();

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Jose Mendoza Cabrera"));
                tablaEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Honorarios Profesionales"));
                tablaEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$1000"));
                tablaEgresos.AddCell(celda);


                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Alan Ituriel Gonzalez"));
                tablaEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Honorarios Profesionales"));
                tablaEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$1000"));
                tablaEgresos.AddCell(celda);


                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Javier Gonzalez Macias"));
                tablaEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("Honorarios Profesionales"));
                tablaEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$1000"));
                tablaEgresos.AddCell(celda);

                document.Add(tablaEgresos);



                iText.Layout.Element.Table tablaTotalEgresos = new iText.Layout.Element.Table(2).UseAllAvailableWidth();

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("TOTAL"));
                tablaTotalEgresos.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$3000"));
                tablaTotalEgresos.AddCell(celda);

                document.Add(tablaTotalEgresos);


                iText.Layout.Element.Table tablaCorte = new iText.Layout.Element.Table(3).UseAllAvailableWidth();

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("INGRESOS").SetFontSize(15));
                tablaCorte.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("EGRESOS").SetFontSize(15));
                tablaCorte.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("GRAN TOTAL").SetFontSize(15));
                tablaCorte.AddHeaderCell(celda.AddStyle(styleCell));

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$13000.00"));
                tablaCorte.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$3000.00"));
                tablaCorte.AddCell(celda);

                celda = new iText.Layout.Element.Cell().Add(new Paragraph("$10000.00"));
                tablaCorte.AddCell(celda);

                document.Add(tablaCorte);

            }


        }

        [WebMethod]
        public static List<itemReporteVenta> buscarReportes(string FI, string FF, string tipo)
        {


            List<string> fechas = new List<string>();

            List<productosReporte> listaProductosReporte;
            productosReporte producto;
            List<itemReporteVenta> listaReportes = new List<itemReporteVenta>();
            itemReporteVenta itemRV;






            List<infoConsultaReportes> ventasBusqueda = VentasLN.getInstance().buscarVentaFecha(FF, FI, tipo);

            if (ventasBusqueda.Count() == 0)
            {
                return new List<itemReporteVenta>();
            }

            ventasBusqueda.ForEach(e => {
                if (!fechas.Any(x => x == e.Fecha))
                {
                    fechas.Add(e.Fecha);
                }
            });

            fechas.ForEach(e =>
            {
                decimal total = 0;
                itemRV = new itemReporteVenta();
                itemRV.Fecha = e;
                listaProductosReporte = new List<productosReporte>();
                foreach (infoConsultaReportes p in ventasBusqueda.Where(x => x.Fecha == e))
                {
                    producto = new productosReporte();
                    producto.Id = p.Id_Producto;
                    producto.Nombre = p.Nombre;
                    producto.Precio = p.Precio;
                    producto.Cantidad = p.Cantidad;
                    producto.Subtotal = p.Subtotal;

                    listaProductosReporte.Add(producto);
                }
                itemRV.Productos = listaProductosReporte;

                listaProductosReporte.ForEach(psub => {
                    total += psub.Subtotal;
                });
                itemRV.Total = total;

                listaReportes.Add(itemRV);

            });

            listaReportes.Count();

            return listaReportes;
        }
    }
}