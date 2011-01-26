using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace HostsEditor
{
	public class Program
	{
		private const string _backupFileExtension = ".bck";

		private static readonly Regex HostsEntryRegex =
			new Regex(@"^(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})[ \t]+(?<dns>.+)");

		private static bool _quiet;

		private static void Main(string[] args)
		{
			if ("/q".Equals(args.LastOrDefault(), StringComparison.OrdinalIgnoreCase))
			{
				_quiet = true;
				args = args.Take(args.Length - 1).ToArray();
			}

			if (args.Length < 1)
			{
				PrintUsage();
				return;
			}

			var hostsFileName = GetHostFileName();
			try
			{
				IPAddress ipAddress;
				if (IPAddress.TryParse(args[0], out ipAddress))
				{
					if (args.Length < 2)
					{
						PrintUsage();
						return;
					}

					var hostName = args[1];

					var lines = GetEntries(hostsFileName);
					var line = GetEntry(lines, hostName);
					if (line != null)
					{
						PrintInfo("Exists already", line);
					}
					else
					{
						MakeBackupOf(hostsFileName);
						using (var sr = File.AppendText(hostsFileName))
						{
							var newEntry = string.Format("{0}\t{1}", ipAddress, hostName);
							// add line break if there isn't one at the end)
							if (lines.Last() != "")
								sr.WriteLine();
							sr.WriteLine(newEntry);
							sr.Close();
							PrintInfo("Added", newEntry);
						}
					}
				}
				else
				{
					var hostName = args[0];
					var lines = GetEntries(hostsFileName);
					var line = GetEntry(lines, hostName);
					if (line == null)
					{
						PrintInfo("Not found", hostName);
					}
					else
					{
						lines.Remove(line);
						MakeBackupOf(hostsFileName);
						File.WriteAllLines(hostsFileName, lines);
						PrintInfo("Removed", hostName);
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				Console.WriteLine("You don't have the necessary rights to access the file {0}.", hostsFileName);
				Console.WriteLine("Make sure you are running this program as an administrator.");
			}
			catch (IOException ex)
			{
				Console.WriteLine("An error occurred while accessing the file {0}:", hostsFileName);
				Console.WriteLine();
				Console.WriteLine(ex.Message);
			}
		}

		private static void PrintInfo(string title, string hostName)
		{
			if (_quiet) return;

			Console.WriteLine();
			Console.WriteLine("{0,-15} {1}", title + ":", hostName);
		}

		private static void MakeBackupOf(string hostsFileName)
		{
			var bckFileName = GetBackupFileName(hostsFileName);
			if (File.Exists(bckFileName))
				File.Delete(bckFileName);
			File.Copy(hostsFileName, bckFileName);
		}

		private static string GetBackupFileName(string hostsFileName)
		{
			return hostsFileName + _backupFileExtension;
		}

		private static string GetEntry(IEnumerable<string> lines, string hostName)
		{
			return (from line in lines
			        let match = HostsEntryRegex.Match(line)
			        where match.Groups["dns"].Value == hostName
			        select line).FirstOrDefault();
		}

		private static IList<string> GetEntries(string hostsFileName)
		{
			return File.ReadAllLines(hostsFileName).ToList();
		}

		private static string GetHostFileName()
		{
			var dir = Environment.GetEnvironmentVariable("windir") ?? @"C:\Windows";
			return Path.Combine(dir, @"System32\drivers\etc\hosts");
		}

		private static void PrintUsage()
		{
			Console.WriteLine(
				@"
Adds and removes entries to and from the Windows hosts file.
Automatically backs up the original hosts file to hosts{1}.
Does not add duplicate entries; instead prints a warning.

Usage:
------
Add:	{0} <ip-address> <host-name> [/q]
			e.g.: {0} 127.0.0.1 my-local-site.com
Remove:	{0} <host-name> [/q]
			e.g.: {0} my-local-site.com

Options:
--------
/q	suppresses all regular output messages, but still shows exceptions
	has to be the last parameter
",
				Environment.GetCommandLineArgs()[0].Split(Path.DirectorySeparatorChar).Last(),
				_backupFileExtension);
		}
	}
}