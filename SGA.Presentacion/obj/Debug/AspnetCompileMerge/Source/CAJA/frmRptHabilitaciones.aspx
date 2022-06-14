<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmRptHabilitaciones.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmRptHabilitaciones" %>
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
    <form id="form2" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>
        <div align="center">
           <table>
               <tr>
                   <td colspan="4" style="text-align: center">
                        <cc1:LabelBase ID="LabelBase1" runat="server">DATOS USUARIO</cc1:LabelBase>
                   </td>
                   
               </tr>
               <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" />
                 </td>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase3" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase4" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
                 
             </tr>
              
               <tr>
                 <td colspan="4">
                     <cc1:LabelBase ID="LabelBase6" runat="server">Nombre: </cc1:LabelBase>
                        <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="180px" Enabled="False"></cc1:TextBoxBase>

                 </td>
                 
             </tr>
              
               <tr>
                 <td colspan="2" style="text-align: left">
                    
                     <cc1:LabelBase ID="LabelBase9" runat="server">Agencia: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="cboAgencias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboAgencias_SelectedIndexChanged">
                     </cc1:ComboBoxOficina>
                    
                 </td>
                 
                 <td colspan="2">
                    
                     <cc1:LabelBase ID="LabelBase8" runat="server">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="cboColaborador" runat="server" AutoPostBack="True">
                     </cc1:ComboBoxUsuario>
                    
                 </td>
                 
             </tr>
              
               <tr>
                 <td colspan="2" style="text-align: left">
                    
                     <cc1:LabelBase ID="LabelBase11" runat="server" style="text-align: left">Fecha inical:  </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFecIni" runat="server" style="text-align: left" />
                    
                 </td>
                 
                 <td colspan="2" style="text-align: left">
                    
                     <cc1:LabelBase ID="LabelBase10" runat="server">Fecha Final: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFecFin" runat="server" />
                    
                 </td>
                 
               </tr>
              
               <tr>
                 <td colspan="4" style="text-align: left">
                    
                     &nbsp;</td>
                 
             </tr>
              
               <tr>
                 <td colspan="4" style="text-align: center">
                        <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click1" />
                   </td>
                 
             </tr>
              
               <tr>
                 <td colspan="4">
                     &nbsp;</td>
                 
             </tr>
              
               </table>
     </div>

    </form>
</body>
</html>

