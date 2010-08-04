using System;
using System.Configuration;
using System.Linq;
using Kockerbeck.ServiceCloud.Client.Gateway;

namespace Kockerbeck.ServiceCloud.Client
{
	class Program
	{
		static void Main()
		{
			// Start up the Cloud Gateway!
			using (var cloud = new CloudServiceClient())
			{
				// Formulate our Request
				var request = new Request
				{
					Argument = 15,
					Services = new[]
					{
						new ServiceCall
						{
							Name = "Decrementer",
							Services = new[]
							{
								new ServiceCall { Name = "Incrementer" },
								new ServiceCall { Name = "Incrementer" },
								new ServiceCall { Name = "Incrementer" },
								new ServiceCall { Name = "Incrementer" },
								new ServiceCall { Name = "Incrementer" },
								new ServiceCall { Name = "Incrementer" },
								new ServiceCall { Name = "Incrementer" },
							},
						},
						new ServiceCall { Name = "Decrementer" },
						new ServiceCall { Name = "Decrementer" },
						new ServiceCall { Name = "Decrementer" },
					},
				};

				// Assign the Endpoint Addresses (avoids redundancy -- this could easily be its own Service)
				request.Services.ToList().ForEach(AssignAddress);

				// Get a Response from the Gateway
				var response = cloud.Execute(request);

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

		private static void AssignAddress(ServiceCall serviceCall)
		{
			if(serviceCall != null && String.IsNullOrEmpty(serviceCall.Address))
			{
				serviceCall.Address = ConfigurationManager.AppSettings["ServiceCloud.EndpointAddress." + serviceCall.Name];
				
				if(String.IsNullOrEmpty(serviceCall.Address))
				{
					throw new Exception("Cannot find EndpointAddress for " + serviceCall.Name);
				}

				if (serviceCall.Services != null && serviceCall.Services.Length > 0)
				{
					serviceCall.Services.ToList().ForEach(AssignAddress);
				}
			}
		}
	}
}