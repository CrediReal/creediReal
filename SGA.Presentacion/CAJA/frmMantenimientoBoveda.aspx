<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmMantenimientoBoveda.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmMantenimientoBoveda" %>
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
                     <cc1:LabelBase ID="LabelBase1" runat="server" style="float: left">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxOficina ID="ComboBoxOficina1" runat="server">
                     </cc1:ComboBoxOficina>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server" style="float: left">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="ComboBoxUsuario1" runat="server">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:LabelBase ID="LabelBase3" runat="server">Cargo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="TextBoxBase1" runat="server" Height="16px" Width="446px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:TextBoxBase ID="TextBoxBase2" runat="server" Height="83px" Width="637px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     &nbsp;
                     <cc1:BotonEditar ID="BotonEditar1" runat="server" />
                     &nbsp;
                     <cc1:BotonCancelar ID="BotonCancelar1" runat="server" />
                    &nbsp;
                     <cc1:BotonGrabar ID="BotonGrabar1" runat="server" style="float: none" />
                     &nbsp;
                     <cc1:BotonSalir ID="BotonSalir1" runat="server" />
                 </td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>
