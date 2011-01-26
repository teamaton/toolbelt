using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HostsEditor
{
	public class Program
	{
		static readonly Regex HostsEntryRegex = new Regex(@"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})[ \t]+(?<dns>.+)");

		static void Main(string[] args)
		{
			if(args.Length < 1)
			{
				PrintUsage();
				return;
			}

			IPAddress ipAddress;
			if (IPAddress.TryParse(args[0], out ipAddress))
			{
				if (args.Length < 2)
				{
					PrintUsage();
					return;
				}

				var hostName = args[1];

				// add
				var hostsFileName = GetHostFileName();
				var lines = File.ReadAllLines(hostsFileName).Where(line=>!line.StartsWith("#"));
				foreach (var line in lines)
				{
					var match = HostsEntryRegex.Match(line);
					if(match.Groups["dns"].Value == hostName)
					{
						Console.WriteLine("Entry already exists!");
						return;
					}
				}

				using(var sr = File.AppendText(hostsFileName))
				{
					sr.WriteLine("{0}\t{1}", ipAddress, hostName);
					sr.Close();
				}
			}
		}

		private static string GetHostFileName()
		{
			var dir = Environment.GetEnvironmentVariable("windir") ?? @"C:\Windows";
			return Path.Combine(dir, @"System32\drivers\etc\hosts");
		}

		private static void PrintUsage()
		{
			Console.WriteLine(@"
Usage:
------
Add:	{0} <ip-address> <host-name>
			e.g.: {0} 127.0.0.1 my-local-site.com
Remove:	{0} <host-name>
			e.g.: {0} my-local-site.com
", Environment.GetCommandLineArgs()[0].Split(Path.DirectorySeparatorChar).Last());

			Console.ReadKey(true);
		}
	}
}
