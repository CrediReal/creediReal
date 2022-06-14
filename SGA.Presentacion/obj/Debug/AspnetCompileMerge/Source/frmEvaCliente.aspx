<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEvaCliente.aspx.cs" Inherits="SGA.Presentacion.frmEvaCliente" %>

<%@ Register Src="~/conBuscarCliente.ascx" TagPrefix="uc1" TagName="conBuscarCliente" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1"/>        
        <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="plugins/timepicker/bootstrap-timepicker.min.css"/>
        <link rel="stylesheet" href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css"/>
		<!-- Website CSS style -->
		<link href="css/bootstrap.min.css" rel="stylesheet"/>
    	<!-- Website Font style -->
	    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css"/>
		<%--<link rel="stylesheet" href="Styles/style.css">--%>
		<!-- Google Fonts -->
		<link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css' />
		<link href='https://fonts.googleapis.com/css?family=Oxygen' rel='stylesheet' type='text/css' />
        <title>Evaluación</title>
        <link rel="stylesheet" href="Styles/chosen.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row main">                
            <div class="form-group">
                 <div class="col-md-12">
                     <uc1:conBuscarCliente runat="server" id="conBuscarCliente" />
                 </div>
                 <br />
                <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-primary" OnClick="btnConsultar_Click" Text="Consultar" />
                &nbsp;<asp:Button runat="server" ID="btnReporteHori" CssClass="btn btn-primary" OnClick="btnReporteHori_Click" Text="Reporte Horizontal" />
                 <br />
                 <br />
                <div class="col-md-12">
                    <asp:GridView ID="GridViewUser"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        CssClass="table table-bordered table-hover"
                        ClientIDMode="Static"
                        runat="server"
                            ShowHeaderWhenEmpty="true">
                        <Columns>                            
                            <asp:BoundField DataField="idSolicitud" HeaderText="Nro Solicitud" />
                            <asp:BoundField DataField="cNombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="cDocumentoID" HeaderText="Documento" />
                            <asp:BoundField DataField="dFechaRegistro" HeaderText="Fecha" dataformatstring="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="nCapitalSolicitado" HeaderText="Monto" />
                            <asp:BoundField DataField="nTasaCompensatoria" HeaderText="Tasa" />
                            <asp:BoundField DataField="nCuotas" HeaderText="Cuotas" />
                            <asp:BoundField DataField="nPlazoCuota" HeaderText="Plazo" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAccion" runat="server" OnClick="lnkAccion_Click" Text='<%# Eval("cAccion")%>' CommandArgument='<%# Eval("idSolicitud")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAccionPyme" runat="server" OnClick="lnkAccionPyme_Click" Text='<%# Eval("cAccionPyme")%>' CommandArgument='<%# Eval("idSolicitud")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
        <asp:HiddenField ID="hUsuario" runat="server" />
        <asp:HiddenField ID="hidSolicitud" runat="server" />
        <asp:HiddenField ID="hidCli" runat="server" />
    </form>

    <script src="Scripts/jquery-3.0.0.js"></script>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootstrap-datetimepicker.js"></script>
    <script src="js/locales/bootstrap-datetimepicker.es.js" charset="UTF-8"></script>
    <script src="plugins/timepicker/bootstrap-timepicker.min.js"></script>
    <script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    
    <script type="text/javascript">
        $('.form_datetime').datetimepicker({
            //language:  'fr',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            forceParse: 0,

        });
        $('.form_date').datetimepicker({
            language: 'es',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 1
        });

        $('.form_time').datetimepicker({
            language: 'es',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 1,
            minView: 0,
            maxView: 1,
            forceParse: 0,
            timezone: 'GMT'
        });


        $(document).ready(function () {
            $("#costo").on("input", function () {
                // allow numbers, a comma or a dot
                var v = $(this).val(), vc = v.replace(/[^0-9,\.]/, '');
                if (v !== vc)
                    $(this).val(vc);
            });
        });
        $('.timepicker').timepicker({
            showInputs: false
        });

        //$(document).ready(function () {
        //    $('#GridViewUser').DataTable({
        //        "paging": true,
        //        "lengthChange": false,
        //        "searching": false,
        //        'ordering': true,
        //        "info": true,
        //        "autoWidth": false
        //    });
        //});

    </script>
		 <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
    <!-- Codigo para que combo se leija escribiendo -->
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
	<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
            
</body>
</html>
