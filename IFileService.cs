namespace FileUpload
{
    public interface IFileService
    {
        public Task<List<BlobDto>> ListAsync();
        public Task<BlobResponseDto> UploadAsync(IFormFile blob);
        public Task<BlobDto?> DownloadAsync(string blobFileName);
        public Task<BlobResponseDto> DeleteAsync(string blobFilename);
    }
}