<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="FrmConsultaSolicitudes.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.FrmConsultaSolicitudes" %>
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
                    <td ></td>
                    <td>
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        
                    </td>
                    <td></td>
                </tr>
                </table>
                <cc1:GridViewBase ID="dtgCreditos" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" OnClick="btnPropuesta_Click" Text="Propuesta" CommandArgument='<%# Eval("Id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnKardex" runat="server" OnClick="btnEvaluacion_Click" Text="Evaluación" CommandArgument='<%# Eval("Id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="Solicitud" />
                        <asp:BoundField DataField="FechaDesembolso" HeaderText="Fecha.Desembolso" DataFormatString="{0:dd/MM/yyyy}" />                        
                        <asp:BoundField DataField="MontoCapital" HeaderText="Monto.Sol."/>
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                           
                    </Columns>
                </cc1:GridViewBase>
            </cc1:PanelBase>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
        
    </form>
</body>
</html>