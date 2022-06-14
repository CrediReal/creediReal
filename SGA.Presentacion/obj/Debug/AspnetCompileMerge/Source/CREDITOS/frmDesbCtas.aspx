<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmDesbCtas.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmDesbCtas" %>
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
        .auto-style4 {
            height: 26px;
            text-align: right;
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
                        <cc1:PanelBase ID="pnInfoCredito" runat="server">
                             <table>
                             <tr class="lblBase">
                                <td class="auto-style4" >Usuario:</td>
                                <td class="auto-style6">                                    
                                    <cc1:TextBoxBase ID="txtUsuarioBlo" runat="server" Width="300px"></cc1:TextBoxBase>
                                    &nbsp;
                                 </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style4" >Oficina:</td>
                                <td class="auto-style6">
                                    &nbsp;
                                    <cc1:TextBoxBase ID="txtAgeBlo" runat="server" Width="300px"></cc1:TextBoxBase>
                                     </td>
                                 </tr>
                                 </table>
                        </cc1:PanelBase>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td align="center">
                        <asp:Button ID="btnLiberar" Enabled="false" runat="server" CssClass="btnBase_Blanco" Height="40px" OnClick="btnLiberar_Click" Text="Liberar" Width="100px" />
                        &nbsp;<cc1:BotonCancelar ID="btnCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
    </form>
</body>
</html>