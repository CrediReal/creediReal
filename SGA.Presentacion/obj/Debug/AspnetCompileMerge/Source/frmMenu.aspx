<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmMenu.aspx.cs" Inherits="SGA.Presentacion.frmMenu" %>

<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
     <style>
         .treeViewBase
            {   
                font-family: 'Segoe UI Semilight', 'Open Sans', Verdana, Arial, Helvetica, sans-serif;
                color: #000000;
             }
         .treeNodeBase
         {
                background-color: #ffffff;
                color: #000000;
                font-family: 'Segoe UI Semilight', 'Open Sans', Verdana, Arial, Helvetica, sans-serif;
                font-size:11px;

            }
         .treeNodeSeleccionado
            {
              background-color: #e2071b;  
              color: #fff; 
            }
     </style>


</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="text-align: left">
                    <cc2:ComboBoxBase ID="cboModulo" runat="server" CssClass="cmbBase" Width="126px" OnSelectedIndexChanged="cboModulo_SelectedIndexChanged" AutoPostBack="true"></cc2:ComboBoxBase>
        <br />
                </td>
            </tr>
            <tr>
                <td>
                     <cc2:TreeViewBase ID="tvMenu" runat="server" CssClass="treeNodeBase" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged"  Width="412px" ExpandDepth="1">
            
        </cc2:TreeViewBase>
                </td>
            </tr>
        </table>
        
       
     
    </form>
</body>
</html>
