using Azure;
using Azure.Storage.Files.Shares;
using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.File;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string localFilePath = @"C:\CRM Team CVs\Akram-Gameel.pdf";
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=storageaccounttest11111;AccountKey=pT1DoiLnGPVhtW7jwFdX6Toz3pMQKMOugeTqMBtRMOTgLEDiyjLwXqS8ZLZl0LGYU5VF+TU3ezKB+fypbLijxw==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstring);
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare Share = fileClient.GetShareReference("testfileshare50");
            Share.CreateIfNotExists();
            CloudFileDirectory Root= Share.GetRootDirectoryReference();
            CloudFileDirectory Dir=Root.GetDirectoryReference("Dir1");
            Dir.CreateIfNotExists();
            CloudFile file=Dir.GetFileReference("File20.pdf");
            using (FileStream stream = File.OpenRead(localFilePath))
            {
                file.Create(stream.Length);
                file.UploadFromStream(stream);
                    
            }

            Console.ReadLine();


        }
    }
}
