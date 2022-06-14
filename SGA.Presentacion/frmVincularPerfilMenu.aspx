<%@ Page Language="C#"  MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmVincularPerfilMenu.aspx.cs" Inherits="SGA.Presentacion.frmVincularPerfilMenu" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script type="text/javascript">
        /*Añadir postBack a Treeview*/
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("","");
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
        <cc1:PanelBase  ID="panelContenido" runat="server">
            <table>
                <tr>
                    <td></td>
                    <td></td>                   
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <cc1:LabelBase ID="LabelBase5" runat="server">Seleccione Perfil:</cc1:LabelBase>
                                </td>                            
                                <td>
                                    <cc1:ComboBoxBase ID="cboPerfil" runat="server" Height="16px" Width="330px" AutoPostBack="True" OnSelectedIndexChanged="cboPerfil_SelectedIndexChanged">
                                    </cc1:ComboBoxBase>
                                </td>
                            </tr>                            
                        </table>
                    </td>                   
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align:left" class="lblBase">Seleccione las visibilidad del Menú:</td>                   
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td >
                        <cc1:PanelBase  ID="PanelMenu" runat="server">
                            <div align="center">
                            <table>
                                <tr>
                                    <td>
                                        <cc1:TreeViewBase ID="TreeViewMenu" runat="server" ShowCheckBoxes="All" OnTreeNodeCheckChanged="check_changed" AutoPostBack="true">
                                        </cc1:TreeViewBase>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align:center">
                                        <cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" />
                                        &nbsp;&nbsp;
                                        <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </cc1:PanelBase>
                    </td>                   
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>                   
                    <td></td>
                </tr>
            </table>
        </cc1:PanelBase>
    </div>
    </form>
</body>
</html>
