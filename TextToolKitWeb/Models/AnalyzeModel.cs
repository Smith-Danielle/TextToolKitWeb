using System;
using System.Linq;
using System.Collections.Generic;
namespace TextToolKitWeb.Models
{
    public class AnalyzeModel
    {
        public AnalyzeModel()
        {
        }

        //From AnalyzeEntry View, to HomeController, then assigned here
        public string UserEntry { get; set; }

        //From HomeController, then assigned here, lastly goes to AnalyzeResult View
        public int LetterCount { get; set; }
        public int NumberCount { get; set; }
        public int SpaceCount { get; set; }
        public int OtherCount { get; set; }
        public int TotalCount { get; set; }
        public string MostChar { get; set; }
        public string LeastChar { get; set; }

        public int WordCount { get; set; }
        public string MostWord { get; set; }
        public string LeastWord { get; set; }

        public List<string> CharList { get; set; }
        public List<int> CharListCount { get; set; }

        public List<string> WordList { get; set; }
        public List<int> WordListCount { get; set; }

        public void CreateAnalysis(string text)
        {
            //Character Data

            //Char Counts
            LetterCount = text.Where(x => char.IsLetter(x)).Count();
            NumberCount = text.Where(x => char.IsNumber(x)).Count();
            SpaceCount = text.Where(x => x == ' ').Count();
            OtherCount = text.Where(x => !char.IsLetter(x) && !char.IsNumber(x) && x != ' ').Count();
            TotalCount = text.Length;

            //Char split, group, count
            var splitChar = text.ToLower().Select(x => x.ToString());
            var statsChar = splitChar.GroupBy(x => x).Select(x => x.Key).Select(x => new { Character = x, CharCount = splitChar.Where(y => y == x).Count() });

            //Get the Most Used Character
            int maxCount = statsChar.Max(x => x.CharCount);
            MostChar = "";
            if (statsChar.All(x => x.CharCount == maxCount))
            {
                MostChar = "All characters occur the same amount of times.";
            }
            else
            {
                var maxCharGroup = statsChar.Where(x => x.CharCount == maxCount).Select(x => x.Character).OrderBy(x => x);
                MostChar = maxCharGroup.Count() > 1 ? string.Join(" ", maxCharGroup).Replace("  ", "Space ") : maxCharGroup.First().Replace(" ", "Space");
                if (MostChar.Contains("Space"))
                {
                    if (MostChar == "Space")
                    {
                        int nextMaxCount = statsChar.Where(x => x.Character != " ").Max(x => x.CharCount);
                        var nextMaxCharGroup = statsChar.Where(x => x.CharCount == nextMaxCount).Select(x => x.Character).OrderBy(x => x);
                        MostChar += " followed by ";
                        MostChar += nextMaxCharGroup.Count() > 1 ? string.Join(" ", nextMaxCharGroup) : nextMaxCharGroup.First();
                    }
                    else
                    {
                        MostChar = MostChar.Replace("Space", "").Trim();
                        MostChar += " Space";
                    }
                }
            }

            //Get the Least Used Character
            LeastChar = "";
            if (MostChar == "All characters occur the same amount of times.")
            {
                LeastChar = "All characters occur the same amount of times.";
            }
            else
            {
                int minCount = statsChar.Min(x => x.CharCount);
                var minCharGroup = statsChar.Where(x => x.CharCount == minCount).Select(x => x.Character).OrderBy(x => x);
                LeastChar = minCharGroup.Count() > 1 ? string.Join(" ", minCharGroup).Replace("  ", "Space ") : minCharGroup.First().Replace(" ", "Space");
                if (LeastChar.Contains("Space"))
                {
                    if (LeastChar == "Space")
                    {
                        int nextMinCount = statsChar.Where(x => x.Character != " ").Min(x => x.CharCount);
                        var nextMinCharGroup = statsChar.Where(x => x.CharCount == nextMinCount).Select(x => x.Character).OrderBy(x => x);
                        LeastChar += " followed by ";
                        LeastChar += nextMinCharGroup.Count() > 1 ? string.Join(" ", nextMinCharGroup) : nextMinCharGroup.First();
                    }
                    else
                    {
                        LeastChar = MostChar.Replace("Space", "").Trim();
                        LeastChar += " Space";
                    }
                }
            }

            //For Viewing Detailed Word and Word Count
            CharList = statsChar.OrderByDescending(x => x.CharCount).ThenBy(x => x.Character).Select(x => x.Character).Select(x => x == " " ? "Space" : x).ToList();
            CharListCount = statsChar.OrderByDescending(x => x.CharCount).ThenBy(x => x.Character).Select(x => x.CharCount).ToList();


            //Word Data

            List<string> splitWords = new List<string>();
            if (text.Contains(' ') || text.Contains('.') || text.Contains('!') || text.Contains('?') || text.Contains(',') || text.Contains(';') || text.Contains(':'))
            {
                string replace = text.Replace('.', ' ').Replace('!', ' ').Replace('?', ' ').Replace(',', ' ').Replace(';', ' ').Replace(':', ' ');
                if (replace.Any(x => x != ' '))
                {
                    splitWords = replace.ToLower().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
                }
            }
            else
            {
                splitWords.Add(text);
            }

            WordCount = splitWords.Count;

            var statsWord = WordCount > 0 ? splitWords.GroupBy(x => x).Select(x => x.Key).Select(x => new { Word = x, WordCount = splitWords.Where(y => y == x).Count() }) : null;

            MostWord = "";
            LeastWord = "";

            if (WordCount == 0)
            {
                MostWord = "Entry contains no words.";
                LeastWord = "Entry contains no words.";
            }
            else
            {
                int maxWordCount = statsWord.Max(x => x.WordCount);
                if (statsWord.All(x => x.WordCount == maxWordCount))
                {
                    MostWord = "All words occur the same amount of times.";
                }
                else
                {
                    var maxWordGroup = statsWord.Where(x => x.WordCount == maxWordCount).Select(x => x.Word).OrderBy(x => x);
                    MostWord = maxWordGroup.Count() > 1 ? string.Join(", ", maxWordGroup) : maxWordGroup.First();
                }

                if (MostWord == "All words occur the same amount of times.")
                {
                    LeastWord = "All words occur the same amount of times.";
                }
                else
                {
                    int minWordCount = statsWord.Min(x => x.WordCount);
                    var minWordGroup = statsWord.Where(x => x.WordCount == minWordCount).Select(x => x.Word).OrderBy(x => x);
                    LeastWord = minWordGroup.Count() > 1 ? string.Join(", ", minWordGroup) : minWordGroup.First();
                }

                //For Viewing Detailed Word and Word Count
                WordList = statsWord.OrderByDescending(x => x.WordCount).ThenBy(x => x.Word).Select(x => x.Word).ToList();
                WordListCount = statsWord.OrderByDescending(x => x.WordCount).ThenBy(x => x.Word).Select(x => x.WordCount).ToList();
            }
        }
    }
}
