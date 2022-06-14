<%@ Page Language="C#" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeBehind="frmCorteFraccionario.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmCorteFraccionario" %>
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
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 120px;
        }
        .auto-style2 {
            width: 120px;
            height: 74px;
        }
        .auto-style3 {
            height: 74px;
        }
        .auto-style4 {
            width: 200px;
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
         <cc1:PanelBase ID="pnlBilletaje" runat="server">
        <table>
            <tr>
                 <td colspan="4" style="font-weight: 700; text-align: center;">
                     <cc1:LabelBase ID="LabelBase8" runat="server">DATOS DE ORIGEN</cc1:LabelBase>
                 </td>
             </tr>
            <tr>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase1" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" Enabled="False" />
                 </td>
                 <td colspan="2" class="auto-style3">
                     <cc1:LabelBase ID="LabelBase2" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td class="auto-style3">
                     <cc1:LabelBase ID="LabelBase3" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
                 
             </tr>
             <tr>
                 <td colspan="4" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase5" runat="server">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="404px" Enabled="False"></cc1:TextBoxBase>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" colspan="2" class="auto-style4">
                     &nbsp;</td>
                 <td style="text-align: left" colspan="2">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td style="text-align: left" colspan="2" class="auto-style4">
                      <cc1:GridViewBase ID="dtgMonedas" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="nValor" HeaderText="Valor"/>
                            <asp:BoundField DataField="cDescripcion" HeaderText="Denominación" />
                            
                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate >
                                    <cc1:TextBoxBase ID="txtCantidad" style="text-align:right" Width="50px" Text='<%#Bind("nCantidad") %>' runat="server" OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true"></cc1:TextBoxBase>
								</ItemTemplate>
                            </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Total">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate >
                                    <cc1:TextBoxBase ID="txtTotal" style="text-align:right" Enabled="false" Width="50px" Text='<%#Bind("nTotal") %>' runat="server"></cc1:TextBoxBase>
								</ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                        <PagerSettings Position="TopAndBottom" />
                    </cc1:GridViewBase>


                    
                 </td>
                 <td style="text-align: left;" colspan="2" valign="top">
                     <cc1:GridViewBase ID="dtgBilletes" runat="server" AutoGenerateColumns="False">
                         <Columns>
                             <asp:BoundField DataField="nValor" HeaderText="Valor" />
                             <asp:BoundField DataField="cDescripcion" HeaderText="Denominación" />
                             <asp:TemplateField HeaderText="Cantidad">
                                 <ItemStyle HorizontalAlign="Center" />
                                 <ItemTemplate>
                                     <cc1:TextBoxBase ID="txtCantidad2" runat="server" AutoPostBack="true" OnTextChanged="txtCantidad2_TextChanged" style="text-align:right" Text='<%#Bind("nCantidad") %>' Width="50px"></cc1:TextBoxBase>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Total">
                                 <ItemStyle HorizontalAlign="Center" />
                                 <ItemTemplate>
                                     <cc1:TextBoxBase ID="txtTotal2" runat="server" Enabled="false" style="text-align:right" Text='<%#Bind("nTotal") %>' Width="50px"></cc1:TextBoxBase>
                                 </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                         <PagerSettings Position="TopAndBottom" />
                     </cc1:GridViewBase>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" class="auto-style1">
                     Total monedas:</td>
                 <td style="text-align: left">
                     <cc1:NumberBox ID="txtMonedas" runat="server" DecimalPlaces="2" Width="80px" Enabled="False"></cc1:NumberBox>
                 </td>
                 <td style="text-align: left">Total billetes</td>
                 <td style="text-align: left">
                     <cc1:NumberBox ID="txtBilletes" runat="server" DecimalPlaces="2" Width="80px" Enabled="False"></cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td colspan="2">
                     Total Billetaje:<cc1:NumberBox ID="txtTotal" runat="server" DecimalPlaces="2" Width="98px" Enabled="False"></cc1:NumberBox>
                 </td>
                 <td style="text-align: left">
                     &nbsp;</td>
             </tr>
            <tr>
                 <td style="text-align: left" class="auto-style1">
                     &nbsp;</td>
                 <td style="text-align: left" colspan="2">
                     &nbsp;</td>
                 <td style="text-align: left">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td style="text-align: left" colspan="4">
                     <cc1:BotonImprimir ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" />
&nbsp;<cc1:BotonEditar ID="btnEditar" runat="server" OnClick="btnEditar_Click" />
&nbsp;<cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" />
&nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" />
                 </td>
             </tr>
        </table>
             </cc1:PanelBase>
       
     </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
