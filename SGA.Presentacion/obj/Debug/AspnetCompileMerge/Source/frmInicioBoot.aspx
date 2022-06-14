<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInicioBoot.aspx.cs" Inherits="SGA.Presentacion.frmInicioBoot" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head id="Head1" runat="server">
     <title>..:: SGC ::...</title>
    <link rel='shortcut icon' type='image/x-icon' href='Imagenes/favicon.ico' />
     <link rel="stylesheet" href="css/stylelogin.css" />
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <script>
        function selectModulo(val) {
            var hmod = document.getElementById("hModulo");
            hmod.value = val;
        }
    </script>
</head>
    

<body>
    <div>
        <p style="font-family:Ebrima; color:#EE1D23; background-color:yellow; text-align:center; font-size:54px; text-anchor:end;">PRUEBAS</p>
    </div>
<div id="form">
  <div class="container">
    <div class="col-lg-6 col-lg-offset-3 col-md-6 col-md-offset-3 col-md-8 col-md-offset-2">
      <div id="userform">
        <ul class="nav nav-tabs nav-justified" role="tablist" runat="server" id="ulMenu">
          <li runat="server" id="liPanel"><a onclick="selectModulo(1)" href="#signup" role="tab" data-toggle="tab">SISTEMA DE GESTIÓN CREDITICIA</a></li>
          <%--<li runat="server" id="liErp"><a href="#login" onclick="selectModulo(2)" role="tab" data-toggle="tab"></a></li>  --%>       
        </ul>
        <div class="tab-content">
          <div class="tab-pane fade active in" id="signup">
            <h2 class="text-uppercase text-center"> <img src="Imagenes/logo.png" width="300" height="80" /></h2>
              <br />
            <form id="Form1" runat="server">
              <div class="row">
                <div class="col-xs-12 col-sm-6">
                  <div class="form-group">
                    
                    <asp:TextBox runat="server" type="text" placeholder="Usuario" class="form-control" id="txtUsuario" required data-validation-required-message="Por favor ingrese el usuario." autocomplete="off" >
                      </asp:TextBox>
                    <p class="help-block text-danger"></p>
                  </div>
                </div>
                <div class="col-xs-12 col-sm-6">
                  <div class="form-group">
                    <asp:TextBox runat="server" type="password" placeholder="Contrase&#241;a" class="form-control" id="txtClave" required data-validation-required-message="Please enter your password" autocomplete="off" />
                    <p class="help-block text-danger"></p>
                  </div>
                </div>
              </div>
              <div class="mrgn-30-top">
                
                  <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-larger btn-block" Text="VALIDAR" OnClick="btnIngresar_Click"  />
              </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6">
                      <div class="form-group">
                        <br />
                        <asp:DropDownList runat="server" ID="cboPerfil" CssClass="btn btn-larger btn-block" Visible="false" >
                            <asp:ListItem Value="1">administrador</asp:ListItem>
                            <asp:ListItem Value="2">Asesor</asp:ListItem>
                          </asp:DropDownList>
                
                      </div>
                </div>
                <div class="col-xs-12 col-sm-6">
                    <div class="form-group">
                    <br />
                   <asp:DropDownList ID="cboSede" runat="server" CssClass="btn btn-larger btn-block" Visible="false">
                                            <asp:ListItem Selected="True" Value="1">SEDE PRINCIPAL</asp:ListItem>
                                            <asp:ListItem Value="2">SEDE-JAUJA</asp:ListItem>
                                            <asp:ListItem Value="3">SEDE-CONCEPCIÓN</asp:ListItem>
                                        </asp:DropDownList>
                
                  </div>
                    </div>
                </div>
              <div class="mrgn-30-top">
                 <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-larger btn-block" Text="INGRESAR" OnClick="btnAceptar_Click"  Visible="false" />
              </div>
                <asp:HiddenField ID="hModulo" Value="1" runat="server" />

            <asp:Panel ID="pnlCambioContrasenia" runat="server" Width="100%"  Visible="False">
                <h2 class="text-uppercase text-center"> CAMBIO DE CONTRASEÑA</h2>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                          <div class="form-group">
                    
                            <asp:TextBox runat="server" type="text" placeholder="Usuario" class="form-control" id="txtClaveAnterior" required data-validation-required-message="Por favor ingrese el usuario." autocomplete="off" >
                              </asp:TextBox>
                            <p class="help-block text-danger"></p>
                          </div>
                        </div>                        
                  </div>
                 <div class="row">
                    <div class="col-xs-12 col-sm-6">
                        <div class="form-group">
                        <asp:TextBox runat="server" type="password" placeholder="Nueva Contrase&#241;a" class="form-control" id="txtNuevaClave" required data-validation-required-message="Please enter your password" autocomplete="off" />
                        <p class="help-block text-danger"></p>
                        </div>
                    </div>
                      <div class="col-xs-12 col-sm-6">
                        <div class="form-group">
                        <asp:TextBox runat="server" type="password" placeholder=" Repita Nueva Contrase&#241;a" class="form-control" id="txtNuevaClave1" required data-validation-required-message="Please enter your password" autocomplete="off" />
                        <p class="help-block text-danger"></p>
                        </div>
                    </div>
                 </div>
                 <div class="row">
                    
                     <div class="mrgn-30-top">
                         <asp:Button ID="BotonAceptar0" runat="server" CssClass="btn btn-larger btn-block" Text="CAMBIAR" OnClick="BotonAceptar0_Click"  />
                      </div>
                      <div class="mrgn-30-top">
                         <asp:Button ID="BotonCancelar0" runat="server" CssClass="btn btn-larger btn-block" Text="CANCELAR" OnClick="BotonCancelar0_Click"  CausesValidation="false" formnovalidate />
                          <asp:HiddenField ID="hClaveAnterior" runat="server" Visible="False" />
                      </div>
                 </div>
                </asp:Panel>
            </form>
          </div>
        
        </div>
      </div>
    </div>
  </div>
  <!-- /.container --> 
</div>
    

<script src="//code.jquery.com/jquery-1.11.3.min.js"></script>
<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
<script src="js/index.js"></script>
</body>
</html>
