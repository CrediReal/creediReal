<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmRegistroClientes.aspx.cs" Inherits="SGA.Presentacion.CLIENTES.frmRegistroClientes" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.2.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/calendarthem.css" rel="stylesheet" />
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    <script type="text/javascript">
        function submitButton(event) {
            if (event.which == 13) {
                $('#btnDistribuir').trigger('click');
            }
        }
    </script>
    <style type="text/css">
        .auto-style4 {
            height: 26px;
            text-align: left;
        }
        .auto-style6 {
            height: 26px;
            text-align: left;
        }

        .auto-style7 {
            text-decoration: underline;
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
           <table>
                 <tr>
                    <td ></td>
                    <td>
                        <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td>
                        <br />
                        <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
                        <asp:HiddenField ID="hIdCuenta" runat="server" />
                        <asp:HiddenField ID="hcOperacion" runat="server" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td ></td>
                    <td>
                        &nbsp;</td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <cc1:PanelBase ID="pnInfoBasico" runat="server" Visible="False">
                             <table>
                             <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Tipo de persona:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboTipPersona" runat="server" Width="130px" Enabled="False">
                                    </cc1:ComboBoxBase>
                                 </td>
                                 <td class="auto-style6">&nbsp;&nbsp; Tipo de documento:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboTipDocumento" runat="server" Width="130px" Enabled="False">
                                    </cc1:ComboBoxBase>
                                 </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Número de documento:</td>
                                <td class="auto-style4">
                                    <cc1:TextBoxBase ID="txtCBDNI" runat="server" MaxLength="11" Width="125px"></cc1:TextBoxBase>
                                     </td>
                                 <td class="auto-style6">&nbsp;&nbsp;&nbsp;Documento adicional:</td>
                                <td class="auto-style4">
                                    <cc1:TextBoxBase ID="txtCBRUC" runat="server" MaxLength="11" Width="125px"></cc1:TextBoxBase>
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;Nro de celular:</td>
                                <td class="auto-style4">
                                     <cc1:TextBoxBase ID="txtCelular" runat="server" MaxLength="11" Width="125px"></cc1:TextBoxBase>
                                     </td>
                                 <td class="auto-style6">
                                     &nbsp;&nbsp;&nbsp; Adicionales RPM/RPC:</td>
                                     <td class="auto-style6">
                                         <cc1:TextBoxBase ID="txtCBNroTel" runat="server" MaxLength="11" Width="125px"></cc1:TextBoxBase>
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Email:</td>
                                <td class="auto-style4" colspan="3">
                                    <cc1:EmailTextBox ID="txtCBCorreoElectronico"  runat="server" Width="398px" />
                                     </td>
                                 </tr>
                                  <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;<span class="auto-style7"><strong>Dirección de cliente:</strong></span></td>
                                <td class="auto-style4">
                                    
                                     </td>
                                 <td class="auto-style6"></td>
                                <td class="auto-style4">
                                    
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Departamento:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboDepartamento" runat="server" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged">
                                    </cc1:ComboBoxBase>
                                 </td>
                                 <td class="auto-style6">&nbsp;&nbsp; Provincia:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboProvincia" runat="server" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="cboProvincia_SelectedIndexChanged">
                                    </cc1:ComboBoxBase>
                                 </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Distrito:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboDistrito" runat="server" Width="130px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 <td class="auto-style6">&nbsp;&nbsp;Tipo de vía:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboTipoVia" runat="server" Width="130px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Condición de la vivienda o local:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboCondicionVivienda" runat="server" Width="130px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 <td class="auto-style6">&nbsp;&nbsp;Material de construcción:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboMaterialConstruccion" runat="server" Width="130px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 </tr>

                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Tiempo de residencia en años:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtAnioResidencia" runat="server" MaxLength="2" Width="35px">0</cc1:NumberBox>
                                     </td>
                                 <td class="auto-style6">&nbsp;&nbsp;Dirección:</td>
                                <td class="auto-style4">
                                    <cc1:TextBoxBase ID="txtDireccion" runat="server" MaxLength="200" Width="250px"></cc1:TextBoxBase>
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Referencia:</td>
                                <td class="auto-style4">
                                    <cc1:TextBoxBase ID="txtReferencia" runat="server" MaxLength="200" Width="250px"></cc1:TextBoxBase>
                                     </td>
                                 <td class="auto-style6">&nbsp;&nbsp;Tipo de dirección:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboTipoDireccion" runat="server" Width="130px">
                                    </cc1:ComboBoxBase>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     </td>
                                 </tr>
                                  <tr class="lblBase">
                                <td class="auto-style6" ></td>
                                <td colspan="2" align="center">
                                    <br />
                                     <asp:Button ID="btnAgregarDireccion" CssClass="btnBase_Blanco" runat="server" Text="Agregar Dirección" OnClick="btnAgregarDireccion_Click" />
                                    <br />
                                     </td>
                                <td class="auto-style4">
                                    
                                     </td>
                                 </tr>

                                  <tr class="lblBase">
                                <td align="center" colspan="4" >
                                    <br />
                                    <cc1:GridViewBase ID="dtgDireccion" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgDireccion_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="cDireccion" HeaderText="Dirección">
                                            <ItemStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cReferenciaDireccion" HeaderText="Referencia">
                                            <ItemStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="idTipoDireccion" Visible="false" />
                                            <asp:BoundField DataField="cTipoDir" HeaderText="Tipo Dirección"/>
                                            <asp:BoundField DataField="idCli" Visible="false" />
                                            <asp:BoundField DataField="idDireccion" Visible="false" />                                            
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
									            <ItemTemplate >
									                <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="delete" ToolTip="Eliminar dirección" CausesValidation="false" Text="Eliminar">Eliminar</asp:LinkButton>
									            </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                        <PagerSettings Position="TopAndBottom" />
                                    </cc1:GridViewBase>

                                </td>
                                 </tr>
                               
                                 </table>
                        </cc1:PanelBase>  
                                           <br />
                         <cc1:PanelBase ID="pnPerNatural" runat="server" Visible="False">
                             <table>
                             <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp; Apellido Paterno:</td>
                                <td class="auto-style4">                                    
                                    <cc1:TextBoxBase ID="txtApePat" runat="server"></cc1:TextBoxBase>
                                 </td>
                                 <td class="auto-style6">Apellido Materno:</td>
                                <td class="auto-style4">                                    
                                    <cc1:TextBoxBase ID="txtApeMat" runat="server"></cc1:TextBoxBase>
                                </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;&nbsp; Nombres:</td>
                                <td class="auto-style4">
                                    <cc1:TextBoxBase ID="txtNomCli" runat="server"></cc1:TextBoxBase>
                                     </td>
                                 <td class="auto-style6">Apellido de casada:</td>
                                <td class="auto-style4">
                                    <cc1:TextBoxBase ID="txtApeCasado" runat="server"></cc1:TextBoxBase>
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Sexo:</td>
                                <td class="auto-style4">
                                     <cc1:ComboBoxBase ID="cboSexo" runat="server" Width="130px">
                                     </cc1:ComboBoxBase>
                                     </td>
                                 <td class="auto-style6">Estado civil:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboEstadoCivil" runat="server" Width="130px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 </tr>
                                  <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Fecha de Nacimiento:</td>
                                <td class="auto-style4">
                                     <cc1:CalendarioBase ID="dtFecNac" runat="server" />
                                      </td>
                                 <td class="auto-style6">Número de hijos:</td>
                                <td class="auto-style4">
                                    <cc1:NumberBox ID="txtNroHijos" runat="server" MaxLength="1" Width="38px">0</cc1:NumberBox>
                                      </td>
                                 </tr>
                                  <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Número de persona dependientes:</td>
                                <td class="auto-style4">
                                     <cc1:NumberBox ID="txtNroPerDep" runat="server" MaxLength="1" Width="38px">0</cc1:NumberBox>
                                      </td>
                                 <td class="auto-style6"></td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                 </tr>
                                  <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;<span class="auto-style7"><strong>Lugar de nacimiento:</strong></span></td>
                                <td class="auto-style4">
                                     &nbsp;</td>
                                 <td class="auto-style6"></td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Departamento:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboDepartamentoNac" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamentoNac_SelectedIndexChanged">
                                    </cc1:ComboBoxBase>
                                 </td>
                                 <td class="auto-style6">&nbsp;&nbsp; Provincia:</td>
                                <td class="auto-style4">                                    
                                    <cc1:ComboBoxBase ID="cboProvinciaNac" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="cboProvinciaNac_SelectedIndexChanged">
                                    </cc1:ComboBoxBase>
                                 </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Distrito:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboDistritoNac" runat="server" Width="250px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 <td class="auto-style6">&nbsp;&nbsp;</td>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Ocupación:</td>
                                <td class="auto-style4">
                                     <cc1:ComboBoxBase ID="cboOcupacion" runat="server" Width="250px">
                                     </cc1:ComboBoxBase>
                                     </td>
                                 <td class="auto-style6">Actividad económica:</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboActividadEco1" runat="server" Width="250px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 </tr>
                                 <tr class="lblBase">
                                <td class="auto-style6" >&nbsp;&nbsp;Grado de instrucción:</td>
                                <td class="auto-style4">
                                     <cc1:ComboBoxBase ID="cboNivInstruc" runat="server" Width="250px">
                                     </cc1:ComboBoxBase>
                                     </td>
                                 <td class="auto-style6">Profesión o dedicación</td>
                                <td class="auto-style4">
                                    <cc1:ComboBoxBase ID="cboProfesion" runat="server" Width="250px">
                                    </cc1:ComboBoxBase>
                                     </td>
                                 </tr>
                                
                                 </table>
                        </cc1:PanelBase>                     
                 
                        <cc1:PanelBase ID="pnlPerJuridica" runat="server" Visible="false">
                             <table>
                             <tr class="lblBase">
                                <td class="auto-style6" >Persona juridica:</td>
                                <td class="auto-style4">                                    
                                    &nbsp;</td>
                                 <td class="auto-style6">Cuota pendiente a pagar:</td>
                                <td class="auto-style4">                                    
                                    &nbsp;</td>
                                 </tr>
                               
                                 </table>
                        </cc1:PanelBase>                     
                 
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <br />
                        <cc1:BotonNuevo ID="BotonNuevo1" runat="server" OnClick="BotonNuevo1_Click" />
&nbsp;<cc1:BotonEditar ID="BotonEditar1" runat="server" OnClick="BotonEditar1_Click" Visible="False" />
&nbsp;<cc1:BotonGrabar ID="BotonGrabar1" runat="server" OnClick="BotonGrabar1_Click" Visible="False" />&nbsp;<cc1:BotonCancelar ID="BotonCancelar1" runat="server" OnClick="BotonCancelar1_Click" />
                    </td>
                    <td></td>
                </tr>
                </table>
        </div>
         <asp:HiddenField ID="hUsuario" runat="server" />
    </form>
</body>
</html>