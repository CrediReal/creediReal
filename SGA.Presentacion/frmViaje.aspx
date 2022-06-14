<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmViaje.aspx.cs" Inherits="SGA.Presentacion.frmViaje" %>

<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<%@ Register src="conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <cc1:PanelBase  ID="pnlBusqueda" runat="server" Visible="true">
            <table>
                 <tr>
                    <td ></td>
                    <td>
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
                </table>
            </cc1:PanelBase>
        <cc1:PanelBase  ID="panelContenido" runat="server">
        <table>
            <tr>
                <td ></td>
                <td>
                    &nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td align="center">
                    <asp:Panel ID="pnlDetalle" runat="server">
                    <table>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase6" runat="server">Número de Viaje:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNumViaje" runat="server" Height="16px" Width="62px" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>  
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase7" runat="server">Estado:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboEstado" runat="server" Height="16px" Width="120px" AutoPostBack="True" Enabled="False">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr> 
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase4" runat="server">Proveedor:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboProveedor" runat="server" Height="16px" Width="363px" Enabled="False">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr>                   
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblUsuario" runat="server">Destino:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtDestino" runat="server" Height="16px" Width="360px" Font-Bold="True" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>

                        
                      
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:CheckBoxBase ID="chcVigente" runat="server" Text="Vigente" Visible="False" />
                            </td>
                            <td style="text-align:left">
                                <asp:HiddenField ID="hTipoOperacion" runat="server" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:center" colspan="2">       
                                <cc1:BotonNuevo ID="BotonNuevo1" runat="server" OnClick="BotonNuevo1_Click"     />
                                <cc1:BotonEditar ID="BotonEditar1" runat="server" Visible="False" OnClick="BotonEditar1_Click"    />
                                <cc1:BotonGrabar ID="BotonGrabar1" runat="server"    Visible="False" OnClick="BotonGrabar1_Click" />
                                &nbsp;
                                <cc1:BotonCancelar ID="BotonCancelar1" runat="server" Visible="False" OnClick="BotonCancelar1_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>
                <td></td>
            </tr>
        </table>
    </cc1:PanelBase>
    </div>    
    </form>
</body>
</html>
