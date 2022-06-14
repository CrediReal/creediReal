<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmGenReporte.aspx.cs" Inherits="SGA.Presentacion.frmGenReporte" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporteador</title>
    <link href="Styles/Site.css" rel="stylesheet" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server">
        <br />
    <asp:scriptmanager id="scptManager" runat="server" enablepagemethods="true"/>
        
    <div align="center">
        <iframe id="frmPrint" name="IframeName" width="500" 
  height="200" runat="server" 
  style="display: none" runat="server"></iframe>
        
        <asp:RadioButtonList ID="rbnOrientacion" runat="server" CssClass="lblBase" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">Horizontal</asp:ListItem>
            <asp:ListItem Selected="True" Value="2">Vertical</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <cc1:BotonImprimir ID="btnPrint" runat="server" OnClick="btnPrint_Click" />
        <cc1:BotonAtras ID="BotonAtras1" runat="server" OnClick="BotonAtras1_Click" /><cc1:BotonSalir ID="BotonSalir1" runat="server" OnClick="BotonSalir1_Click" />
        <p>
    </div>
    <div>
            <rsweb:ReportViewer ID="rptViewerLocal" ShowPrintButton="true" runat="server" Width="100%" Height="700px"  SizeToReportContent="true" ShowBackButton="False" ShowFindControls="False" ShowRefreshButton="False" ShowZoomControl="False">
            </rsweb:ReportViewer>
    </div>
    
    </form>
</body>
</html>
