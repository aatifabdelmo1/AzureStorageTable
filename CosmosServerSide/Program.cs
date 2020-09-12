using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CosmosServerSide
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string EndpointUri = "https://sqlcosmos111.documents.azure.com:443";
            // The primary key for the Azure Cosmos account.
            string PrimaryKey = "aULr1s07gMJu8jRKFexAMn7KdZiz4LC8C2zCyjW53ldKpumDxkTCbrDFKi4nPH9EZHaMgt90HmJjBBK5mTV61Q==";
            Container container;
            Database database;
            CosmosClient cosmosclient = new CosmosClient(EndpointUri, PrimaryKey);
            database = cosmosclient.GetDatabase("FamilyDatabase");
            container = database.GetContainer("FamilyContainer");
            CreateSp("HollowWorld", container).GetAwaiter();
            ViewSp(container).GetAwaiter();
            ExcuteSp(container, "HollowWorld").GetAwaiter();
            Console.ReadLine();

        }


        public  async static Task CreateSp(string SpId, Container container)
        {
            

            var sppropertiew = new StoredProcedureProperties()
            {
                Id = SpId,
                Body = await File.ReadAllTextAsync($@"C:\Courses\Azure Development Course labs\Azure Storage\Azure Implement Table\AzureStoragTableAsp.net\AzureStorageTable\CosmosServerSide\SP\{SpId}.js")

        };
            var result = await container.Scripts.CreateStoredProcedureAsync(sppropertiew);
            Console.WriteLine($"Sp {result.Resource.Id} is Created with {result.RequestCharge}");
            }
        public  async static Task ViewSp(Container container)
        {
            var Iteration = container.Scripts.GetStoredProcedureQueryIterator<StoredProcedureProperties>();
            var Sps = await Iteration.ReadNextAsync();

            int count = 0;

            foreach(var sp in Sps)
            {
                count++;
                Console.WriteLine($"Sp : {sp.Id}");
            }

            Console.WriteLine($"Count is {count}");
        }
        public async static Task  ExcuteSp(Container container ,string SPName)
        {
            var PK = new PartitionKey(string.Empty);
            var reesult = await container.Scripts.ExecuteStoredProcedureAsync<string>(SPName, PK, null);
            var messages = reesult.Resource;
            Console.WriteLine(messages);
        }

    }
}
