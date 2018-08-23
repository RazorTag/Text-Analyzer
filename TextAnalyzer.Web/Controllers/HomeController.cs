using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TextAnalyzer.Web.Models;
using static TextAnalyzer.UniqueWords;

namespace TextAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var DataModel = new TextAnalysisModel();
            //DataModel.TotalWordCount = 0;
            //DataModel.UniqueWordCount = 0;
            //DataModel.UniqueWordCounts = new UniqueWord[0];

            //return View(DataModel);

            return AnalyzeText("");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Analysis a text for word occurrences.
        /// </summary>
        /// <param name="text">The text to analyze.</param>
        /// <returns>A new view to display.</returns>
        [HttpPost]
        public ActionResult AnalyzeText(string text)
        {
            var textAnalysis = new AnalyzeText(text);
            var dataModel = new TextAnalysisModel();
            dataModel.TotalWordCount = textAnalysis.TotalWordCount;
            dataModel.UniqueWordCount = textAnalysis.UniqueWordCount;
            dataModel.UniqueWordCounts = textAnalysis.UniqueWordCounts.ToArray();
            dataModel.Text = text;

            return View("Index", dataModel);
        }
    }
}
