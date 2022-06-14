<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteVisitasUbigeo.aspx.cs" Inherits="SGA.Presentacion.frmReporteVisitasUbigeo" %>

<%@ Register TagPrefix="cc1" Namespace="SGA.Controles" Assembly="SGA.Controles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
            <cc1:PanelBase ID="panelContenido" runat="server">
                <table>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td align="">
                            <table>
                                <tr>
                                    <td>
                                        <cc1:LabelBase ID="LabelBase14" runat="server">Departamento:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:ComboBoxDepartamento ID="cboDepartamento" runat="server" AddTodos="True"
                                               AutoPostBack="True"
                                                OnSelectedIndexChanged="cboDepartamento_OnSelectedIndexChanged"/>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:LabelBase ID="lblProvincia" runat="server" Visible="False">Provincia:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:ComboBoxProvincia ID="cboProvincia" runat="server" AddTodos="True"
                                               AutoPostBack="True" Visible="False"
                                                OnSelectedIndexChanged="cboProvincia_OnSelectedIndexChanged"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:LabelBase ID="lblDistrito" runat="server" Visible="False">Distrito:</cc1:LabelBase>
                                    </td>
                                    <td>
                                        <cc1:ComboBoxDistrito ID="cboDistrito" runat="server" 
                                            AddTodos="True" Visible="False"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">&nbsp;</td>
                    </tr>
                </table>
            </cc1:PanelBase>
        </div>
    </form>
</body>
</html>
