<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="FrmRptListadoPer.aspx.cs" Inherits="SGA.Presentacion.TALENTO.FrmRptListadoPer" %>
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
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Agencia: </cc1:LabelBase>
                     
                 </td>
                 <td>
                    <cc1:ComboBoxBase ID="cboAgencia1" runat="server">
                     </cc1:ComboBoxBase>
                 </td>
                 
             </tr>

            <tr>                 
                 <td>
                      <cc1:LabelBase ID="LabelBase3" runat="server">Estado: </cc1:LabelBase>
                     
                 </td>
                 <td><cc1:ComboBoxBase ID="cboEstPersonal" runat="server">
                     </cc1:ComboBoxBase>
                 </td>
                 
             </tr>
             
             <tr>                 
                 <td>
                     <cc1:LabelBase ID="LabelBase4" runat="server">Cargo: </cc1:LabelBase>
                     
                 </td>
                 <td>
                     <cc1:ComboBoxBase ID="cboCargoPersonal" runat="server">
                     </cc1:ComboBoxBase>
                 </td>
                 
             </tr>
             
            
             
             <tr>                 
                 <td colspan="2" style="text-align: center">
                     <br />
                     <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" />
                     
                 </td>
                 
             </tr>
             
        </table>
     </div>
    </form>
</body>
</html>
