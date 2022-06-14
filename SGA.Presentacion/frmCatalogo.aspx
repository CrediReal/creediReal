<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCatalogo.aspx.cs" Inherits="SGA.Presentacion.frmCatalogo" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>
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
                                <cc1:LabelBase ID="LabelBase12" runat="server">Producto:</cc1:LabelBase>
                            </td>
                            <td>
                                <cc1:TextBoxBase ID="txtNombreProducto" runat="server" Height="20px" Width="389px" MaxLength="50"></cc1:TextBoxBase>
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
                    <cc1:ListBoxBase ID="lstProductos" runat="server" Height="90px" Visible="false" Width="500px">
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
                    <cc1:BotonEditar ID="BotonEditar1" runat="server" OnClick="BotonEditar1_Click" Visible="False" />
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
                                <cc1:LabelBase ID="LabelBase6" runat="server">Código de Catálogo:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtCodigoCatalogo" runat="server" Enabled="False" Height="16px" Width="62px"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>                     
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="lblUsuario" runat="server">Nombre del Producto:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtProducto" runat="server" Height="16px" Width="360px" Font-Bold="True" Enabled="False"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                       
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase9" runat="server">Tipo de Bien:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboTipoBien" runat="server" Height="16px" Width="363px" Enabled="False">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr> 
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase7" runat="server">Grupo Activo:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TreeViewBase ID="trvGrupoActivo" runat="server" AutoPostBack="true" OnSelectedNodeChanged="trvGrupoActivo_SelectedNodeChanged" Enabled="False">
                                        </cc1:TreeViewBase>
                            </td>
                            <td></td>
                        </tr> 
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase11" runat="server">Código Externo(Barras):</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtCodigoExterno" runat="server" Enabled="False" Height="16px" Width="215px"></cc1:TextBoxBase>
                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase1" runat="server">Unidad de Compra:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboUniCompra" runat="server" Height="16px" Width="363px" Enabled="False">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase3" runat="server">Unidad de Almacenaje:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:ComboBoxBase ID="cboUniAlmacenaje" runat="server" Height="16px" Width="363px" Enabled="False">
                                </cc1:ComboBoxBase>
                            </td>
                            <td></td>
                        </tr>
                       
                        <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase2" runat="server">Valor de Conversión:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:NumberBox ID="txtValorConversion" CssClass="txtNumerico" runat="server" Width="150px" Height="16px" Enabled="False" DecimalPlaces="2">0.00</cc1:NumberBox>
                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase4" runat="server"></cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                            <cc1:CheckBoxBase ID="chcActivo" runat="server" Text="Es un Activo?" Enabled="False" />

                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase8" runat="server">Cantidad:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:NumberBox ID="txtCantidad" CssClass="txtNumerico" runat="server" Width="150px" Height="16px" Enabled="False" DecimalPlaces="2">0</cc1:NumberBox>
                            </td>
                            <td></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase10" runat="server">Precio Unitario:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:NumberBox ID="txtPrecioUnitario" CssClass="txtNumerico" runat="server" Width="150px" Height="16px" Enabled="False" DecimalPlaces="2">0.00</cc1:NumberBox>
                            </td>
                            <td></td>
                        </tr>
                      <tr>
                            <td></td>
                            <td style="text-align:left">
                                <cc1:LabelBase ID="LabelBase5" runat="server">Observaciones:</cc1:LabelBase>
                            </td>
                            <td style="text-align:left">
                                <cc1:TextBoxBase ID="txtObservaciones" runat="server" Height="48px" Width="360px" Font-Bold="True" Enabled="False" TextMode="MultiLine"></cc1:TextBoxBase>
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
                                <asp:HiddenField ID="hIdGrupo" runat="server" />
                                <asp:HiddenField ID="hIdCatalogo" runat="server" />
                            </td>
                            <td>&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align:center" colspan="2">       
                                &nbsp; &nbsp;
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
