using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCloud.Extensions;

namespace ServiceCloud
{
	public class Gateway : IGateway
	{
		public Response Execute(Request request)
		{
			var response = new Response();
			
			var servicesFound = new List<string>();

			foreach (var service in request.Services)
			{
				Console.WriteLine(String.Format(" \t Request to call service {0}", service));

				var serviceType = GetCloudServices().SingleOrDefault(s => String.Equals(s.Name, service));

				Console.WriteLine(String.Format(" \t Type is: {0}", serviceType == null ? "UNKNOWN" : serviceType.FullName));

				if(serviceType != null)
				{
					var cloudServiceInstance = Activator.CreateInstance(serviceType) as ICloudService;
					if(cloudServiceInstance == null)
					{
						Console.WriteLine("EXCEPTION: Cannot create instance of {0}", serviceType.FullName);
						continue;
					}

					cloudServiceInstance.Execute(request);
					servicesFound.Add(serviceType.FullName);
				}
			}

			// Assign the ReturnObject as the object that the last service touched
			response.ReturnObject = request.Argument; 

			// Add the Array of Services that were run & their order
			response.ServicesRan = servicesFound.ToArray();

			return response;
		}

		private static List<Type> _cloudServices;
		public static List<Type> GetCloudServices()
		{
			if (_cloudServices != null) return _cloudServices;

			var cloudType = typeof(ICloudService);
			_cloudServices = AppDomain.CurrentDomain.GetAssemblies().ToList()
				.SelectMany(s => s.GetTypes())
				.Where(p => p.IsImplementationOf(cloudType))
				.ToList();

			return _cloudServices;
		}
	}
}