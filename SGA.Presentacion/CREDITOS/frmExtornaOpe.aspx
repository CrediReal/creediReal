<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmExtornaOpe.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmExtornaOpe" %>
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

                        <cc1:GridViewBase ID="dtgSolExt" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandArgument='<%# Eval("IdKardex")%>' OnClick="BotonConsultar1_Click" Text="Seleccionar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idSolAproba" HeaderText="Solicitud" />
                                 <asp:BoundField DataField="dFecAprSol" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec.Aproba" />
                                 <asp:BoundField DataField="IdKardex" HeaderText="Nro Ope." />
                                <asp:BoundField DataField="cMoneda" HeaderText="Moneda" />
                                <asp:BoundField DataField="nMontoOperacion" HeaderText="Monto Ope." />
                                <asp:BoundField DataField="cTipoOperacion" HeaderText="Tipo Ope." />
                                                     
                            </Columns>
                        </cc1:GridViewBase>
                    </td>
                </tr>
                <tr>
                    
                    <td colspan="3">
                        <asp:HiddenField ID="HidKardex" runat="server" />
                        </td>
                </tr>
                 <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase1" runat="server">Número de kardex:</cc1:LabelBase></td>
                    <td class="auto-style2">                        
                        <cc1:NumberBox ID="nudNroKardex" runat="server" DecimalPlaces="0" MaxLength="6" Enabled="False"></cc1:NumberBox>
                    &nbsp;<cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" Visible="False" />
                    </td>
                    <td></td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase5" runat="server">Número de crédito:</cc1:LabelBase></td>
                    <td class="auto-style2">                        
                        <cc1:NumberBox ID="txtBase1" runat="server" DecimalPlaces="0" MaxLength="6" Enabled="False"></cc1:NumberBox>
                    &nbsp;</td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase9" runat="server">Cliente:</cc1:LabelBase></td>
                    <td class="auto-style2">                        
                        <cc1:TextBoxBase ID="txtBase2" runat="server" Width="315px" Enabled="False"></cc1:TextBoxBase>
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
                        <cc1:TextBoxBase ID="txtBase6" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase6" runat="server">Tipo Operación:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:TextBoxBase ID="txtBase7" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase7" runat="server">Tipo de Moneda:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:TextBoxBase ID="txtBase8" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase8" runat="server">Monto Operación:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:TextBoxBase ID="txtBase9" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase3" runat="server">Motivo Extorno:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        
                        <cc1:TextBoxBase ID="txtBase10" runat="server" Enabled="False"></cc1:TextBoxBase>
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
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase4" runat="server">Operación:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:TextBoxBase ID="txtBase3" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase10" runat="server">Fecha:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:TextBoxBase ID="txtBase4" runat="server" Enabled="False"></cc1:TextBoxBase>
                    </td>
                    <td></td>
                </tr>
              <tr>
                    <td class="auto-style1" >
                        <cc1:LabelBase ID="LabelBase11" runat="server">Usuario:</cc1:LabelBase></td>
                    <td class="auto-style2">
                        
                        <cc1:TextBoxBase ID="txtBase5" runat="server" Enabled="False"></cc1:TextBoxBase>
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
                        <cc1:BotonCancelar ID="btnCancelar" runat="server" OnClick="BotonCancelar1_Click" />
                    &nbsp;<cc1:BotonGrabar ID="btnExtorno" runat="server" OnClick="BotonGrabar1_Click" />
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