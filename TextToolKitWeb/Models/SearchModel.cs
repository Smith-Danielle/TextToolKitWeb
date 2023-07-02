using System;
using System.Linq;
using System.Collections.Generic;
namespace TextToolKitWeb.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
        }

        //From SearchEntry View, to HomeController, then assigned here
        public string UserTextEntry { get; set; }
        public string UserSearchItem { get; set; }

        //From HomeController, then assigned here, lastly goes to AnalyzeResult View
        public List<int> SearchIndices = new List<int>();

        public void CreateSearch(string text, string item)
        {
            if (text.ToLower().Contains(item.ToLower()))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (i + item.Length <= text.Length)
                    {
                        string checkSubstring = text.Substring(i, item.Length);
                        if (checkSubstring.ToLower() == item.ToLower())
                        {
                            SearchIndices.Add(i);
                        }
                    }
                }
            }
        }
    }
}
