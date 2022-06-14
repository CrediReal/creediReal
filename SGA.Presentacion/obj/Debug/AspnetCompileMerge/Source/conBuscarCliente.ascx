<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conBuscarCliente.ascx.cs" Inherits="SGA.Presentacion.conBuscarCliente" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<style type="text/css">
    .auto-style1 {
        height: 1px;
    }
</style>

<cc1:PanelBase ID="pnlBusqueda" runat="server" Visible="true">

    <table style="width: 100%;">
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:RadioButtonList CssClass="lblBase" ID="rblTipoBusqueda" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblTipoBusqueda_SelectedIndexChanged">
                    <%--<asp:ListItem Value="3">Código Cliente</asp:ListItem>--%>
                    <asp:ListItem Value="1">Documento</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">Nombres</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td rowspan="2">
                <cc1:BotonBuscar ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" />
            </td>
        </tr>

        <tr>
            <td>
                &nbsp;&nbsp;
                <cc1:LabelBase ID="lblBusqueda" runat="server">Valor a buscar</cc1:LabelBase>
            </td>
            <td>
                <cc1:TextBoxBase ID="txtValBusqueda" runat="server" Width="350px"></cc1:TextBoxBase>
            </td>
        </tr>
    </table>

</cc1:PanelBase>

<cc1:PanelBase ID="pnlClientes" runat="server" Visible="false">

    <table style="width: 100%;">       
            <tr>
                <td align="center">
                    <cc1:ListBoxBase ID="lisclientes" runat="server" 
                        OnSelectedIndexChanged="lisclientes_SelectedIndexChanged" 
                        Visible="true"></cc1:ListBoxBase>
                </td>
            </tr>
           
    </table>

</cc1:PanelBase>

<cc1:PanelBase ID="pnlDatos" runat="server" Visible="false">

    <table style="width: 100%;">
       
            <tr>
                <td>
                    <cc1:LabelBase ID="LabelBase4" runat="server">&nbsp;&nbsp;Nombres:</cc1:LabelBase>
                </td>
                <td>
                    <cc1:TextBoxBase ID="txtNombres" runat="server" Width="350px" ReadOnly="True"></cc1:TextBoxBase>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <cc1:LabelBase ID="LabelBase5" runat="server">&nbsp;&nbsp;Documento:</cc1:LabelBase>
                </td>
                <td class="auto-style1">
                    <cc1:TextBoxBase ID="txtDocumento" runat="server" Width="350px" ReadOnly="True"></cc1:TextBoxBase>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <cc1:LabelBase ID="LabelBase6" runat="server">Dirección:</cc1:LabelBase>
                </td>
                <td>
                    <cc1:TextBoxBase ID="txtDireccion" runat="server" Width="350px" ReadOnly="True"></cc1:TextBoxBase>
                </td>
            </tr>--%>
    </table>

</cc1:PanelBase>

