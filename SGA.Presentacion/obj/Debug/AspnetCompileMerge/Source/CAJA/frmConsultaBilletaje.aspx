<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmConsultaBilletaje.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmConsultaBilletaje" %>
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
        }
        .auto-style2 {
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
                 <td colspan="4">
                     <cc1:LabelBase ID="LabelBase2" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server"></cc1:TextBoxBase>    
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server"></cc1:TextBoxBase>
                 </td>
                 
             </tr>
             <tr>
                 <td colspan="6" style="text-align: left">
                     <cc1:LabelBase ID="LabelBase5" runat="server">Nombre: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="404px"></cc1:TextBoxBase>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" colspan="2" class="auto-style2">
                     <cc1:LabelBase ID="LabelBase11" runat="server">Fecha: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpProceso" runat="server" />
                 </td>
                 <td style="text-align: left" colspan="2" class="auto-style2">
                     <cc1:LabelBase ID="LabelBase12" runat="server">Colaborador: </cc1:LabelBase>
                     <cc1:ComboBoxUsuario ID="cboColaborador" runat="server" >
                     </cc1:ComboBoxUsuario>
                 </td>
                 <td style="text-align: right" colspan="2" class="auto-style2">
                     <cc1:BotonProcesar ID="BotonProcesar1" runat="server" OnClick="BotonProcesar1_Click" />
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center" colspan="3" class="auto-style2">
                      &nbsp;</td>
                 <td style="text-align: center" colspan="3">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td style="text-align: center" colspan="3" class="auto-style2">
                      <cc1:PanelBase ID="PanelBase1" runat="server">
                          <cc1:GridViewBase ID="dtgMonedas" runat="server" AutoGenerateColumns="False">
                              <Columns>
                                  <asp:BoundField DataField="cDescripcion" HeaderText="Denominación" />
                                  <asp:BoundField DataField="nCantidad" HeaderText="Cantidad" />
                                  <asp:BoundField DataField="nTotal" HeaderText="Total" />
                              </Columns>
                          </cc1:GridViewBase>
                      </cc1:PanelBase>


                    
                 </td>
                 <td style="text-align: center" colspan="3">
                     <cc1:PanelBase ID="PanelBase2" runat="server">
                         <cc1:GridViewBase ID="dtgBilletes" runat="server" AutoGenerateColumns="False">
                             <Columns>
                                 <asp:BoundField DataField="cDescripcion" HeaderText="Denominación" />
                                 <asp:BoundField DataField="nCantidad" HeaderText="Cantidad" />
                                 <asp:BoundField DataField="nTotal" HeaderText="Total" />
                             </Columns>
                         </cc1:GridViewBase>
                     </cc1:PanelBase>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left" class="auto-style1" colspan="3">
                     <cc1:LabelBase ID="LabelBase13" runat="server">TOTAL MONEDAS: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonMoneda" runat="server" DecimalPlaces="2">0.00</cc1:NumberBox>
                 </td>
                 <td style="text-align: left" class="auto-style1" colspan="3">
                     <cc1:LabelBase ID="LabelBase14" runat="server">TOTAL BILLETES: </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonBillete" runat="server" DecimalPlaces="2">0.00</cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center" class="auto-style1" colspan="6">
                     <cc1:LabelBase ID="LabelBase15" runat="server">TOTSL CORTE FRACCIONARIO S/. </cc1:LabelBase>
                     <cc1:NumberBox ID="txtMonTotal" runat="server" DecimalPlaces="2">0.00</cc1:NumberBox>
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center" colspan="6">
                     <cc1:BotonImprimir ID="btnImprimir" runat="server" />
&nbsp;&nbsp;&nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" />
                 </td>
             </tr>
        </table>
        <table>

        </table>
     </div>
    </form>
</body>
</html>