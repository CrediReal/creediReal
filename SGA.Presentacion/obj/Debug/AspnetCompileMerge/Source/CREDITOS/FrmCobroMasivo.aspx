<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FrmCobroMasivo.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.FrmCobroMasivo" %>
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
        <script type="text/javascript">
            function quit() {
                $('#btnCancelar1').trigger('click');
                event.returnValue = "Are you sure you have finished?"
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
           <table>
                 <tr>
                    <td ></td>
                    <td colspan="2">
                        Asesor:</td>
                    <td colspan="2">
                        <cc1:ComboBoxBase ID="cboPersonalCreditos1" runat="server" Width="320px" Enabled="False">
                        </cc1:ComboBoxBase>
                    </td>
                    <td></td>
                </tr>
                 <tr>
                    <td >&nbsp;</td>
                    <td colspan="2">
                        Moneda:</td>
                    <td colspan="2">
                        <cc1:ComboBoxBase ID="cboMoneda1" runat="server">
                        </cc1:ComboBoxBase>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                 <tr>
                    <td >&nbsp;</td>
                    <td colspan="4">
                        <cc1:BotonProcesar ID="btnProcesar1" CausesValidation="false" runat="server" OnClick="btnProcesar1_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
               <tr>
                    <td ></td>
                    <td colspan="4">
                        <asp:HiddenField ID="hIdAsesor" runat="server" Value="-1" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td colspan="4">
                        <cc1:GridViewBase ID="dtgBase1" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="idcuenta,nSaldoTot,nSalMora,nMonPagMora,nSalAPagar">

                            <Columns>
                                <asp:BoundField DataField="nCuotasVen" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idCli" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idUsuario" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="idAgencia" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="lSeleCta" Visible="false"></asp:BoundField>
                                 <asp:CheckBoxField DataField="lSeleCta" HeaderText="Sel." ReadOnly="False" Visible="false" />
                                    <asp:TemplateField HeaderText="Sel.">
                                    <ItemTemplate>
                                     <asp:CheckBox ID="chkSel" runat="server" AutoPostBack="True" oncheckedchanged="chkSel_CheckedChanged" />
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                <asp:BoundField DataField="idCuenta" HeaderText="Cuenta"></asp:BoundField>
                                <asp:BoundField DataField="cNombre" HeaderText="Cliente"></asp:BoundField>
                                <asp:BoundField DataField="nMonCuoIni" HeaderText="Cuota Ini."></asp:BoundField>
                                <asp:BoundField DataField="nAtraso" HeaderText="Atr.Cre"></asp:BoundField>
                                <asp:BoundField DataField="nCuoPen" HeaderText="Cuo.Pend"></asp:BoundField>
                                <asp:BoundField DataField="nAtrCuoPen" HeaderText="Atr.Cuo"></asp:BoundField>
                                <asp:BoundField DataField="nFechProg" HeaderText="Fec.Prog" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="nSaldoTot" HeaderText="Sal.Tot.Cre" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="nSalAPagar" HeaderText="Sal.Cuo" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="nSalMora" HeaderText="Sal.Mora" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:TemplateField HeaderText="Pago Cuota">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
								    <ItemTemplate >
                                        <cc1:TextBoxBase ID="txtMonPagCuota" style="text-align:right" Width="50px" Text='<%#Bind("nMonPagCuota") %>' runat="server" OnTextChanged="txtMonPagCuota_TextChanged" AutoPostBack="true"></cc1:TextBoxBase>
								    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:BoundField DataField="nMonPagMora" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="nNumeroTelefono" HeaderText="Teléfono"></asp:BoundField>
                            </Columns>

                        </cc1:GridViewBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        Total Créditos</td>
                    <td>
                        <cc1:NumberBox ID="txtNumRea4" runat="server" Enabled="False" Width="50px"></cc1:NumberBox>
                    
                    </td>
                    <td>
                        Importe de Cuotas:</td>
                    <td>
                        <cc1:NumberBox ID="txtNumRea1" runat="server" DecimalPlaces="2" Enabled="False" Width="100px"></cc1:NumberBox>
                    
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td>
                        Total Créditos Cobrados</td>
                    <td>
                        <cc1:NumberBox ID="txtNumRea5" runat="server" Enabled="False" Width="50px"></cc1:NumberBox>
                    
                    </td>
                    <td>
                        Importe de Moras:</td>
                    <td>
                        <cc1:NumberBox ID="txtNumRea2" runat="server" DecimalPlaces="2" Enabled="False" Width="100px"></cc1:NumberBox>
                    
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td colspan="4" align="center">
                        <br />
                        Total Cobrado:<cc1:NumberBox ID="txtNumRea3" runat="server" DecimalPlaces="2" Enabled="False" Width="100px"></cc1:NumberBox>
                        <br />
                    
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td colspan="4">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td ></td>
                    <td colspan="4">
                        <cc1:BotonGrabar ID="btnGrabar1" CausesValidation="false" runat="server" Enabled="False" OnClick="btnGrabar1_Click" />&nbsp;<cc1:BotonCancelar ID="btnCancelar1" runat="server" OnClick="btnCancelar1_Click" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
        <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
