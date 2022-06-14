<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCambioEstadoSol.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmCambioEstadoSol" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    
    <script src="../Scripts/jquery-1.4.2.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script type="text/javascript">
        function handleKeyPress(e) {
            var key = e.keyCode || e.which;
            if (key == 13) {
                document.getElementById('BotonConsultar1').click();
            }

        }
    </script>
    <style type="text/css">
        .auto-style3 {
            text-align: left;
        }
        .auto-style4 {
            height: 26px;
            text-align: left;
        }
        .auto-style5 {
            height: 10px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
            </h2>
        </div>
        <div align="center">
           <table>
                 <tr>
                    <td ></td>
                    <td>
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                        <asp:HiddenField ID="hdIdSolicitud" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                   <cc1:PanelBase ID="pnlSolicitud" runat="server" Visible="false">
                    <table>
                             <tr class="lblBase">
                                <td class="auto-style4" >Fecha de registro:</td>
                                <td class="auto-style4">
                                    <cc1:CalendarioBase ID="dtpFechaSol" runat="server" Enabled="False" />
                                 </td>
                                 <td class="auto-style4">Asesor:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboAsesor" runat="server"></cc1:ComboBoxBase></td>
                            </tr>

                         <tr class="lblBase">
                                <td class="auto-style3" >Estado Solicitud:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboEstado" runat="server" Enabled="False"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >Tipo Crédito</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboTipoCre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoCre_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>

                              <tr class="lblBase">
                                <td class="auto-style3" >Monto Capital:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMonto" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox></td>
                                 <td class="auto-style3" >Sub Tipo:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboSubTipoCre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSubTipoCre_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>
                            <tr class="lblBase">
                                <td class="auto-style3" >Moneda:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboMoneda" runat="server" Enabled="False"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >Producto:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboProducto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboProducto_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>

                             <tr class="lblBase">
                                <td class="auto-style3" >Nro. Cuotas:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtCuotas" runat="server"></cc1:NumberBox></td>
                                 <td class="auto-style3" >Sub Producto:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboSubProducto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSubProducto_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>
                             <tr class="lblBase">
                                <td class="auto-style4" >Días de gracia:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtDiasGracia" runat="server" Enabled="False"></cc1:NumberBox></td/>
                                 <td class="auto-style4" >Tipo periodo:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboPeriodo" runat="server" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>

                        <tr class="lblBase">
                                <td class="auto-style3" >Frecuencia:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtFrecuencia" runat="server"></cc1:NumberBox>
                                    </td>
                                 <td class="auto-style3" >:Tipo Cálculo:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboTipoCalculo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoCalculo_SelectedIndexChanged" Enabled="False">
                                    </cc1:ComboBoxBase>
                                </td>
                            </tr>
                     
                         <tr class="lblBase">
                                <td class="auto-style3" >Fecha desembolso:</td>
                                <td class="auto-style3">
                                    <cc1:CalendarioBase ID="dtpFechaDesembolso" runat="server" Enabled="False" />
                                </td>
                                 <td class="auto-style3" >Tasa de Interés</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtTasaInteres" runat="server" DecimalPlaces="4" Width="77px" Enabled="False"></cc1:NumberBox>
                                    <cc1:CheckBoxBase ID="chTasaEspecial" runat="server" Text="¿Tasa especial?" AutoPostBack="True" OnCheckedChanged="chTasaEspecial_CheckedChanged" />
                                </td>
                            </tr>
                          <tr class="lblBase">
                                <td class="auto-style3" >Forma de desembolso:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboModDesemb1" runat="server">
                                    </cc1:ComboBoxBase>
                                </td>
                                 <td class="auto-style3" >Tasa Moratoria:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtTasaMora" runat="server" DecimalPlaces="2" Width="75px" Enabled="False"></cc1:NumberBox></td>
                            </tr>
                        <tr class="lblBase">
                                <td class="auto-style3" >Observaciones:</td>
                                <td colspan="3" class="auto-style3">
                                    <cc1:TextBoxBase ID="txtObservacion" runat="server" Height="61px" TextMode="MultiLine" Width="480px" Enabled="False"></cc1:TextBoxBase>
                                </td>
                                    
                            </tr>
                        
                         </table>
                     </cc1:PanelBase>
                        <table>
                            <tr class="lblBase">
                                <td class="auto-style5" ><br />Cambiar estado a:&nbsp;&nbsp;&nbsp;
                                    
                                               <cc1:ComboBoxBase ID="cboNuevoEstado" runat="server"></cc1:ComboBoxBase>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            </table>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" />&nbsp;<cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
         <asp:HiddenField ID="hPerfil" runat="server" />
    </form>
</body>
</html>