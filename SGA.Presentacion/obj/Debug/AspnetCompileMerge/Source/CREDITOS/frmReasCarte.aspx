<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmReasCarte.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmReasCarte" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 151px; vertical-align:top
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
            <table align="center" width="100%">
                <tr>
                    <td width="50%">
                            <cc1:LabelBase ID="LabelBase1" runat="server">Asesor origen</cc1:LabelBase>
                            <br />
                            <cc1:ComboBoxBase ID="cboAseOri" runat="server" AutoPostBack="True" Height="16px" Width="250px" OnSelectedIndexChanged="cboAseOri_SelectedIndexChanged1">
                            </cc1:ComboBoxBase>
                    </td>
                    <td width="50%">
                            <cc1:LabelBase ID="LabelBase2" runat="server">Asesor destino</cc1:LabelBase>
                            <br />
                            <cc1:ComboBoxBase ID="cboAseDes" runat="server" AutoPostBack="True" Height="16px" Width="250px" OnSelectedIndexChanged="cboAseDes_SelectedIndexChanged1">
                            </cc1:ComboBoxBase>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" valign="Top">
                        
                            <cc1:LabelBase ID="LabelBase3" runat="server">Créditos de origen</cc1:LabelBase>
                            <br />
                            <cc1:GridViewBase ID="dtgCreOri" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="dtgCreOri_pageIndexChanging" EnablePersistedSelection="True" OnRowDataBound="OnRowDataBound" DataKeyNames="lSeleCta,idcuenta" PageSize="15">
                                <Columns>
                                  <asp:CheckBoxField DataField="lSeleCta" HeaderText="Seleccionar" ReadOnly="False" Visible="false" />
                                    <asp:TemplateField>
                                    <ItemTemplate>
                                     <asp:CheckBox ID="chkSel" runat="server" AutoPostBack="True" oncheckedchanged="chkSel_CheckedChanged" />
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:BoundField DataField="idcuenta" HeaderText="Cuenta" ReadOnly="True" />
                                    <asp:BoundField DataField="idCli" HeaderText="Cliente" ReadOnly="True" />
                                    <asp:BoundField DataField="cNombre" HeaderText="Nombre" ReadOnly="true"  ItemStyle-Width="300px"/>
                                </Columns>
                                <PagerSettings Position="Bottom" />
                            </cc1:GridViewBase>
                            <cc1:LabelBase ID="lblBase1" runat="server">Créditos </cc1:LabelBase>
                        
                    </td>
                    <td class="auto-style1" valign="Top">
                            <cc1:LabelBase ID="LabelBase4" runat="server">Créditos destino</cc1:LabelBase>
                            <br />
                            <cc1:GridViewBase ID="dtgCreDes" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="dtgCreDes_pageIndexChanging" PageSize="15">
                                <Columns>
                                  <asp:CheckBoxField DataField="lSeleCta" HeaderText="Seleccionar" ReadOnly="True" Visible="false" />
                                    <asp:BoundField DataField="idcuenta" HeaderText="Cuenta" ReadOnly="True" />
                                    <asp:BoundField DataField="idCli" HeaderText="Cliente" ReadOnly="True" />
                                    <asp:BoundField DataField="cNombre" HeaderText="Nombre" ReadOnly="true" ItemStyle-Width="300px" />
                                </Columns>
                                <PagerSettings Position="Bottom" />
                            </cc1:GridViewBase>
                            <cc1:LabelBase ID="lblBase2" runat="server">Créditos </cc1:LabelBase>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <cc1:BotonCancelar ID="btnCancelar1" runat="server"/>&nbsp;
                        <cc1:BotonGrabar ID="btnGrabar1" runat="server" OnClick="btnGrabar1_Click"/>
                    </td>
                </tr>
                </table>
          
        </div>
    </form>
</body>
</html>