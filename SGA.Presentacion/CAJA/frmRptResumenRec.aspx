<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmRptResumenRec.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmRptResumenRec" %>
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
            text-align: center;
        }
    </style>


    </head>
<body>
    <form id="form2" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>

        <table align="center">
        <tr>
                 <td colspan="2">
                     
                     <cc1:LabelBase ID="LabelBase3" runat="server">Tipo recibo: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboTipRec" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipRec_SelectedIndexChanged">
                     </cc1:ComboBoxBase>
                     
                 </td>
                 <td colspan="2">
                     
                     <cc1:LabelBase ID="LabelBase4" runat="server">Moneda: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboMoneda" runat="server" AutoPostBack="True" >
                     </cc1:ComboBoxBase>
                     
                 </td>
                 
             </tr>
            <tr>
                 <td colspan="4" style="text-align: center">
                     
                     <cc1:LabelBase ID="LabelBase1" runat="server">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="cboAgencias" runat="server" AutoPostBack="True">
                     </cc1:ComboBoxOficina>
                     
                 </td>
                 </tr>
        <tr>
                 <td colspan="2">
                     
                     <cc1:LabelBase ID="LabelBase5" runat="server">Concepto: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboConcepto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboConcepto_SelectedIndexChanged">
                     </cc1:ComboBoxBase>
                     
                 </td>
                 <td colspan="2">
                     
                     <cc1:LabelBase ID="LabelBase6" runat="server">Detalle: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboDetalle" runat="server" AutoPostBack="True">
                     </cc1:ComboBoxBase>
                     
                 </td>
                 
             </tr>
        <tr>
                 <td colspan="2">
                     
                     <cc1:LabelBase ID="LabelBase7" runat="server">Fecha inicial: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFecIni" runat="server" />
                     
                 </td>
                 <td colspan="2">
                     
                     <cc1:LabelBase ID="LabelBase8" runat="server">Fecha final: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFecFin" runat="server" />
                     
                 </td>
                 
             </tr>
        <tr>
                 <td class="auto-style1" colspan="4">
                     
                     <cc1:BotonImprimir ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" />
                     
                 </td>
                 
             </tr>
        <tr>
                 <td>
                     
                     &nbsp;</td>
                 <td colspan="2" >
                       
                     &nbsp;</td>
                 <td>
                     
                     &nbsp;</td>
                 
             </tr>
        </table>

    </form>
</body>
</html>