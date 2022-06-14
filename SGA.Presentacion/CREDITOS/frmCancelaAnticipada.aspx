<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCancelaAnticipada.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmCancelaAnticipada" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.2.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script type="text/javascript">
        function submitButton(event) {
            if (event.which == 13) {
                $('#btnDistribuir').trigger('click');
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
                        <asp:HiddenField ID="hIdCuenta" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td>
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
                    <td>
                        <cc1:PanelBase ID="pnInfoCredito" runat="server" Visible="False">
                             <table>
                             <tr class="lblBase">
                                <td class="auto-style6" >Saldo Capital:</td>
                                <td class="auto-style4">                                    
                                    <cc1:NumberBox ID="txtSalCap" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>
                                 </td>
                                 <td class="auto-style6">&nbsp; Saldo Mora:</td>
                                <td class="auto-style4">                                    
                                    <cc1:NumberBox ID="txtSalMor" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>
                                </td>
                                 <td class="auto-style4">  
                                     <cc1:CheckBoxBase ID="chbPagoTotal" Text="Total" runat="server" AutoPostBack="True" OnCheckedChanged="chbPagoTotal_CheckedChanged"  />             
                                    <%--<cc1:NumberBox ID="NumberBox1" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>--%>
                                </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >Saldo Intereses:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtSalInt" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox></td>
                                 <td class="auto-style6">&nbsp; Saldo Gastos:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtSalGas" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox></td>
                                <td class="auto-style4">
                                    <%--<cc1:NumberBox ID="NumberBox2" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox></td>--%>
                                    <asp:HiddenField ID="h_saldoInteresFecha" runat="server" />
                                    <asp:HiddenField ID="h_saldoInteresPactado" runat="server" />
                                    <asp:HiddenField ID="h_totalFecha" runat="server" />
                                    <asp:HiddenField ID="h_totalPactado" runat="server" />
                                 </tr>
                                 </table>
                        </cc1:PanelBase>

                   <cc1:PanelBase ID="pnlDetalle" runat="server" Visible="False"> 
                    <table>
                             <tr class="lblBase">
                                <td class="auto-style5" >&nbsp;</td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                 <td class="auto-style5">Monto Neto:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtTotPag" runat="server" DecimalPlaces="2" onKeyDown="submitButton(event)"></cc1:NumberBox>
                                    <asp:Button ID="btnDistribuir" runat="server" OnClick="btnDistribuir_Click" style="display:none" Text="&gt;&gt;" />
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
                    
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" Visible="False" />&nbsp;<cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" Visible="False" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>