using System;
using System.Collections.Generic;
using System.Linq;

namespace Kockerbeck.ServiceCloud
{
	public class Program
	{
		/// <summary>
		/// Main Execution 
		/// </summary>
		static void Main()
		{
			var hosts = new List<Host>();
			var services = Gateway.GetCloudServices();
			foreach (var cloudServiceType in services)
			{
				if (cloudServiceType.IsInterface) continue; // Skip the interfaces

				Console.WriteLine("Starting {0}...", cloudServiceType.Name);

				// Add to the Hosts collection
				hosts.Add(new Host(cloudServiceType));
			}

			// Start each Host
			hosts.ForEach(h => h.Open());
			
			Console.WriteLine("Listening...");
			Console.WriteLine("[Hit Enter to Exit]");
			Console.ReadLine();

			// Close each Host
			hosts.ForEach(h => h.Close());
		}
	}
}