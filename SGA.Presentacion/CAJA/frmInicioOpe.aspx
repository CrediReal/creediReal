<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmInicioOpe.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmInicioOpe" %>
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
    <div align="center">
         <table>
             <tr>
                 <td colspan="3" style="font-weight: 700; text-align: center;">
                         <cc1:LabelBase ID="LabelBase4" runat="server">DATOS DEL USUARIO</cc1:LabelBase>
                     </td>
             </tr>
             <tr>                 
                 <td>
                     <cc1:LabelBase ID="LabelBase1" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" />
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
             </tr>
             <tr>
                 <td colspan="3" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase5" runat="server">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="404px" Enabled="False"></cc1:TextBoxBase>
                 </td>
             </tr>
             <tr>
                 <td colspan="3" style="font-weight: 700; text-align: center;">
                     <cc1:LabelBase ID="LabelBase6" runat="server">SALDO INICIAL DEL DÍA</cc1:LabelBase>
                 </td>
             </tr>
             <tr>
                 <td colspan="3" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase7" runat="server">Saldo en soles: </cc1:LabelBase>
                     &nbsp;&nbsp;
                     <cc1:TextBoxBase ID="txtMonSoles" runat="server" Enabled="False"></cc1:TextBoxBase>
                     &nbsp;&nbsp;
                     <cc1:LabelBase ID="LabelBase8" runat="server">Nuevos soles</cc1:LabelBase>
                 </td>
             </tr>
             <tr>
                 <td colspan="3" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase9" runat="server">Saldo en dólares: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtMonDolares" runat="server" Enabled="False"></cc1:TextBoxBase>
                     &nbsp;
                     <cc1:LabelBase ID="LabelBase10" runat="server">Dólares americanos</cc1:LabelBase>
                 </td>
             </tr>
             <tr>
                 <td></td>
                     <td>
                       
                     </td>
                 <td></td>
             </tr>
             <tr>
                 <td colspan="3" style="text-align: right">&nbsp;&nbsp;
                     <cc1:BotonProcesar ID="BotonProcesar1" runat="server" OnClick="BotonProcesar1_Click" />
                 </td>
             </tr>
         </table>
    </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
