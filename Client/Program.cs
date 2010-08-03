using System;
using System.Linq;

namespace Client
{
	class Program
	{
		static void Main()
		{
			using (var cloud = new Gateway.GatewayClient())
			{
				var response = cloud.Execute(new Gateway.Request
				{
					Argument = 10,
					Services = new[]
					{
						"Incrementer",
						"Incrementer",
						"Incrementer",
						"Decrementer",
						"Incrementer", 
						"Decrementer",
						"Incrementer", 
						"Decrementer",
						"Incrementer",
					},
				});

				if (response == null)
				{
					Console.WriteLine("Response is NULL!!!");
					Console.ReadLine();
					return;
				}

				Console.WriteLine("ReturnObject is {0}", response.ReturnObject);

				if (response.ServicesRan != null && response.ServicesRan.Length > 0)
				{
					response.ServicesRan.ToList().ForEach(s => Console.WriteLine("Ran on Service: {0}", s));
				}

				Console.ReadLine();
			}
		}
	}
}
