using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using MultiDataBases.Core;

namespace MultiDataBases
{
    static class Program
    {
        private static ServiceHost _serviceHost;

        static void Main()
        {
            InitializeService();
            Console.WriteLine("Service is initialized...");
            StartService();

            Console.WriteLine("Press any key to close service...");
            Console.ReadLine();
            StopService();
        }

        private static void StopService()
        {
            _serviceHost.Close();
        }

        private static void StartService()
        {
            _serviceHost.Open();
        }

        private static void InitializeService()
        {
            _serviceHost = new ServiceHost(typeof(DatabaseService), new[]
                {
                    new Uri("http://localhost:8080/MultiDatabase")
                });
            _serviceHost.AddServiceEndpoint(typeof(IDatabaseService), new BasicHttpBinding(), "");
            _serviceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            _serviceHost.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexHttpBinding(), "MEX");
        }
    }
}
