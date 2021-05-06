using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using System.Collections.Generic;

namespace ListBlob
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                // set the connection string in App.config
                BlobServiceClient Client = new BlobServiceClient(System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString);
                Client.GetBlobContainers();
                // iteration listing all containers in Azure Service
                foreach (BlobContainerItem container in Client.GetBlobContainers())
                {
                    var containerName = container.Name;
                    // Print Container Name
                    Console.WriteLine("Container: " + containerName);
                    BlobContainerClient containerToAnalyze = new BlobContainerClient(System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString, containerName);
                    StorageCredentials credentials = new StorageCredentials(System.Configuration.ConfigurationManager.AppSettings[0], System.Configuration.ConfigurationManager.AppSettings[1]);
                    // Get the URI of each container
                    Uri uri = new Uri(containerToAnalyze.Uri.AbsoluteUri);
                    CloudBlobContainer blobContainer = new CloudBlobContainer(uri, credentials);
                    // Get the permission of each container
                    BlobContainerPermissions permissions = blobContainer.GetPermissions();
                    SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy();
                    //permissions.SharedAccessPolicies[permissions.SharedAccessPolicies.Keys] = policy; --> this line won't work
                    //Console.WriteLine("Perm Container: " + policy.Permissions);
                    /* TODO: Foreach  in permissions.SharedAccessPolicies[permissions.SharedAccessPolicies.Keys] to iterate all Keys and get the permissions for each key */
                    
                    // List the blobs inside each container
                    foreach (BlobItem blob in containerToAnalyze.GetBlobs())
                    {
                        Console.WriteLine("Blob: " + blob.Name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
    }
    }
}
