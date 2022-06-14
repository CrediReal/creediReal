<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmExtornoDesembolso.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmExtornoDesembolso" %>
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
                    
                    <td colspan="3">

                        &nbsp;</td>
                </tr>
                <tr>
                    
                    <td colspan="3">
                        <asp:HiddenField ID="HidKardex" runat="server" />
                        </td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase5" runat="server">Número de crédito:</cc1:LabelBase></td>
                    <td class="auto-style2">                        
                        <cc1:NumberBox ID="txtcuenta" runat="server" DecimalPlaces="0" MaxLength="6"></cc1:NumberBox>
                    &nbsp;<cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style1" >
                        &nbsp;</td>
                    <td class="auto-style2">                        
                        &nbsp;</td>
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
                        <cc1:LabelBase ID="LabelBase2" runat="server">Fecha desembolso:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        <cc1:TextBoxBase ID="dtpFecDes" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase6" runat="server">Capital:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:TextBoxBase ID="txtMonto" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase7" runat="server">Usuario:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False" Width="312px"></cc1:TextBoxBase>
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
                        <cc1:BotonCancelar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" />
                    &nbsp;<cc1:BotonGrabar ID="btnExtorno" runat="server" OnClick="btnExtorno_Click" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3" >
                        &nbsp;</td>
                </tr>
                
                </table>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>