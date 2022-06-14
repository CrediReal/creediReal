<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmPlanPagos.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmPlanPagos" %>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
            </h2>
        </div>
        <div align="center">
            <table>
                <tr>
                    <td colspan="2" >
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                    </td>
                    <td></td>
                </tr>
                
                 <tr>
                    <td>                        
                        <cc1:LabelBase ID="LabelBase1" runat="server">Monto:</cc1:LabelBase>
                    </td>
                    <td>
                        <cc1:TextBoxBase ID="txtMonto" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase2" runat="server">Cuotas:</cc1:LabelBase></td>
                    <td>
                        <cc1:TextBoxBase ID="nudCuotas" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase6" runat="server">Día de pago:</cc1:LabelBase></td>
                    <td>
                        <cc1:TextBoxBase ID="nudPlazo" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase3" runat="server">Fecha desembolso:</cc1:LabelBase></td>
                    <td>
                        <cc1:CalendarioBase ID="dtFechaDesembolso" runat="server" Enabled="False" />

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase4" runat="server">Días de gracias:</cc1:LabelBase></td>
                    <td>
                        <cc1:TextBoxBase ID="txtDiasGracia" runat="server" Enabled="False"></cc1:TextBoxBase>                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase5" runat="server">Tasa interés:</cc1:LabelBase></td>
                    <td>
                        <cc1:TextBoxBase ID="TxtTasa" runat="server" Enabled="False"></cc1:TextBoxBase>                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" >
                       
                        &nbsp;<cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" />
                    &nbsp;<cc1:BotonProcesar ID="BotonProcesar1" runat="server" OnClick="BotonProcesar1_Click" />
                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3" >
                        <cc1:GridViewBase ID="dtgBase1" runat="server" AutoGenerateColumns="false">
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