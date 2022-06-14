<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmEmisionRecibos.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmEmisionRecibos" %>
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
        .auto-style2 {
            height: 1px;
            text-align: left;
        }
    </style>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Esta Seguro de Extornar el Recibo?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            <cc1:LabelBase ID="lblOpcion" runat="server">titulo: </cc1:LabelBase>
        </h2>
    
    </div>
    <div align="center" runat="server" class="pnlBase" id="pnlRecibo">
           <table>
               <tr>
                    <td colspan="3" style="font-weight: 700; text-align: center" >
                        <cc1:LabelBase ID="LabelBase2" runat="server">DATOS DEL SISTEMA</cc1:LabelBase>

                    </td>
                   
               </tr>
               <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase1" runat="server">Fecha: </cc1:LabelBase>
                        <cc1:CalendarioBase ID="dtpFechaSis" runat="server" Enabled="False" />
                    </td>
                    <td>
                        <cc1:LabelBase ID="LabelBase5" runat="server">Codigo: </cc1:LabelBase>
                        <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>
                        
                    </td>
                    <td style="text-align: left">
                        <cc1:CheckBoxBase ID="chcBuscar" runat="server" Text="Buscar recibo" AutoPostBack="True" OnCheckedChanged="chcBuscar_CheckedChanged" /> 

                    </td>
               </tr>
               <tr>
                    <td colspan="2" class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase3" runat="server">Usuario: </cc1:LabelBase>
                        &nbsp;<cc1:TextBoxBase ID="txtUsuario" runat="server" Width="180px" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td rowspan="2">
                        <cc1:LabelBase ID="LabelBase6" runat="server">N° recibo</cc1:LabelBase>
                        <cc1:TextBoxBase ID="txtNroRec" runat="server" Enabled="False"></cc1:TextBoxBase>

                    &nbsp;<cc1:BotonBuscar ID="BotonBuscar1" runat="server" OnClick="BotonBuscar1_Click" />

                    </td>
               </tr>
               <tr>
                    <td colspan="2" class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase4" runat="server">Nombre: </cc1:LabelBase>
                        <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="180px" Enabled="False"></cc1:TextBoxBase>
                    </td>
               </tr>
               <tr>
                    <td colspan="3" style="font-weight: 700; text-align: center" >
                        <cc1:LabelBase ID="LabelBase7" runat="server" style="text-align: left">DATOS GENERALES</cc1:LabelBase>
                    </td>
               </tr>
               <tr>
                    <td colspan="2" class="auto-style2" >
                        <cc1:LabelBase ID="LabelBase8" runat="server">Tipo de recibo: </cc1:LabelBase>
                        <cc1:ComboBoxBase ID="cboTipRec" runat="server" Height="16px" Width="130px" Enabled="False" OnSelectedIndexChanged="cboTipRec_SelectedIndexChanged" AutoPostBack="True"></cc1:ComboBoxBase>
                    </td>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase11" runat="server">Moneda: </cc1:LabelBase>
                        <cc1:ComboBoxBase ID="cboMoneda" runat="server" Height="16px" Width="130px" Enabled="False"></cc1:ComboBoxBase>
                    </td>
                </tr>
               <tr>
                    <td colspan="2" class="auto-style2" ><cc1:LabelBase ID="LabelBase9" runat="server">Concepto: </cc1:LabelBase>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <cc1:ComboBoxBase ID="cboConcepto" runat="server" Height="16px" Width="130px" Enabled="False" OnSelectedIndexChanged="cboConcepto_SelectedIndexChanged"></cc1:ComboBoxBase>

                    </td>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase12" runat="server">Detalle: </cc1:LabelBase>
                        &nbsp;&nbsp;
                        <cc1:ComboBoxBase ID="cboDetalle" runat="server" Height="16px" Width="130px" Enabled="False" OnSelectedIndexChanged="cboDetalle_SelectedIndexChanged"></cc1:ComboBoxBase>
                    </td>
                </tr>
               <tr>
                    <td colspan="2" class="auto-style2" ><cc1:LabelBase ID="lblAge" runat="server">Ag. destino: </cc1:LabelBase>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <cc1:ComboBoxOficina ID="cboAgencias" runat="server" OnSelectedIndexChanged="cboAgencias_SelectedIndexChanged">
                        </cc1:ComboBoxOficina>
                    </td>
                    <td>&nbsp;</td>
                </tr>
               <tr>
                    <td colspan="3" class="auto-style2" style="font-weight: 700" >

                    </td>
                </tr>

               <tr>
                    <td colspan="3" >
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" EnableTheming="False" />
                    </td>
                </tr>
               <tr>
                    <td >
                        <cc1:LabelBase ID="LabelBase13" runat="server">Monto recibido: </cc1:LabelBase>
                        <cc1:NumberBox ID="txtMonRec" runat="server" Enabled="False" DecimalPlaces="2" >0.00</cc1:NumberBox>
                    </td>
                    <td>
                        
                        &nbsp;</td>
                    <td>
                        <cc1:LabelBase ID="LabelBase15" runat="server">Total recibo: </cc1:LabelBase>
                        <cc1:NumberBox ID="txtTotRec" runat="server" Enabled="False" OnTextChanged="txtMonRec_TextChanged" DecimalPlaces="2" >0.00</cc1:NumberBox>
                    </td>
                </tr>
               <tr>
                    <td colspan="3" style="text-align: left" >
                        <cc1:LabelBase ID="LabelBase16" runat="server">Sustento: </cc1:LabelBase>
                        <cc1:TextBoxBase ID="txtSustento" runat="server" Height="50px" Width="590px" Enabled="False"></cc1:TextBoxBase>
                    </td>
                </tr>

               <tr>
                    <td colspan="3" >
                        <cc1:BotonImprimir ID="btnImprimir" runat="server" Visible="False" />
                        &nbsp;<cc1:BotonNuevo ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" />
                        &nbsp;<cc1:BotonGrabar ID="btnGrabar" runat="server" OnClick="BotonGrabar1_Click" />
                        &nbsp;<cc1:BotonCancelar ID="btnCancelar" runat="server" />
                        &nbsp;<cc1:BotonQuitar ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" OnClientClick="Confirm()" />
                    </td>
                </tr>
           </table>
     </div>
                
 <asp:HiddenField ID="hUsuario" runat="server" />
         <asp:HiddenField ID="hPerfil" runat="server" />
    </form>
</body>
</html>
