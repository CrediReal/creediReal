<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmVerificarCierre.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmVerificarCierre" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">

        .auto-style4
        {
            height: 21px;
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
        <table style="width: 562px">
             <tr>
                 <td colspan="3">
                     <cc1:LabelBase ID="LabelBase1" runat="server">VERIFICAR EL CIERRE DE LAS AGENCIAS</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="3">
                     <cc1:LabelBase ID="LabelBase2" runat="server" style="float: left">Fecha: </cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:TextBoxBase ID="TextBoxBase1" runat="server" Height="134px" Width="626px"></cc1:TextBoxBase>
                 </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: right" class="auto-style4">
                     <cc1:BotonProcesar ID="BotonProcesar1" runat="server" />
                     &nbsp;
                     <cc1:BotonSalir ID="BotonSalir1" runat="server" />
                     &nbsp;
                     &nbsp;</td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>