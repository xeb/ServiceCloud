using System;
using System.Linq;
using System.ServiceModel;

namespace Kockerbeck.ServiceCloud.Services
{
	[ServiceContract]
	public interface IDecrementer : ICloudService
	{
		[OperationContract]
		int Decrement(int i);
	}
}