<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmSaldosInstit.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmSaldosInstit" %>
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
        <table align="center">
             <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase1" runat="server" style="float: left">Fecha de Proceso: </cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" />
                 </td>
                 <td>
                     <cc1:BotonProcesar ID="btnProcesar" runat="server" OnClick="btnProcesar_Click" />
                 </td>
             </tr>

             <tr>
                 <td colspan="2">
                     <cc1:GridViewBase ID="dtgSaldoIntit" runat="server" AutoGenerateColumns="False">
                         <Columns>
                            <asp:BoundField DataField="cNombreAge" HeaderText="Agencias" />
                            <asp:BoundField DataField="nMontoCieSol" HeaderText="Saldo cierre" />
                            <asp:BoundField DataField="cEstado" HeaderText="Estado" />
                         </Columns>
                     </cc1:GridViewBase>
                 </td>
             </tr>

             <tr>
                 <td style="text-align: right" colspan="2">
                                    
                     &nbsp;
                                                         
                     &nbsp;
                     &nbsp;&nbsp;</td>
             </tr>

        </table>
     </div>
    </form>
</body>
</html>

