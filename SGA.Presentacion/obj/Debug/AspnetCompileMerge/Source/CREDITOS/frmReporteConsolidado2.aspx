<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteConsolidado2.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmReporteConsolidado2" %>
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
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
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
        <div align="center" class="pnlBase">
           <table>
                             <tr class="lblBase">
                                <td class="auto-style4" >Días de atraso mínimo:</td>
                                <td class="auto-style4">
                                    <cc1:numericUpDown ID="txtNroDia" runat="server" Maximun="9999" Minimun="-9999">-9999</cc1:numericUpDown>
                                 </td>
                                 <td class="auto-style4">&nbsp;</td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                            </tr>
               
                            <tr class="lblBase">
                                <td class="auto-style3" >Asesor:</td>
                                <td class="auto-style3">
                                    <cc1:ComboBoxBase ID="cboAsesor" runat="server"></cc1:ComboBoxBase></td>
                                 <td class="auto-style3" >&nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                            </tr>

                         <tr class="lblBase">
                                <td class="auto-style3" >&nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                 <td class="auto-style3" >&nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                            </tr>

                              <tr class="lblBase">
                                <td class="auto-style3" >:</td>
                                <td class="auto-style3">
                                    <asp:Button ID="btnResumen" runat="server" CssClass="btnBase_Blanco" Text="VER REPORTE" Height="40px" OnClick="btnResumen_Click" Width="94px" />
                                  </td>
                                 <td class="auto-style3" >
                                    <asp:Button ID="btnResumenCartera" runat="server" CssClass="btnBase_Blanco" Text="RESUMEN CARTERA" Height="40px" OnClick="btnResumenCartera_Click" Width="144px" />
                                  </td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                            </tr>
                                                    
                         </table>
        </div>
        <asp:HiddenField ID="hPerfil" runat="server" />
    </form>
</body>
</html>