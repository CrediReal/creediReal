<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmBarPri.aspx.cs" Inherits="SGA.Presentacion.frmBarPri" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2
        {
            font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
            font-size: 12px;
            width: 635px
        }
    </style>
</head>
<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server" method="post">
        <table border="0" >
			<tr>
				<td height="20" width="80%">
					<strong><asp:Label id="lblUsuario" CssClass="lblBase" runat="server">Administrador del Portal</asp:Label> &nbsp;-&nbsp; <asp:Label id="lblNombres" runat="server" CssClass="lblBase">Nombres</asp:Label>
                    <asp:Label id="lblFecha" runat="server" CssClass="lblBase">fecha</asp:Label></strong>
				</td>					
				<td align="right" height="20" >
                    <asp:Button id="btnMapaProceso" runat="server" Text="Mapa Proceso" CssClass = "botonD" Visible="false"
                        onclick="btnMapaProceso_Click"></asp:Button>
                </td>
                <td align="right" height="20">
					<asp:Button id="btnAtras" runat="server" Text="Inicio" CssClass="botonD" 
                        onclick="btnAtras_Click"></asp:Button></td>
                <td align="right" height="20">
					<asp:Button id="btnCerrSession" runat="server" Text="Cerrar Sesión" 
                        CssClass="botonD" onclick="btnCerrSession_Click"></asp:Button>
                </td>
			</tr>
            <tr>
                <td style="height:10px"></td>
                <td></td>
                <td></td>
                <td></td>                
            </tr>
		</table>
    </form>
</body>
</html>
