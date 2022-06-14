<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmReiniciarPassword.aspx.cs" Inherits="SGA.Presentacion.frmReiniciarPassword" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Esta seguro de cambiar la contraseña?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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
                    <cc1:ListBoxBase ID="lstUsuarios" runat="server" Height="145px" Visible="false" Width="550px">
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
                    </td>
                <td></td>
            </tr>
            <tr>
                <td ></td>
                <td></td>
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
                                <cc1:ComboBoxBaseSexo ID="ComboBoxBaseSexo2" runat="server" Height="19px" Width="147px" Enabled="False">
                                </cc1:ComboBoxBaseSexo>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase1" runat="server">Apellido Paterno:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtApellidoPaterno" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase5" runat="server">Apellido Materno:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtApellidoMaterno" runat="server"  Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase2" runat="server">Primer Nombre:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombre1" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase4" runat="server">Segundo Nombre:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombre2" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="txtDNI" runat="server">DNI:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="NumberBox1" runat="server" Width="140px" Height="16px" MaxLength="8" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:CheckBoxBase ID="chcVigente" runat="server" Text="Vigente" Visible="False" />
                            </td>
                            <td style="text-align:left">
                                <asp:HiddenField ID="hTipoOperacion" runat="server" Visible="False" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="center" colspan="2"> 
                                <table style="text-align:center">
                                    <tr>
                                        <td>
                                            <cc1:BotonProcesarCierre ID="BotonProcesarCierre1" OnClick = "OnConfirm" OnClientClick="Confirm()"  runat="server" Text="Cambiar Contraseña" />
                                             </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                            <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                                        </td> 
                                    </tr>
                                </table>    
                                
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

         <asp:HiddenField ID="hPerfil" runat="server" />

    </form>
</body>
</html>

