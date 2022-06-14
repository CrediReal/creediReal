<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCliente.aspx.cs" Inherits="SGA.Presentacion.frmCliente" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>
<%@ Register src="conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.js" type="text/javascript"></script> 
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.4/jquery-ui.min.js" type="text/javascript"></script> 
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.3/themes/base/jquery-ui.css" type="text/css" media="all" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script type="text/javascript">
        function handleKeyPress(e) {
            var key = e.keyCode || e.which;
            if (key == 13) {
                document.getElementById('BotonConsultar1').click();
            }

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
                    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
                    <table>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase7" runat="server">Tipo de Persona:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboTipoPersona" runat="server" Height="16px" Width="150px"  AutoPostBack="True" OnSelectedIndexChanged="cboTipoPersona_SelectedIndexChanged">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr> 
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblNombres" runat="server">Nombres:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombres" runat="server" Enabled="False" Height="16px"  Width="360px" MaxLength="350"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>                     
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblNombre" runat="server">Nombre:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtNombre" runat="server" Height="16px" Width="360px" Font-Bold="True" Enabled="False" MaxLength="50"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblApeParteno" runat="server">Apellido Paterno:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtApePaterno" runat="server" Height="16px" Width="360px" Font-Bold="True" Enabled="False" MaxLength="50"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblMaterno" runat="server">Apellido Materno:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtApeMaterno" runat="server" Height="16px" Width="360px" Font-Bold="True" Enabled="False" MaxLength="50"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase11" runat="server">Tipo de Documento:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboTipoDocumento" runat="server" Height="16px"  Width="150px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr> 
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase3" runat="server">Nro.Documento:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtDocumento" runat="server" Height="16px" Width="100px" Font-Bold="True" Enabled="False" Rows="15"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase10" runat="server">R.U.C.:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtdocumentoAdi" runat="server" Height="16px" Width="100px" Font-Bold="True" Enabled="False" MaxLength="15"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                       
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase1" runat="server">Dirección:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtDireccion" runat="server" Width="360px" Height="16px" MaxLength="300" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase12" runat="server">Departamento:</cc1:LabelBase>
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
                                <cc1:LabelBase ID="LabelBase13" runat="server">Provincia:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboProvincia" runat="server" AutoPostBack="True" Height="16px" Width="363px" OnSelectedIndexChanged="cboProvincia_SelectedIndexChanged">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase14" runat="server">Distrito:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboDistrito" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase8" runat="server">Dirección Adicional:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtDireccionAdi" runat="server" Width="360px" Height="16px" MaxLength="300" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase16" runat="server">Departamento:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboDepartamentoAdi" runat="server" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="cboDepartamentoAdi_SelectedIndexChanged" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase17" runat="server">Provincia:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboProvinciaAdi" runat="server" AutoPostBack="True" Height="16px" Width="363px" OnSelectedIndexChanged="cboProvinciaAdi_SelectedIndexChanged">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase18" runat="server">Distrito:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboDistritoAdi" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblFechaNac" runat="server">Fecha de Nacimiento:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:CalendarioBase ID="calFechaNac" runat="server" />
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase5" runat="server">Teléfono:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtTelefonos" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase9" runat="server">Teléfonos Adicionales:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtTelefonosAdi" runat="server" Width="360px" Height="16px" MaxLength="100" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase2" runat="server">E-mail:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtEmail" runat="server" Width="360px" Height="16px" MaxLength="150" Enabled="False"></cc1:TextBoxBase>
                             
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase6" runat="server">E-mail Adicional:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtEmailAdi" runat="server" Width="360px" Height="16px" MaxLength="150" Enabled="False"></cc1:TextBoxBase>
                             
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase4" runat="server">Contacto:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtContacto" runat="server" Width="360px" Height="16px" MaxLength="300" Enabled="False"></cc1:TextBoxBase>
                             
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase20" runat="server">Cargo Contacto:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtCargo" runat="server" Width="360px" Height="16px" MaxLength="200" Enabled="False"></cc1:TextBoxBase>
                             
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase15" runat="server">Asesor asignado:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboAsesor" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                             <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase19" runat="server">Oficina asignada:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboOficina" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        
                         <tr>
                            <td>&nbsp;</td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase21" runat="server">Tipo:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboTipo" runat="server" Height="16px" Width="363px">
                                </cc1:ComboBoxBase>
                            </td>
                            <td>&nbsp;</td>
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
                       
                    </table>
                </asp:Panel>
                    <table>
                    <tr>
                            <td></td>
                            <td style="text-align:center">       
                                <cc1:BotonNuevo ID="BotonNuevo1" runat="server" OnClick="BotonNuevo1_Click"  />
                                <cc1:BotonEditar ID="BotonEditar1" runat="server" OnClick="BotonEditar1_Click" />
                                <cc1:BotonGrabar ID="BotonGrabar1" runat="server" Visible="False" OnClick="BotonGrabar1_Click" />
                                &nbsp;
                                <cc1:BotonCancelar ID="BotonCancelar1" runat="server"  Visible="False" OnClick="BotonCancelar1_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>
    </cc1:PanelBase>
    </div>    
    </form>
</body>
</html>
