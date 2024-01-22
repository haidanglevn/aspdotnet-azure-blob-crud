using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace FileUpload.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllBlobs()
        {
            var result = await _fileService.ListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _fileService.UploadAsync(file);
            return Ok(result);
        }

        [HttpGet]
        [Route("filename")]
        public async Task<IActionResult> Download(string filename)
        {
            try
            {
                var result = await _fileService.DownloadAsync(filename);
                if (result is null)
                {
                    return NotFound("File not found.");
                }

                // File method sets the Content-Disposition header to "attachment" by default
                return File(result.Content, result.ContentType, result.Name);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("filename")]
        public async Task<IActionResult> Delete(string filename)
        {
            var result = await _fileService.DeleteAsync(filename);
            return Ok(result);
        }
    }
}
