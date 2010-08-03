using System;
using System.Linq;
using System.ServiceModel;

namespace Kockerbeck.ServiceCloud.Services
{
	[ServiceContract]
	public interface IIncrementer : ICloudService
	{
		[OperationContract]
		int Increment(int i);
	}
}