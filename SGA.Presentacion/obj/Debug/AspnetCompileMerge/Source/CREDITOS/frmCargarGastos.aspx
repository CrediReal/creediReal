<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmCargarGastos.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmCargarGastos" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">
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
          <table>
                 <tr>
                 
                    <td colspan="2" >
                        <cc1:PanelBase ID="PanelBase1" runat="server">
                            <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                        </cc1:PanelBase>
                        
                    </td>
                    
                </tr>
                 <tr>
                 
                    <td colspan="2" >
                        <cc1:BotonProcesar ID="BotonProcesar2" runat="server" OnClick="BotonProcesar2_Click" />
                        <asp:HiddenField ID="hIdCuenta" runat="server" />
                    </td>
                    
                </tr>
              <tr>
                    <td class="auto-style2" >
                        <cc1:PanelBase ID="PanelBase2" runat="server">
                            <cc1:LabelBase ID="LabelBase1" runat="server" style="font-weight: 700">Datos del crédito: </cc1:LabelBase>
                            <br />
                            <cc1:LabelBase ID="LabelBase8" runat="server">Tipo de crédito:</cc1:LabelBase>
                            &nbsp;&nbsp;
                            <cc1:TextBoxBase ID="txtTipoCredito" runat="server"></cc1:TextBoxBase>
                            <br />
                            <cc1:LabelBase ID="LabelBase3" runat="server">Moneda:</cc1:LabelBase>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <cc1:ComboBoxBase ID="cboMoneda" runat="server">
                            </cc1:ComboBoxBase>
                            <br />
                            <cc1:LabelBase ID="LabelBase4" runat="server">Monto:</cc1:LabelBase>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <cc1:NumberBox ID="txtMonto" runat="server"></cc1:NumberBox>
                            <br />
                            <cc1:LabelBase ID="LabelBase5" runat="server">Cuotas:</cc1:LabelBase>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <cc1:TextBoxBase ID="txtCuotas" runat="server"></cc1:TextBoxBase>
                        </cc1:PanelBase>
                    </td>
                    <td class="auto-style2" rowspan="3" >
                        <cc1:PanelBase ID="PanelBase3" runat="server" Height="278px" style="text-align: center">
                            <cc1:CheckBoxBase ID="chcAplicaTodo" runat="server" style="text-align: left" Text="Aplicar a todos" />
                            <br />
                            <cc1:GridViewBase ID="dtgPlanPagos" runat="server" AutoGenerateColumns="False" PageSize="20" Height="121px">
                                <Columns>
                                <asp:BoundField DataField="IdNum" HeaderText="Nro"></asp:BoundField>
                                <asp:BoundField DataField="Fecha_Desembolso" HeaderText="Fec. Prog"></asp:BoundField>
                                <asp:BoundField DataField="cProducto" HeaderText="Monto"></asp:BoundField>
                                <asp:BoundField DataField="nMonto" HeaderText="Gastos"></asp:BoundField>
                                <asp:BoundField DataField="Monto_Cuota" HeaderText="Mon. Total"></asp:BoundField>
                                <asp:BoundField DataField="nAtraso" HeaderText="Aplicar"></asp:BoundField>
                            </Columns>
                            </cc1:GridViewBase>
                        </cc1:PanelBase>
                    </td>
                </tr>
              <tr>
                    <td >
                        <cc1:PanelBase ID="PanelBase4" runat="server" style="text-align: left">
                            <cc1:LabelBase ID="LabelBase6" runat="server" style="font-weight: 700">Carga de gasto</cc1:LabelBase>
                            <br />
                            <cc1:LabelBase ID="LabelBase9" runat="server">Tipo de carga: </cc1:LabelBase>
                            &nbsp;&nbsp;&nbsp;
                            <cc1:ComboBoxBase ID="cboTipoGasto" runat="server">
                            </cc1:ComboBoxBase>
                            <br />
                            <cc1:LabelBase ID="LabelBase7" runat="server">Monto a cargar: </cc1:LabelBase>
                            &nbsp; <cc1:NumberBox ID="txtMontoCarga" runat="server"></cc1:NumberBox>
                        </cc1:PanelBase>
                    </td>
                </tr>
              <tr>
                    <td >
                        <cc1:CheckBoxBase ID="chcQuitGastos" runat="server" Text="Quitar gasto" />
                        <br />
                        <cc1:BotonProcesar ID="btnProcesar" runat="server" />
&nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" />
                    </td>
                </tr>
              <tr>
                    <td colspan="2" style="text-align: right" >
                        <cc1:BotonGrabar ID="btnGrabar" runat="server" />
                    </td>
                </tr>
              
            
          </table>



        </div>
    </form>
</body>
</html>