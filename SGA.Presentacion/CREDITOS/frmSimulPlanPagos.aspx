<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmSimulPlanPagos.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmSimulPlanPagos" %>
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
                        <cc1:LabelBase ID="LabelBase1" runat="server">Monto:</cc1:LabelBase></td>
                    <td class="auto-style2">                        
                        <cc1:NumberBox ID="txtMonto" runat="server" DecimalPlaces="2"></cc1:NumberBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase2" runat="server">Cuotas:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        <cc1:NumberBox ID="nudCuotas" runat="server" MaxLength="3">30</cc1:NumberBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase6" runat="server">Frecuencia de pago:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:NumberBox ID="nudPlazo" runat="server" MaxLength="2">1</cc1:NumberBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase3" runat="server">Fecha desembolso:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        <cc1:CalendarioBase ID="dtFechaDesembolso" runat="server" />

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase4" runat="server">Días de gracias:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:NumberBox ID="txtDiasGracia" runat="server" MaxLength="3"></cc1:NumberBox>                  
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase5" runat="server">Tasa interés:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:NumberBox ID="TxtTasa" runat="server" DecimalPlaces="4" MaxLength="6">0.0000</cc1:NumberBox>                   
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    </td>
                    <td class="auto-style2">
                        <cc1:BotonProcesar ID="BotonProcesar1" runat="server" OnClick="BotonProcesar1_Click" />                       
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3" >
                        <cc1:GridViewBase ID="dtgBase1" runat="server" AutoGenerateColumns="false" >
                            <columns >
                                
                                <asp:BoundField DataField ="cuota" HeaderText ="N° Cuota" />
                                <asp:BoundField DataField ="fecha" HeaderText = "Fecha Pago" DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField ="dias" HeaderText = "Frecuencia Pago"/>
                                <asp:BoundField DataField ="capital" HeaderText = "Capital" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField ="interes" HeaderText = "Interés" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField ="imp_cuota" HeaderText = "Importe de Cuota" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                            </columns >
                        </cc1:GridViewBase>
                        
                    </td>
                </tr>
                
                </table>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>