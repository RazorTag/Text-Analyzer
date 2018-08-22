using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextAnalyzer.Web.Models;
using static TextAnalyzer.UniqueWords;

namespace TextAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Text Analyzer";

            var DataModel = new TextAnalysisModel();
            DataModel.TotalWordCount = 2;
            DataModel.UniqueWordCount = 1;
            DataModel.UniqueWordCounts = new UniqueWord[] { new UniqueWord("word", 2) };
            ViewData["TotalWordCount"] = DataModel.TotalWordCount;
            ViewData["UniqueWordCount"] = DataModel.UniqueWordCount;
            ViewData["UniqueWordCounts"] = DataModel.UniqueWordCounts;
            return View(DataModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
