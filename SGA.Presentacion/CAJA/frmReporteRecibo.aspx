<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmReporteRecibo.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmReporteRecibo" %>
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
            width: 382px;
        }
        .auto-style2
        {
            width: 237px;
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

    <div align="center">
        <table>
             
             <tr>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase6" runat="server" style="float: left">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="ComboBoxOficina1" runat="server">
                     </cc1:ComboBoxOficina>
                 </td>
                 <td class="auto-style1">
                     <cc1:LabelBase ID="LabelBase7" runat="server" style="float: left">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="ComboBoxUsuario1" runat="server">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase8" runat="server" style="float: none; text-align: center;">Rango Fechas</cc1:LabelBase>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase9" runat="server" style="float: left">Fecha Inicial: </cc1:LabelBase>
                 </td>
                 <td class="auto-style1">
                     <cc1:LabelBase ID="LabelBase15" runat="server" style="float: left">Fecha Final: </cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase10" runat="server">Filtros</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td class="auto-style2">
                     <cc1:LabelBase ID="LabelBase11" runat="server" style="float: left">Tipo Recibo: </cc1:LabelBase>
                 </td>
                 <td class="auto-style1">
                     <cc1:LabelBase ID="LabelBase12" runat="server" style="float: left">Moneda: </cc1:LabelBase>
                     <cc1:ComboBoxMoneda ID="ComboBoxMoneda1" runat="server" OnSelectedIndexChanged="ComboBoxMoneda1_SelectedIndexChanged">
                     </cc1:ComboBoxMoneda>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <asp:CheckBoxList ID="CheckBoxList1" runat="server" style="text-align: left; float: left">
                         <asp:ListItem>CENTRAL DE RIESGO</asp:ListItem>
                         <asp:ListItem>CONSTANCIA</asp:ListItem>
                         <asp:ListItem>DUPLICADO</asp:ListItem>
                         <asp:ListItem>HABILITACIÓN</asp:ListItem>
                         <asp:ListItem>INGRESO POR MORA</asp:ListItem>
                         <asp:ListItem>OTROS INGRESOS</asp:ListItem>
                     </asp:CheckBoxList>
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

