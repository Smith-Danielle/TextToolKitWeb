using System;
using System.Linq;
using System.Collections.Generic;
namespace TextToolKitWeb.Models
{
    public class ModifyModel
    {
        public ModifyModel()
        {
        }

        //From ModifyEntry View, to HomeController, then assigned here
        //Methods Below will also override this and go to ModifyResult View
        public string UserModEntry { get; set; }

        //From ModifyRemoveEntry or ModifyReplaceEntry View, to HomeController, then assigned here
        public string UserRemoveItem{ get; set; }
        public string UserReplaceCurrentItem{ get; set; }
        public string UserReplaceNewItem { get; set; }

        //From Methods Below, lastly goes to ModifyResult View
        public string ResultStatus{ get; set; }
        public string ResultMessage { get; set; }
        public string SpecialMessage { get; set; }
        //public string ModifiedText { get; set; }

        //Called from Controller
        public void RemoveItem(string text, string item)
        {
            if (text.Contains(item))
            {
                string modifiedText = text;
                while (modifiedText.Contains(item))
                {
                    modifiedText = modifiedText.Replace(item, "");
                }
                UserModEntry = modifiedText;
                ResultStatus = "Modification Successful:";
                ResultMessage = $"{item} was removed from text entry";
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = $"Text entry does not contain {item} for removal";
            }
        }

        public void ReplaceItem(string text, string replaceCurrent, string replaceNew)
        {
            if (text.Contains(replaceCurrent))
            {
                UserModEntry = text.Replace(replaceCurrent, replaceNew);
                ResultStatus = "Modification Successful:";
                ResultMessage = $"{replaceCurrent} was replaced with {replaceNew}";
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = $"Text entry does not contain {replaceCurrent} in order to be replaced by {replaceNew}";
            }
        }

        public void OrderAll(string text)
        {
            if (text.Length > 1)
            {
                string modifiedText = string.Join("", text.OrderBy(x => x));
                if (modifiedText != UserModEntry)
                {
                    UserModEntry = modifiedText;
                    ResultStatus = "Modification Successful:";
                    ResultMessage = "All characters in text entry have been ordered.";
                }
                else
                {
                    ResultStatus = "Modification Unsuccessful:";
                    ResultMessage = "Text entry is already ordered.";
                }
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Insufficient character amount for ordering. Text entry only contains one character.";
            }
        }
        
        public void ReverseAll(string text)
        {
            if (text.Length > 1)
            {
                UserModEntry = string.Join("", text.ToArray().Reverse());
                ResultStatus = "Modification Successful:";
                ResultMessage = "All characters in text entry have been reversed.";
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Insufficient character amount for reversal. Text entry only contains one character.";
            }
        }

        public void Capitalize(string text)
        {
            if (text.Where(x => char.IsLetter(x)).Count() > 0)
            {
                string modifiedText = string.Join("", text.Select(x => char.IsLetter(x) ? char.ToUpper(x) : x));
                if (modifiedText != UserModEntry)
                {
                    UserModEntry = modifiedText;
                    ResultStatus = "Modification Successful:";
                    ResultMessage = "All letters in text entry have been capitalized.";
                }
                else
                {
                    ResultStatus = "Modification Unsuccessful:";
                    ResultMessage = "All letters in text entry are already capitalized.";
                }
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Text entry does not contain letters for capitalization.";
            }
        }

        public void Lowercase(string text)
        {
            if (text.Where(x => char.IsLetter(x)).Count() > 0)
            {
                string modifiedText = string.Join("", text.Select(x => char.IsLetter(x) ? char.ToLower(x) : x));
                if (modifiedText != UserModEntry)
                {
                    UserModEntry = modifiedText;
                    ResultStatus = "Modification Successful:";
                    ResultMessage = "All letters in text entry have been lowercased.";
                }
                else
                {
                    ResultStatus = "Modification Unsuccessful:";
                    ResultMessage = "All letters in text entry are already lowercased.";
                }
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Text entry does not contain letters for lowercase action.";
            }
        }

        //Check if entry has valid words
        public List<string> WordPresence(string wordCheck)
        {
            List<string> splitWords = new List<string>();
            if (wordCheck.Contains(' ') || wordCheck.Contains('.') || wordCheck.Contains('!') || wordCheck.Contains('?') || wordCheck.Contains(',') || wordCheck.Contains(';') || wordCheck.Contains(':'))
            {
                string replace = wordCheck.Replace('.', ' ').Replace('!', ' ').Replace('?', ' ').Replace(',', ' ').Replace(';', ' ').Replace(':', ' ');
                if (replace.Any(x => x != ' '))
                {
                    splitWords = replace.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
                }
            }
            else
            {
                splitWords.Add(wordCheck);
            }
            return splitWords;
        }

        public void OrderWords(string text)
        {
            var wordList = WordPresence(text);
            if (wordList.Count > 0)
            {
                if (wordList.Count > 1)
                {
                    string modifiedText = string.Join(" ", wordList.OrderBy(x => x));
                    if (modifiedText != UserModEntry)
                    {
                        UserModEntry = modifiedText;
                        ResultStatus = "Modification Successful:";
                        ResultMessage = "All words in text entry have been ordered.";
                    }
                    else
                    {
                        ResultStatus = "Modification Unsuccessful:";
                        ResultMessage = "All words in text entry are already ordered.";
                    }
                }
                else
                {
                    ResultStatus = "Modification Unsuccessful:";
                    ResultMessage = $"Insufficient amount of words for ordering. Text entry only contains one qualified word.";
                }
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Text entry does not contain any qualified words for ordering.";
            }
        }

        public void ReverseWords(string text)
        {
            var wordList = WordPresence(text);
            if (wordList.Count > 0)
            {
                if (wordList.Where(x => x.Length > 1).Count() > 0) 
                {
                    string modifiedText = string.Join(" ", wordList.Select(x => string.Join("", x.ToArray().Reverse())));
                    UserModEntry = modifiedText;
                    ResultStatus = "Modification Successful:";
                    ResultMessage = "Each word in text entry has been reversed.";
                }
                else
                {
                    ResultStatus = "Modification Unsuccessful:";
                    ResultMessage = "Insufficient amount of characters per word for reversal. Character count for each qualified word is only one.";
                }
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Text entry does not contain any qualified words for reversal.";
            }
        }
        public void InitialCaseWords(string text)
        {
            var wordList = WordPresence(text);
            if (wordList.Count > 0)
            {
                if (wordList.Select(x => x.Where(y => char.IsLetter(y)).Count()).Where(x => x != 0).Count() > 0)
                {
                    string modifiedText = string.Join(" ", wordList.Select(x => x.Length > 1 ? $"{char.ToUpper(x[0])}{x.Substring(1).ToLower()}" : x.ToUpper()));
                    if (modifiedText != UserModEntry)
                    {
                        UserModEntry = modifiedText;
                        ResultStatus = "Modification Successful:";
                        ResultMessage = "Each word in text entry has been initial cased.";
                    }
                    else
                    {
                        ResultStatus = "Modification Unsuccessful:";
                        ResultMessage = "Each word in text entry has already been initial cased.";
                    }
                }
                else
                {
                    ResultStatus = "Modification Unsuccessful:";
                    ResultMessage = "Text entry does not contain any qualified words with letters for inital casing.";
                }
            }
            else
            {
                ResultStatus = "Modification Unsuccessful:";
                ResultMessage = "Text entry does not contain any qualified words for inital casing.";
            }
        }
    }
}
