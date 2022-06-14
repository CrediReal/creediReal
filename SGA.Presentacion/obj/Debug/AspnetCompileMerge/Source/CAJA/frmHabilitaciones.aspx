<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmHabilitaciones.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmHabilitaciones" %>
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
    <div align="center" runat="server" class="pnlBase" id="pnlHabilita">
        <table>
            <tr>
                 <td colspan="3" style="font-weight: 700; text-align: center;">
                     <cc1:LabelBase ID="LabelBase8" runat="server">DATOS DE ORIGEN</cc1:LabelBase>
                 </td>
             </tr>
            <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase1" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" Enabled="False" />
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
                 
             </tr>
             <tr>
                 <td colspan="3" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase5" runat="server">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="404px" Enabled="False"></cc1:TextBoxBase>
                 </td>
             </tr>
            <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase4" runat="server">Tipo Habilitación: </cc1:LabelBase>
                     &nbsp;<cc1:ComboBoxBase ID="cboTipoHab" runat="server" Height="16px" Width="161px" OnSelectedIndexChanged="cboTipoHab_SelectedIndexChanged" AutoPostBack="True"></cc1:ComboBoxBase>
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase6" runat="server">Tipo moneda: </cc1:LabelBase>
                     <cc1:ComboBoxBase ID="cboMoneda" runat="server" OnSelectedIndexChanged="cboMoneda_SelectedIndexChanged"></cc1:ComboBoxBase>
                 </td>
             </tr>
            <tr>
                 <td colspan="3" style="text-align: center; font-weight: 700;">
                     <cc1:LabelBase ID="LabelBase7" runat="server">DESTINATARIO</cc1:LabelBase>
                 </td>
             </tr>
            <tr>
                 <td colspan="2" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase9" runat="server">Colaborador: </cc1:LabelBase>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <cc1:ComboBoxBase ID="cboUsuario" runat="server" Height="16px" Width="161px"></cc1:ComboBoxBase>
                 </td>
                 <td rowspan="2">
                     &nbsp;</td>
                 
             </tr>
            <tr>
                 <td colspan="2" style="text-align: left">
                    <cc1:LabelBase ID="LabelBase10" runat="server">Monto habilitación: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonHab" runat="server" DecimalPlaces="2"></cc1:NumberBox>
                 </td>
             </tr>
            <tr>
                 <td colspan="3" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase11" runat="server">Sustento: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtSustento" runat="server" style="text-align: left" Height="58px" Width="405px"></cc1:TextBoxBase>
                 </td>
             </tr>
            <tr>
                 <td colspan="3" style="text-align: right">
                     &nbsp;<cc1:BotonNuevo ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" />
&nbsp;&nbsp;
                     <cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="BotonGrabar1_Click" />
                 &nbsp;</td>
             </tr>
        </table>
    </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
