$(document).ready(function () {
    $(".txtNumeric").change(function () {
        var txtCantidad = $(this).closest("tr").find("input[name*='txtCantidad']");
        var txtPrecio = $(this).closest("tr").find("input[name*='txtPrecio']");
        var txtTotal = $(this).closest("tr").find("input[name*='txtTotal']");
        var tabla = $(this).closest("table");
        if (!$.isNumeric(txtCantidad.val()))
            return;
        if (!$.isNumeric(txtPrecio.val()))
            return;
        var total = parseFloat(txtCantidad.val()) * parseFloat(txtPrecio.val());
        txtTotal.val(total);
        SumaTodasFilas(tabla)
    });

});

function SumaTodasFilas(tabla) {
    var suma = 0;
    tabla.find("tr").each(function () {
        var txtTotal = $(this).find("input[name*='txtTotal']");
        if (!$.isNumeric(txtTotal.val()))
            return;
        suma += parseFloat(txtTotal.val());
    })
    var txtTotalVenta = tabla.find("input[name*='txtTotVenta']");
    txtTotalVenta.val(suma);
}