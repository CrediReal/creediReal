<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCabecera.aspx.cs" Inherits="SGA.Presentacion.frmCabecera" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
</head>
<body>
    <header>
	    <div class="title">
        <h1 class="fontface" id="title">SISTEMA DE GESTIÓN CREDITICIA</h1>
        </div>
        <div align="right" valign="top">
            <div style="font-size:9px; font-family:Arial; color:white">
                Desarrollado por Fast Solutions SAC<br />
                © <%= DateTime.Now.Year.ToString() %> | Todos los Derechos Reservados<br />
                Sistema de Gestión Crediticia
            </div>
        </div>
    </header>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>