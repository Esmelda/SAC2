﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace SAC.formularios
{
    public partial class frm_BuscarVenta : System.Web.UI.Page
    {
        metodos.Metodos_Ventas venta = new metodos.Metodos_Ventas();
        protected void Page_Load(object sender, EventArgs e)
        {
            Gridview_Paciente.DataSource = venta.TodaslasVentas();
            Gridview_Paciente.DataBind();
        }

        protected void Gridview_Paciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gridview_Paciente.Rows)
            {

                if (row.RowIndex == Gridview_Paciente.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    String[] vector = venta.BuscarVenta((row.Cells[0].Text).ToString());
                    lbl_codigo.Text = vector[0];
                    lbl_cedula.Text = vector[1];
                    lbl_fecha.Text = vector[2];
                    lbl_detalle.Text = vector[3];
                    lbl_monto.Text = vector[4];
                    lbl_saldo.Text = vector[5];
                    string script = @"<script type='text/javascript'>
                        document.getElementById('tabla').style.display = 'block' ;
                        document.getElementById('grid').style.display = 'none';                  
                        </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";

                }
            }
        }

        protected void Gridview_Paciente_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            for (int i = 0; i < Gridview_Paciente.Columns.Count; i++)
            {
                TableHeaderCell cell = new TableHeaderCell();
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = Gridview_Paciente.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox";
                cell.Controls.Add(txtSearch);
                row.Controls.Add(cell);
            }
            Gridview_Paciente.HeaderRow.Parent.Controls.AddAt(1, row);
        }

        protected void Gridview_Paciente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gridview_Paciente, "select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click para seleccionar esta fila.";
            }
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>
                        document.getElementById('grid').style.display = 'block' ;
                        document.getElementById('tabla').style.display = 'none';                  
                        </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }
    }
}