<%@ Page Language="C#" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeBehind="frmAperturaCaja.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmAperturaCaja" %>
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
                     <cc1:LabelBase ID="LabelBase1" runat="server" BackColor="#CC0000" BorderColor="Red" BorderStyle="Solid" style="text-align: center">MANIPULAR LA SIGUIENTE OPCIÓN DE FORMA RESPONSABLE</cc1:LabelBase>
                 </td>
             </tr>

             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Agencias: </cc1:LabelBase>
                     <cc1:ComboBoxProvincia ID="cboAgencias" runat="server" AutoPostBack="True" CssClass="cmbBase" OnSelectedIndexChanged="cboAgencias_SelectedIndexChanged1" >
                     </cc1:ComboBoxProvincia>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="cboColaborador" runat="server" AutoPostBack="True" CssClass="cmbBase">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:CheckBoxBase ID="chcApeCaja" runat="server" style="color: #000099; font-weight: 700" Text="APERTURA DE CAJA CERRADA" AutoPostBack="True" OnCheckedChanged="chcApeCaja_CheckedChanged1" />
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:CheckBoxBase ID="chcCorteFracc" runat="server" style="font-weight: 700; color: #00FF00" Text="HABILITAR BILLETAJE" AutoPostBack="True" OnCheckedChanged="chcCorteFracc_CheckedChanged1" />
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase4" runat="server">Sustento: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtSustento" runat="server" Height="55px" Width="381px"></cc1:TextBoxBase>
                 </td>
             </tr>

             <tr>
                 <td colspan="2" style="text-align: center">
                     <cc1:BotonAceptar ID="btnAceptar" runat="server" OnClick="btnAceptar_Click1" />
                 </td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>

