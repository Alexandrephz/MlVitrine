using Azure.Storage.Blobs;

namespace MlVitrine.Services
{
    public class ImageUpload
    {
        public async static Task<string> UploadToAzure(IFormFile fileToPersist, string saveAsFileName)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=mlvitrine;AccountKey=h/luzCjFB3QGVCxLTmfdpv5tv4I8NjwC02nzoyB7XN95b4kg/JhskeICA5aYfanX9U1sPLi2E12i+AStC6xVIg==;EndpointSuffix=core.windows.net";
            string containerName = "mlvitrine123131azure";
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            try
            {
                // Get a reference to a blob
                BlobClient blob = container.GetBlobClient(saveAsFileName);

                //check if image file already exists on container
                if (await blob.ExistsAsync()){
                   await blob.DeleteIfExistsAsync();
                }
                // Open the file and upload its data

                using (Stream file = fileToPersist.OpenReadStream())
                {
                    blob.Upload(file);
                }

                return blob.Uri.AbsoluteUri;
            }
            catch
            {
                // log error
                return "error";
            }
        }

    }
}
