﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_ReporteCxC.aspx.cs" Inherits="SAC.formularios.frm_ReporteCxC" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/materialize.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../js/quicksearch.js"></script>
    <title></title>
    <style>
        .boton1 {
            width: 123px;
            height: 150px;
            background-image: url(../images/Descargar1.png);
            border: 0px;
        }
        .boton2 {
            width: 123px;
            height: 150px;
            background-image: url(../images/Personas1.png);
            border: 0px;
            overflow: hidden;
        }

        .boton1:hover {
            -webkit-transform: scale(1.25);
            -moz-transform: scale(1.25);
            -ms-transform: scale(1.25);
            -o-transform: scale(1.25);
            transform: scale(1.25);
        }

        .boton2:hover {
            -webkit-transform: scale(1.25);
            -moz-transform: scale(1.25);
            -ms-transform: scale(1.25);
            -o-transform: scale(1.25);
            transform: scale(1.25);
        }

        .espacio {
            padding-top: 5%;
        }
    </style>
</head>
<body oncopy="return false" onpaste="return false">
    <header>
        <h2 style="text-align: center">Reporte de cuentas por cobrar</h2>
    </header>
    <div class="container">
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server" ID="sm">
            </asp:ScriptManager>
            <div id="botones" class="espacio row">

                <center><div class="col s6">
                    <asp:Button ID="btn_todos" CssClass="boton1" runat="server" Text=""  title="Reporte general" OnClick="btn_todos_Click" />
                </div></center>
                <center> <div class="col s6">
                    <asp:Button ID="btn_uno" CssClass="boton2" runat="server" Text="" title="Reporte individual" OnClick="btn_uno_Click" />
                </div></center>
            </div>
  <div class="input-field col s3 ">
                            <asp:TextBox ID="txtSearch" runat="server" title="Nombre"></asp:TextBox>
                            <label class="active" for="first_name2">Nombre:</label>
                        </div>
            <div id="busqueda" style="display: none" class="espacio">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                      <asp:Button ID="InvisButton" runat="server" Style="display: none;" OnClick="InvisButton_Click" />
                                <asp:GridView ID="Gridview_Venta" aligne="center" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" class="col s12"
                                    runat="server" AutoGenerateColumns="False" Height="174px" AllowPaging="true" PageSize="3" Width="100%" OnRowDataBound="Gridview_Venta_RowDataBound" OnSelectedIndexChanged="Gridview_Venta_SelectedIndexChanged" OnPageIndexChanging="Gridview_Venta_PageIndexChanging">
                                    <Columns>
                                <asp:BoundField DataField="cedulaPaciente" HeaderText="Cédula del paciente" ItemStyle-Width="100">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre1Paciente" HeaderText="Primer nombre" ItemStyle-Width="100">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre2Paciente" HeaderText="Segundo nombre" ItemStyle-Width="100">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="apellido1Paciente" HeaderText="primer apellido" ItemStyle-Width="100">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="apellido2Paciente" HeaderText="Segundo apellido" ItemStyle-Width="100">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                            </Columns>

                            <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PagerStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row espacio">
                    <div class="col s12"></div>
                    <div class="col s12"></div>
                    <div class="col s12"></div>
                    <div class="col s4"></div>
                    <div class="col s2">
                        <asp:Button ID="btn_descarga" CssClass="waves-effect waves-light btn" runat="server" Text="Descargar" OnClick="btn_descarga_Click" />
                    </div>
                    <div class="col s2">
                        <asp:Button ID="btn_volver" CssClass="waves-effect waves-light btn" runat="server" Text="Volver" OnClick="btn_volver_Click" />
                    </div>
                </div>
            </div>

            <script src="js/materialize.min.js"></script>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript" src="../js/quicksearch.js"></script>

            
        </form>
    </div>
  <script type="text/javascript">
         $(document).ready(function () {
             $('#<%=txtSearch.ClientID%>').bind('keyup', function () {
                $('#<%=InvisButton.ClientID%>').click();

             });
         });
    </script>
</body>
</html>
