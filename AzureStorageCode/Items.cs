using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.File;


namespace AzureBlobStorage
{
    class items: TableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Rate { get; set; }
        public bool Active { get; set; }
        public items() { }
        public items(string partition, string primarykey)
        {
            this.PartitionKey = partition;
            this.RowKey = primarykey; //Guid.NewGuid().ToString();
        }
    }
}
