<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmHabilicionAgencia.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmHabilicionAgencia" %>
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
                 <td colspan="4">
                     <cc1:LabelBase ID="LabelBase1" runat="server">Datos Usuario</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server" style="text-align: justify; float: left">Fecha: </cc1:LabelBase>
                 </td>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase3" runat="server" style="float: left">Código</cc1:LabelBase>
                     </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase4" runat="server" style="float: left">Usuario: </cc1:LabelBase>
                     </td>
             </tr>

             <tr>
                 <td colspan="4">
                     <cc1:LabelBase ID="LabelBase5" runat="server">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="TextBoxBase1" runat="server" Width="585px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase6" runat="server" style="float: left">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="ComboBoxOficina1" runat="server">
                     </cc1:ComboBoxOficina>
                 </td>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase7" runat="server" style="float: left">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="ComboBoxUsuario1" runat="server">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="4">
                     <cc1:LabelBase ID="LabelBase8" runat="server" style="float: none; text-align: center;">Rango Fechas</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase9" runat="server" style="float: left">Fecha Inicial: </cc1:LabelBase>
                 </td>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase10" runat="server" style="float: left">Fecha Final: </cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="4" style="text-align: right">
                                    
                     <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" />
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
