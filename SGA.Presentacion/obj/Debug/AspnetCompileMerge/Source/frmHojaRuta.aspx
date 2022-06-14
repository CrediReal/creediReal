<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmHojaRuta.aspx.cs" Inherits="SGA.Presentacion.frmHojaRuta" %>

<%@ Register TagPrefix="cc1" Namespace="SGA.Controles" Assembly="SGA.Controles" %>
<%@ Register Src="~/conBuscarCliente.ascx" TagPrefix="uc1" TagName="conBuscarCliente" %>

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
                <asp:Panel ID="pnlBotones" runat="server" CssClass="pnlContenedor">
                    <cc1:BotonNuevo ID="BotonNuevo" runat="server" OnClick="BotonNuevo_Click" />
                </asp:Panel>
                <asp:Panel ID="pnlResultados" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvHojaRutas" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="idHojaRuta" HeaderText="Codigo" ReadOnly="True" />
                            <asp:BoundField DataField="cNomOficina" HeaderText="Oficina" />
                            <asp:BoundField DataField="dFecIni" HeaderText="Fecha Inicio" />
                            <asp:BoundField DataField="dFecFin" HeaderText="Fecha Inicio"  />
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
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase1" runat="server" 
                                    Font-Bold="True" Font-Strikeout="False">Fecha Inicio:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtFecIni" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase2" runat="server" 
                                    Font-Bold="True" Font-Strikeout="False">Fecha Fin:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:TextBoxBase ID="txtFecFin" runat="server"/>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="1" style="text-align: left">
                                <div>
                                    <table>
                                        <tr>
                                            <td>Departamento:</td>
                                            <td>
                                                <cc1:ComboBoxDepartamento ID="cboDepartamento" runat="server"
                                                        AutoPostBack="true" 
                                                        OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Provincia:</td>
                                            <td>
                                                <cc1:ComboBoxProvincia ID="cboProvincia" runat="server"
                                                    AutoPostBack="true" 
                                                    OnSelectedIndexChanged="cboProvincia_SelectedIndexChanged"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Distrito:</td>
                                            <td>
                                                <cc1:ComboBoxDistrito ID="cboDistrito" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <cc1:BotonBuscar ID="btnBuscarCli" runat="server" OnClick="btnBuscarCli_Click" />
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </div>
                                <div>
                                    <cc1:BotonAgregarItem ID="btnAgregar" runat="server" OnClick="btnAgregar_Click"/>
                                </div>   
                                <cc1:GridViewBase ID="grvClientes" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <cc1:CheckBoxBase ID="chcSeleccion" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblIdCliBus" runat="server" Text='<%# Eval("idCliente") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombres">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblNombres" runat ="server" Text='<%# Eval("cNombres") %>'></cc1:LabelBase>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText ="Documento">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblDocumento" runat ="server" Text='<%# Eval("cDocumento") %>'></cc1:LabelBase>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText ="Direccion">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblDireccion" runat ="server" Text='<%# Eval("cDireccion") %>'></cc1:LabelBase>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Position="TopAndBottom" />
                                </cc1:GridViewBase>
                            </td>
                            <td>
                                <cc1:GridViewBase ID="grvDetalleHojaRuta" runat="server" AutoGenerateColumns="False"
                                       EmptyDataText="No se agregaron datos.">
                                    <Columns>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblIdInterno" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idDetHojaRuta" Visible="False" />
                                        <asp:TemplateField Visible ="false">
                                            <ItemTemplate>
                                                <cc1:LabelBase ID="lblCliente" runat ="server" Text='<%# Eval("idCli") %>'></cc1:LabelBase>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cNombres" HeaderText = "Nombres" />
                                        <asp:BoundField DataField="cDocumento" HeaderText ="Documento" />
                                        <asp:BoundField DataField="cDireccion" HeaderText="Direccion"/>
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
                            <td>
                                
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
