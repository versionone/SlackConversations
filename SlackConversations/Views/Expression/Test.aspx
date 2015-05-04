<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<title>Test Slack Integration</title>
	<style>
		label 
		{
			display: block;
			margin-bottom: 2em;
		}

		input, textarea
		{
			width: 400px;
			display: block;
		}

		textarea 
		{
			height: 100px;
		}
	</style>
</head>
<body>
	<h1>Send a message to Slack</h1>
	<div>
		<form action="<%= Url.Action("Changed", "Expression") %>" method="POST">
			<label>
				Author
				<input type="text" name="AuthorName" value="Dan Lash" />
			</label>

			<label>
				Is Newly Created
				<input type="text" name="IsNewlyCreated" value="True" />
			</label>

			<label>
				Content
				<textarea name="Content">This is a test! <%= DateTime.Now %></textarea>
			</label>

			<button type="submit">Send It!</button>
		</form>
	</div>
</body>
</html>
