using System;
using System.Linq;
using System.ServiceModel;

namespace Kockerbeck.ServiceCloud
{
	[ServiceContract]
	public interface ICloudService
	{
		[OperationContract]
		Response Execute(Request request);
	}
}