<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmActivarCaja.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmActivarCaja" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style3
        {
            height: 72px;
        }
        .auto-style4
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="frmAperturaCaja" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>

    <div align="center">
        <table>
             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase1" runat="server">MANIPULAR ESTA OPCIÓN CON RESPONSABILIDAD, CUIDADO Y UN BUEN SUSTENTO</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server" style="float: left">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="ComboBoxOficina1" runat="server">
                     </cc1:ComboBoxOficina>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server" style="float: left">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="ComboBoxUsuario1" runat="server">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:CheckBoxBase ID="CheckBoxBase1" runat="server" Text="APERTURAR CAJA CERRADA" style="float: left" />
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:CheckBoxBase ID="CheckBoxBase2" runat="server" Text="HABILITAR BILLETAJE O CORTE FRACCIONARIO" style="float: left" />
                 </td>
             </tr>

             <tr>
                 <td colspan="2" class="auto-style3">
                     <cc1:LabelBase ID="LabelBase4" runat="server" style="float: left">Sustento: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="TextBoxBase1" runat="server" Height="43px" Width="532px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: right" class="auto-style4">
                     <cc1:BotonAceptar ID="BotonAceptar1" runat="server" />
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
