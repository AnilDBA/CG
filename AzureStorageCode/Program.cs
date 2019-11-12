using System;
using System.IO;
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
    class Program
    {
        static void Main(string[] args)
        {
            //BlobTest();
            //TableTest();
            //QueueTest();    
            FileTest();
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }//main
        static CloudStorageAccount ValidateConnection()
        {
            try
            {
                string con = System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"];
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(con);
                Console.WriteLine("Connection success");
                return storageAccount;
            }
            catch (StorageException ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return null;
            }
        }
        static void BlobTest()
        {
            CloudStorageAccount storageAccount = ValidateConnection();
            if (storageAccount == null)
            {
                return;
            }
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("myfiles");
            container.CreateIfNotExists();
            Console.WriteLine("Container Created");
            //here we are uploading y2.xml but we are uploading in blob container with filename as myxml.xml
            CloudBlockBlob blob = container.GetBlockBlobReference("myxml.xml");
            //upload file
            blob.UploadFromFile(@"d:\y2.xml");
            Console.WriteLine("File Uploaded");
            //--download file myxml.xml and save it as d:\t2.xml
            blob = container.GetBlockBlobReference("myxml.xml");
            MemoryStream ms = new MemoryStream();
            blob.DownloadToStream(ms);
            FileStream fls = new FileStream(@"d:\t2.xml", FileMode.Append);
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            fls.Write(bytes, 0, bytes.Length);
            ms.Close();
        }
        static void TableTest()
        {
            CloudStorageAccount storageAccount = ValidateConnection();
            if (storageAccount == null)
            {
                return;
            }
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable tbl= tableClient.GetTableReference("items");
            tbl.CreateIfNotExists();
            items itm = new items("P1","item4");
            itm.Name = "iPhone";
            itm.Type = "Mobile";
            itm.Rate = 50000;
            itm.Active = true;
            TableOperation opr = TableOperation.Insert(itm);
            tbl.Execute(opr);//insert
            Console.WriteLine("Item added");
            itm.Rate = 95000;
            opr = TableOperation.Replace(itm);
            tbl.Execute(opr);//update
            Console.WriteLine("Item updated");
            opr = TableOperation.Retrieve<items>("P1","item4",null);//retrive single row
            TableResult tblresult = tbl.Execute(opr);
            if(tblresult.Result != null)
            {
                var i = tblresult.Result as items;
                Console.WriteLine("Item:{0}, Type:{1}, Rate:{2}, Active:{3}", i.Name, i.Type,Convert.ToString(i.Rate),Convert.ToString(i.Active));
            }            
            opr = TableOperation.Delete(itm);//delete
            tbl.Execute(opr);
            var rows = tbl.ExecuteQuery(new TableQuery<items>()).ToList();
            foreach (var item in rows)
            {
                Console.WriteLine("Item:{0}, Type:{1}, Rate:{2}, Active:{3}", item.Name, item.Type, item.Rate.ToString(),item.Active.ToString());
            }
        }
        static void QueueTest()
        {
            CloudStorageAccount storageAccount = ValidateConnection();
            if (storageAccount == null)
            {
                return;
            }
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("testqueue");
            queue.CreateIfNotExists();
            Console.WriteLine("Queue created");
            CloudQueueMessage msg = new CloudQueueMessage("Hello, World");
            queue.AddMessage(msg);
            CloudQueueMessage peekedMessage = queue.PeekMessage();
            Console.WriteLine(peekedMessage.AsString);
            CloudQueueMessage message = queue.GetMessage();
            message.SetMessageContent("Updated contents.");
            // Make it invisible for another 60 seconds.
            queue.UpdateMessage(message, TimeSpan.FromSeconds(20.0),  MessageUpdateFields.Content | MessageUpdateFields.Visibility);
            msg = new CloudQueueMessage("test1212");
            queue.AddMessage(msg);
            CloudQueueMessage retrievedMessage = queue.GetMessage();
            if (retrievedMessage != null)
            {
                queue.DeleteMessage(retrievedMessage);
            }
            foreach (CloudQueueMessage msg1 in queue.GetMessages(20, TimeSpan.FromMinutes(5)))
            {
                // Process all messages in less than 5 minutes, deleting each message after processing.                
                queue.DeleteMessage(msg1);
                Console.WriteLine("Deleted:" + msg1.AsString);
            }
        }
        static void FileTest()
        {
            CloudStorageAccount storageAccount = ValidateConnection();
            if (storageAccount == null)
            {
                return;
            }
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("myfiles");
            share.CreateIfNotExists();
            Console.WriteLine("share Created");
            CloudFileDirectory root = share.GetRootDirectoryReference();
            CloudFileDirectory dir = root.GetDirectoryReference("mydirectory");//create directory
            dir.CreateIfNotExists();
            string filepath = @"d:\test.txt";
            //upload file
            CloudFile file = dir.GetFileReference("t1.txt");
            file.UploadFromFile(filepath);
            Console.WriteLine("File Uploaded");
            //download file
            file = dir.GetFileReference("t1.txt");
            file.DownloadToFile("d:\\t3.txt",FileMode.Append);
            Console.WriteLine("File downloaded");
        }
    }
}
