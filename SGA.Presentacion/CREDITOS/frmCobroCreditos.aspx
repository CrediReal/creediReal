<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCobroCreditos.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmCobroCreditos" %>
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
    <link href="../Styles/cssGeneral.css" rel="stylesheet" />
   
    <script type="text/javascript">
        function submitButton(event) {
            if (event.which == 13) {
                $('#btnDistribuir').trigger('click');
            }
        }
        function submitButtonMora(event) {
            if (event.which == 13) {
                $('#btnDistribuiMora').trigger('click');
            }
        }
    </script>
    <style type="text/css">
        .auto-style3 {
            text-align: right;
        }
        .auto-style4 {
            height: 26px;
            text-align: right;
        }
        .auto-style5 {
            text-align: left;
        }
        .auto-style6 {
            height: 26px;
            text-align: left;
        }

        .auto-style7 {
            width: 593px;
        }

    </style>
    
</head>
<body>
    <form id="form1" runat="server">

        <asp:Button ID="btnDistribuir" runat="server" Text=">>" OnClick="btnDistribuir_Click" style="display:none"/>
        <div align="left">
           <table>
                 <tr>
                    <td ></td>
                    <td class="auto-style7">
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td class="auto-style7">
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                        <asp:HiddenField ID="hIdCuenta" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td class="auto-style7">
                        <cc1:GridViewBase ID="dtgCreditos" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="dtgCreditos_SelectedIndexChanged">

                            <Columns>
                                <asp:BoundField DataField="IdNum" HeaderText="Cuenta"></asp:BoundField>
                                <asp:BoundField DataField="Fecha_Desembolso" HeaderText="Desembolso"></asp:BoundField>
                                <asp:BoundField DataField="cProducto" HeaderText="Producto"></asp:BoundField>
                                <asp:BoundField DataField="nMonto" HeaderText="Capital"></asp:BoundField>
                                <asp:BoundField DataField="Monto_Cuota" HeaderText="Cuota"></asp:BoundField>
                                <asp:BoundField DataField="nAtraso" HeaderText="Atraso"></asp:BoundField>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate >
									    <asp:LinkButton id="P" runat="server" CommandArgument="Seleccionar" CommandName="select" ToolTip="Seleccionar cuenta" CausesValidation="false" Text="Seleccionar">Seleccionar</asp:LinkButton>
									</ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>

                        </cc1:GridViewBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td class="auto-style7">
                    </td>
                    <td></td>
                </tr>
                
                </table>

             <cc1:PanelBase ID="pnInfoCredito" runat="server" Width="600">
                             <table>

                             <tr class="lblBase">
                                <td class="auto-style6" >MEDIO DE PAGO:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboMedioPago" runat="server"></cc1:ComboBoxBase>
                                 </td>
                                 <td class="auto-style6">NRO. DE OPERACIÓN:</td>
                                <td class="auto-style4">                                    
                                    <cc1:NumberBox ID="txtNroOperacion" runat="server" Enabled="True" MaxLength="20"></cc1:NumberBox>
                                </td>
                             </tr>

                             <tr class="lblBase">
                                <td class="auto-style6" >Nro de Cuotas:</td>
                                <td class="auto-style4">                                    
                                    <cc1:NumberBox ID="txtTotalCuotas" runat="server" Enabled="False"></cc1:NumberBox>
                                 </td>
                                 <td class="auto-style6">Cuota pendiente a pagar:</td>
                                <td class="auto-style4">                                    
                                    <cc1:NumberBox ID="txtPriCuotaPen" runat="server" Enabled="False"></cc1:NumberBox>
                                </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >Cuotas pendientes:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtCuotasPendientes" runat="server" Enabled="False"></cc1:NumberBox></td>
                                 <td class="auto-style6">Dias de atraso:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtDiasAtraso" runat="server" Enabled="False"></cc1:NumberBox></td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >Cuotas vencidas:</td>
                                <td class="auto-style4">
                                     <cc1:NumberBox ID="txtCuotasVencidas" runat="server" Enabled="False"></cc1:NumberBox></td>
                                 <td class="auto-style6"></td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                 </tr>
                                 </table>
                        </cc1:PanelBase>
                    
                   <cc1:PanelBase ID="pnlDetalle" runat="server" Width="600"> 
                    <table>
                             <tr class="lblBase">
                                <td class="auto-style6" >Capital:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtCapitalPen" runat="server" DecimalPlaces="2" ReadOnly="True"></cc1:NumberBox>
                                 </td>
                                 <td class="auto-style6">Capital:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtCapitalPag" runat="server" DecimalPlaces="2" ReadOnly="True" Enabled="False"></cc1:NumberBox>
                                 </td>
                            </tr>
                              <tr class="lblBase">
                                <td class="auto-style5" >Interés:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtInteresPen" runat="server" DecimalPlaces="2" ReadOnly="True"></cc1:NumberBox>
                                  </td>
                                 <td class="auto-style5" >Interés:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtInteresPag" runat="server" DecimalPlaces="2" ReadOnly="True" Enabled="False"></cc1:NumberBox>
                                  </td>
                            </tr>
                            <tr class="lblBase">
                                <td class="auto-style5" >Otros:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtOtrosPen" runat="server" DecimalPlaces="2" ReadOnly="True"></cc1:NumberBox>
                                </td>
                                 <td class="auto-style5" >Otros:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtOtrosPag" runat="server" DecimalPlaces="2" ReadOnly="True" Enabled="False"></cc1:NumberBox>
                                </td>
                            </tr>

                             <tr class="lblBase">
                                <td class="auto-style5" >Total Pendiente:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtTotalPen" runat="server" DecimalPlaces="2" ReadOnly="True"></cc1:NumberBox>
                                 </td>
                                 <td class="auto-style5" >&nbsp;</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMoraPag" runat="server" DecimalPlaces="2" ReadOnly="True" Enabled="False"></cc1:NumberBox>
                                 </td>
                            </tr>
                             <tr class="lblBase">
                                <td class="auto-style5" >Sub Total Deuda:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtSubTotalDeuda" runat="server" DecimalPlaces="2" ReadOnly="True"></cc1:NumberBox>
                                 </td/>
                                 <td class="auto-style5" >Monto Neto:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMontoNeto" runat="server" DecimalPlaces="2" onKeyDown="submitButton(event)"></cc1:NumberBox>
                                    
                                 </td>
                            </tr>

                             <tr class="lblBase">
                                 <td class="auto-style5">Mora del crédito:</td>
                                 <td class="auto-style4">
                                     <cc1:NumberBox ID="txtMoraPen" runat="server" DecimalPlaces="2" ReadOnly="True"></cc1:NumberBox>
                                 </td>
                                 <td class="auto-style5">
                                     <cc1:CheckBoxBase ID="chbMora" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxBase1_CheckedChanged" Text="¿Mora?" />
                                 </td>
                                 <td class="auto-style3">
                                     <cc1:NumberBox ID="txtMontoMora" runat="server" DecimalPlaces="2" Enabled="False" onKeyDown="submitButtonMora(event)"></cc1:NumberBox>                                      
                                      <asp:Button ID="btnDistribuiMora" runat="server" Text=">>" OnClick="btnDistribuiMora_Click" />
                                 </td>
                             </tr>

                        <tr class="lblBase">
                                <td class="auto-style5" >&nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                 <td class="auto-style5" >Total pago:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtTotalPago" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>
                                </td>
                            </tr>
                      <tr class="lblBase">
                                <td class="auto-style5" >&nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                 <td class="auto-style5" >Recibido:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtMonEfectivo" runat="server" DecimalPlaces="2" AutoPostBack="True" OnTextChanged="txtMonEfectivo_TextChanged"></cc1:NumberBox>
                                    <asp:Button ID="btnRecibido" runat="server" Text=">>" OnClick="btnRecibido_Click" style="display:none"/>
                                </td>
                            </tr>
                         <tr class="lblBase">
                                <td class="auto-style5" >&nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                 <td class="auto-style5" >A devolver:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMonDiferencia" runat="server" DecimalPlaces="2" ReadOnly="True" Enabled="False"></cc1:NumberBox>
                                </td>
                            </tr>
                          </table>
                   
                   </cc1:PanelBase>

                            <table>
                                <tr align="center">
                                        <td ></td>
                                        <td><br />
                                            <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" />
                                            &nbsp;<cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                                            &nbsp;<asp:Button ID="btnKardex" runat="server" CssClass="btnBase_Blanco" Height="40px" OnClick="btnKardex_Click" Text="Kardex" Visible="True" Width="120px" />
                                            &nbsp;<asp:Button ID="btnCronograma" runat="server" CssClass="btnBase_Blanco" Height="40px" OnClick="btnCronograma_Click" Text="Cronograma" Visible="True" Width="120px" />
                                        </td>
                                        <td></td>
                                    </tr>
                            </table>

            <cc1:GridViewBase ID="dtgCronograma" runat="server" AutoGenerateColumns="False" Visible="false">
                       <columns >                                
                            <asp:BoundField DataField ="idCuota" HeaderText ="N° Cuota" />
                            <asp:BoundField DataField ="EstadoCuota" HeaderText = "Estado"/>
                            <asp:BoundField DataField ="dFechaProg" HeaderText = "Fecha Prog." DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:BoundField DataField ="dFechaPago" HeaderText = "Fecha de Pago" DataFormatString="{0:dd/MM/yyyy}"/>                                
                            <asp:BoundField DataField ="nCapital" HeaderText = "Capital" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField ="nCapitalPagado" HeaderText = "Capital Pagado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField ="nInteres" HeaderText = "Interés" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                           <asp:BoundField DataField ="nInteresPagado" HeaderText = "Interés Pagado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            </columns >
                    </cc1:GridViewBase>
            <cc1:GridViewBase ID="dtgKardex" runat="server" AutoGenerateColumns="False" Visible="false">
                       <columns >                                
                            <asp:BoundField DataField ="NumOrdPag" HeaderText ="N°" />
                            <asp:BoundField DataField ="dFechaOpe" HeaderText = "Fecha Pago." DataFormatString="{0:dd/MM/yyyy}"/>                              
                            <asp:BoundField DataField ="nMontoOperacion" HeaderText = "Monto Pago" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField ="nMontoCapital" HeaderText = "Capital Pagado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField ="nMontoInteres" HeaderText = "Interés Pagado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                           <asp:BoundField DataField ="nMontoMora" HeaderText = "Mora Pagada" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                           <asp:BoundField DataField ="nMontoOtros" HeaderText = "Otros Pagado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField ="nSaldoCap" HeaderText = "Saldo Capital" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField ="cWinUser" HeaderText = "Usuario"/>
                            </columns >
                    </cc1:GridViewBase>
        </div>
        
        <asp:HiddenField ID="hUsuario" runat="server" />
        
    </form>
</body>
</html>