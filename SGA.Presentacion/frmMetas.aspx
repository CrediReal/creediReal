<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMetas.aspx.cs" Inherits="SGA.Presentacion.frmMetas" %>

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
                <asp:Panel ID="pnlBusqueda" runat="server">
                    <div>
                        <label>Año:</label>
                        <cc1:ComboBoxAnios ID="cboAniosBus" runat="server" nIni="2000" nFin="2050"/>
                        <label>Mes:</label>
                        <cc1:ComboBoxMeses ID="cboMesesBus" runat="server"/>
                        </div>
                    <div>
                        <label>Oficina:</label>
                        <cc1:ComboBoxOficina ID="cboOficinaBus" runat="server"/>
                        <label>Tipo meta:</label>
                        <cc1:ComboBoxTipoMeta ID="cboTipoMetaBus" runat="server"/>
                    </div>
                    <div>
                        <cc1:BotonBuscar ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" />
                    </div>

                </asp:Panel>
                <asp:Panel ID="pnlResultados" runat="server" CssClass="pnlContenedor">
                    <cc1:GridViewBase ID="grvMetas" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grvMetas_OnRowDataBound">
                        <Columns>
                            <asp:BoundField DataField="nAnio" HeaderText="Año" ReadOnly="True" />
                            <asp:BoundField DataField="cMes" HeaderText="Mes" ReadOnly="True" />
                            <asp:BoundField DataField="cNomOficina" HeaderText="Oficina" ReadOnly="True" />
                            <asp:BoundField DataField="cUsuario" HeaderText="Usuario" ReadOnly="True" />
                            <asp:BoundField DataField="cTipoMeta" HeaderText="Tipo Meta" ReadOnly="True" />
                            <asp:BoundField DataField="nValor" HeaderText="Valor" ReadOnly="True" />
                            <%--<asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <cc1:NumberBox ID="txtValor" runat="server"></cc1:NumberBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
                                <cc1:LabelBase ID="lblMeta" runat="server" Font-Bold="True" Font-Strikeout="False">Año:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxAnios ID="cboAnios" runat="server" nIni="2000" nFin="2050"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase1" runat="server" Font-Bold="True" Font-Strikeout="False">Mes:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxMeses ID="cboMeses" runat="server"></cc1:ComboBoxMeses>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase2" runat="server" Font-Bold="True" Font-Strikeout="False">Oficina:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxOficina ID="cboOficina" runat="server" 
                                    AutoPostBack="True"    
                                    OnSelectedIndexChanged="cboOficina_OnSelectedIndexChanged"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase3" runat="server" Font-Bold="True" Font-Strikeout="False">Usuario:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxUsuario ID="cboUsuario" runat="server" cPerfiles="0" AddTodos="True"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase4" runat="server" Font-Bold="True" Font-Strikeout="False">Tipo Meta:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:ComboBoxTipoMeta ID="cboTipoMeta" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <cc1:LabelBase ID="LabelBase5" runat="server" Font-Bold="True" Font-Strikeout="False">Valor:</cc1:LabelBase>
                            </td>
                            <td style="text-align: left">
                                <cc1:NumberBox ID="txtValor" runat="server" DecimalPlaces="4"/>
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
