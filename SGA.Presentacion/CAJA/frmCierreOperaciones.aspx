<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCierreOperaciones.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmCierreOperaciones" %>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>

     <div align="center" runat="server" class="pnlBase" id="pnlCierre">
        <table>
             <tr>                 
                 <td>
                     <cc1:LabelBase ID="LabelBase1" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" />
                 </td>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase2" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td>
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
                 <td colspan="4" style="text-align: center">
                     <cc1:LabelBase ID="LabelBase6" runat="server" style="font-weight: 700">CUADRE CAJA</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="4" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase14" runat="server">Saldo Inicial S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtSalIniSol" runat="server" DecimalPlaces="2" Enabled="False">0</cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: center">
                     <cc1:LabelBase ID="LabelBase7" runat="server">Ingresos</cc1:LabelBase>
                 </td>
                 <td colspan="2" style="text-align: center">
                     <cc1:LabelBase ID="LabelBase8" runat="server">Egresos</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:GridViewBase ID="dtgIngSoles" runat="server" AutoGenerateColumns="False">
                         <columns >                                
                                <asp:BoundField DataField ="cTipoOperacion" HeaderText ="Tipo operación" />
                                <asp:BoundField DataField ="nMontoOperacion" HeaderText = "Monto operación" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            </columns >
                     </cc1:GridViewBase>
                 </td>
                 <td colspan="2" style="text-align: left">
                     <cc1:GridViewBase ID="dtgEgrSoles" runat="server" AutoGenerateColumns="False">
                         <Columns>
                                <asp:BoundField DataField ="cTipoOperacion" HeaderText ="Tipo operación" />
                                <asp:BoundField DataField ="nMontoOperacion" HeaderText = "Monto operación" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                         </Columns>
                     </cc1:GridViewBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase9" runat="server">Total Ingreso: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonIngSol" runat="server" DecimalPlaces="2" Enabled="False">0</cc1:NumberBox>
                 </td>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase10" runat="server">Total Egreso: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonEgrSol" runat="server" DecimalPlaces="2" Enabled="False">0</cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td colspan="4" style="text-align: center">
                     <cc1:LabelBase ID="LabelBase11" runat="server">Saldo Final S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtSalFinSol" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase12" runat="server">Saldo Final Corte S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtCortSoles" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>
                 </td>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase13" runat="server">Diferencia S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtDifSoles" runat="server" DecimalPlaces="2" Enabled="False"></cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td colspan="4" style="text-align: center">
                     <cc1:LabelBase ID="lblSoles" runat="server">CIERRE EN SOLES OK...</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="4" style="text-align: center">
                     <asp:Button ID="btnImpDetall" runat="server" Text="Detallle" OnClick="btnImpDetall_Click" CssClass="botonD" Height="40px" Width="100px" />
                     &nbsp;<asp:Button ID="btnImprimir" runat="server" Text="Resumen"  OnClick="btnImprimir_Click" CssClass="botonD" Height="40px" Width="100px"/>

&nbsp;<cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" />
                 </td>
             </tr>

        </table>
     </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
