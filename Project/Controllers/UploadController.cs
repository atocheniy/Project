using Project.Helpers;
using Project.Services;
using Microsoft.AspNetCore.Mvc;
namespace Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly FrequencyService _service;

        public UploadController()
        {
            _service = new FrequencyService();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            var text = await FileHelper.ReadTextAsync(file);
            var result = _service.Analyze(text);
            var filename = FileHelper.SaveToCsv(result);

            return Ok(new { filename });
        }

        [HttpGet("download/{filename}")]
        public IActionResult Download(string filename)
        {
            var path = Path.Combine("wwwroot/results", filename);
            if (!System.IO.File.Exists(path))
                return NotFound();

            return PhysicalFile(path, "application/octet-stream", filename);
        }
    }
}
