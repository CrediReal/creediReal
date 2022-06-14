<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVoucher.aspx.cs" Inherits="SGA.Presentacion.frmVoucher" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporteador</title>
   <%-- <link href="Styles/Site.css" rel="stylesheet" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />--%>

</head>

<body>
    <form id="form1" runat="server">
        <br />
        
    <div align="left">
        <iframe id="frmPrint" name="IframeName" width="500" 
  height="200" runat="server" 
  style="display: none" runat="server"></iframe>
        
        <br />
        <cc1:BotonImprimir ID="btnPrint" runat="server" OnClick="btnPrint_Click" />
        <cc1:BotonSalir ID="BotonSalir1" runat="server" OnClick="BotonSalir1_Click" />
        <p>
    </div>
    
    </form>
</body>
</html>