<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDepositoCuentaAhorro.aspx.cs" Inherits="SGA.Presentacion.AHORROS.frmDepositoCuentaAhorro" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <uc1:conBuscarCliente ID="conBuscarCliente" runat="server" />
                </td>
            </tr>
            <tr>
                <td ></td>
                <td>
                    <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                    <asp:HiddenField ID="hIdCuenta" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <cc1:LabelBase ID="lbTipoCuenta" Text="Número de Cuenta" runat="server"></cc1:LabelBase>
                    </td>
                <td>
                    <cc1:TextBoxBase ID="txtNumeroCuenta" runat="server"></cc1:TextBoxBase>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <cc1:LabelBase ID="LabelBase1" Text="Monto a Depositar" runat="server"></cc1:LabelBase>
                </td>
                <td class="auto-style2">
                    <cc1:TextBoxBase ID="txtMonto" runat="server"></cc1:TextBoxBase>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <cc1:LabelBase ID="LabelBase2" Text="DNI Depositante" runat="server"></cc1:LabelBase>
                </td>
                <td class="auto-style2">
                    <cc1:TextBoxBase ID="txtDNIDepositante" runat="server"></cc1:TextBoxBase>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:LabelBase ID="LabelBase3" Text="Fecha" runat="server"></cc1:LabelBase>
                </td>
                <td>
                    <cc1:TextBoxBase ID="txtFecha" runat="server" Enabled="False">23/02/2020</cc1:TextBoxBase>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" />
                </td>
                <td>
                    <cc1:BotonCancelar ID="btnCancelar" runat="server" />
                </td>
            </tr>
           
        </table>
        <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
