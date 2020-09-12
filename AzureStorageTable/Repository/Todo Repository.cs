using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageTable.Repository
{
    public class Todo_Repository
    {
        private CloudTable table;
        public Todo_Repository()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storageaccounttest11111;AccountKey=pT1DoiLnGPVhtW7jwFdX6Toz3pMQKMOugeTqMBtRMOTgLEDiyjLwXqS8ZLZl0LGYU5VF+TU3ezKB+fypbLijxw==;EndpointSuffix=core.windows.net");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("Todo");

        }

        public IEnumerable<Entities>GetAll()
        {
            var query = new TableQuery<Entities>();
            var entities = table.ExecuteQuery(query);
            return entities;
        }

        public void CreateorUpdate(Entities entity)
        {
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
            table.Execute(insertOrMergeOperation);
        }

        public Entities GetByRowId(string rowid,string PartionKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Entities>(PartionKey,rowid);
            TableResult result = table.Execute(retrieveOperation);
            Entities entity = result.Result as Entities;
            return entity;
        }


    }



    public class Entities :TableEntity
    {
        public bool Completed { get; set; }

        public string Item { get; set; }
    }
}
