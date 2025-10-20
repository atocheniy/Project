using Project.Models;

namespace Project.Helpers
{
    public class FileHelper
    {
        public static async Task<string> ReadTextAsync(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            return await reader.ReadToEndAsync();
        }

        public static string SaveToCsv(List<FrequencyResult> results)
        {
            var filename = $"result_{Guid.NewGuid()}.csv";
            var path = Path.Combine("wwwroot/results", filename);

            Directory.CreateDirectory("wwwroot/results");

            using var writer = new StreamWriter(path);
            writer.WriteLine("Word,Count");
            foreach (var item in results)
                writer.WriteLine($"{item.Word},{item.Count}");

            return filename;
        }
    }
}
