<%@ Page CodeBehind="logg.aspx.cs" Inherits="Logging.Logg" Language="C#"  %>
<%@ Import Namespace="System.IO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title></title>
</head>
<body>
		<form id="Form" runat="server">
		<div>
			<%= string.Join("<br/>", File.ReadAllLines(Server.MapPath(@"log\app.log"))) %>
		</div>
		</form>
</body>
</html>
