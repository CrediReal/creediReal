<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReimprimeVoucher.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmReimprimeVoucher" %>

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
                 <tr class="txtBase">
                    <td >Nro de Cuenta </td>
                    <td>
                        <cc1:NumberBox ID="txtCuenta" runat="server" MaxLength="5"></cc1:NumberBox>
                    </td>
                    <td></td>
                </tr>
                <tr class="txtBase">
                    <td >Nro de Kardex</td>
                    <td>
                        <cc1:NumberBox ID="txtKardex" runat="server" MaxLength="5"></cc1:NumberBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        
                        <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" />
                        
                    </td>
                    <td></td>
                </tr>
                </table>
                
            </cc1:PanelBase>
        </div>
        
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>