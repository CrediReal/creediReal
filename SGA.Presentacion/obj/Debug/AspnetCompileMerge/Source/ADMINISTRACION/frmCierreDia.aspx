<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCierreDia.aspx.cs" Inherits="SGA.Presentacion.ADMINISTRACION.frmCierreDia" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <link href="../CREDITOS/css/bootstrap.css" rel="stylesheet" />
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
                       <div class="alert alert-danger">
  <strong>ALERTA!</strong> Este es un proceso irreversible, <br/>verificar que todas las cajas cerraron y que no falta ninguna operación.
</div>Fecha Actual: <asp:Label ID="lblFechaSistema" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:GridViewBase ID="dtgLisProCierre" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="cProceso" HeaderText="PROCESOS DE CIERRE" />
                                <asp:BoundField DataField="dFechaProceso" HeaderText="FECHA" />
                                <asp:BoundField DataField="cEstado" HeaderText="ESTADO" />
                                <asp:BoundField DataField="idProceso"  Visible="false" />
                                <asp:BoundField DataField="idAplicativo"  Visible="false" />
                                <asp:BoundField DataField="cStoreProc"  Visible="false"/>
                                <asp:BoundField DataField="nOrdenEjecucion" Visible="false"/>
                            </Columns>
                        </cc1:GridViewBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        
                        <cc1:BotonProcesar ID="BotonProcesar1" runat="server" OnClick="BotonProcesar1_Click" />
                        
                    </td>
                    <td></td>
                </tr>
                </table>
            </cc1:PanelBase>
        </div>
        
        
    </form>
</body>
</html>