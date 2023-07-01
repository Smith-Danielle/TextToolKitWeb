using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TextToolKitWeb.Models;

namespace TextToolKitWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Analyze
        //Module Entry
        public IActionResult AnalyzeEntry()
        {
            return View("AnalyzeEntry");
        }
        //Module Result
        [HttpPost]
        public ActionResult FormAnalyze(AnalyzeModel analyze)
        {
            ViewBag.UserEntry = analyze.UserEntry;
            if (!string.IsNullOrEmpty(analyze.UserEntry))
            {
                analyze.CreateAnalysis(analyze.UserEntry);
                ViewBag.LetterCount = analyze.LetterCount;
                ViewBag.NumberCount = analyze.NumberCount;
                ViewBag.OtherCount = analyze.OtherCount;
                ViewBag.SpaceCount = analyze.SpaceCount;
                ViewBag.TotalCount = analyze.TotalCount;
                ViewBag.MostChar = analyze.MostChar;
                ViewBag.LeastChar = analyze.LeastChar;
                ViewBag.WordCount = analyze.WordCount;
                ViewBag.MostWord = analyze.MostWord;
                ViewBag.LeastWord = analyze.LeastWord;
                ViewBag.WordList = analyze.WordList;
                ViewBag.WordListCount = analyze.WordListCount;
                ViewBag.AllCharList = analyze.AllCharList;
                ViewBag.CharListCount = analyze.CharListCount;
            }

            return View("AnalyzeResult");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
