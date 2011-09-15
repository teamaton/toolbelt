using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI;
using log4net;

namespace Logging
{
	public class Logg : Page
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Logg));
		private static readonly ILog Log2 = LogManager.GetLogger("Logg");

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Log.IsInfoEnabled)
				Log.InfoFormat("Logging away at " + DateTime.Now.ToLongTimeString());
			if (Log2.IsDebugEnabled)
				Log2.DebugFormat("Logging away at " + DateTime.Now.ToLongTimeString());
		}

		public string ToDebugString(object obj, int maxdepth, int depth=0)
		{
			if (obj == null)
				return "null";

			if (obj is IConvertible)
				return obj.ToString();

			if (depth >= maxdepth)
				return "...";

			var sb = new StringBuilder();

			if (depth > 0)
				sb.AppendLine();

			foreach (var propertyInfo in obj.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance))
			{
				sb.Append(new string(' ', 2*depth)).Append(propertyInfo.Name).Append(": ");
				try
				{
					var value = propertyInfo.GetValue(obj, new object[0]);
					sb.AppendLine(ToDebugString(value, maxdepth, depth + 1));
				}
				catch (Exception ex)
				{
					sb.AppendLine(string.Format("[{0}]", ex.Message));
				}
			}

			// remove newline from end of string
			var newLine = Environment.NewLine;
			if (sb.Length >= newLine.Length)
				sb.Replace(newLine, "", sb.Length - newLine.Length, newLine.Length);

			return sb.ToString();
		}
	}
}