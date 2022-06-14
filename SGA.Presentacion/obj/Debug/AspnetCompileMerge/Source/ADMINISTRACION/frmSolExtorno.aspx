<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmSolExtorno.aspx.cs" Inherits="SGA.Presentacion.ADMINISTRACION.frmSolExtorno" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.2.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
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
            text-align: right;
        }
        .auto-style2 {
            text-align: left;
        }
        .auto-style4 {
            /*font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;*/
        font-family: Corbel;
            font-size: 12px;
            height: 20px;
            border: 1px solid #d4d4d4;
            text-decoration: underline;
        }
        .auto-style5 {
            text-align: center;
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
          <table class="pnlBase">

                 <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase1" runat="server">Número de kardex:</cc1:LabelBase></td>
                    <td class="auto-style2">                        
                        <cc1:NumberBox ID="nudNroKardex" runat="server" DecimalPlaces="0" MaxLength="6"></cc1:NumberBox>
                    &nbsp;<cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style4" >
                        <strong>Datos de la operación</strong></td>
                    <td class="auto-style2">                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase2" runat="server">Módulo:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="txtModulo" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase6" runat="server">Tipo Operación:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:ComboBoxBase ID="cboTipoOperacion" runat="server" Enabled="False">
                        </cc1:ComboBoxBase>
                        
                        
                    </td>
                    <td></td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase7" runat="server">Tipo de Moneda:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:ComboBoxBase ID="cboMoneda" runat="server" Enabled="False">
                        </cc1:ComboBoxBase>
                        
                        
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase8" runat="server">Monto Operación:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:NumberBox ID="txtMonOpe" runat="server" MaxLength="8" DecimalPlaces="2" Enabled="False">0.00</cc1:NumberBox>
                        
                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase3" runat="server">Motivo Extorno:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:ComboBoxBase ID="cboMotivoExtorno" runat="server">
                        </cc1:ComboBoxBase>
                        
                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase4" runat="server">Sustento Extorno:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:TextBoxBase ID="txtSustento" runat="server" TextMode="MultiLine" Width="274px"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" ></td>
                    <td class="auto-style2">
                        
                        &nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2" >
                        <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    &nbsp;<cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3" >
                        &nbsp;</td>
                </tr>
                
                </table>
        </div>
    </form>
</body>
</html>