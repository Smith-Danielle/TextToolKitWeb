using System;
using System.Linq;
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

        //Maybe create a method that checks if the text contains words, (or creates a list for words) to use within methods that manipulate words, use same word logic as analysis
        public void OrderWords(string text)
        {

        }

        public void ReverseWords(string text)
        {
            //do i want to do this one?
            //think about punctuation and how that would work
        }

        public void CapitalizeWords(string text)
        {
            //if method for words checks out
            //then check if words contains letters, if so perform action
        }
    }
}
