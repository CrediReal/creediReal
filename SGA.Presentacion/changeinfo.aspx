        <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changeinfo.aspx.cs" Inherits="SGA.Presentacion.changeinfo" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="js/jquery-3.2.1.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-inline">
        <br />
     <div class="row">
         <div class="col-sm-6 col-lg-4 margin-row">

              <div class="form-group">
                <label for="txtNombre">Nombre</label>
                <input runat="server" type="text" class="form-control" id="txtNombre" placeholder="Nombre">
              </div>
             <div class="text-center">
             
            <asp:Button runat="server" CssClass="btn btn-default" ID="btnBuscar" OnClick="btnBuscar_Click" Text="Buscar" />
  
        
            <br />
            <asp:GridView OnSelectedIndexChanged="dtgSolicitud_SelectedIndexChanged" ID="dtgSolicitud" runat="server" DataKeyNames="idSolicitud,cNombre,nCapitalSolicitado,nPlazoCuota,nCuotas,nTasaCompensatoria,dFechaRegistro,idEstado" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
					    <ItemTemplate >
						    <asp:LinkButton id="P" runat="server" CommandArgument="Seleccionar" CommandName="select" ToolTip="Seleccionar" CausesValidation="false" Text="Selec">Selec</asp:LinkButton>
					    </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="idSolicitud" HeaderText="ID" />  
                     <asp:BoundField DataField="cNomCorto" HeaderText="Nombres" />                    
                    <asp:BoundField DataField="nCapitalSolicitado" HeaderText="Monto" />
                    <asp:BoundField DataField="dFechaRegistro" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="cNombre"  Visible="false" />
                    <asp:BoundField DataField="nPlazoCuota"  Visible="false" />
                    <asp:BoundField DataField="nCuotas"  Visible="false" />
                    <asp:BoundField DataField="nTasaCompensatoria"  Visible="false" />
                    <asp:BoundField DataField="idEstado"  Visible="false" />
                    
                </Columns>
            </asp:GridView>
                 </div>
         </div>
</div>
        
         <div class="row">
             <div class="col-sm-6 col-lg-4 margin-row">
                 <div class="form-group">
                    <label for="txtMonto">Monto</label>
                    <input runat="server" type="text" class="form-control" id="txtMonto" placeholder="Monto">
                  </div>
             </div>
             <div class="col-sm-6 col-lg-4 margin-row">
                 <div class="form-group">
                    <label for="txtCuotas">Cuotas</label>
                    <input runat="server" type="text" class="form-control" id="txtCuotas" placeholder="Cuotas">
                  </div>
             </div>
             <div class="col-sm-6 col-lg-4 margin-row">
                 <div class="form-group">
                    <label for="txtPlazo">Plazo</label>
                    <input runat="server" type="text" class="form-control" id="txtPlazo" placeholder="Plazo">
                  </div>
             </div>
        </div>

         <div class="row">
             <div class="col-sm-6 col-lg-4 margin-row">
                 <div class="form-group">
                    <label for="txtTasa">Tasa</label>
                    <input runat="server" type="text" class="form-control" id="txtTasa" placeholder="Tasa">
                  </div>
             </div>
             <div class="col-sm-6 col-lg-4 margin-row">
                 <div class="form-group">
                    <label for="txtEstado">Estado</label>
                    <input runat="server" type="text" class="form-control" id="txtEstado" placeholder="Estado">
                  </div>
             </div>
             <div class="col-sm-6 col-lg-4 margin-row"></div>
        </div>
           
        
        <div class="text-center">
             <br/>
            <asp:Button runat="server" CssClass="btn btn-default" ID="btnactualizar" OnClick="btnactualizar_Click" Text="Actualizar" />
  
        </div>
       
    </form>
</body>
</html>
