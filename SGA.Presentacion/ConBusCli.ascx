<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConBusCli.ascx.cs" Inherits="SGA.Presentacion.ConBusCli" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<cc1:PanelBase ID="pnlBusqueda" runat="server" Visible="true">

    <table style="width: 100%;">
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:RadioButtonList ID="rblTipoBusqueda" CssClass="lblBase" runat="server"
                        RepeatDirection="Horizontal" AutoPostBack="True"
                        OnSelectedIndexChanged="rblTipoBusqueda_SelectedIndexChanged">
                    <asp:ListItem Value="1">Código Cliente</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">Documento</asp:ListItem>
                    <asp:ListItem Value="3">Nombres</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td rowspan="2">
                <cc1:BotonBuscar ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:LabelBase ID="lblBusqueda" runat="server">Valor a buscar</cc1:LabelBase>
            </td>
            <td>
                <cc1:TextBoxBase ID="txtValBusqueda" runat="server" Width="350px"></cc1:TextBoxBase>
            </td>
        </tr>
    </table>

</cc1:PanelBase>

<cc1:PanelBase ID="pnlClientes" runat="server" Visible="false">
    <cc1:GridViewBase ID="grvClientes" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="idCliente" HeaderText="Codigo" />
            <asp:BoundField DataField="cNombres" HeaderText="Nombres" />
            <asp:BoundField DataField="cDocumento" HeaderText="Documento" />
            <asp:BoundField DataField="cDireccion" HeaderText="Direccion" />
            <asp:TemplateField>
                <ItemTemplate>
                    <cc1:BotonMiniAceptar ID="btnAceptar" runat="server" 
                        OnClick ="btnAceptar_Click"
                        CommandArgument='<%# Eval("idCliente")%>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc1:GridViewBase>
</cc1:PanelBase>

<cc1:PanelBase ID="pnlDatos" runat="server" Visible="false">
    <table style="width: 100%;">
        <tr>
            <td>
                <cc1:LabelBase ID="LabelBase4" runat="server">Nombres:</cc1:LabelBase>
            </td>
            <td>
                <cc1:TextBoxBase ID="txtNombres" runat="server" Width="350px" ReadOnly="True"></cc1:TextBoxBase>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <cc1:LabelBase ID="LabelBase5" runat="server">Documento:</cc1:LabelBase>
            </td>
            <td class="auto-style1">
                <cc1:TextBoxBase ID="txtDocumento" runat="server" Width="350px" ReadOnly="True"></cc1:TextBoxBase>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:LabelBase ID="LabelBase6" runat="server">Dirección:</cc1:LabelBase>
            </td>
            <td>
                <cc1:TextBoxBase ID="txtDireccion" runat="server" Width="350px" ReadOnly="True"></cc1:TextBoxBase>
            </td>
        </tr>
    </table>
</cc1:PanelBase>
