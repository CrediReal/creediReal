<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmRegistroSolicitudCredito.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmRegistroSolicitudCredito" %>
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
                        <br />
                        <cc1:BotonConsultar ID="BotonConsultar1" OnClick="BotonConsultar1_Click" runat="server" />
                        <asp:HiddenField ID="Transaccion" runat="server" Value="X" />
                        <asp:HiddenField ID="hIdSolciitud" runat="server" />
                        <br />
                    </td>
                    <td></td>
                </tr>
                    <td ></td>
                    <td>
                        <cc1:GridViewBase ID="dtgCreditos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="dtgCreditos_SelectedIndexChanged">

                            <Columns>
                                <asp:BoundField DataField="IdNum" HeaderText="Cuenta"></asp:BoundField>
                                <asp:BoundField DataField="Fecha_Desembolso" HeaderText="Desembolso"></asp:BoundField>
                                <asp:BoundField DataField="cProducto" HeaderText="Producto"></asp:BoundField>
                                <asp:BoundField DataField="nMonto" HeaderText="Capital"></asp:BoundField>
                                <asp:BoundField DataField="Monto_Cuota" HeaderText="Cuota"></asp:BoundField>
                                <asp:BoundField AccessibleHeaderText="SaldoCap" DataField="nSalCap" HeaderText="Sal.Capital" />
                                <asp:BoundField DataField="nSalInt" HeaderText="Sal.Interes" />
                                <asp:BoundField DataField="nSalMor" HeaderText="Sal.Mora" />
                                <asp:BoundField DataField="nAtraso" HeaderText="Atraso"></asp:BoundField>
                                <asp:BoundField DataField="Frecuencia" HeaderText="Frecuencia" />
                                <asp:TemplateField HeaderText="Selec.">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate >
									    <asp:CheckBox id="P" runat="server" CommandArgument="Seleccionar" CommandName="select" 
                                            ToolTip="Seleccionar cuenta" CausesValidation="false" Text="Sel." AutoPostBack="True" 
                                            OnCheckedChanged="P_CheckedChanged" />
                                        <%--%# Convert.ToBoolean(Eval("tuCampoTipoBit")) %--%>
                                        <%--<asp:LinkButton id="P" runat="server" CommandArgument="Seleccionar" CommandName="select" ToolTip="Seleccionar cuenta" CausesValidation="false" Text="Seleccionar">Seleccionar</asp:LinkButton>--%>
									</ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>

<PagerSettings Position="TopAndBottom"></PagerSettings>
                        </cc1:GridViewBase>
                    </td>
                    <td></td>
                <tr>    
                    <td ></td>
                    <td>
                     <cc1:PanelBase runat="server" ID="pnlSolicitud" Visible="false"> 
                    <table>
                             <tr class="lblBase">
                                <td class="auto-style4" >Operación:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboOperacion" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="cboOperacion_SelectedIndexChanged"></cc1:ComboBoxBase></td>
                                 <td class="auto-style4">Tipo Crédito</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboTipoCre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoCre_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>
                              <tr class="lblBase">
                                <td class="auto-style3" >Monto Capital:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMontoCapital" runat="server" DecimalPlaces="2" AutoPostBack="True" OnTextChanged="txtMontoCapital_TextChanged"></cc1:NumberBox></td>
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
                                    <cc1:NumberBox ID="txtCuotas" runat="server" AutoPostBack="True" OnTextChanged="txtCuotas_TextChanged">30</cc1:NumberBox></td>
                                 <td class="auto-style3" >Sub Producto:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboSubProducto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSubProducto_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>
                             <tr class="lblBase">
                                <td class="auto-style3" >Días de gracia:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtDiasGracia" runat="server"></cc1:NumberBox></td/>
                                 <td class="auto-style3" >Tipo periodo:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboPeriodo" runat="server" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>

                        <tr class="lblBase">
                                <td class="auto-style3" >Fecha desembolso:</td>
                                <td class="auto-style3">
                                    <cc1:CalendarioBase ID="dtpFechaDesembolso" runat="server" />
                                    </td>
                                 <td class="auto-style3" >Frecuencia:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboFrecuencia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboFrecuencia_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Diario</asp:ListItem>
                                        <asp:ListItem Value="7">Semanal</asp:ListItem>
                                        <asp:ListItem Value="15">Quincenal</asp:ListItem>
                                        <asp:ListItem Value="30">Mensual</asp:ListItem>
                                        <asp:ListItem Value="4">Cada 4 días</asp:ListItem>
                                        <asp:ListItem Value="10">Cada 10 días</asp:ListItem>
                                        <asp:ListItem Value="20">Cada 20 días</asp:ListItem>
                                        <asp:ListItem Value="45">Cada 45 días</asp:ListItem>
                                        <asp:ListItem Value="60">Cada 60 días</asp:ListItem>
                                        <asp:ListItem Value="0">Otro</asp:ListItem>
                                    </cc1:ComboBoxBase>
                                    <cc1:NumberBox ID="txtFrecuencia" runat="server" AutoPostBack="True" OnTextChanged="txtFrecuencia_TextChanged" Enabled="False" Width="30px">1</cc1:NumberBox></td>
                            </tr>
                      <tr class="lblBase">
                                <td class="auto-style3" >Estado:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboEstado" runat="server"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >Destino:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboDestino" runat="server"></cc1:ComboBoxBase></td>
                            </tr>
                         <tr class="lblBase">
                                <td class="auto-style3" >Asesor:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboAsesor" runat="server"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >Tipo Cálculo:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboTipoCalculo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoCalculo_SelectedIndexChanged" Enabled="False"></cc1:ComboBoxBase></td>
                            </tr>
                          <tr class="lblBase">
                                <td class="auto-style3" >Tasa de interés:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboTasaInteres" runat="server"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >Tasa Moratoria:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtTasaMora" runat="server" DecimalPlaces="2"></cc1:NumberBox></td>
                            </tr>
                        <tr class="lblBase">
                                <td class="auto-style3" >Observaciones:</td>
                                <td colspan="3" class="auto-style3">
                                    <cc1:TextBoxBase ID="txtObservacion" runat="server" Height="61px" TextMode="MultiLine" Width="480px"></cc1:TextBoxBase>
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
                        <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" Visible="False"  />&nbsp;<cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>