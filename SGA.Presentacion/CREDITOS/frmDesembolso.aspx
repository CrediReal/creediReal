<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmDesembolso.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmDesembolso" %>
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
                </tr> <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                        <asp:HiddenField ID="hIdCuenta" runat="server" />
                        <asp:HiddenField ID="hIdKardex" runat="server" />
                        <asp:HiddenField ID="hIdSolicitud" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:PanelBase ID="pnlDesembolso" runat="server" Visible="false">  
                    <table >
                        <tr class="lblBase">
                                <td class="auto-style3" >Fecha desembolso:</td>
                                <td class="auto-style3">
                                    <cc1:CalendarioBase ID="dtpFechaSol" runat="server" />
                                    </td>
                                 <td class="auto-style3" >Nro. Cuotas:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtNroCuotas" runat="server"></cc1:NumberBox></td>
                            </tr>
                              <tr class="lblBase">
                                <td class="auto-style3" >Monto Capital:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMontoCapital" runat="server" DecimalPlaces="2"></cc1:NumberBox></td>
                                 <td class="auto-style3" >Monto 1ra cuota:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMonPriCuo" runat="server"></cc1:NumberBox></td>
                            </tr>
                            <tr class="lblBase">
                                <td class="auto-style3" >Moneda:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboMoneda" runat="server"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >Fecha de Pago de 1ra Cuota::</td>
                                <td class="auto-style3">
                                    <cc1:CalendarioBase ID="dtpFecPriCuo" runat="server" />
                                    </td>
                            </tr>

                             <tr class="lblBase">
                                <td class="auto-style3" >Total entrega:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtTotEntrega" runat="server" DecimalPlaces="2"></cc1:NumberBox></td>
                                 <td class="auto-style3" >Modalidad de Pago:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboModDesemb1" runat="server"></cc1:ComboBoxBase></td>
                            </tr>
                        <tr class="lblBase">
                                <td class="auto-style3" >Observaciones:</td>
                                <td colspan="3" class="auto-style3">
                                    <cc1:GridViewBase ID="dtgGasto" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="idTipoGasto" Visible="false"/>
                                            <asp:BoundField DataField="cTipoCalculo" Visible="false"/>
                                            <asp:BoundField DataField="cDesTipoCal" Visible="false"/>
                                            <asp:BoundField DataField="nFactor" Visible="false"/>
                                            <asp:BoundField DataField="cGasto"  HeaderText="Concepto"/>
                                            <asp:BoundField DataField="nMonAplica"  HeaderText="Monto"/>
                                        </Columns>

                                    </cc1:GridViewBase>
                                    <cc1:TextBoxBase ID="txtObservacion" runat="server" Height="61px" TextMode="MultiLine" Width="225px"></cc1:TextBoxBase>
                                </td>
                                    
                            </tr>
                         </table>
                    </cc1:PanelBase>
                    
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" Visible="False" />
&nbsp;<cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" Visible="False" />&nbsp;<cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
        
 <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>