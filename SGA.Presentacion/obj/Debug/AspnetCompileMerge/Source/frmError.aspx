<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmError.aspx.cs" Inherits="SGA.Presentacion.frmError" %>
<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="lblBase">
        <img src="Styles/error1.png" />&nbsp;&nbsp;&nbsp;&nbsp;  Ocurrió un error inesperado presione el botón "Atrás" para ir al INICIO<br />
        <br />
        Error: <cc1:LabelBase ID="lblError" runat="server" Text="Error" ForeColor="Red"></cc1:LabelBase>
        <br />
        <br />
        
        <p align="center"
            ><cc1:BotonAtras ID="BotonAtras1" runat="server" OnClick="BotonAtras1_Click" /></p>
        
    </div>
    </form>
</body>
</html>