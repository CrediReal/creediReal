<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmDetalleRecibo.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmDetalleRecibo" %>
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
    <form id="form2" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>

    <div align="center">
        <table>
             
             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase6" runat="server" style="float: left">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="ComboBoxOficina1" runat="server">
                     </cc1:ComboBoxOficina>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase7" runat="server" style="float: left">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="ComboBoxUsuario1" runat="server">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase10" runat="server">Filtros</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase11" runat="server" style="float: left">Tipo Recibo: </cc1:LabelBase>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase12" runat="server" style="float: left">Moneda: </cc1:LabelBase>
                     <cc1:ComboBoxMoneda ID="ComboBoxMoneda1" runat="server" OnSelectedIndexChanged="ComboBoxMoneda1_SelectedIndexChanged">
                     </cc1:ComboBoxMoneda>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase13" runat="server" style="float: left">Concepto: </cc1:LabelBase>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase14" runat="server" style="float: left">Detalle: </cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase8" runat="server" style="float: none; text-align: center;">Rango Fechas</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase9" runat="server" style="float: left">Fecha Inicial: </cc1:LabelBase>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase15" runat="server" style="float: left">Fecha Final: </cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: right">
                                    
                     <cc1:BotonImprimir ID="BotonImprimir1" runat="server" />
                     &nbsp;
                     <cc1:BotonSalir ID="BotonSalir1" runat="server" />
                                    
                     &nbsp;
                     &nbsp;
                 </td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>

