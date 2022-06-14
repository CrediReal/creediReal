<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmAprobacionSolicitud.aspx.cs" Inherits="SGA.Presentacion.ADMINISTRACION.frmAprobacionSolicitud" %>

<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <script type="text/javascript">
        function handleKeyPress(e) {
            var key = e.keyCode || e.which;
            if (key == 13) {
                document.getElementById('BotonConsultar1').click();
            }

        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 18px;
        }
        .auto-style2 {
            text-align: left;
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
            <cc1:PanelBase  ID="pnlBusqueda" runat="server" Visible="true">
            <table>
                 <tr>
                    <td colspan="4" >
                        <cc1:GridViewBase ID="dtgLisSoliciAproba" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandArgument='<%# Eval("idSolAproba")%>' OnClick="Onselected_Click" Text="Seleccionar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cNombreAge" HeaderText="Agencia" />
                                <asp:BoundField DataField="cNomUsuReg" HeaderText="Usuario Solicitante" />
<asp:BoundField DataField="cTipoOperacion" HeaderText="Operación" />
                                <asp:BoundField DataField="nValAproba" HeaderText="Valor" />
                                <asp:BoundField DataField="dFecSolici" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Solicitud" />
                            </Columns>
                        </cc1:GridViewBase>
                     </td>
                </tr>
                <tr class="txtBase">
                    <td >Nro Solicitud</td>
                    <td>
                        <cc1:TextBoxBase ID="txtIdSolAproba" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td class="auto-style1">Cliente</td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="txtNomCliente" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                </tr>
                 <tr class="txtBase">
                    <td >Producto</td>
                    <td>
                        <cc1:TextBoxBase ID="txtProducto" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                    <td class="auto-style1">Operación</td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="txtTipoOperacion" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                </tr>
                <tr class="txtBase">
                    <td >Nro documento</td>
                    <td>
                        <cc1:TextBoxBase ID="txtDocument" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                    <td class="auto-style1">Moneda</td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="txtMoneda" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                </tr>
                 <tr class="txtBase">
                    <td >Valor</td>
                    <td>
                        <cc1:TextBoxBase ID="txtValAproba" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                    <td class="auto-style1">Motivo</td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="txtMotivo" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                </tr>
                <tr class="txtBase">
                    <td >Fecha solicitud</td>
                    <td>
                        <cc1:TextBoxBase ID="txtFecSolici" runat="server" Enabled="False"></cc1:TextBoxBase>
                     </td>
                    <td class="auto-style1">Sustento</td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="txtSustento" runat="server" Enabled="False" Width="300px"></cc1:TextBoxBase>
                     </td>
                </tr>
                <tr class="txtBase">
                    <td >Opinión</td>
                    <td colspan="3">
                        
                        <cc1:TextBoxBase ID="txtOpinion" runat="server" TextMode="MultiLine" Width="500px" Height="44px"></cc1:TextBoxBase>
                        
                    </td>
                </tr>
                <tr class="txtBase">
                    <td ></td>
                    <td>
                        
                    </td>
                    <td class="auto-style1">
                        <asp:HiddenField ID="HidNivelAprRanOpe" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="HidSolAproba" runat="server" />
                    </td>
                </tr>
                 <tr class="txtBase">
                    <td ></td>
                    <td colspan="3">
                        
                        <cc1:BotonAceptar ID="btnAprobar" runat="server" OnClick="btnAprobar_Click" />
                        &nbsp;<asp:Button ID="btnRechazar" runat="server" CssClass="btnBase_Blanco" Height="40px" OnClick="btnRechazar_Click" Text="Rechazar" Width="101px" />
                        &nbsp;<asp:Button ID="btnActualizar" runat="server" CssClass="btnBase_Blanco" Height="40px" OnClick="btnActualizar_Click" Text="Actualizar" Width="101px" />
                        
                    </td>
                </tr>
                 <tr class="txtBase">
                    <td ></td>
                    <td>
                        
                    </td>
                    <td class="auto-style1"></td>
                    <td>&nbsp;</td>
                </tr>
                </table>
            </cc1:PanelBase>
        </div>
         <asp:HiddenField ID="hPerfil" runat="server" />
        
    </form>
</body>
</html>