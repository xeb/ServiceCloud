using System;
using System.Linq;
using System.ServiceModel;

namespace Kockerbeck.ServiceCloud.Services
{
	public class Decrementer : IDecrementer
	{
		public int Decrement(int i)
		{
			return i-1;
		}

		public Response Execute(Request request)
		{
			if(request == null) throw new FaultException("Request is NULL");

			var response = new Response
			{
				ReturnObject = request.Argument
			};

			if (request.Argument == null) return response;
			if (request.Argument.GetType() != typeof(int)) return response;

			response.ReturnObject = Decrement((int)request.Argument);

			return response;
		}
	}
}