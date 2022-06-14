<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInicio.aspx.cs" Inherits="SGA.Presentacion.frmInicio" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Software de Gestión Crediticia</title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
     <script type="text/javascript">
         function focoInicio() {
             var txt = document.getElementById('TxtUsuario');
             txt.focus();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server" onfocus="focoInicio()">
        <br />
        <br />
        <div align="center">
            <table align="center">
                <tr>
                    <td align="center">
                        <h2 style="font-family:Ebrima; color:#EE1D23">SISTEMA DE GESTIÓN CREDITICIA</h2>
                        <h1 style="font-family:Ebrima; color:#EE1D23; background-color:yellow">PRUEBAS</h1>
                        <p style="font-family:Ebrima; color:#EE1D23">&nbsp;</p>
                    </td>
                </tr>
            </table>
	    </div>
        <div>
            <table id="Table1"  cellspacing="0" cellpadding="0" width="495" align="center" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="10" border="0">
                        <tr>
                            <td>                        
                                <table cellpadding="0" width="495" border="0">
                                    <tr>
                                        <td height="2px"></td>
                                    </tr>
                                    <tr height="60">
								        <td class="txtAzulita_sp" colspan="3" align="center">
                                            <img src="Imagenes/logo.png" align="center" width="330" height="100"/>
								        </td>
								        <td valign="top"></td>
							        </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="20px"></td>
                        </tr>
                  </table>
                </td>
                <td>
                </td>
            </tr>
            </table>       
        </div>
        <div align="center">
            <table>                            
                <tr>
                    <td valign="top" align="center" colspan="4">
                        <cc1:PanelBase  ID="pnlLogin" runat="server" BackColor="White" Width="300">          
                            <table>
                            <tr>
				                <td class="auto-style2"></td>
				                <td  align="right">
                                    <cc1:LabelBase ID="LabelBase1" runat="server" Text="Usuario:"></cc1:LabelBase>
                                    

				                </td>
				                <td>
                                    <asp:TextBox ID="TxtUsuario" runat="server" CssClass="txt" tabIndex="1" Width="120px"></asp:TextBox>
                                </td>								
			                </tr>
			                <tr>
				                <td class="auto-style2" ></td>
				                <td align="right">
                                    <cc1:LabelBase ID="LabelBase2" runat="server" Text="Contraseña:"></cc1:LabelBase></td>
				                <td>
                                    <asp:TextBox ID="TxtPassword" runat="server" CssClass="txt" tabIndex="2" TextMode="Password" Width="120px"></asp:TextBox>
                                </td>								
			                </tr>
			                <tr>
				                <td class="style2"></td>
				                <td class="auto-style2" >
                                    &nbsp;</td>
				                <td>
                                    <asp:Button ID="btnIngresar" runat="server" CssClass="btnBase_Aceptar" onclick="btnIngresar_Click" Text="Ingresar &gt;&gt;" />
                                    <asp:LinkButton ID="lnkCambioClave" runat="server" Visible="False">Cambiar Clave</asp:LinkButton>
                                </td>
			                </tr>
                        </table>
                        </cc1:PanelBase>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
					<td valign="top" align="center" colspan="4">
                        <div id="ABC">
                        <cc1:PanelBase ID="pnlDatosUsuario" runat="server" Width="400" BackColor="White" Visible="False">
                            <table>
                                <tr>
                                    <td class="auto-style1"></td>                        
                                    <td class="auto-style6"></td>                        
                                    <td></td>
                                    <td class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <cc1:LabelBase ID="LabelBase8" runat="server">
                                            Seleccione el Perfil:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:ComboBoxBase ID="cboPerfil" runat="server" Height="19px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="cboPerfil_SelectedIndexChanged">
                                        </cc1:ComboBoxBase>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>                        
                                    <td colspan="2" style="text-align:left">
                                        <cc1:LabelBase ID="lblMensajeError" runat="server" ForeColor="Red" Visible="False">
                                            *</cc1:LabelBase>
                                    </td>
                                    <td></td>
                                </tr>
                                    <tr>
                                    <td></td>                        
                                    <td align="center" colspan="2">
                                        <cc1:LabelBase ID="lblOficina" runat="server" Text="Oficina">
                                           </cc1:LabelBase>
                                    </td>
                                </tr>
                                    <tr>
                                    <td></td>                        
                                    <td align="center" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>                        
                                    <td>
                                        <cc1:BotonAceptar ID="BotonAceptar" runat="server" OnClick="BotonAceptar_Click" />
                                    </td>                        
                                    <td style="text-align:right">
                                        <cc1:BotonCancelar ID="BotonCancelar" runat="server" OnClick="BotonCancelar_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>                        
                                    <td class="auto-style6"></td>                        
                                    <td>
                                        &nbsp;</td>
                                    <td></td>
                                </tr>
                            </table>
                        </cc1:PanelBase>
                        </div>
					</td>
				</tr>
                <tr>
                    <td valign="top" align="center" colspan="4">
                        <cc1:PanelBase ID="pnlCambioContrasenia" runat="server" Width="500" BackColor="White" Visible="False">
                            <table>
                                <tr>
                                    <td>&nbsp; &nbsp;</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <cc1:LabelBase ID="LabelBase13" runat="server" Font-Bold="True">CAMBIO DE CONTRASEÑA</cc1:LabelBase>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <cc1:LabelBase ID="LabelBase14" runat="server">Usuario:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:TextBoxBase ID="txtClaveAnterior" runat="server" Width="143px"></cc1:TextBoxBase>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <cc1:LabelBase ID="LabelBase11" runat="server">Ingrese su nueva Clave:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:TextBoxBase ID="txtNuevaClave" runat="server" Width="143px" TextMode="Password"></cc1:TextBoxBase>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <cc1:LabelBase ID="LabelBase12" runat="server">Repita su nueva clave:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:TextBoxBase ID="txtNuevaClave1" runat="server" Width="143px" TextMode="Password"></cc1:TextBoxBase>
                                    </td>
                                </tr>                                            
                                <tr>
                                    <td colspan="2">
                                        <cc1:BotonAceptar ID="BotonAceptar0" runat="server" OnClick="BotonAceptar0_Click" />
                                        &nbsp;
                                        <cc1:BotonCancelar ID="BotonCancelar0" runat="server" OnClick="BotonCancelar0_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:HiddenField ID="hClaveAnterior" runat="server" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </cc1:PanelBase>
                    </td>
                </tr>
            </table>
	    </div>
    </form>
    <footer>
        <!--<img src="Images/linea.png" width="100%" />-->
        <div style="font-size:9px; font-family:Arial" align="center">
           Desarrollado por Fast Solutions SAC<br />
                 © <%= DateTime.Now.Year.ToString() %>| Todos los Derechos Reservados<br />
                Sistema de Gestión Crediticia
        </div>
    </footer>
</body>
</html>
