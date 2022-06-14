<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmResumenSaldos.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmResumenSaldos" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
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
                 <td>
                     <cc1:LabelBase ID="LabelBase1" runat="server" style="float: left">Fecha de Proceso: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="CalendarioBase1" runat="server" />
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:TextBoxBase ID="TextBoxBase1" runat="server" Height="151px" Width="636px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td style="text-align: right">
                                    
                     <cc1:BotonProcesar ID="BotonProcesar1" runat="server" />
                     &nbsp;
                     <cc1:BotonSalir ID="BotonSalir1" runat="server" />
                                    
                     &nbsp;
                     &nbsp;&nbsp;</td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>

