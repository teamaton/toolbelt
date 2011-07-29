<%@  language="VBSCRIPT" %>
<%Option Explicit '*** This must be the FIRST statement ***%>
<div>
	<%
dim validChars, textNo, textYes, phoneRegex
validChars = "123"
textNo = "123 hallo"
textYes = "(+34) 45/345-567-00.32"

Function isPhoneNumber(text)
	dim phoneRegex
	Set phoneRegex = new regexp 'Create the RegExp object
	phoneRegex.Pattern = "^[\d\.\(\)/+ -]*$" ' allow only: 0123456789./()-+ and space
	phoneRegex.IgnoreCase = true
	isPhoneNumber = phoneRegex.Test(text)
End Function
%>
	<% if isPhoneNumber(textNo) then %>
	false
	<% else %>
	true
	<% End If%>
	<hr />
	<% if isPhoneNumber(textYes) then %>
	true
	<% else %>
	false
	<% End If%>
</div>
