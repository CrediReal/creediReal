<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="SGA.Presentacion.frmUsuario" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

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
                <td></td>
                <td>&nbsp; &nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <table>
                       
                        <tr>
                            <td>
                                <cc1:LabelBase ID="LabelBase7" runat="server">Usuario:</cc1:LabelBase>
                            </td>
                            <td>
                                <cc1:TextBoxBase ID="txtNombreUsuario" runat="server" Height="20px" Width="389px" MaxLength="50"></cc1:TextBoxBase>
                            </td>
                            <td>
                                <asp:Button ID="BotonConsultar1" runat="server" Text="Buscar" OnClick="BotonConsultar1_Click" CssClass="btn_Consultar"/>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <cc1:ListBoxBase ID="lstUsuarios" runat="server" Height="90px" Visible="false" Width="500px">
                    </cc1:ListBoxBase>
                </td>                
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center">
                    <cc1:BotonEditar ID="BotonEditar1" runat="server" OnClick="BotonEditar1_Click" Visible="false" />
                    &nbsp;
                    <cc1:BotonNuevo ID="BotonNuevo1" runat="server" OnClick="BotonNuevo1_Click" />
                </td>
                <td></td>
            </tr>
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
                                <cc1:LabelBase ID="LabelBase6" runat="server">Código:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtCodigo" runat="server" Enabled="False" Height="16px" Width="142px"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>                     
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblUsuario" runat="server" Font-Bold="True" Font-Strikeout="False">USUARIO:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False" Height="16px" Width="360px" Font-Bold="True"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase3" runat="server">Sexo:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="ComboBoxBase2" runat="server" Height="16px" Width="147px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase1" runat="server">Apellido Paterno:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtApellidoPaterno" runat="server" Width="360px" Height="16px" MaxLength="100"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase5" runat="server">Apellido Materno:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtApellidoMaterno" runat="server" Width="360px" Height="16px" MaxLength="100"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase2" runat="server">Primer Nombre:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombre1" runat="server" Width="360px" Height="16px" MaxLength="100"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase4" runat="server">Segundo Nombre:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombre2" runat="server" Width="360px" Height="16px" MaxLength="100"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="txtDNI" runat="server">DNI:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="NumberBox1" runat="server" Height="16px" MaxLength="8" Width="138px"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase9" runat="server">Perfil:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboPerfil" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr>                        
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase10" runat="server">Departamento:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboDepartamento" runat="server" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase11" runat="server">Provincia:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboProvincia" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:CheckBoxBase ID="chcVigente" runat="server" Text="Vigente" Visible="True" />
                            </td>
                            <td style="text-align:left">
                                <asp:HiddenField ID="hTipoOperacion" runat="server" Visible="False" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:center" colspan="2">       
                                <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" />
                                &nbsp;
                                <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
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
