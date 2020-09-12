using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageTable.Models
{
    public class Tododo
    {
        public string Id { get; set; }

        public string Group { get; set; }

        public bool Completed { get; set; }

        public string Item { get; set; }
    }
}
