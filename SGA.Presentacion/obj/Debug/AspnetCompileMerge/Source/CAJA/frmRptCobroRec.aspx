<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmRptCobroRec.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmRptCobroRec" %>
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
            height: 30px;
        }
    </style>


    </head>
<body>
    <form id="form2" runat="server">
    <div>
        <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
         </h2>    
    </div>
        <table align="center">
            <tr>
                <td>                  
                     
                    <cc1:LabelBase ID="LabelBase1" runat="server">Agencias: </cc1:LabelBase>
                    <cc1:ComboBoxBase ID="cboAgencias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboAgencias_SelectedIndexChanged">
                    </cc1:ComboBoxBase>
                     
                 </td>
                <td>                  
                     
                    <cc1:LabelBase ID="LabelBase2" runat="server">Coloborador: </cc1:LabelBase>
                    <cc1:ComboBoxBase ID="cboColaborador" runat="server" AutoPostBack="True">
                    </cc1:ComboBoxBase>
                     
                 </td>
            </tr>

            <tr>
                <td colspan="2" style="text-align: center; font-weight: 700;">                  
                     
                    <cc1:LabelBase ID="LabelBase3" runat="server">RANGOS DE FECHA</cc1:LabelBase>
                     
                 </td>
            </tr>

            <tr>
                <td>                  
                     
                    <cc1:LabelBase ID="LabelBase4" runat="server">Fecha Inicio: </cc1:LabelBase>
                    <cc1:CalendarioBase ID="dtpFecIni" runat="server" />
                     
                 </td>
                <td>                  
                     
                    <cc1:LabelBase ID="LabelBase5" runat="server">Fecha Fin: </cc1:LabelBase>
                    <cc1:CalendarioBase ID="dtpFecFin" runat="server" />
                     
                 </td>
            </tr>

            <tr>
                <td colspan="2" style="text-align: center; font-weight: 700;">                  
                     
                    <cc1:LabelBase ID="LabelBase6" runat="server">FILTROS</cc1:LabelBase>
                     
                 </td>
            </tr>

            <tr>
                <td class="auto-style1">                  
                     
                    <cc1:LabelBase ID="LabelBase7" runat="server">Tipo Recibo: </cc1:LabelBase>
                    <cc1:ComboBoxBase ID="cboTipRec" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipRec_SelectedIndexChanged">
                    </cc1:ComboBoxBase>
                     
                 </td>
                <td class="auto-style1">                  
                     
                    <cc1:LabelBase ID="LabelBase9" runat="server">Concepto de Recibo: </cc1:LabelBase>
                     
                    <cc1:ComboBoxBase ID="cboConcepto" runat="server" >
                    </cc1:ComboBoxBase>
                     
                 </td>
            </tr>

            <tr>
                <td colspan="2" style="text-align: center">                  
                     
                    <cc1:LabelBase ID="LabelBase8" runat="server">Moneda: </cc1:LabelBase>
                    <cc1:ComboBoxBase ID="cboMoneda" runat="server">
                    </cc1:ComboBoxBase>
                     
                 </td>
            </tr>

            <tr>
                <td colspan="2">                  
                     
                    &nbsp;</td>
            </tr>

            <tr>
                <td colspan="2" style="text-align: center">                  
                     
                    <cc1:BotonImprimir ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" />
                     
                 </td>
            </tr>

        </table>

    </form>
</body>
</html>

