using System;
using System.Collections.Generic;
using System.ServiceModel;
using MultiDataBases.Core;

namespace DatabaseClient.Core
{
    public class DatabaseManager
    {
        private readonly IDatabaseService m_client;

        public DatabaseManager()
        {
            var channel = new ChannelFactory<IDatabaseService>(new BasicHttpBinding(),
                new EndpointAddress(new Uri("http://localhost:8080/MultiDatabase")));
            m_client = channel.CreateChannel();
            SomeMethod();
        }

        private void SomeMethod()
        {
            var list = m_client.GetFaculties();
        }
    }
}