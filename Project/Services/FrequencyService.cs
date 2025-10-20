using Project.Models;
using System.Text.RegularExpressions;

namespace Project.Services
{
    public class FrequencyService
    {
        public List<FrequencyResult> Analyze(string text)
        {
            var words = Regex.Matches(text.ToLower(), @"\b\w+\b")
                             .Cast<Match>()
                             .Select(m => m.Value);

            var frequency = words.GroupBy(w => w)
                                 .Select(g => new FrequencyResult { Word = g.Key, Count = g.Count() })
                                 .OrderByDescending(f => f.Count)
                                 .ToList();

            return frequency;
        }
    }
}
