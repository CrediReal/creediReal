<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistroVisita.aspx.cs" Inherits="SGA.Presentacion.frmRegistroVisita" %>

<%@ Register TagPrefix="cc1" Namespace="SGA.Controles" Assembly="SGA.Controles" %>
<%@ Register Src="~/conBuscarCliente.ascx" TagPrefix="uc1" TagName="conBuscarCliente" %>
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
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
            </h2>
        </div>
        <div align="center">
            <cc1:PanelBase ID="panelContenido" runat="server">
                <asp:Panel ID="pnlResultados" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvHojaRutas" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="idHojaRuta" HeaderText="Codigo" ReadOnly="True" />
                            <asp:BoundField DataField="cNomOficina" HeaderText="Oficina" />
                            <asp:BoundField DataField="dFecIni" HeaderText="Fecha Inicio" />
                            <asp:BoundField DataField="dFecFin" HeaderText="Fecha Inicio" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <cc1:BotonEditar ID="btnEditar" runat="server" CommandArgument='<%# Eval("idHojaRuta") %>'
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
                <asp:Panel ID="pnlDetalle" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvDetalleHojaRuta" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="No se agregaron datos.">
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <cc1:LabelBase ID="lblIdInterno" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="idDetHojaRuta" Visible="False" />
                            <asp:BoundField DataField="idCli" Visible="False" />
                            <asp:BoundField DataField="cNombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="cDocumento" HeaderText="Documento" />
                            <asp:BoundField DataField="cDireccion" HeaderText="Direccion" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <cc1:BotonEditar ID="btnEditarDetalle" runat="server" CommandArgument='<%# Eval("idDetHojaRuta") %>'
                                        OnClick="btnEditarDetalle_Click" />
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
                                <cc1:TextBoxBase ID="txtCodigo" runat="server" Enabled="False" Height="16px" Width="142px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase4" runat="server">Cliente:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <uc1:ConBusCli ID="ConBusCli" runat="server" OcultarBusqueda ="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase1" runat="server">Tipo contacto:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left; font-weight: 700;">
                                <cc1:ComboBoxTipoContacto ID="cboTipoContacto" runat="server" Width="150"></cc1:ComboBoxTipoContacto>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase7" runat="server">Resultado:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left; font-weight: 700;">
                                <cc1:ComboBoxTipoContacto ID="cboResultado" runat="server" Width="300">
                                    <asp:ListItem Value="1">PROGRAMADO</asp:ListItem>
                                    <asp:ListItem Value="2">VISITADO</asp:ListItem>
                                    <asp:ListItem Value="3">NO CONCRETADO</asp:ListItem>
                                </cc1:ComboBoxTipoContacto>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase3" runat="server">Fecha visita:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtFecVisita" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase2" runat="server">Comentario:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtComentario" runat="server" TextMode="MultiLine" Width="500" Height="60"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="2" style="text-align: left">
                                <cc1:CheckBoxBase ID="chcProxVisita" runat="server" 
                                    Text ="Proxima visita?"
                                    AutoPostBack="true"
                                    OnCheckedChanged="chcProxVisita_CheckedChanged"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase5" runat="server">Fecha y hora de prox visita:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtFecHoraProxVisita" runat="server" Enabled ="false" />
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
