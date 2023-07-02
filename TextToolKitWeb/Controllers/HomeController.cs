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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //Analyze

        //Module Entry
        public IActionResult AnalyzeEntry()
        {
            return View("AnalyzeEntry");
        }

        //Module Result, if entry not empty
        [HttpPost]
        public ActionResult FormAnalyze(AnalyzeModel analyze)
        {
            if (!string.IsNullOrEmpty(analyze.UserEntry))
            {
                ViewBag.UserEntry = analyze.UserEntry;
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
                ViewBag.CharList = analyze.CharList;
                ViewBag.CharListCount = analyze.CharListCount;
                return View("AnalyzeResult");
            }
            return View("AnalyzeEntry");
        }


        //Search

        //Module Entry
        public IActionResult SearchEntry()
        {
            return View("SearchEntry");
        }

        //Module Result
        [HttpPost]
        public ActionResult FormSearch(SearchModel search)
        {
            if (!string.IsNullOrEmpty(search.UserTextEntry) && !string.IsNullOrEmpty(search.UserSearchItem))
            {
                ViewBag.UserTextEntry = search.UserTextEntry;
                ViewBag.UserSearchItem = search.UserSearchItem;
                search.CreateSearch(search.UserTextEntry, search.UserSearchItem);
                ViewBag.SearchIndices = search.SearchIndices;
                return View("SearchResult");
            }
            return View("SearchEntry");
        }
    }
}
