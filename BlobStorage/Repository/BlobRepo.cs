using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorage.Repository
{
    public class BlobRepo
    {
        private BlobContainerClient containerClient;
        public string containerURL;

        public BlobRepo()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storageaccounttest11111;AccountKey=pT1DoiLnGPVhtW7jwFdX6Toz3pMQKMOugeTqMBtRMOTgLEDiyjLwXqS8ZLZl0LGYU5VF+TU3ezKB+fypbLijxw==;EndpointSuffix=core.windows.net");
            containerClient = blobServiceClient.GetBlobContainerClient("testc");
            containerURL = containerClient.Uri.ToString();

        }

        public void UploadBlob(string fileName,string path)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using FileStream uploadFileStream = File.OpenRead(path);
            blobClient.UploadAsync(uploadFileStream, true).GetAwaiter();
            uploadFileStream.Close();
        }

        public List<BlobItem> GetBlob()
        {
            List<BlobItem> items = new List<BlobItem>();
            items = containerClient.GetBlobs().ToList();
            return items;
        }

        public void deleteblob(string name)
        {
            BlobClient blobClient = containerClient.GetBlobClient(name);
            blobClient.DeleteIfExists();

        }
    }
}
