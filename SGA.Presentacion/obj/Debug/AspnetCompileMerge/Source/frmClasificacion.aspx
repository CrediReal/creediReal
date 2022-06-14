<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmClasificacion.aspx.cs" Inherits="SGA.Presentacion.frmClasificacion" %>

<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

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
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
            </h2>
        </div>
        <div align="center">
            <cc1:PanelBase ID="panelContenido" runat="server">
                <asp:Panel ID="pnlBotones" runat="server" CssClass="pnlContenedor">
                    <cc1:BotonNuevo ID="BotonNuevo" runat="server" OnClick="BotonNuevo_Click" />
                </asp:Panel>
                <asp:Panel ID="pnlBusqueda" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvClasificacions" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="idClasificacion" HeaderText="C&#243;digo" ReadOnly="True" />
                            <asp:BoundField DataField="cClasificacion" HeaderText="Clasificacion" ReadOnly="True" />
                            <asp:CheckBoxField DataField="lVigente" HeaderText="Vigente" ReadOnly="True" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <cc1:BotonEditar ID="btnEditar" runat="server" CommandArgument='<%# Eval("idClasificacion") %>'
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
                                <cc1:TextBoxBase ID="txtCodigo" runat="server" Enabled="False" Height="16px" Width="142px"></cc1:TextBoxBase>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="lblClasificacion" runat="server" Font-Bold="True" Font-Strikeout="False">Clasificacion:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtClasificacion" runat="server" Enabled="False" Height="16px" Width="360px" Font-Bold="True"></cc1:TextBoxBase>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <cc1:CheckBoxBase ID="chcVigente" runat="server" Text="Vigente" Visible="True" />
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
