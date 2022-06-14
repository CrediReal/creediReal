<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmLibroCaja.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmLibroCaja" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1
        {
        }
        .auto-style2 {
            width: 66px;
        }
        .auto-style3 {
            width: 186px;
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
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase1" runat="server">DATOS USUARIO</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td class="auto-style3">
                     <cc1:LabelBase ID="LabelBase2" runat="server" style="float: left">Código: </cc1:LabelBase>
                     <cc1:NumberBox ID="NumberBox5" runat="server"></cc1:NumberBox>
                 </td>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase13" runat="server" style="float: left">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="TextBoxBase2" runat="server" Width="208px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td class="auto-style3">
                     <cc1:LabelBase ID="LabelBase5" runat="server" style="float: left" Width="343px">CUADRE CAJA GENERAL - AGENCIAS</cc1:LabelBase>
                 </td>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase14" runat="server" style="float: left">Fecha: </cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td class="auto-style3">
                     &nbsp;</td>
                 <td class="auto-style2">
                     &nbsp;</td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase8" runat="server" style="float: left">Cuadre Caja: </cc1:LabelBase>
                     :
                     <cc1:GridViewBase ID="GridViewBase1" runat="server">
                     </cc1:GridViewBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" class="auto-style1">
                     <cc1:LabelBase ID="LabelBase9" runat="server" style="float: left">SALDO INICIAL: </cc1:LabelBase>
                     <cc1:NumberBox ID="NumberBox1" runat="server"></cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td class="auto-style3">
                     <cc1:LabelBase ID="LabelBase11" runat="server" style="float: left">TOTAL INGRESOS: </cc1:LabelBase>
                     <cc1:NumberBox ID="NumberBox3" runat="server"></cc1:NumberBox>
                 </td>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase12" runat="server" style="float: left">TOTAL EGRESOS: </cc1:LabelBase>
                     <cc1:NumberBox ID="NumberBox4" runat="server"></cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" class="auto-style1">
                     <cc1:LabelBase ID="LabelBase15" runat="server">SALDO FINAL:</cc1:LabelBase>
                     <cc1:NumberBox ID="NumberBox6" runat="server"></cc1:NumberBox>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     
                     <cc1:BotonProcesar ID="BotonProcesar1" runat="server" />
                 &nbsp;
                     <cc1:BotonImprimir ID="BotonImprimir1" runat="server" />
                     &nbsp;
                     <cc1:BotonSalir ID="BotonSalir1" runat="server" />
                 </td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>