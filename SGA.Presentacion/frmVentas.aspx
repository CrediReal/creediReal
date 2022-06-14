<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVentas.aspx.cs" Inherits="SGA.Presentacion.frmVentas" %>

<%@ Register TagPrefix="cc1" Namespace="SGA.Controles" Assembly="SGA.Controles" %>
<%@ Register Src="~/conBuscarCliente.ascx" TagPrefix="uc1" TagName="conBuscarCliente" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.js"></script>
    <script src="Scripts/myScripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
            </h2>
        </div>
        <div align="center">
            <cc1:PanelBase ID="panelContenido" runat="server">
                <asp:Panel ID="pnlBotones" runat="server" CssClass="pnlContenedor">
                    <cc1:BotonNuevo ID="BotonNuevo" runat="server" OnClick="BotonNuevo_Click" />
                </asp:Panel>
                <asp:Panel ID="pnlBusqueda" runat="server">
                    <div>
                        <label>Oficina:</label>
                        <cc1:ComboBoxOficina ID="cboOficinaBus" runat="server" />
                    </div>
                    <div>
                        <cc1:BotonBuscar ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" />
                    </div>

                </asp:Panel>
                <asp:Panel ID="pnlResultados" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvVentas" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="idVenta" HeaderText="Codigo" ReadOnly="True" />
                            <asp:BoundField DataField="cNombres" HeaderText="Cliente" ReadOnly="True" />
                            <asp:BoundField DataField="cNomOficina" HeaderText="Oficina" ReadOnly="True" />
                            <asp:BoundField DataField="nTotalVenta" HeaderText="Total" ReadOnly="True" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <cc1:BotonEditar ID="btnEditar" runat="server" CommandArgument='<%# Eval("idVenta") %>'
                                        OnClick="BotonEditar_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Position="TopAndBottom" />
                    </cc1:GridViewBase>
                </asp:Panel>
                <asp:Panel ID="pnlForm" runat="server" CssClass="pnlContenedor">
                    <table>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase6" runat="server">Código:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtCodigo" runat="server" Enabled="False" Height="16px" Width="142px"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <uc1:conBuscarCliente runat="server" ID="conBuscarCliente" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase1" runat="server" Font-Bold="True" Font-Strikeout="False">Oficina:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxOficina ID="cboOficina" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <cc1:BotonAgregar ID="BotonAgregar" runat="server" OnClick="BotonAgregar_OnClick"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <cc1:GridViewBase ID="grvDetalleVenta" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="true"
                                       OnRowDataBound="grvDetVentas_OnRowDataBound">
                                    <Columns>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblIdInterno" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idDetVenta" Visible="False" />
                                        <asp:TemplateField HeaderText="Marca">
                                            <ItemTemplate>
                                                <cc1:ComboBoxBaseMarca ID="cboMarca" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modelo">
                                            <ItemTemplate>
                                                <cc1:ComboBoxModelo ID="cboModelo" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <cc1:NumberBox ID="txtCantidad" runat="server" Width="50px" 
                                                    CssClass="txtNumeric"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <cc1:NumberBox ID="txtPrecio" runat="server" Width="50px"
                                                    CssClass="txtNumeric" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <cc1:NumberBox ID="txtTotal" runat="server" Enabled="False" Width="50px"/>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <cc1:NumberBox ID="txtTotVenta" runat="server" Enabled="False" Width="50px"/>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <cc1:BotonQuitar ID="btnQuitarDetalle" runat="server" CommandArgument='<%# Eval("idInterno") %>'
                                                    OnClick="btnQuitarDetalle_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Position="TopAndBottom" />
                                </cc1:GridViewBase>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="2">
                                <cc1:BotonGrabar ID="BotonGrabar" runat="server" OnClick="BotonGrabar_Click" />
                                &nbsp;
                                <cc1:BotonCancelar ID="BotonCancelar" runat="server" OnClick="BotonCancelar_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </cc1:PanelBase>
        </div>
    </form>
</body>
</html>
