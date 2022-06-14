<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteViajes.aspx.cs" Inherits="SGA.Presentacion.frmReporteViajes" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.js" type="text/javascript"></script> 
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.4/jquery-ui.min.js" type="text/javascript"></script> 
     <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.3/themes/base/jquery-ui.css" type="text/css" media="all" />
   
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
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
        <cc1:PanelBase  ID="panelContenido" runat="server">
        <table>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td align="">
                    <table>
                        <tr><td><cc1:LabelBase ID="LabelBase14" runat="server">Fec.Inicio:</cc1:LabelBase> </td>
                            <td><cc1:CalendarioBase ID="calInicio" runat="server" /></td>
                            <td><cc1:LabelBase ID="LabelBase15" runat="server">Fec.Fin:</cc1:LabelBase></td>
                            <td><cc1:CalendarioBase ID="calFin" runat="server" /></td>

                        </tr>
                    </table>

                </td>
                <td>&nbsp;</td>
            </tr>
            
            
            <tr>
                <td class="auto-style1" ></td>
                <td>
                    
                </td>
                <td></td>
            </tr>

           
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td align="center">
                    &nbsp;</td>
                <td></td>
            </tr>
        </table>
    </cc1:PanelBase>
    </div>   
    </form>
</body>
</html>

