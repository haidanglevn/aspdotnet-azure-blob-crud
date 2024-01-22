using Azure.Storage;
using Azure.Storage.Blobs;

namespace FileUpload
{
    public class FileService : IFileService
    {
        private readonly string _storageAccount;
        private readonly string _key;
        private readonly BlobContainerClient _filesContainer;

        public FileService(IConfiguration configuration)
        {
            _storageAccount = configuration["StorageAccount"];
            _key = configuration["StorageKey"];
            var credential = new StorageSharedKeyCredential(_storageAccount, _key);
            var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
            _filesContainer = blobServiceClient.GetBlobContainerClient("files");
        }

        public async Task<List<BlobDto>> ListAsync()
        {
            List<BlobDto> files = [];

            await foreach (var file in _filesContainer.GetBlobsAsync())
            {
                string uri = _filesContainer.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;
        }

        public async Task<BlobResponseDto> UploadAsync(IFormFile blob)
        {
            BlobResponseDto response = new();
            BlobClient client = _filesContainer.GetBlobClient(blob.FileName);

            await using (Stream? data = blob.OpenReadStream())
            {
                await client.UploadAsync(data, true);
            }

            response.Status = $"File {blob.FileName} Uploaded Successfully.";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;

            return response;
        }

        public async Task<BlobDto?> DownloadAsync(string blobFileName)
        {
            BlobClient file = _filesContainer.GetBlobClient(blobFileName);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFileName;
                string contentType = content.Value.Details.ContentType;

                return new BlobDto
                {
                    Content = blobContent,
                    Name = name,
                    ContentType = contentType
                };
            }

            return null;
        }

        public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            BlobClient file = _filesContainer.GetBlobClient(blobFilename);
            await file.DeleteAsync();

            return new BlobResponseDto
            {
                Error = false,
                Status = $"File {blobFilename} Deleted Successfully."
            };
        }
    }
}