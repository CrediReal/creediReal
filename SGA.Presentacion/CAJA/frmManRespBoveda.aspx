<%@ Page Language="C#" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeBehind="frmManRespBoveda.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmManRespBoveda" %>
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
            height: 23px;
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
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase1" runat="server">Agencia: </cc1:LabelBase>
                     <cc1:ComboBoxProvincia ID="cboAgencias" runat="server" OnSelectedIndexChanged="cboAgencias_SelectedIndexChanged" AutoPostBack="True" CssClass="cmbBase">
                     </cc1:ComboBoxProvincia>
                 </td>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase2" runat="server">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="cboColaborador" runat="server" OnSelectedIndexChanged="cboColaborador_SelectedIndexChanged" CssClass="cmbBase">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="4" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase3" runat="server">Cargo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCargo" runat="server" Width="279px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="4">
                     <cc1:GridViewBase ID="dtgResBov" runat="server" AutoGenerateColumns="False">
                         <columns >

                                <asp:BoundField DataField ="idUsuario" HeaderText ="ID" />
                                <asp:BoundField DataField ="cNombre" HeaderText = "NOMBRE" />
                                <asp:BoundField DataField ="cCargo" HeaderText = "CARGO"/>
                         </columns >
                     </cc1:GridViewBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="4">
                     <cc1:BotonEditar ID="btnEditar" runat="server" OnClick="btnEditar_Click" />
&nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" />
&nbsp;<cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" />
                 </td>
             </tr>

             <tr>
                 <td class="auto-style1"></td>
                 <td class="auto-style1" colspan="2">
                     </td>
                 <td class="auto-style1">
                     </td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>
