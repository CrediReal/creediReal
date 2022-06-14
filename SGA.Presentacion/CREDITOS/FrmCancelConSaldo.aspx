<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FrmCancelConSaldo.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.FrmCancelConSaldo" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">

        .auto-style6 {
            height: 26px;
            text-align: left;
        }

        .auto-style4 {
            height: 26px;
            text-align: right;
        }
        .auto-style5 {
            text-align: left;
        }
        .auto-style3 {
            text-align: right;
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

                    
                   <cc1:PanelBase runat="server" ID="pnlDetalle" Width="500px"> 
                    <table>
                             <tr class="lblBase">
                                <td class="auto-style6" >Motivo:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboMot" runat="server">
                                        <asp:ListItem Selected="True" Value="1">Problemas en el Negocio</asp:ListItem>
                                        <asp:ListItem Value="2">Problemas Familiares</asp:ListItem>
                                        <asp:ListItem Value="3">Falta de Visita de Cobranza</asp:ListItem>
                                        <asp:ListItem Value="4">A Pedido de la Gerencia</asp:ListItem>
                                    </cc1:ComboBoxBase>

                                 </td>
                                 <td class="auto-style6">Capital:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox runat="server" DecimalPlaces="2" DecimalSymbol="." Value="0" ReadOnly="True" Enabled="False" ID="txtCap"></cc1:NumberBox>

                                 </td>
                            </tr>
                              <tr class="lblBase">
                                <td class="auto-style5" colspan="2" >Detalle del motivo:</td>
                                 <td class="auto-style5" >Interés:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox runat="server" DecimalPlaces="2" DecimalSymbol="." Value="0" ReadOnly="True" Enabled="False" ID="txtInt"></cc1:NumberBox>

                                  </td>
                            </tr>
                            <tr class="lblBase">
                                <td class="auto-style5" colspan="2" rowspan="3" >
                                    <cc1:TextBoxBase ID="txtMot" runat="server" Height="60px" TextMode="MultiLine" Width="260px"></cc1:TextBoxBase>
                                </td>
                                 <td class="auto-style5" >Otros:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox runat="server" DecimalPlaces="2" DecimalSymbol="." Value="0" ReadOnly="True" Enabled="False" ID="txtOtro"></cc1:NumberBox>

                                </td>
                            </tr>

                             <tr class="lblBase">
                                 <td class="auto-style5" >Mora:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtMora" runat="server" DecimalPlaces="2" DecimalSymbol="." Enabled="False" ReadOnly="True" Value="0"></cc1:NumberBox>

                                 </td>
                            </tr>
                             <tr class="lblBase">
                                 <td class="auto-style5" >TOTAL:</td>
                                <td class="auto-style3">
                                    <cc1:NumberBox ID="txtTotal" runat="server" DecimalPlaces="2" DecimalSymbol="." Enabled="False" ReadOnly="True" Value="0"></cc1:NumberBox>

                                    
                                 </td>
                            </tr>

                             <tr class="lblBase">
                                 <td class="auto-style5">&nbsp;</td>
                                 <td class="auto-style4">
                                     &nbsp;</td>
                                 <td class="auto-style5">
                                     &nbsp;</td>
                                 <td class="auto-style3">
                                     &nbsp;</td>
                             </tr>

                          </table>
                   
                   </cc1:PanelBase>


                    </td>
                    <td></td>
                </tr>
                
                </table>
            <br />
                                            <cc1:BotonGrabar runat="server" TextoEnviando="Guardando..." CausesValidation="False" Text="Grabar" ID="BotonGrabar1" OnClick="BotonGrabar1_Click"></cc1:BotonGrabar>
&nbsp;<cc1:BotonCancelar runat="server" ID="BotonCancelar1" OnClick="BotonCancelar1_Click"></cc1:BotonCancelar>

        </div>
        
 <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>