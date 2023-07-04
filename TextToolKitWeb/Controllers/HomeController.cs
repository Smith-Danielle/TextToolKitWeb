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

        //Module New Entry
        [HttpPost]
        public ActionResult SearchNewItem(SearchModel search)
        {
            ViewBag.UserTextEntry = search.UserTextEntry;
            return View("SearchNewItem");
        }


        //Modify

        //Module Entry
        public IActionResult ModifyEntry()
        {
            return View("ModifyEntry");
        }

        //Module Result, if entry not empty
        [HttpPost]
        public ActionResult FormModify(ModifyModel modify, string command, string modified = null)
        {
            if (modified != null)
            {
                modify.UserModEntry = modified;
            }
            if (!string.IsNullOrEmpty(modify.UserModEntry))
            {
                //Replace and Remove go to custom views first before going to result view
                ViewBag.UserModEntry = modify.UserModEntry;
                if (command.Equals("Remove Item from Text"))
                {
                    return View("ModifyRemoveEntry");
                }
                else if (command.Equals("Replace Item in Text"))
                {
                    return View("ModifyReplaceEntry");
                }
                else
                {
                    // Remove and Replace, after they have been sent to custom views to get additional items (removeitem, replacecurrentitem, replacenewitem), will now go to result view
                    if (command.Equals("Remove"))
                    {
                        if (!string.IsNullOrEmpty(modify.UserRemoveItem))
                        {
                            ViewBag.UserRemoveItem = modify.UserRemoveItem;
                            modify.RemoveItem(modify.UserModEntry, modify.UserRemoveItem);
                        }
                        else
                        {
                            return View("ModifyRemoveEntry");
                        }
                    }
                    if (command.Equals("Replace"))
                    {
                        if (!string.IsNullOrEmpty(modify.UserReplaceCurrentItem) && !string.IsNullOrEmpty(modify.UserReplaceNewItem))
                        {
                            ViewBag.UserReplaceCurrentItem = modify.UserReplaceCurrentItem;
                            ViewBag.UserReplaceNewItem = modify.UserReplaceNewItem;
                            modify.ReplaceItem(modify.UserModEntry, modify.UserReplaceCurrentItem, modify.UserReplaceNewItem);
                        }
                        else
                        {
                            return View("ModifyReplaceEntry");
                        }
                    }
                    //Rest of actions to go to result view
                    //Full Text Entry Modifications
                    if (command.Equals("Order All Characters in Text"))
                    {
                        modify.OrderAll(modify.UserModEntry);
                    }
                    if (command.Equals("Reverse Entire Text"))
                    {
                        modify.ReverseAll(modify.UserModEntry);
                    }
                    if (command.Equals("Capitalize All Letters in Text"))
                    {
                        modify.Capitalize(modify.UserModEntry);
                    }
                    if (command.Equals("Lowercase All Letters Text"))
                    {
                        modify.Lowercase(modify.UserModEntry);
                    }
                    //Word Modifications
                    if (command.Equals("Order All Words in Text"))
                    {
                        modify.OrderWords(modify.UserModEntry);
                    }
                    if (command.Equals("Reverse Each Word in Text"))
                    {
                        modify.ReverseWords(modify.UserModEntry);
                    }
                    if (command.Equals("Capitalize First Letter of Each Word in Text"))
                    {
                        modify.CapitalizeWords(modify.UserModEntry);
                    }
                    ViewBag.UserModEntry = modify.UserModEntry;
                    ViewBag.ResultMessage = modify.ResultMessage;
                    ViewBag.ResultStatus = modify.ResultStatus;
                    //ViewBag.ModifiedText = modify.ModifiedText;
                    return View("ModifyResult");
                }
            }
            return View("ModifyEntry");
        }
    }
}
