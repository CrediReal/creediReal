<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="frmEvaluacion.aspx.cs" Inherits="SGA.Presentacion.CREDITOS.frmEvaluacion" %>
<%@ Register src="~/conBuscarCliente.ascx" tagname="conBuscarCliente" tagprefix="uc1" %>
<%@ Register Assembly="SGA.Controles" Namespace="SGA.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/jquery-3.1.1.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/bootstrap.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="Styles/cssGeneral.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
      <%--  <div>
            <h2>
                <cc1:LabelBase ID="lblOpcion" runat="server">titulo:</cc1:LabelBase>
            </h2>
        </div>--%>
        <div align="center">
          

            <cc1:PanelBase ID="pnlBalance" runat="server" Width="100%">
                
                    <br />
                
                <asp:Button ID="btnActCorriente" CssClass="btn btn-danger" runat="server" Text="Activo corriente" OnClick="btnActCorriente_Click" Width="115px" />
                  &nbsp;<asp:Button ID="btnActNoCorriente" CssClass="btn btn-info" runat="server" Text="Activo no corriente " OnClick="btnActNoCorriente_Click" Width="135px" />
                  &nbsp;<asp:Button ID="btnPasCorriente" CssClass="btn btn-warning" runat="server" Text="Pasivo corriente " OnClick="btnPasCorriente_Click" Width="120px" />
                  &nbsp;<asp:Button ID="btnPasNoCorriente" CssClass="btn btn-success" runat="server" Text="Pasivo no corriente" OnClick="btnPasNoCorriente_Click" Width="140px" />
                  &nbsp;<asp:Button ID="btnPatrimonio" CssClass="btn btn-danger" runat="server" Text="Patrimonio" OnClick="btnPatrimonio_Click" Width="86px" />
                &nbsp;<asp:Button ID="btnEstadoResultado" CssClass="btn btn-primary" runat="server" Text="Estado Resultado" OnClick="btnEstadoResultado_Click" Width="130px" />
                    <br />
                <br />
                <cc1:PanelBase ID="pnlActivoCorriente" runat="server" Visible="false">

                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">

                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingSix">
                      <h4 class="panel-title">
                        <a  role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseSix" aria-expanded="false" aria-controls="collapseSix" class="collapsed">
                          CAJA(EFECTIVO)
                        </a>
                      </h4>
                    </div>
                    <div id="collapseSix" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingSix" aria-expanded="false">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Caja - efectivo: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtCajaEfectivo" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                              
                          </table>
                         
                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingOne">
                      <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne" class="collapsed">
                          BANCOS
                        </a>
                      </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                      <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Banco: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtBanco" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtBancoVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="botonAgregaBanco" OnClick="botonAgregaBanco_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgBancos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgBancos_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>

                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingTwo">
                      <h4 class="panel-title">
                        <a  role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" class="collapsed">
                          CUENTAS POR COBRAR A CLIENTES
                        </a>
                      </h4>
                    </div>
                    <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Cuenta por cobrar: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtCtaCobrar" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtCtaCobrarVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarCtaCobrar" OnClick="BotonAgregarCtaCobrar_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgCtaCobrar" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgCtaCobrar_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>
                                                   
                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingThree">
                      <h4 class="panel-title">
                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree" class="collapsed">
                          ADELANTOS REALIZADOS A PROVEEDORES
                        </a>
                      </h4>
                    </div>
                    <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Adelantos realizados: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtAdelanto" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtAdelantoVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarAdelanto" OnClick="BotonAgregarAdelanto_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgAdelanto" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgAdelanto_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>
                              

                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingFour">
                      <h4 class="panel-title">
                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFour" aria-expanded="false" aria-controls="collapseFour" class="collapsed">
                          INVENTARIO
                        </a>
                      </h4>
                    </div>
                    <div id="collapseFour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFour">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Item inventario: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtInventario" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtInventarioVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarInventario" OnClick="BotonAgregarInventario_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgInventario" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgInventario_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>
                         
                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingFive">
                      <h4 class="panel-title">
                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFive" aria-expanded="false" aria-controls="collapseFive" class="collapsed">
                          OTROS
                        </a>
                      </h4>
                    </div>
                    <div id="collapseFive" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFive">

                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Otros activos: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtOtrosActVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                              
                          </table>
                         
                      </div>
                    </div>
                  </div>

                </div>

                </cc1:PanelBase>

                <cc1:PanelBase ID="pnlActNoCorriente" runat="server" Visible="false">

                <div class="panel-group" id="accordion2" role="tablist" aria-multiselectable="true">
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingOne1">
                      <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne1" aria-expanded="true" aria-controls="collapseOne1">
                          MUEBLES Y ENSERES</a></h4>
                    </div>
                    <div id="collapseOne1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne1">
                      <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Muebles/Enseres: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtMueble" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtMuebleVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarMueble" OnClick="BotonAgregarMueble_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <tr>
                                  <td colspan="5">
                                  </td></tr>
                                  <tr>
                                      <td colspan="5">
                                          <cc1:GridViewBase ID="dtgMueble" runat="server" AutoGenerateColumns="False" OnRowDeleting="dtgMueble_RowDeleting" ShowHeaderWhenEmpty="True">
                                              <Columns>
                                                  <asp:BoundField DataField="id" Visible="False" />
                                                  <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                                  <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" />
                                                  <asp:TemplateField HeaderText="">
                                                      <ItemStyle HorizontalAlign="Center" />
                                                      <ItemTemplate>
                                                          <asp:LinkButton ID="P" runat="server" CausesValidation="false" CommandArgument="Eliminar" CommandName="Delete" Text="Quitar" ToolTip="Quitar">Quitar</asp:LinkButton>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </cc1:GridViewBase>
                                      </td>
                                  </tr>
                              
                          </table>

                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingTwo2">
                      <h4 class="panel-title">
                          <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion2" href="#collapseTwo2" aria-expanded="false" aria-controls="collapseTwo2">
                          INMUEBLE, MAQUINARIA Y EQUIPO</a>
                      </h4>
                    </div>
                    <div id="collapseTwo2" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo2">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Inmueble/maquinaria/equipo: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtInmueble" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtInmuebleVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarInmueble" OnClick="BotonAgregarInmueble_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <tr>
                                  <td colspan="5">
                                  </td></tr>
                                  <tr>
                                      <td colspan="5">
                                          <cc1:GridViewBase ID="dtgInmueble" runat="server" AutoGenerateColumns="False" OnRowDeleting="dtgInmueble_RowDeleting" ShowHeaderWhenEmpty="True">
                                              <Columns>
                                                  <asp:BoundField DataField="id" Visible="False" />
                                                  <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                                  <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" />
                                                  <asp:TemplateField HeaderText="">
                                                      <ItemStyle HorizontalAlign="Center" />
                                                      <ItemTemplate>
                                                          <asp:LinkButton ID="P" runat="server" CausesValidation="false" CommandArgument="Eliminar" CommandName="Delete" Text="Quitar" ToolTip="Quitar">Quitar</asp:LinkButton>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </cc1:GridViewBase>
                                      </td>
                                  </tr>
                              
                          </table>
                                                   
                      </div>
                    </div>
                  </div>
                </div>

                </cc1:PanelBase>

                <cc1:PanelBase ID="pnlPasivo" runat="server" Visible="false">

                <div class="panel-group" id="accordion3" role="tablist" aria-multiselectable="true">
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingOne2">
                      <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion3" href="#collapseOne2" aria-expanded="true" aria-controls="collapseOne2">
                         DEUDA A CORTO PLAZO(<= 1 año)</a></h4>
                    </div>
                    <div id="collapseOne2" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne2">
                      <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Deuda corto plazo: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtDeuda" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtDeudaVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarDeuda" OnClick="BotonAgregarDeuda_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <tr>
                                  <td colspan="5">
                                  </td></tr>
                                  <tr>
                                      <td colspan="5">
                                          <cc1:GridViewBase ID="dtgDeuda" runat="server" AutoGenerateColumns="False" OnRowDeleting="dtgDeuda_RowDeleting" ShowHeaderWhenEmpty="True">
                                              <Columns>
                                                  <asp:BoundField DataField="id" Visible="False" />
                                                  <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                                  <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" />
                                                  <asp:TemplateField HeaderText="">
                                                      <ItemStyle HorizontalAlign="Center" />
                                                      <ItemTemplate>
                                                          <asp:LinkButton ID="P" runat="server" CausesValidation="false" CommandArgument="Eliminar" CommandName="Delete" Text="Quitar" ToolTip="Quitar">Quitar</asp:LinkButton>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </cc1:GridViewBase>
                                      </td>
                                  </tr>
                              
                          </table>

                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingTwo4">
                      <h4 class="panel-title">
                          <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion3" href="#collapseTwo4" aria-expanded="false" aria-controls="collapseTwo4">
                         ADELANTO RECIBIDO DE PROVEEDORES</a>
                      </h4>
                    </div>
                    <div id="collapseTwo4" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo4">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Adelanto: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtAdelantoProv" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtAdelantoProvVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarAdelantoProv" OnClick="BotonAgregarAdelantoProv_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <tr>
                                  <td colspan="5">
                                  </td></tr>
                                  <tr>
                                      <td colspan="5">
                                          <cc1:GridViewBase ID="dtgAdelantoProv" runat="server" AutoGenerateColumns="False" OnRowDeleting="dtgAdelantoProv_RowDeleting" ShowHeaderWhenEmpty="True">
                                              <Columns>
                                                  <asp:BoundField DataField="id" Visible="False" />
                                                  <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                                  <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" />
                                                  <asp:TemplateField HeaderText="">
                                                      <ItemStyle HorizontalAlign="Center" />
                                                      <ItemTemplate>
                                                          <asp:LinkButton ID="P" runat="server" CausesValidation="false" CommandArgument="Eliminar" CommandName="Delete" Text="Quitar" ToolTip="Quitar">Quitar</asp:LinkButton>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </cc1:GridViewBase>
                                      </td>
                                  </tr>
                              
                          </table>
                                                   
                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingFive3">
                      <h4 class="panel-title">
                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion3" href="#collapseFive3" aria-expanded="false" aria-controls="collapseFive3">
                          OTROS
                        </a>
                      </h4>
                    </div>
                    <div id="collapseFive3" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingFive3">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Otros pasivos: </td>
                                  <td>
                                     </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtOtrosPasCorVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                              
                          </table>
                         
                      </div>
                    </div>
                  </div>

                </div>

                </cc1:PanelBase>

                <cc1:PanelBase ID="pnlPasivoNoCor" runat="server" Visible="false">

                <div class="panel-group" id="accordion4" role="tablist" aria-multiselectable="true">
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingOne3">
                      <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion4" href="#collapseOne3" aria-expanded="true" aria-controls="collapseOne3">
                        DEUDA A LARGO PLAZO (> 1 año)</a></h4>
                    </div>
                    <div id="collapseOne3" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne3">
                      <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Deuda largo plazo: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtDeudaLargo" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtDeudaLargoVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="BotonAgregarDeudaLargo" OnClick="BotonAgregarDeudaLargo_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <tr>
                                  <td colspan="5">
                                  </td></tr>
                                  <tr>
                                      <td colspan="5">
                                          <cc1:GridViewBase ID="dtgDeudaLargo" runat="server" AutoGenerateColumns="False" OnRowDeleting="dtgDeudaLargo_RowDeleting" ShowHeaderWhenEmpty="True">
                                              <Columns>
                                                  <asp:BoundField DataField="id" Visible="False" />
                                                  <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                                  <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80" />
                                                  <asp:TemplateField HeaderText="">
                                                      <ItemStyle HorizontalAlign="Center" />
                                                      <ItemTemplate>
                                                          <asp:LinkButton ID="P" runat="server" CausesValidation="false" CommandArgument="Eliminar" CommandName="Delete" Text="Quitar" ToolTip="Quitar">Quitar</asp:LinkButton>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                          </cc1:GridViewBase>
                                      </td>
                                  </tr>
                              
                          </table>

                      </div>
                    </div>
                  </div>
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingFive4">
                      <h4 class="panel-title">
                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion4" href="#collapseFive4" aria-expanded="false" aria-controls="collapseFive4">
                          OTROS
                        </a>
                      </h4>
                    </div>
                    <div id="collapseFive4" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingFive4">
                      <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Otros pasivo no corriente: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtOtrosPasNoCorVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                              
                          </table>
                         
                      </div>
                    </div>
                  </div>

                </div>

                </cc1:PanelBase>

                 <cc1:PanelBase ID="pnlPatrimonio" runat="server" Visible="false">

                <div class="panel-group" id="accordion5" role="tablist" aria-multiselectable="true">
                  <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingOne4">
                      <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion5" href="#collapseOne4" aria-expanded="true" aria-controls="collapseOne4">
                        PATRIMONIO</a></h4>
                    </div>
                    <div id="collapseOne4" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne4">
                      <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Patrimonio: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtPatrimonioVal" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                              <tr>
                                  <td colspan="5">
                                  </td></tr>                              
                          </table>

                      </div>
                    </div>
                  </div>
                </div>

                </cc1:PanelBase>

                <cc1:PanelBase runat="server" ID="pnlEstadoresultado" Visible="false">
                    <h4>Ingresos por Ventas/Servicios</h4>
                    <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Descripción: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtDescripcion" runat="server" Width="200px"></cc1:TextBoxBase></td>
                                  <td>Cantidad: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtCantidad" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>
                                  </td>
                                   <td>Precio: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtPrecio" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>
                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="btnAgregarVentaServicio" OnClick="btnAgregarVentaServicio_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgVentaServicio" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgVentaServicio_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nCantidad" HeaderText="Cantidad" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="nPrecio" HeaderText="Precio" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>

                      </div>
                     <h4>Costos por Ventas/Producción</h4>
                    <div class="panel-body">
                       
                          <table>
                              <tr>
                                  <td>Descripción: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtDescProd" runat="server" Width="200px"></cc1:TextBoxBase></td>
                                  <td>Cantidad: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtCantidadProd" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>
                                  </td>
                                   <td>Precio: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtPrecioProd" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>
                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="btnAgregarProd" OnClick="btnAgregarProd_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgVentaProd" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgVentaProd_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Descripción" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nCantidad" HeaderText="Cantidad" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="nPrecio" HeaderText="Precio" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>

                      </div>
                     <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Costo operativo: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtCostoOperativo" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                            <tr>
                                  <td>Tributos: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtTributo" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                                <tr>
                                  <td>Transporte: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtTransporte" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                             <tr>
                                  <td>Alquileres: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtAlquileres" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                             <tr>
                                  <td>Agua Luz Teléfono: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtAguaLuzTel" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                            <tr>
                                  <td>Otros: </td>
                                  <td>
                                      </td>
                                  <td> </td>
                                  <td>
                                      <cc1:NumberBox ID="txtOtros" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      </td>
                              </tr>
                              
                          </table>
                         
                      </div>

                    <h4>Créditos Directos</h4>
                    <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Entidad financiera: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtEntidad" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtMontoEntidad" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="btnAgregarFinanciera" OnClick="btnAgregarFinanciera_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgCreditoDirecto" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgCreditoDirecto_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Entidad financiera" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>
                                                   
                      </div>

                     <h4>Créditos Indirectos/Contingentes</h4>
                    <div class="panel-body">
                        <table>
                              <tr>
                                  <td>Entidad financiera: </td>
                                  <td>
                                      <cc1:TextBoxBase ID="txtEntidadIndirecto" runat="server" Width="280px"></cc1:TextBoxBase></td>
                                  <td>Monto: </td>
                                  <td>
                                      <cc1:NumberBox ID="txtMontoIndirecto" runat="server" Width="60px" DecimalPlaces="2"></cc1:NumberBox>

                                  </td>
                                  <td>
                                      &nbsp;&nbsp;&nbsp;
                                      <cc1:BotonAgregarMini ID="btnAgregarEntidadIndirecto" OnClick="btnAgregarEntidadIndirecto_Click" runat="server" Height="38px" Width="38px" /></td>
                              </tr>
                              <td colspan="5"></td>
                              <tr>
                                  <td colspan="5">
                                     <cc1:GridViewBase ID="dtgCreditosIndirectos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDeleting="dtgCreditosIndirectos_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="False" />
                                            <asp:BoundField DataField="cDescripcion" HeaderText="Entidad financiera" ItemStyle-Width="350" />
                                            <asp:BoundField DataField="nMonto" HeaderText="Monto" DataFormatString="{0:0,0.00}"  ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right"  />
                                             <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
									        <ItemTemplate >
									            <asp:LinkButton id="P" runat="server" CommandArgument="Eliminar" CommandName="Delete" ToolTip="Quitar" CausesValidation="false" Text="Quitar">Quitar</asp:LinkButton>
									        </ItemTemplate>
                                        </asp:TemplateField> 
                                        </Columns>
                                       
                                    </cc1:GridViewBase>
                                  </td>
                              </tr>
                          </table>
                                                   
                      </div>
                </cc1:PanelBase>
            </cc1:PanelBase>

            <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button runat="server" ID="btnCancelar" formnovalidate CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_Click" />
            <asp:Button runat="server" ID="btnImprimir" CssClass="btn btn-warning" Text="Imprimir Reporte" OnClick="btnImprimir_Click" Visible="False" />
        </div>
        <asp:HiddenField runat="server" ID="hidSolicitud" Value="0" />
    </form>
</body>
</html>