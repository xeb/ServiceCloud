using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ServiceCloud
{
	/// <summary>
	/// The default Service Host
	/// </summary>
	/// <see cref="http://msdn.microsoft.com/en-us/library/aa395224.aspx"/>
	public class Host : ServiceHost
	{
		public Host(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
		{
			
		}
        
		protected override void ApplyConfiguration()
		{
			base.ApplyConfiguration();

			ServiceMetadataBehavior mexBehavior = Description.Behaviors.Find<ServiceMetadataBehavior>();
			if (mexBehavior != null)
			{
				//Metadata behavior has already been configured, 
				//so we don't have any work to do.
				return;
			}
			
			mexBehavior = new ServiceMetadataBehavior();
			Description.Behaviors.Add(mexBehavior);

			//Add a metadata endpoint at each base address
			//using the "/mex" addressing convention
			foreach (Uri baseAddress in BaseAddresses)
			{
				if (baseAddress.Scheme == Uri.UriSchemeHttp)
				{
					mexBehavior.HttpGetEnabled = true;
					AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
				}
				else if (baseAddress.Scheme == Uri.UriSchemeHttps)
				{
					mexBehavior.HttpsGetEnabled = true;
					AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpsBinding(), "mex");
				}
				else if (baseAddress.Scheme == Uri.UriSchemeNetPipe)
				{
					AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexNamedPipeBinding(), "mex");
				}
				else if (baseAddress.Scheme == Uri.UriSchemeNetTcp)
				{
					AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
				}
			}

		}
	}
}
