<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="frmReporteVentasData.aspx.cs" Inherits="SGA.Presentacion.frmReporteVentasData" %>
<%@ Register TagPrefix="cc1" Namespace="SGA.Controles" Assembly="SGA.Controles" %>
<%@ Register Src="~/ConBusCli.ascx" TagPrefix="uc1" TagName="ConBusCli" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.4/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.3/themes/base/jquery-ui.css" type="text/css" media="all" />

    <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="js/bootstrap.min.js"></script>

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
        <div align="left">
            <cc1:PanelBase ID="panelContenido" runat="server">
        <div class="container">
  <h3>REPORTE DE VENTAS</h3>
  <%--<p><strong>Note:</strong> The <strong>data-parent</strong> attribute makes sure that all collapsible elements under the specified parent will be closed when one of the collapsible item is shown.</p>--%>
            <table>
            <tr><td>
                <p><strong>Año : </strong>
                    <cc1:ComboBoxBase ID="cboAnios" runat="server">
                        <asp:ListItem>2017</asp:ListItem>
                        <asp:ListItem Selected="True">2016</asp:ListItem>
                        <asp:ListItem>2015</asp:ListItem>
                        <asp:ListItem>2014</asp:ListItem>
                        <asp:ListItem>2013</asp:ListItem>
                        <asp:ListItem>2012</asp:ListItem>
                        <asp:ListItem>2011</asp:ListItem>
                        <asp:ListItem>2010</asp:ListItem>
                        <asp:ListItem>2009</asp:ListItem>
                    </cc1:ComboBoxBase></p>
                    </td>
                </tr>
        </table>
  <div class="panel-group" id="accordion">
    <div class="panel panel-default">
      <div class="panel-heading">
        <h4 class="panel-title">
          <a data-toggle="collapse" data-parent="#accordion" href="#collapse1"> Ventas por Colaborador</a>
        </h4>
      </div>
        
      <div id="collapse1" class="panel-collapse collapse in">
        <div class="panel-body">
            <table>
                
                <tr>
                    <td>
                        <br />
                        <p class="glyphicon-user">Colaboradores - <cc1:CheckBoxBase ID="chbTodoCol" Text="Todos" runat="server" AutoPostBack="True" Checked="True"  TextAlign="Left" OnCheckedChanged="chbTodoCol_CheckedChanged" /></p>
                        
                        <asp:CheckBoxList ID="chblTrabajadores" runat="server" RepeatColumns="5" aria-labelledby="dLabel" Font-Names="Calibri" >
                        </asp:CheckBoxList>
                    </td>
                  </tr>
                <tr>
                    <td>
                         <br />
                        <p class="glyphicon-user">Oficinas -<cc1:CheckBoxBase ID="chbTodoOfi" Text="Todos" runat="server" AutoPostBack="True" Checked="True"  TextAlign="Left" OnCheckedChanged="chbTodoOfi_CheckedChanged"  /></p>
                        <asp:CheckBoxList ID="chblOficinas" runat="server"  RepeatDirection="Horizontal" Font-Names="Calibri">
                        </asp:CheckBoxList>
                    </td>
                  </tr>
                 <tr>

                     <td>
                         <br />
                         <cc1:BotonImprimir ID="BotonImprimir1" runat="server" OnClick="BotonImprimir1_Click" />
                     </td>

                 </tr>
            </table>

        </div>
      </div>
    </div>
    <div class="panel panel-default">
      <div class="panel-heading">
        <h4 class="panel-title">
          <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Ventas por Sucursal</a>
        </h4>
      </div>
      <div id="collapse2" class="panel-collapse collapse in">
        <div class="panel-body">
            <table>
                <tr>
                    <td>
                        <br />
                        <p class="glyphicon-user">Mes - <cc1:CheckBoxBase ID="chbMesTodos" Text="Todos" runat="server" AutoPostBack="True" Checked="True"  TextAlign="Left" OnCheckedChanged="chbMesTodos_CheckedChanged" /></p>
                        
                        <asp:CheckBoxList ID="chblMes" runat="server" RepeatColumns="4" aria-labelledby="dLabel" Font-Names="Calibri" >
                        </asp:CheckBoxList>
                    </td>
                  </tr>
                <tr>
                    <td>
                        <br />
                        <p class="glyphicon-user">Categoría  - <cc1:CheckBoxBase ID="chbTodosCategoria" Text="Todos" runat="server" AutoPostBack="True" Checked="True"  TextAlign="Left" OnCheckedChanged="chbTodosCategoria_CheckedChanged" /></p>
                        
                        <asp:CheckBoxList ID="chblCategorias" runat="server" RepeatDirection="Horizontal" Font-Names="Calibri" >
                        </asp:CheckBoxList>
                    </td>
                  </tr>
                <tr>
                    <td>
                        <cc1:BotonImprimir ID="BotonImprimir2" runat="server" OnClick="BotonImprimir2_Click" />
                    </td>
                </tr>
            </table>

        </div>
      </div>
    </div>
    <div class="panel panel-default">
      <div class="panel-heading">
        <h4 class="panel-title">
          <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Ventas por Cliente</a>
        </h4>
      </div>
      <div id="collapse3" class="panel-collapse collapse in">
        <div class="panel-body">
            <table>
                <tr>
                    
                    <td>
                        <uc1:ConBusCli runat="server" ID="ConBusCli" />
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <cc1:BotonImprimir ID="BotonImprimir3" runat="server" OnClick="BotonImprimir3_Click" />
                    </td>
                </tr>
            </table>

        </div>
      </div>
    </div>
  </div>
</div>

        
                
            </cc1:PanelBase>
        </div>
    </form>
</body>
</html>
