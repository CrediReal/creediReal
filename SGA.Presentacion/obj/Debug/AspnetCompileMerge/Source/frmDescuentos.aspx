<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDescuentos.aspx.cs" Inherits="SGA.Presentacion.frmDescuentos" %>

<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>
<%@ Register Src="~/ConBusCli.ascx" TagPrefix="uc1" TagName="ConBusCli" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <cc1:labelbase id="lblOpcion" runat="server">titulo:</cc1:labelbase>
            </h2>
        </div>
        <div align="center">
            <cc1:panelbase id="panelContenido" runat="server">
                <asp:Panel ID="pnlBusCli" runat="server" CssClass="pnlContenedor">
                    <uc1:ConBusCli runat="server" ID="ConBusCli" OnClienteChanged="ConBusCli_ClienteChanged"/>
                </asp:Panel>
                <asp:Panel ID="pnlBusqueda" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvDescuentos" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="idDescuentoCli" HeaderText="C&#243;digo" ReadOnly="True" />
                            <asp:BoundField DataField="nDescuento" HeaderText="Descuento" ReadOnly="True" />
                            <asp:BoundField DataField="nMaxDescuento" HeaderText="Descuento Máximo" ReadOnly="True" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <cc1:BotonEditar ID="btnEditar" runat="server" CommandArgument='<%# Eval("idDescuentoCli") %>'
                                        OnClick="BotonEditar_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Position="TopAndBottom" />
                    </cc1:GridViewBase>
                </asp:Panel>
                <asp:Panel ID="pnlBotones" runat="server" CssClass="pnlContenedor">
                    <cc1:BotonNuevo ID="BotonNuevo" runat="server" OnClick="BotonNuevo_Click" />
                </asp:Panel>
                <asp:Panel ID="pnlForm" runat="server" CssClass="pnlContenedor">
                    <table>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase6" runat="server">Código:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtCodigo" runat="server" Enabled="False" Height="16px" Width="142px"></cc1:TextBoxBase>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase2" runat="server" Font-Bold="True" Font-Strikeout="False">Clasificacion:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxClasificacion ID="cboClasificacion" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="lblClasificacion" runat="server" Font-Bold="True" Font-Strikeout="False">Descuento:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtDescuento" runat="server" Enabled="False" Height="16px" Width="360px" Font-Bold="True"></cc1:TextBoxBase>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase1" runat="server" Font-Bold="True" Font-Strikeout="False">Max. Descuento:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtMaxDescuento" runat="server" Enabled="False" Height="16px" Width="360px" Font-Bold="True"></cc1:TextBoxBase>
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
            </cc1:panelbase>
        </div>
    </form>
</body>
</html>
