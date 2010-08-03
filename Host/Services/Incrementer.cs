using System;
using System.Linq;
using System.ServiceModel;

namespace Kockerbeck.ServiceCloud.Services
{
	public class Incrementer : IIncrementer
	{
		public int Increment(int i)
		{
			return i + 1;
		}

		public Response Execute(Request request)
		{
			if (request == null) throw new FaultException("Request is NULL");

			var response = new Response
			{
				ReturnObject = request.Argument
			};

			if (request.Argument == null) return response;
			if (request.Argument.GetType() != typeof(int)) return response;

			response.ReturnObject = Increment((int)request.Argument);

			return response;
		}
	}
}