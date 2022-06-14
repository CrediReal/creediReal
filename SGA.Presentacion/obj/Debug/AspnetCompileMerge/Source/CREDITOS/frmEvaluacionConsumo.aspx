<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEvaluacionConsumo.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmEvaluacionConsumo" %>
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
    
    
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
       

        <div class="container">

            <div class="col-lg-3-col-md-3 col-sm-3" id="divRegistro" runat="server" visible="true">
            <div class="row">
                <h4> 1. INGRESOS BRUTOS</h4>
                 <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Ingreso Bruto del cliente </label>
                        <asp:TextBox  runat="server" ID="txtIngresoBruto" CssClass="form-control" Text="0.00" onchange="sumatotal();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Ingreso Bruto del Cónyuge </label>
                        <asp:TextBox runat="server" ID="txtIngresoConyuge" CssClass="form-control" Text="0.00" onchange="sumatotal();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Comisiones </label>
                        <asp:TextBox  runat="server" ID="txtComisiones" CssClass="form-control" Text="0.00" onchange="sumatotal();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Otros ingresos </label>
                        <asp:TextBox  runat="server" ID="txtOtrosIngresos" CssClass="form-control" Text="0.00" onchange="sumatotal();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">% Remuneración Bruta(Convenio) </label>
                        <asp:TextBox  runat="server" ID="txtRemuneracionBruta" CssClass="form-control" Text="0.00" onchange="sumatotal();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>--%>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">TOTAL INGRESOS BRUTOS </label>
                        <asp:TextBox Width="100%" runat="server" ID="txtTotalIngresoBruto" CssClass="btn btn-primary" Text="0.00" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
            </div>
         </div>

            <div class="col-lg-3-col-md-3 col-sm-3" id="div1" runat="server" visible="true">
            <div class="row">
                <h4> 2. INGRESOS NETOS</h4>
                 <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Ingresos Netos del cliente </label>
                        <asp:TextBox  runat="server" ID="txtIngresoNeto" CssClass="form-control" Text="0.00" onchange="sumatotalneto();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Ingreso Netos del Cónyuge </label>
                        <asp:TextBox runat="server" ID="txtIngresoNetoConyuge" CssClass="form-control" Text="0.00" onchange="sumatotalneto();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Comisiones </label>
                        <asp:TextBox  runat="server" ID="txtComisionesNeto" CssClass="form-control" Text="0.00" onchange="sumatotalneto();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Otros ingresos </label>
                        <asp:TextBox  runat="server" ID="txtOtrosIngresosNeto" CssClass="form-control" Text="0.00" onchange="sumatotalneto();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">TOTAL INGRESOS NETOS </label>
                        <asp:TextBox Width="100%" runat="server" ID="txtTotalIngresoNeto" CssClass="btn btn-primary" Text="0.00" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
            </div>
         </div>

            <div class="col-lg-3-col-md-3 col-sm-3" id="div2" runat="server" visible="true">
            <div class="row">
                <h4> 3. EGRESOS MENSUALES</h4>
                <h5><B>A. GASTOS FAMILIARES</B></h5>
                 <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Alimentación </label>
                        <asp:TextBox  runat="server" ID="txtAlimentacion" CssClass="form-control" Text="0.00" onchange="sumatotalfamiliar();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Educación </label>
                        <asp:TextBox runat="server" ID="txtEducacion" CssClass="form-control" Text="0.00" onchange="sumatotalfamiliar();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Transporte </label>
                        <asp:TextBox  runat="server" ID="txtTransporte" CssClass="form-control" Text="0.00" onchange="sumatotalfamiliar();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Alquileres </label>
                        <asp:TextBox  runat="server" ID="txtAlquiler" CssClass="form-control" Text="0.00" onchange="sumatotalfamiliar();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Servicios </label>
                        <asp:TextBox  runat="server" ID="txtServicios" CssClass="form-control" Text="0.00" onchange="sumatotalfamiliar();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">Otros gastos e imprevistos </label>
                        <asp:TextBox  runat="server" ID="txtImprevistos" CssClass="form-control" Text="0.00" onchange="sumatotalfamiliar();"
                        onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">TOTAL GASTOS FAMILIARES </label>
                        <asp:TextBox Width="100%" runat="server" ID="txtTotalgastoFamiliar" CssClass="btn btn-primary" Text="0.00" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
            </div>
         </div>

            <div class="col-lg-3-col-md-3 col-sm-3" id="div3" runat="server" visible="true">
            <div class="row"><br /> 
                <h5><b> B. CRÉDITOS COMERCIALES</b></h5>
                 <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Banco </label>
                        <asp:TextBox  runat="server" ID="txtBanco" CssClass="form-control" Text="" placeholder="" ></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Monto </label>
                        <asp:TextBox  runat="server" ID="txtMontoBanco" CssClass="form-control" Text="0" ></asp:TextBox>
                         
                     </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group"><br />
                         <asp:Button runat="server" ID="btnAgregarCreditoComercial" CssClass="btn btn-success" Text="+" OnClick="btnAgregarCreditoComercial_Click" />
                         
                         </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group">
                       <asp:GridView ID="dtgCreditosComerciales" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgCreditosComerciales_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="id" Visible="False" />
                                    <asp:BoundField DataField="cDescripcion" HeaderText="Banco" ItemStyle-Width="600" />
                                    <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                        <asp:TemplateField HeaderText="">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate >
									    <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									</ItemTemplate>
                                </asp:TemplateField> 
                                </Columns>
                                       
                            </asp:GridView>
                     </div>
                </div>
                 <div class="col-lg-12">                
                         
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">TOTAL CRÉDITOS COMERCIALES </label>
                        <asp:TextBox Width="100%" runat="server" ID="txtTotalCreditos" CssClass="btn btn-primary" Text="0.00" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
            </div>
         </div>

        </div>	

        <div class="container">
            <div class="col-lg-3-col-md-3 col-sm-3" id="div4" runat="server" visible="true">
            <div class="row">
                <h5><b> C. ENDEUDAMIENTO EN EL SISTEMA</b></h5>
                <h5><u> Créditos Directos</u></h5>
                 <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Banco </label>
                        <asp:TextBox  runat="server" ID="txtBancoDirecto" CssClass="form-control" Text="" placeholder="" ></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-12">
                     <div class=" form-group">
                         <label class=" control-label">Monto </label>
                        <asp:TextBox  runat="server" ID="txtMontoDirecto" CssClass="form-control" Text="0" ></asp:TextBox>
                         <asp:Button runat="server" ID="BtnAgregarDirecto" CssClass="btn btn-success" Text="+" OnClick="BtnAgregarDirecto_Click" />
                         
                     </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group"><br />
                         
                         </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group">
                       <asp:GridView ID="DtgCreditoDirecto" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="DtgCreditoDirecto_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="id" Visible="False" />
                                    <asp:BoundField DataField="cDescripcion" HeaderText="Banco" ItemStyle-Width="600" />
                                    <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                        <asp:TemplateField HeaderText="">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate >
									    <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									</ItemTemplate>
                                </asp:TemplateField> 
                                </Columns>
                                       
                            </asp:GridView>
                     </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">TOTAL CRÉDITOS DIRECTOS </label>
                        <asp:TextBox Width="100%" runat="server" ID="txtTotalCreditoDirecto" CssClass="btn btn-primary" Text="0.00" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
             </div>
        </div>

            <div class="col-lg-3-col-md-3 col-sm-3" id="div5" runat="server" visible="true">
            <div class="row">
                <br />
                <h5><u> Créditos Indirectos</u></h5>
                 <div class="col-lg-12">
                     <div class="form-group">
                         <label class=" control-label">Banco </label>
                        <asp:TextBox  runat="server" ID="txtBancoIndirecto" CssClass="form-control" Text="" placeholder="" ></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-12">
                     <div class=" form-group">
                         <label class=" control-label">Monto </label>
                        <asp:TextBox  runat="server" ID="txtMontoIndirecto" CssClass="form-control" Text="0" ></asp:TextBox>
                         <asp:Button runat="server" ID="BtnAgregarIndirecto" CssClass="btn btn-success" Text="+" OnClick="BtnAgregarIndirecto_Click" />
                         
                     </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group"><br />
                         
                         </div>
                </div>
                <div class="col-lg-12">
                     <div class="form-group">
                       <asp:GridView ID="DtgCreditoInDirecto" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="DtgCreditoInDirecto_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="id" Visible="False" />
                                    <asp:BoundField DataField="cDescripcion" HeaderText="Banco" ItemStyle-Width="600" />
                                    <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                        <asp:TemplateField HeaderText="">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate >
									    <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									</ItemTemplate>
                                </asp:TemplateField> 
                                </Columns>
                                       
                            </asp:GridView>
                     </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">TOTAL CRÉDITOS INDIRECTOS </label>
                        <asp:TextBox Width="100%" runat="server" ID="txtTotalCreditoIndirecto" CssClass="btn btn-primary" Text="0.00" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
             </div>
        </div>

            <div class="col-lg-6-col-md-6 col-sm-6" id="div6" runat="server" visible="true">
                  <h4>INFORMACIÓN ECONÓMICA DE LA UNIDAD FAMILIAR</h4>
                <div class="row">
              
                 <h5><b>4. INFORMACIÓN FAMILIAR</b></h5>
                 <div class="col-lg-6">
                     <div class="form-group">
                         <label class=" control-label">Nro de hijos </label>
                        <asp:TextBox  runat="server" ID="txtNumHijos" CssClass="form-control" Text="0"></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <label class=" control-label">Nro Dependientes </label>
                        <asp:TextBox runat="server" ID="txtDependientes" CssClass="form-control" Text="0"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <label class=" control-label">Edades hijos </label>
                        <asp:TextBox  runat="server" ID="txtEdadHijo" CssClass="form-control" Text="0"></asp:TextBox>
                    </div>
                </div>
                    <div class="col-lg-6">
                    <div class="form-group">
                        <label class=" control-label">.</label>
                    </div>
                </div><div class="col-lg-6">
                    <div class="form-group">
                        <label class=" control-label">.</label>
                    </div>
                </div>
               <h5><b>5. EDUCACIÓN</b></h5>
                <div class="col-lg-6">
                    
                    <div class="form-group">
                        <label class=" control-label">Colegio </label>
                        <asp:TextBox  runat="server" ID="txtColegio" CssClass="form-control" Text="" placeholder="Colegio..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <label class=" control-label">Universidad </label>
                        <asp:TextBox  runat="server" ID="txtUniversidad" CssClass="form-control" Text="" placeholder="Universisdad..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <label class=" control-label">Monto Pensión </label>
                        <asp:TextBox  runat="server" ID="txtMontoPension" CssClass="form-control" Text="0.00"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <label class=" control-label">6. OBSERVACIONES </label>
                       <asp:TextBox  runat="server" ID="txtObservaciones" CssClass="form-control" Text="" TextMode="MultiLine" Rows="3"></asp:TextBox> 
                    </div>
                </div>
            </div>
            </div>
        </div>
        <div class="container">	
            <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary"  Text="Guardar Evaluación"/>
        </div>
        <asp:HiddenField ID="hUsuario" runat="server" />
        <asp:HiddenField ID="hidSolicitud" runat="server" />
        <asp:HiddenField ID="hidCli" runat="server" />
    </form>
</body>
    <script src="../js/jquery-3.2.1.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script>
        function sumatotal()
        {
            if (!document.getElementById("txtIngresoBruto").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtIngresoBruto").value = "0.00";
            }
            else if (!document.getElementById("txtIngresoConyuge").value.match(/^\d+/))
            {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtIngresoConyuge").value = "0.00";
            }
            else if (!document.getElementById("txtComisiones").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtComisiones").value = "0.00";
            }
            else if (!document.getElementById("txtOtrosIngresos").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtOtrosIngresos").value = "0.00";
            }
            //else if (!document.getElementById("txtRemuneracionBruta").value.match(/^\d+/)) {
            //    alert("Por favor ingresa un valor correcto");
            //    document.getElementById("txtRemuneracionBruta").value = "0.00";
            //}
            else {
                var a = Number(document.getElementById("txtIngresoBruto").value);
                var b = Number(document.getElementById("txtIngresoConyuge").value);
                var c = Number(document.getElementById("txtComisiones").value);
                var d = Number(document.getElementById("txtOtrosIngresos").value);
                //var e = Number(document.getElementById("txtRemuneracionBruta").value);
            }
            
            var total=a+b+c+d;
            document.getElementById("txtTotalIngresoBruto").value=total.toString();        
        };

        function sumatotalneto() {
            if (!document.getElementById("txtIngresoNeto").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtIngresoNeto").value = "0.00";
            }
            else if (!document.getElementById("txtIngresoNetoConyuge").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtIngresoNetoConyuge").value = "0.00";
            }
            else if (!document.getElementById("txtComisionesNeto").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtComisionesNeto").value = "0.00";
            }
            else if (!document.getElementById("txtOtrosIngresosNeto").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtOtrosIngresosNeto").value = "0.00";
            }
            else {
                var a = Number(document.getElementById("txtIngresoNeto").value);
                var b = Number(document.getElementById("txtIngresoNetoConyuge").value);
                var c = Number(document.getElementById("txtComisionesNeto").value);
                var d = Number(document.getElementById("txtOtrosIngresosNeto").value);
            }

            var total = a + b + c + d;
            document.getElementById("txtTotalIngresoNeto").value = total.toString();
        };

        function sumatotalfamiliar() {
            if (!document.getElementById("txtAlimentacion").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtAlimentacion").value = "0.00";
            }
            else if (!document.getElementById("txtEducacion").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtEducacion").value = "0.00";
            }
            else if (!document.getElementById("txtTransporte").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtTransporte").value = "0.00";
            }
            else if (!document.getElementById("txtServicios").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtServicios").value = "0.00";
            }
            else if (!document.getElementById("txtImprevistos").value.match(/^\d+/)) {
                alert("Por favor ingresa un valor correcto");
                document.getElementById("txtImprevistos").value = "0.00";
            }
            else {
                var a = Number(document.getElementById("txtAlimentacion").value);
                var b = Number(document.getElementById("txtEducacion").value);
                var c = Number(document.getElementById("txtTransporte").value);
                var d = Number(document.getElementById("txtServicios").value);
                var e = Number(document.getElementById("txtImprevistos").value);
            }

            var total = a + b + c + d + e;
            document.getElementById("txtTotalgastoFamiliar").value = total.toString();
        };

        function suma() {

            sumatotal();
            sumatotalneto();
            sumatotalfamiliar();
        }
    </script>

    
</html>
