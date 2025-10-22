using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Helpers;
using Project.Services;
using System.Text.Json;

namespace Project.Controllers
{
    public class FilesController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly FrequencyService _service;

        public FilesController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _service = new FrequencyService();
        }

        // GET: FilesController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResultPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var text = await FileHelper.ReadTextAsync(file);
                var result = _service.Analyze(text);
                var filename = FileHelper.SaveToCsv(result);

                TempData["UploadedFileName"] = file.FileName;
                TempData["Data"] = JsonSerializer.Serialize(result);

                return RedirectToAction(nameof(ResultPage));
            }

            ViewBag.ErrorMessage = "Файл не выбран.";
            return View("Index");
        }

        [HttpGet("download/{filename}")]
        public async Task<IActionResult> DownloadFile(string filename)
        {

            var path = Path.Combine("wwwroot/results", filename);
            if (!System.IO.File.Exists(path))
                return NotFound();


            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            return PhysicalFile(path, "application/octet-stream", filename);
        }

        // GET: FilesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
