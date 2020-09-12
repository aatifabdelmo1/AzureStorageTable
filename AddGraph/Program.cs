using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace AddGraph
{
    class Program
    {
        private static string Host = "graohical.gremlin.cosmos.azure.com";
        private static string PrimaryKey = "BCYsDuNo3a3tP4PR0xZFMhy6kO9Ru4rT3IZbJs2X0BGZbipH7QKdiXvrGZkUI7DCXIxZxS3bNgo8vMo04Y1sGA==";
        private static string Database = "Test";
        private static string Container = "Sample1";
        private static Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
        {
            {"Add John","g.addV('person').property('id','John').property('age',25).property('city','Egypt')" },
            {"Add Fawzy","g.addV('person').property('id','Fawzy').property('age',30).property('city','Egypt')" },
            {"Add Dxc","g.addV('company').property('id','Dxc').property('founded',2000).property('city','Egypt')" },
            {"Add johnWorksAt", "g.V().has('id','John').addE('worksat').to(g.V().has('id','Dxc'))" },
            {"Add FawzyWorksAt", "g.V().has('id','Fawzy').addE('worksat').to(g.V().has('id','Dxc'))" },
            {"Add FawzyManage", "g.V().has('id','Fawzy').addE('Manage').to(g.V().has('id','John'))" }
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string containerLink = "/dbs/" + Database + "/colls/" + Container;
            var gremlinServer = new GremlinServer(Host, 443, true,
                                        username: containerLink,
                                        password: PrimaryKey);

            ConnectionPoolSettings connectionPoolSettings = new ConnectionPoolSettings()
            {
                MaxInProcessPerConnection = 100,
                PoolSize = 50,
                ReconnectionAttempts = 10,
                ReconnectionBaseDelay = TimeSpan.FromMilliseconds(1000)
            };

            var webSocketConfiguration =
                new Action<ClientWebSocketOptions>(options =>
                {
                    options.KeepAliveInterval = TimeSpan.FromSeconds(100);
                });

            using (var client = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType,connectionPoolSettings, webSocketConfiguration))
            {

                foreach (var query in gremlinQueries)
                {
                   client.SubmitAsync(query.Value).GetAwaiter();
                    
                    Console.WriteLine(query.Key);

                }
            }

            Console.ReadLine();

        }

    }
}
