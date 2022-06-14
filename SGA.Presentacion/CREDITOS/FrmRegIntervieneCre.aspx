<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FrmRegIntervieneCre.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.FrmRegIntervieneCre" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>
<%@ Register Src="~/conBuscarInterviniente.ascx" TagPrefix="uc2" TagName="conBuscarInterviniente" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Scripts/jquery-1.4.2.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style2 {
            width: 10px;
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
                    <td class="auto-style2" ></td>
                    <td>
                            <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style2" ></td>
                    <td>
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                        
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style2" >&nbsp;</td>
                    <td>
                       
                        <cc1:LabelBase ID="lblSolicitud" runat="server">Nro. Solicitud: </cc1:LabelBase>
                        <asp:HiddenField ID="hdSolicitud" runat="server" />
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td class="auto-style2" ></td>
                    <td>
                        <uc2:conBuscarInterviniente runat="server" ID="conBuscarInterviniente" Visible="false" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style2" ></td>
                    <td>
                        <cc1:PanelBase ID="pnlIntervinientes" runat="server" Visible="false"> 
                    <table>
                             <tr class="lblBase">
                                <td class="auto-style4" >Tipo de intervención:<cc1:ComboBoxBase ID="cboTipoInterv" runat="server">
                                    </cc1:ComboBoxBase>
                                    &nbsp;&nbsp;
                                    <cc1:BotonAgregarItem ID="BotonAgregarItem1" runat="server" OnClick="BotonAgregarItem1_Click" />
                                 </td>
                            </tr>
                              <tr class="lblBase">
                                <td class="auto-style3" >
                                    <cc1:GridViewBase ID="dtgIntervinientes" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgIntervinientes_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="cTipoModif" Visible="False" />
                                            <asp:BoundField DataField="idCli" Visible="False" />
                                            <asp:BoundField DataField="idTipoInterv" Visible="False" />
                                            <asp:BoundField DataField="cNombre" HeaderText="Nombres" ItemStyle-Width="250" />
                                            <asp:BoundField DataField="cTipoIntervencion" HeaderText="Tipo Interv." ItemStyle-Width="80"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                    <br />
                                    <cc1:CheckBoxBase ID="chcGarantia" runat="server" Text="¿Desembolso con Garantia?" Checked="True" />
                                    <br />
                                  </td>
                            </tr>                         
                         </table>
                    </cc1:PanelBase>
                    
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style2" ></td>
                    <td>
                        <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" Visible="false" />&nbsp;
                        <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
        
 <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>