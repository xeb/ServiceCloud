using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCloud.Extensions;

namespace ServiceCloud
{
	class Program
	{
		static void Main()
		{
			List<Host> hosts = new List<Host>
			{
				new Host(typeof (Gateway)) // Add the Gateway service by default
			};

			foreach (var cloudServiceType in Gateway.GetCloudServices())
			{
				Console.WriteLine("Starting {0}...", cloudServiceType.Name);
				hosts.Add(new Host(cloudServiceType));
			}

			hosts.ForEach(h => h.Open());
			
			Console.WriteLine("Listening...");
			Console.WriteLine("[Hit Enter to Exit]");
			Console.ReadLine();

			hosts.ForEach(h => h.Close());
		}
	}
}
