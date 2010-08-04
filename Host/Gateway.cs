using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Kockerbeck.ServiceCloud.Extensions;

namespace Kockerbeck.ServiceCloud
{
	public class Gateway : ICloudService
	{
		public Response Execute(Request request)
		{
			if (request == null) throw new FaultException("Request is NULL");

			var servicesFound = new List<string>();

			// Go through each service
			foreach (var service in request.Services)
			{
				CallService(service, request, servicesFound);
			}

			return new Response
			{
				// Assign the ReturnObject as the object that the last service touched
				ReturnObject = request.Argument,

				// Add the Array of Services that were run & their order
				ServicesRan = servicesFound.ToArray(),
			};
		}

		private static void CallService(ServiceCall service, Request request, ICollection<string> servicesFound)
		{
			Console.WriteLine(String.Format(" \t Request to call service {0}", service.Name));

			// Find the Service Interface Type
			var serviceType = GetCloudServices().SingleOrDefault(s => s.IsInterface && String.Equals(s.Name, String.Format("I{0}", service.Name)));
			if (serviceType == null)
			{
				Console.WriteLine("\t Type is UNKNOWN for {0}", service.Name);
				return;
			}

			Console.WriteLine(String.Format("\t Type is: {0}", serviceType.FullName));

			// Intiatialize a ChannelFactory
			using (var channelFactory = new ChannelFactory<ICloudService>(new WSHttpBinding(), new EndpointAddress(service.Address)))
			//using (var channelFactory = new ChannelFactory<ICloudService>(new WSHttpBinding()))
			{
				channelFactory.Open();

				Console.WriteLine("Calling Service {0} at Address {1}", service.Name, service.Address);

				try
				{
					// Create the Channel & Execute the Response
					var response = channelFactory.CreateChannel().Execute(request);

					// Replace the Original Request Argument
					request.Argument = response.ReturnObject;

					// Add the Service to the ServicesFound collection
					servicesFound.Add(serviceType.FullName);
				}
				catch (Exception ex)
				{
					Console.WriteLine("EXCEPTION - {0}", ex.Message);
				}

				channelFactory.Close();
			}

			// Recursively Call any other services
			if(service.Services != null && service.Services.Length > 0)
			{
				service.Services.ToList().ForEach(s => CallService(s, request, servicesFound));
			}
		}

		private static List<Type> _cloudServices;

		/// <summary>
		/// Get all ICloudService implementations
		/// </summary>
		/// <returns></returns>
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