<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPropuesta.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmPropuesta" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css"/>
    <link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css' />
	<link href='https://fonts.googleapis.com/css?family=Oxygen' rel='stylesheet' type='text/css' />
    <script type="text/javascript">
        function handleKeyPress(e) {
            var key = e.keyCode || e.which;
            if (key == 13) {
                document.getElementById('BotonConsultar1').click();
            }
        }
    </script>
    <link href="~/Styles/cssGeneral.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row"> 
                <div class="col-lg-12">
                <uc1:conBuscarCliente ID="conBuscarCliente1" runat="server" />
                </div>
                <cc1:BotonConsultar ID="BotonConsultar1" runat="server" OnClick="BotonConsultar1_Click" />
            </div>
			</div>	
        <div class="container" id="divRegistro" runat="server" visible="false">
        <div class="row">
        <h4> I. INFORMACION DEL CLIENTE Y NEGOCIO</h4>
         <div class="col-lg-6">
             <div class="form-group">
             <label class=" control-label">ENTORNO FAMILIAR </label>
             <asp:TextBox Width="100%" runat="server" ID="txtEntorno" CssClass="form-control" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox>
                 </div>
        </div>
        <div class="col-lg-6">
            <div class="form-group">
             <label>GIRO Y UBICACION DEL NEGOCIO </label>
           <asp:TextBox Width="100%" runat="server" ID="txtGiroUbicacion" CssClass="form-control" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox>
                </div>
        </div>
    </div>
        <div class="row">
        <h4> II. EXPERIENCIA CREDITICIA </h4>
         <div class="col-lg-12">
             <div class="form-group">
             <asp:TextBox Width="100%" runat="server" ID="txtExperienciaCrediticia" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                 </div>
        </div>
        </div>
        <div class="row">
        <h4> III. EVALUACION - ANALISIS FINANCIERO  </h4>
         <div class="col-lg-12">
             <div class="form-group">
             <asp:TextBox Width="100%" runat="server" ID="txtEvaAnalisisFinanciero" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:TextBox>
                 </div>
        </div>
        </div>
        <div class="row">
        <h4> IV. GARANTIAS DEL CREDITO </h4>
         <div class="col-lg-12">
             <div class="form-group">
             <asp:TextBox Width="100%" runat="server" ID="txtGarantia" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                 </div>
        </div>
        </div>
        <div class="row">
        <h4> V. CONCLUSIONES RESPECTO AL CREDITO PROPUESTO </h4>
         <div class="col-lg-12">
             <div class="form-group">
             <asp:TextBox Width="100%" runat="server" ID="txtConclusion" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                 </div>
        </div>
        </div>
        <div class="row">
        <h4> VI. REFERENCIAS (Tres referencias por lo menos una con celular )   </h4>
         <div class="col-lg-12">
             <div class="form-group">
             <asp:TextBox Width="100%" runat="server" ID="txtReferencia" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                 </div>
        </div>
        </div>
        <div class="row">
        <h4> VII. PRINCIPALES CLIENTES Y PROVEEDORES   </h4>
         <div class="col-lg-12">
             <div class="form-group">
             <asp:TextBox Width="100%" runat="server" ID="txtProveedores" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
         </div>
        </div>
        </div>

            <div class="col-md-12">
                           <div class="form-group">
                              <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-info" OnClick="btnGuardar_Click" Text="Guardar" />
                               <asp:Button ID="btnCancelar" runat="server" formnovalidate CssClass="btn btn-warning" OnClick="btnCancelar_Click" Text="Cancelar" />
                              <asp:Button ID="btnImprimir" runat="server" formnovalidate CssClass="btn btn-success" OnClick="btnImprimir_Click" Text="Imprimir" Visible="false" />
                              
                            </div>
                        </div>	
          </div>	
              
    <asp:HiddenField ID="hUsuario" runat="server" />
        <asp:HiddenField ID="hidPropuesta" runat="server" />
        <asp:HiddenField ID="hidSolicitud" runat="server" />
        <asp:HiddenField ID="hidCli" runat="server" />
    </form>
</body>
    <script src="../js/jquery-3.2.1.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.js"></script>
</html>
