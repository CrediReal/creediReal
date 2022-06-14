<%@ Page Language="C#"  MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmConfirmarHab.aspx.cs" Inherits="SGA.Presentacion.CAJA.frmConfirmarHab" %>
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
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            <cc1:LabelBase ID="lblOpcion" runat="server">titulo: </cc1:LabelBase>
        </h2>    
    </div>
    <div align="center">
           <table>
               <tr>
                   <td colspan="3">
                        <cc1:LabelBase ID="LabelBase1" runat="server">DATOS USUARIO</cc1:LabelBase>
                   </td>
                   
               </tr>
               <tr>
                 <td>
                     <cc1:LabelBase ID="LabelBase2" runat="server">Fecha:</cc1:LabelBase>
                     <cc1:CalendarioBase ID="dtpFechaSis" runat="server" />
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase3" runat="server">Codigo: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtCodUsu" runat="server" Enabled="False"></cc1:TextBoxBase>    
                 </td>
                 <td>
                     <cc1:LabelBase ID="LabelBase4" runat="server">Usuario: </cc1:LabelBase>
                     <cc1:TextBoxBase ID="txtUsuario" runat="server" Enabled="False"></cc1:TextBoxBase>
                 </td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                     <cc1:LabelBase ID="LabelBase6" runat="server">Nombre: </cc1:LabelBase>
                        <cc1:TextBoxBase ID="txtNomUsu" runat="server" Width="180px" Enabled="False"></cc1:TextBoxBase>

                 </td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                    
                     <cc1:CheckBoxBase ID="CheckBoxBase1" runat="server" Text="Habilitaciones Recibidas" AutoPostBack="True" Checked="True" OnCheckedChanged="CheckBoxBase1_CheckedChanged" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <cc1:CheckBoxBase ID="CheckBoxBase2" runat="server" Text="Habilitaciones Remitidas" AutoPostBack="True" OnCheckedChanged="CheckBoxBase2_CheckedChanged" />
                    
                 </td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                        <cc1:LabelBase ID="LabelBase5" runat="server">HABILITACIONES PENDIENTES</cc1:LabelBase>
                   </td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                     <cc1:PanelBase ID="PanelBase1" runat="server">
                         <cc1:GridViewBase ID="dtgHabPen" runat="server" AutoGenerateColumns="False">
                             <columns >
                                <asp:BoundField DataField ="idhabilita" HeaderText = "Codigo Habilitación"/>
                                <asp:BoundField DataField ="cdescripcion" HeaderText = "Tipo Habilitación"/>
                                <asp:BoundField DataField ="cmoneda" HeaderText = "Moneda" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField ="nmontohab" HeaderText = "Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField ="cnombre" HeaderText = "Usuario que Realiza Habilitación"/>
                                
                            </columns >
                         </cc1:GridViewBase>
                     </cc1:PanelBase>
                   </td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                     &nbsp;</td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                     <cc1:BotonAceptar ID="BotonAceptar1" runat="server" OnClick="BotonAceptar1_Click" />
&nbsp;
                     <cc1:BotonQuitar ID="btnRechazar" runat="server" OnClick="btnRechazar_Click" />
&nbsp;&nbsp;</td>
                 
             </tr>
              
               <tr>
                 <td colspan="3">
                     &nbsp;</td>
                 
             </tr>
              
           </table>
     </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>
