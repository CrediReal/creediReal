<%@ Page Language="C#" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeBehind="frmConsultaCieOpe.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmConsultaCieOpe" %>
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
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 51px;
        }
        .auto-style2 {
            height: 28px;
        }
        .auto-style5 {
            height: 30px;
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
        <table>
            <tr>
                 <td colspan="6" style="font-weight: 700; text-align: center;">
                     <cc1:LabelBase ID="LabelBase8" runat="server">DATOS DE ORIGEN</cc1:LabelBase>
                 </td>
             </tr>
            <tr>
                 <td class="auto-style1">
                     <cc1:LabelBase ID="LabelBase1" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" />
                 </td>
                 <td colspan="3">
                     <cc1:LabelBase ID="LabelBase2" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
                 
                 <td>
                     <cc1:BotonProcesar ID="btnProcesar" runat="server" OnClick="btnProcesar_Click" />
                 </td>
                 
             </tr>
             <tr>
                 <td colspan="6" style="text-align: left" class="auto-style5">
                     <cc1:LabelBase ID="LabelBase5" runat="server">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="404px" Enabled="False"></cc1:TextBoxBase>
                 </td>
             </tr>
             <tr>
                 <td colspan="3" style="text-align: left" class="auto-style2">
                     <cc1:LabelBase ID="LabelBase11" runat="server">Fecha: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpProceso" runat="server" />
                 </td>
                 <td colspan="3" style="text-align: left" class="auto-style2">
                     <cc1:LabelBase ID="LabelBase12" runat="server">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="cboColaborador" runat="server" OnSelectedIndexChanged="cboColaborador_SelectedIndexChanged">
                     </cc1:ComboBoxUsuario>
                 </td>
             </tr>
             <tr>
                 <td colspan="6" style="text-align: left" class="auto-style2">
                     <cc1:LabelBase ID="LabelBase13" runat="server">SALDO INICIAL S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtSalIniSol" runat="server" DecimalPlaces="2" Width="163px">0.00</cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center" colspan="2">
                     <cc1:LabelBase ID="LabelBase9" runat="server">INGRESO</cc1:LabelBase>
                 </td>
                 <td style="text-align: center" colspan="4">
                     <cc1:LabelBase ID="LabelBase10" runat="server">EGRESO</cc1:LabelBase>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" colspan="2">
                      <cc1:GridViewBase ID="dtgIngSoles" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="cTipoOperacion" HeaderText="Tipo operación" />
                            <asp:BoundField DataField="nMontoOperacion" HeaderText="Total operación" ReadOnly="True" />
                            
                        </Columns>
                        <PagerSettings Position="TopAndBottom" />
                    </cc1:GridViewBase>


                    
                 </td>
                 <td style="text-align: left" colspan="4">
                      <cc1:GridViewBase ID="dtgEgrSoles" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="cTipoOperacion" HeaderText="Tipo operación" />
                            <asp:BoundField DataField="nMontoOperacion" HeaderText="Total operación" ReadOnly="True" />
                            
                        </Columns>
                        <PagerSettings Position="TopAndBottom" />
                    </cc1:GridViewBase>


                    
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" class="auto-style5" colspan="3">
                     <cc1:LabelBase ID="LabelBase14" runat="server">TOTAL INGRESOS: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonIngSol" runat="server" DecimalPlaces="2">0.00</cc1:NumberBox>
                 </td>
                 <td style="text-align: left" class="auto-style5" colspan="3">
                     <cc1:LabelBase ID="LabelBase15" runat="server">TOTAL EGRESOS: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonEgrSol" runat="server" DecimalPlaces="2">0.00</cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center" class="auto-style5" colspan="6">
                     <cc1:LabelBase ID="LabelBase16" runat="server">SALDOS FINAL S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtSalFinSol" runat="server" DecimalPlaces="2" style="font-weight: 700">0</cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" class="auto-style5" colspan="3">
                     <cc1:LabelBase ID="LabelBase17" runat="server">SALDO FINAL CORTE S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtCortSoles" runat="server" DecimalPlaces="2" style="font-weight: 700">0.00</cc1:NumberBox>
                 </td>
                 <td style="text-align: left" class="auto-style5" colspan="3">
                     <cc1:LabelBase ID="LabelBase18" runat="server">DIFERENCIA S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtDifSoles" runat="server" DecimalPlaces="2" style="font-weight: 700">0.00</cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center" colspan="6">
                     <asp:Button ID="btnImprimir1" runat="server" Text="Detallle" OnClick="btnImprimir1_Click" CssClass="botonD" Height="40px" Width="100px" />
                     &nbsp;<asp:Button ID="btnImprimir" runat="server" Text="Resumen"  OnClick="btnImprimir_Click" CssClass="botonD" Height="40px" Width="100px"/>

&nbsp;&nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" />
                 </td>
             </tr>
        </table>
        <table>

        </table>
     </div>
         <asp:HiddenField ID="hUsuario" runat="server" />

    </form>
</body>
</html>
