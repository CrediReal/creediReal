<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmAlmacen.aspx.cs" Inherits="SGA.Presentacion.frmAlmacen" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<%@ Register src="conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                                <cc1:LabelBase ID="LabelBase7" runat="server">Almacén:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboAlmacen" runat="server" Height="16px" Width="363px" OnSelectedIndexChanged="cboAlmacen_SelectedIndexChanged" AutoPostBack="True">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr> 
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase6" runat="server">Código de almacén:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtCodigoAlmacen" runat="server" Enabled="False" Height="16px" Width="62px"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>                     
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblUsuario" runat="server">Nombre de almacén:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombreAlmacen" runat="server" Height="16px" Width="360px" Font-Bold="True" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase3" runat="server">Código Externo:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtCodExterno" runat="server" Height="16px" Width="70px" Font-Bold="True" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                       
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase1" runat="server">Dirección:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtDireccion" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase5" runat="server">Referencia dirección:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtRefDirecc" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase2" runat="server">Teléfono almacén:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtTelefono" runat="server" Width="150px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        
                      
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase9" runat="server">Responsable almacén:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboResponsable" runat="server" Height="16px" Width="363px" Enabled="False">
                                </cc1:ComboBoxBase>
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
                                <cc1:BotonNuevo ID="BotonNuevo1" runat="server" OnClick="BotonNuevo1_Click" />
                                <cc1:BotonEditar ID="BotonEditar1" runat="server" OnClick="BotonEditar1_Click" />
                                <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" Visible="False" />
                                &nbsp;
                                <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" Visible="False" />
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
