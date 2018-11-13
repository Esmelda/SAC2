﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace SAC.formularios
{
    public partial class frm_ReporteCxC : System.Web.UI.Page
    {
        metodos.Metodos_Ventas cuenta = new metodos.Metodos_Ventas();
        public static DataTable tabla;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_todos_Click(object sender, EventArgs e)
        {
            tabla = cuenta.CuentaXCobrarImprimir();
            int limite = tabla.Rows.Count - 1;
            Double suma = 0;
            Double suma2 = 0;

            String nombrepdf = "Reporte de Cuentas por Cobrar.pdf";
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename= '" + nombrepdf + "'");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.LETTER.Rotate(), 36, 36, 36, 36);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            PdfPTable tbl_Imprimir = new PdfPTable(8);


            tbl_Imprimir.SetWidthPercentage(new float[] { 80, 80, 80, 80, 80, 80, 80, 80 }, PageSize.A4);
            //Encabezado de la tabla
            tbl_Imprimir.AddCell(new Paragraph("Código venta"));
            tbl_Imprimir.AddCell(new Paragraph("Cédula paciente"));
            tbl_Imprimir.AddCell(new Paragraph("Primer nombre"));
            tbl_Imprimir.AddCell(new Paragraph("Segundo nombre"));
            tbl_Imprimir.AddCell(new Paragraph("Fecha venta"));
            tbl_Imprimir.AddCell(new Paragraph("Detalle de la venta"));
            tbl_Imprimir.AddCell(new Paragraph("Monto total de la venta"));
            tbl_Imprimir.AddCell(new Paragraph("Saldo de la venta"));

            //Fondo para que se note la diferencia
            foreach (PdfPCell celda in tbl_Imprimir.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 3;
            }
            for (int i = 0; i <= limite; i++)
            {
                PdfPCell celda1 = new PdfPCell(new Paragraph((tabla.Rows[i][0]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda2 = new PdfPCell(new Paragraph((tabla.Rows[i][1]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda3 = new PdfPCell(new Paragraph((tabla.Rows[i][2]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda4 = new PdfPCell(new Paragraph((tabla.Rows[i][3]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda5 = new PdfPCell(new Paragraph((tabla.Rows[i][4]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda6 = new PdfPCell(new Paragraph((tabla.Rows[i][5]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda7 = new PdfPCell(new Paragraph((tabla.Rows[i][6]).ToString(), FontFactory.GetFont("Arial", 12)));
                PdfPCell celda8 = new PdfPCell(new Paragraph((tabla.Rows[i][7]).ToString(), FontFactory.GetFont("Arial", 12)));

                tbl_Imprimir.AddCell(celda1);
                tbl_Imprimir.AddCell(celda2);
                tbl_Imprimir.AddCell(celda3);
                tbl_Imprimir.AddCell(celda4);
                tbl_Imprimir.AddCell(celda5);
                tbl_Imprimir.AddCell(celda6);
                tbl_Imprimir.AddCell(celda7);
                tbl_Imprimir.AddCell(celda8);
                suma = suma + Convert.ToInt32(tabla.Rows[i][6]);
                suma2 = suma2 + Convert.ToInt32(tabla.Rows[i][7]);
            }
            DateTime date = DateTime.Now;
            String date2 = date.ToShortDateString();
            int numero = 1;
            var pagina = new Paragraph("Pág." + numero);
            var titulo1 = new Paragraph("Clínica Dental\n Dra. Alina Camacho B.\n Reporte de cuentas por cobrar");
            var fecha = new Paragraph("Fecha: " + date2);
            var monto = new Paragraph("Total: ₡ " + suma + "     Saldo: ₡" + suma2);

            pagina.SpacingBefore = 1; //Espacio antes
            titulo1.SpacingBefore = 0;
            fecha.SpacingBefore = 0;
            monto.SpacingBefore = 1;

            pagina.SpacingAfter = 0; //Espacio después
            titulo1.SpacingAfter = 0;
            fecha.SpacingAfter = 3;
            monto.SpacingAfter = 2;

            pagina.Alignment = 2; //0-izquierda, 1-centro,2-derecho
            titulo1.Alignment = 1;
            fecha.Alignment = 2;
            monto.Alignment = 2;

            pdfDoc.Add(pagina); //Agrega los elementos al pdf
            pdfDoc.Add(titulo1);
            pdfDoc.Add(fecha);
            pdfDoc.Add(tbl_Imprimir);
            pdfDoc.Add(monto);

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void btn_uno_Click(object sender, EventArgs e)
        {
            Gridview_Venta.DataSource = cuenta.CuentaXCobrarReporte();
            Gridview_Venta.DataBind();
            string script = @"<script type='text/javascript'>
                            document.getElementById('busqueda').style.display = 'block';
                            </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }

        

        protected void Gridview_Venta_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            for (int i = 0; i < Gridview_Venta.Columns.Count; i++)
            {
                TableHeaderCell cell = new TableHeaderCell();
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = Gridview_Venta.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox";
                cell.Controls.Add(txtSearch);
                row.Controls.Add(cell);
            }
            Gridview_Venta.HeaderRow.Parent.Controls.AddAt(1, row);
        }

        protected void Gridview_Venta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gridview_Venta, "select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click para seleccionar esta fila.";
            }
        }

        protected void Gridview_Venta_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gridview_Venta.Rows)
            {
                if (row.RowIndex == Gridview_Venta.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;

                    String cedula = row.Cells[0].Text;
                    string scripts = @"<script type='text/javascript'>
                    alert('"+cedula+"');</script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", scripts, false);
                    //tabla = cuenta.PersonaVenta(cedula);
                    //int limite = tabla.Rows.Count - 1;
                    //Double saldo = 0;
                    //Double monto = 0;
                    //Double abono = 0;

                    //String nombrepdf = "Reporte de Cuentas por Cobrar " + cedula + ".pdf";
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename= '" + nombrepdf + "'");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //StringReader sr = new StringReader(sw.ToString());
                    //Document pdfDoc = new Document(PageSize.LETTER.Rotate(), 36, 36, 36, 36);
                    //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    //pdfDoc.Open();

                    //// Se crean las tablas y se les da el tamaño de las columnas
                    //PdfPTable tabla_venta = new PdfPTable(5);
                    //PdfPTable tabla_cuenta = new PdfPTable(4);
                    //tabla_venta.SetWidthPercentage(new float[] { 80, 80, 80, 80, 80 }, PageSize.A4);
                    //tabla_cuenta.SetWidthPercentage(new float[] { 100, 100, 100, 100 }, PageSize.A4);

                    ////Encabezado de la tabla
                    //tabla_venta.AddCell(new Paragraph("Código de la venta"));
                    //tabla_venta.AddCell(new Paragraph("Fecha de la venta"));
                    //tabla_venta.AddCell(new Paragraph("Detalle de la venta"));
                    //tabla_venta.AddCell(new Paragraph("Monto total de la venta"));
                    //tabla_venta.AddCell(new Paragraph("Saldo de la venta"));

                    ////Encabezado de la tabla
                    //tabla_cuenta.AddCell(new Paragraph("Código del abono"));
                    //tabla_cuenta.AddCell(new Paragraph("Código de la venta"));
                    //tabla_cuenta.AddCell(new Paragraph("Monto del abono"));
                    //tabla_cuenta.AddCell(new Paragraph("Fecha del abono"));

                    ////Fondo para que se note la diferencia
                    //foreach (PdfPCell celda in tabla_venta.Rows[0].GetCells())
                    //{
                    //    celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                    //    celda.HorizontalAlignment = 1;
                    //    celda.Padding = 3;
                    //}

                    ////Fondo para que se note la diferencia
                    //foreach (PdfPCell celda in tabla_cuenta.Rows[0].GetCells())
                    //{
                    //    celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                    //    celda.HorizontalAlignment = 1;
                    //    celda.Padding = 3;
                    //}

                    //// lleno de datos las dos tablas
                    //for (int i = 0; i <= limite; i++)
                    //{
                    //    PdfPCell celda1 = new PdfPCell(new Paragraph((tabla.Rows[i][5]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda2 = new PdfPCell(new Paragraph((tabla.Rows[i][6]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda3 = new PdfPCell(new Paragraph((tabla.Rows[i][7]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda4 = new PdfPCell(new Paragraph((tabla.Rows[i][8]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda5 = new PdfPCell(new Paragraph((tabla.Rows[i][9]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda6 = new PdfPCell(new Paragraph((tabla.Rows[i][10]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda7 = new PdfPCell(new Paragraph((tabla.Rows[i][11]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda8 = new PdfPCell(new Paragraph((tabla.Rows[i][12]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    PdfPCell celda9 = new PdfPCell(new Paragraph((tabla.Rows[i][13]).ToString(), FontFactory.GetFont("Arial", 12)));
                    //    tabla_venta.AddCell(celda1);
                    //    tabla_venta.AddCell(celda2);
                    //    tabla_venta.AddCell(celda3);
                    //    tabla_venta.AddCell(celda4);
                    //    tabla_venta.AddCell(celda5);
                    //    tabla_cuenta.AddCell(celda6);
                    //    tabla_cuenta.AddCell(celda7);
                    //    tabla_cuenta.AddCell(celda8);
                    //    tabla_cuenta.AddCell(celda9);
                    //    monto = monto + Convert.ToInt32(tabla.Rows[i][8]);
                    //    saldo = saldo + Convert.ToInt32(tabla.Rows[i][9]);
                    //    abono = abono + Convert.ToInt32(tabla.Rows[i][12]);
                    //}

                    //// Creo encabezado
                    //DateTime date = DateTime.Now;
                    //String date2 = date.ToShortDateString();
                    //int numero = 1;
                    //var pagina = new Paragraph("Pág." + numero);
                    //var titulo1 = new Paragraph("Clínica Dental\n Dra. Alina Camacho B.\n Reporte de cuentas por cobrar");
                    //var persona = new Paragraph("Cédula: " + tabla.Rows[1][0].ToString() + "\n Nombre: " + tabla.Rows[1][1].ToString() + " " + tabla.Rows[1][2].ToString() + " " + tabla.Rows[1][3].ToString() + " " + tabla.Rows[1][4].ToString());
                    //var fecha = new Paragraph("Fecha: " + date2);
                    //var monto_imprimir = new Paragraph("Total: ₡ " + monto + "     Saldo: ₡" + saldo);
                    //var monto_imprimir2 = new Paragraph("Total: ₡ " + abono);

                    //pagina.SpacingBefore = 1; //Espacio antes
                    //titulo1.SpacingBefore = 0;
                    //persona.SpacingBefore = 0;
                    //fecha.SpacingBefore = 0;
                    //monto_imprimir.SpacingBefore = 1;
                    //monto_imprimir2.SpacingBefore = 1;

                    //pagina.SpacingAfter = 0; //Espacio después
                    //titulo1.SpacingAfter = 0;
                    //persona.SpacingAfter = 0;
                    //fecha.SpacingAfter = 3;
                    //monto_imprimir.SpacingAfter = 2;
                    //monto_imprimir2.SpacingAfter = 2;

                    //pagina.Alignment = 2; //0-izquierda, 1-centro, 2-derecha
                    //titulo1.Alignment = 1;
                    //persona.Alignment = 0;
                    //fecha.Alignment = 2;
                    //monto_imprimir.Alignment = 2;
                    //monto_imprimir2.Alignment = 2;

                    //pdfDoc.Add(pagina); //Agrega los elementos al pdf
                    //pdfDoc.Add(titulo1);
                    //pdfDoc.Add(persona);
                    //pdfDoc.Add(fecha);
                    //pdfDoc.Add(tabla_venta);
                    //pdfDoc.Add(monto_imprimir);
                    //pdfDoc.Add(tabla_cuenta);
                    //pdfDoc.Add(monto_imprimir2);

                    //htmlparser.Parse(sr); // Termina el pdf y se descarga
                    //pdfDoc.Close();
                    //Response.Write(pdfDoc);
                    //Response.End();
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }
        }
    }
}