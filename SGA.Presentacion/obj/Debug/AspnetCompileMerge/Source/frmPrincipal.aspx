<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPrincipal.aspx.cs" Inherits="SGA.Presentacion.frmPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
    <head>
		<title>...::: SGC :::...</title>  
	</head>  
 
    <frameset rows="70,43,*">  
        <frame name="cabecera" src="frmCabecera.aspx" frameBorder="no" marginWidth="0" marginHeight="0" scrolling="no"></frame>
        <frame src="frmBarPri.aspx" frameBorder="no" noResize scrolling="no">
	        <frameset cols="300,*">        
		        <frame src="frmMenu.aspx" name="TreeInFrameLeft" id="TreeInFrameLeft" marginWidth="0" marginHeight="0" scrolling="yes">
		            <frame src="frmBienvenida.aspx" name="Contenido" id="TreeInFrameMain" marginWidth="0" marginHeight="0">
	        </frameset>
    </frameset>
</html>
