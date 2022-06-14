<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmRegistroPersonal.aspx.cs" Inherits="SGA.Presentacion.TALENTO.frmRegistroPersonal" %>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>
    <div>
        <table align="center">
             <tr>                 
                 <td colspan="2">
                     <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                 </td>
                 
             </tr>
             
             <tr>                 
                 <td colspan="2" style="text-align: center">
                     <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                 </td>
                 
             </tr>
             
             <tr>                 
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Agencia: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboAgencia1" runat="server" AutoPostBack="True">
                     </cc1:ComboBoxBase>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Estado: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboEstPersonal" runat="server" AutoPostBack="True">
                     </cc1:ComboBoxBase>
                 </td>
                 
             </tr>
             
             <tr>                 
                 <td>
                     <cc1:LabelBase ID="LabelBase4" runat="server">Cargo: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboCargoPersonal" runat="server" AutoPostBack="True">
                     </cc1:ComboBoxBase>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase5" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtidUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
                 
             </tr>
             
             <tr>                 
                 <td>
                     <cc1:LabelBase ID="lblBase1" runat="server">Fec. Ini: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpInicioPersonal" runat="server" />
                 </td>
                 <td>
                     <cc1:LabelBase ID="lblBase4" runat="server">Fec. Cese: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpCesePersonal" runat="server" />
                 </td>
                 
             </tr>
             
             <tr>                 
                 <td colspan="2" style="text-align: center">
                     <cc1:BotonEditar ID="btnEditar" runat="server" OnClick="btnEditar_Click" />
&nbsp;<cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" />
&nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" />
                 </td>
                 
             </tr>
             
        </table>
     </div>
    </form>
</body>
</html>
