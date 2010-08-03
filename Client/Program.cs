using System;
using System.Collections.Generic;
using System.Linq;
using Kockerbeck.ServiceCloud.Client.Gateway;

namespace Kockerbeck.ServiceCloud.Client
{
	class Program
	{
		static void Main()
		{
			// The services at our disposal (and their locations)
			List<ServiceCall> services = new List<ServiceCall>
			{
				new ServiceCall { Name = "Decrementer", Address = "http://localhost:8731/Design_Time_Addresses/ServiceCloud.Services/Decrementer/" },
				new ServiceCall { Name = "Incrementer", Address = "http://localhost:8731/Design_Time_Addresses/ServiceCloud.Services/Incrementer/" },
			};

			// Start up the Cloud Gateway!
			using (var cloud = new CloudServiceClient())
			{
				// Formulate our Request and get a Response from the Gateway
				var response = cloud.Execute(new Request
				{
					Argument = 15,
					Services = new[]
					{
					    services[0], // Decrement
					    services[0], // Decrement
					    services[0], // Decrement
					    services[1], // Increment
					    services[1], // Increment
					},
				});

				if (response == null)
				{
					Console.WriteLine("Response is NULL!!!");
					Console.ReadLine();
					return;
				}

				Console.WriteLine("ReturnObject is {0}", response.ReturnObject);

				// Find out which services ran
				if (response.ServicesRan != null && response.ServicesRan.Length > 0)
				{
					response.ServicesRan.ToList().ForEach(s => Console.WriteLine("Ran on Service: {0}", s));
				}

				cloud.Close();
			}

			Console.ReadLine();
		}
	}
}