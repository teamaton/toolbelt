<%@ Page Language="C#"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Class example</title>
	<style type="text/css">
		.red {color: Red}
		.green {color: Green}
		.bold {font-weight:bold }
	</style>
</head>
<body>
		<form id="Form" runat="server">
		<div>
			<asp:HyperLink runat="server" ID="hlClass" Text="HyperLink with class" class="red" />
			<asp:HyperLink runat="server" ID="hlCssClass" Text="HyperLink with class" CssClass="green" />
			<asp:Button Text="Make bold" runat="server" ID="btBold" OnClick="MakeBold" />
		</div>
		</form>
</body>
</html>

<script runat="server">
	private void MakeBold(object sender, EventArgs e)
	{
		hlClass.Attributes["class"] += " bold";
		hlClass.CssClass += " bold";
		hlCssClass.Attributes["class"] += " bold";
		hlCssClass.CssClass += " bold";
	}
</script>
