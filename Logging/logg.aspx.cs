using System;
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
	}
}