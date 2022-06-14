<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmContenidoMenu.aspx.cs" Inherits="SGA.Presentacion.frmContenidoMenu" %>

<%@ Register assembly="SGA.Controles" namespace="SGA.Controles" tagprefix="cc2" %>
<!--<!DOCTYPE html>
<!--
This is a starter template page. Use this page to start your new project from
scratch. This page gets rid of all links and provides the needed markup only.
-->
<html>
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>..:: SGC ::..</title>
  <link rel='shortcut icon' type='image/x-icon' href='Imagenes/favicon.ico' />
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect. -->
  <link rel="stylesheet" href="dist/css/skins/skin-purple-light.min.css">
  

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

  <!-- Google Font -->
  <link rel="stylesheet"
        href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
    <script src="js/jquery-1.8.3.min.js"></script>
   <script type="text/javascript">
       function go(loc, cOpcion) {
           var iframe = document.getElementById('frmcontenido');
           iframe.src = loc;
           document.getElementById('cOpcion').innerText = cOpcion;
       }
       function resizeIframe(obj) {
           obj.style.height = 0;
           var scroll = obj.contentWindow.document.body.scrollHeight + 40;
           obj.style.height = scroll + 'px';
       }
    </script>
    
</head>
<!--
BODY TAG OPTIONS:
=================
Apply one or more of the following classes to get the
desired effect
|---------------------------------------------------------|
| SKINS         | skin-blue                               |
|               | skin-black                              |
|               | skin-purple                             |
|               | skin-yellow                             |
|               | skin-red                                |
|               | skin-green                              |
|---------------------------------------------------------|
|LAYOUT OPTIONS | fixed                                   |
|               | layout-boxed                            |
|               | layout-top-nav                          |
|               | sidebar-collapse                        |
|               | sidebar-mini                            |
|---------------------------------------------------------|
-->
<body class="hold-transition skin-purple-light fixed">
    <form id="Form1" runat="server">
<div class="wrapper">

  <!-- Main Header -->
  <header class="main-header">

    <!-- Logo -->
    <a href="index2.html" class="logo">
      <!-- mini logo for sidebar mini 50x50 pixels -->
      <span class="logo-mini"><b>SGC</span>
      <!-- logo for regular state and mobile devices -->
      <%--<span class="logo-lg"><b>SGC</b></span>--%>
        <span class="logo-lg"><b><P style="color:#EE1D23; background-color:yellow;">PRUEBAS</P> </b></span>
    </a>

    <!-- Header Navbar -->
    <nav class="navbar navbar-static-top" role="navigation">
      <!-- Sidebar toggle button-->
      <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
        <span class="sr-only">Toggle navigation</span>
      </a>
      <!-- Navbar Right Menu -->
      <div class="navbar-custom-menu">
        <ul class="nav navbar-nav">
          <!-- Messages: style can be found in dropdown.less-->
          <li class="dropdown messages-menu">
            <!-- Menu toggle button -->
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="fa fa-envelope-o"></i>
              <span class="label label-success">1</span>
            </a>
            <ul class="dropdown-menu">
              <li class="header">Tiene 1 mensajes</li>
              <li>
                <!-- inner menu: contains the messages -->
                <ul class="menu">
                  <li><!-- start message -->
                    <a href="#">
                      <div class="pull-left">
                        <!-- User Image -->
                        <img runat="server" id="imgUsuario" src="Imagenes/usuarios/user.jpg" class="img-circle" alt="User Image">
                      </div>
                      <!-- Message title and timestamp -->
                      <h4>
                        Soporte de TI
                        <small><i class="fa fa-clock-o"></i> 5 mins</small>
                      </h4>
                      <!-- The message -->
                      <p>Recuerda enviar los errores del sistema <br /> al grupo de soporte de sistemas</p>
                    </a>
                  </li>
                  <!-- end message -->
                </ul>
                <!-- /.menu -->
              </li>
              <li class="footer"><a href="#">Ver todos los mensajes</a></li>
            </ul>
          </li>
          <!-- /.messages-menu -->

          <!-- Notifications Menu -->
          <li class="dropdown notifications-menu">
            <!-- Menu toggle button -->
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="fa fa-bell-o"></i>
              <span class="label label-warning">1</span>
            </a>
            <ul class="dropdown-menu">
              <li class="header">Tiene 1 notificaciones</li>
              <li>
                <!-- Inner Menu: contains the notifications -->
                <ul class="menu">
                  <li><!-- start notification -->
                    <a href="#">
                      <i class="fa fa-users text-aqua"></i> Próximamente mas novedades
                    </a>
                  </li>
                  <!-- end notification -->
                </ul>
              </li>
              <li class="footer"><a href="#">Ver todo</a></li>
            </ul>
          </li>
          <!-- Tasks Menu -->
          <li class="dropdown tasks-menu">
            <!-- Menu Toggle Button -->
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="fa fa-flag-o"></i>
              <span class="label label-danger">0</span>
            </a>
            <ul class="dropdown-menu">
              <li class="header">Tiene 0 tareas</li>
              <li>
                <!-- Inner menu: contains the tasks -->
                <ul class="menu">
                  <li><!-- Task item -->
                    <a href="#">
                      <!-- Task title and progress text -->
                      <h3>
                        <%--Evento Road Show--%>
                        <small class="pull-right"><%--20%--%></small>
                      </h3>
                      <!-- The progress bar -->
                      <div class="progress xs">
                        <!-- Change the css width attribute to simulate progress -->
                        <div class="progress-bar progress-bar-aqua" style="width: 0%" role="progressbar"
                             aria-valuenow="20" aria-valuemin="0" aria-valuemax="100">
                          <span class="sr-only">20% Complete</span>
                        </div>
                      </div>
                    </a>
                  </li>
                  <!-- end task item -->
                </ul>
              </li>
              <li class="footer">
                <a href="#">Ver todas las tareas</a>
              </li>
            </ul>
          </li>
          <!-- User Account Menu -->
          <li class="dropdown user user-menu">
            <!-- Menu Toggle Button -->
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <!-- The user image in the navbar-->
              <img  runat="server" id="imgUsuario2" src="dist/img/user.jpg" class="user-image" alt="User Image">
              <!-- hidden-xs hides the username on small devices so only the image appears. -->
              <span class="hidden-xs">
                <asp:Label ID="lblNombreUser" runat="server" Text="User"></asp:Label>

              </span>
            </a>
            <ul class="dropdown-menu">
              <!-- The user image in the menu -->
              <li class="user-header">
                <img  runat="server" id="imgUsuario3" src="dist/img/user.jpg" class="img-circle" alt="User Image">

                <p>
                  <asp:Label ID="lblNombreUserCargo" runat="server" Text="User-Cargo"></asp:Label>
                  <small><asp:Label ID="lblFechaInicio" runat="server" Text="FechaInicio"></asp:Label></small>
                </p>
              </li>
              <!-- Menu Body -->
              <%--<li class="user-body">
                <div class="row">
                  <div class="col-xs-4 text-center">
                    <a href="#">Vegas</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href="#">Ventas</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href="#">Meta</a>
                  </div>
                </div>
                <!-- /.row -->
              </li>--%>
              <!-- Menu Footer-->
              <li class="user-footer">
                <div class="pull-left">
                  <a href="#" class="btn btn-default btn-flat">Perfil</a>
                </div>
                <div class="pull-right">
                  <%--<a href="#" class="btn btn-default btn-flat">Salir--%>
                      <asp:Button ID="btnSalir" Text="Salir" CssClass="btn btn-default btn-flat" runat="server" OnClick="btnSalir_Click" />
                  <%--</a>--%>
                </div>
              </li>
            </ul>
          </li>
          <!-- Control Sidebar Toggle Button -->
          <li>
            <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
          </li>
        </ul>
      </div>
    </nav>
  </header>
  <!-- Left side column. contains the logo and sidebar -->
  <aside class="main-sidebar">

    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">

      <!-- Sidebar user panel (optional) -->
      <div class="user-panel">
        <div class="pull-left image">
          <img  runat="server" id="imgUsuario4" src="dist/img/user.jpg" class="img-circle" alt="User Image">
        </div>
        <div class="pull-left info">
          <p><asp:Label ID="lblNombreUserMenu" runat="server" Text="User"></asp:Label></p>
          <!-- Status -->
          <a href="#"><i class="fa fa-circle text-success"></i> En Línea</a>
        </div>
      </div>

      <!-- search form (Optional) -->
      <%--<form action="#" method="get" class="sidebar-form">
        <div class="input-group">
          <input type="text" name="q" class="form-control" placeholder="Buscar...">
          <span class="input-group-btn">
              <button type="submit" name="search" id="search-btn" class="btn btn-flat"><i class="fa fa-search"></i>
              </button>
            </span>
        </div>
      </form>--%>
      <!-- /.search form -->

      <!-- Sidebar Menu -->
      <ul class="sidebar-menu" data-widget="tree" id="ulMenu" runat="server">
        <li class="header">
            <cc2:LabelBase ID="lblInfoFecha" runat="server"></cc2:LabelBase><br />
            <cc2:LabelBase ID="lblInfoAgencia" runat="server"></cc2:LabelBase>
        </li>
          
        <!-- Optionally, you can add icons to the links -->
        <li class="active">
            <a href="#">
                <%--<i class="fa fa-dashboard"></i> --%>
                <span>Módulo: <cc2:ComboBoxBase ID="cboModulo" runat="server" CssClass="cmbBase" Width="126px" OnSelectedIndexChanged="cboModulo_SelectedIndexChanged" AutoPostBack="true"></cc2:ComboBoxBase></span> 
                <span class="pull-right-container">
                    <%--<i class="fa fa-dashboard"></i>--%>
                </span>
            </a>
        </li>
          
      </ul>
      <!-- /.sidebar-menu -->
    </section>
    <!-- /.sidebar -->
  </aside>

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h2 id="cOpcion">
        Sistema de Gestión Crediticia - CrediReal
        <%--<small>Nosotros tenemos que ser el cambio que queremos ver en el mundo</small>--%>
      </h2>
      <ol class="breadcrumb">
        <li><a href="frmContenidoMenu.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
        <%--<li class="active">Inicio</li>--%>
        <%--<li class="active">Here</li>--%>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content container-fluid">

      <!--------------------------
        | Your Page Content Here |
        -------------------------->

        <iframe style="background-color:white" frameborder="0" id="frmcontenido" class="iframe-placeholder" src="frmBienvenida.aspx" width="100%" onload="resizeIframe(this)"></iframe>
        <%--<div id="loadingMessage">Cargando...</div>--%>

    </section>
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->

  <!-- Main Footer -->
  <footer class="main-footer">
    <!-- To the right -->
    <div class="pull-right hidden-xs">
      Powered By AdminLTE  <%= DateTime.Now.Year.ToString() %>  &nbsp;&nbsp;&nbsp;
    </div>
    <!-- Default to the left -->
    <strong>Copyright &copy;  <%= DateTime.Now.Year.ToString() %>  <a href="#">Fast Solutions</a>.</strong> All rights reserved.
  </footer>

  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Create the tabs -->
    <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
      <li class="active"><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
      <li><a href="#control-sidebar-settings-tab" data-toggle="tab"><i class="fa fa-gears"></i></a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
      <!-- Home tab content -->
      <div class="tab-pane active" id="control-sidebar-home-tab">
        <h3 class="control-sidebar-heading">Actividad reciente</h3>
        <ul class="control-sidebar-menu">
          <li>
            <a href="#">
              <i class="menu-icon fa fa-birthday-cake bg-red"></i>

              <div class="menu-info">
                <h4 class="control-sidebar-subheading">Cumpleaños de Alan</h4>

                <p>Será el 23 de Mayo</p>
              </div>
            </a>
          </li>
        </ul>
        <!-- /.control-sidebar-menu -->

        <h3 class="control-sidebar-heading">Progreso de Tareas</h3>
        <ul class="control-sidebar-menu">
          <li>
            <a href="#">
              <h4 class="control-sidebar-subheading">
                Avance de mora
                <span class="pull-right-container">
                    <span class="label label-danger pull-right">70%</span>
                  </span>
              </h4>

              <div class="progress progress-xxs">
                <div class="progress-bar progress-bar-danger" style="width: 70%"></div>
              </div>
            </a>
          </li>
        </ul>
        <!-- /.control-sidebar-menu -->

      </div>
      <!-- /.tab-pane -->
      <!-- Stats tab content -->
      <div class="tab-pane" id="control-sidebar-stats-tab">Stats Tab Content</div>
      <!-- /.tab-pane -->
      <!-- Settings tab content -->
      <div class="tab-pane" id="control-sidebar-settings-tab">
       <%-- <form method="post">--%>
          <h3 class="control-sidebar-heading">Configuración General</h3>

          <div class="form-group">
            <label class="control-sidebar-subheading">
              Activar envio de alerta?
              <input type="checkbox" class="pull-right" checked>
            </label>

            <p>
              Si presentase algún error o modificación del sistema, por favor comunicarse con el área de Tecnología de Información
            </p>
          </div>
          <!-- /.form-group -->
        <%--</form>--%>
      </div>
      <!-- /.tab-pane -->
    </div>
  </aside>
  <!-- /.control-sidebar -->
  <!-- Add the sidebar's background. This div must be placed
  immediately after the control sidebar -->
  <div class="control-sidebar-bg"></div>
</div>
<!-- ./wrapper -->

<!-- REQUIRED JS SCRIPTS -->

<!-- jQuery 3 -->
<script src="bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- AdminLTE App -->
<script src="dist/js/adminlte.min.js"></script>

<!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. -->
         <asp:HiddenField ID="hUsuario" runat="server" />
        <asp:HiddenField ID="hPerfil" runat="server" />
</form>
</body>
</html>