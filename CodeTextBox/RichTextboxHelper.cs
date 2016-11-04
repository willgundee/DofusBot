using System.Linq;
using System.Windows.Forms;

namespace Moonlight
{
    internal class RichTextboxHelper
    {
        #region Methods
        public static string GetLastWord(RichTextBox richTextbox)
        {
            int pos = richTextbox.SelectionStart;

            while (pos > 1)
            {
                string substr = richTextbox.Text.Substring(pos - 1, 1);
                if (substr == ")")
                {
                    int nbParenthesis = 1;
                    while (nbParenthesis != 0 && pos > 1)
                    {
                        pos--;
                        substr = richTextbox.Text.Substring(pos - 1, 1);
                        if (substr == "(")
                            nbParenthesis--;
                        else if (substr == ")")
                            nbParenthesis++;
                    }
                    if (pos > 1)
                    {
                        pos--;
                        substr = richTextbox.Text.Substring(pos - 1, 1);
                    }
                }
                if (!(char.IsLetterOrDigit(substr,0) || substr == "_" || substr == "."))
                {
                    string word = richTextbox.Text.Substring(pos, richTextbox.SelectionStart - pos);
                    if (word.Contains("(") && word.Contains(")"))
                    {
                        int ouverture = word.IndexOf(word.First(x => x == '('));
                        int fermeture = word.IndexOf(word.Last(x => x == ')'));
                        word = word.Remove(ouverture + 1, fermeture - ouverture - 1);
                    }
                    return word;
                }

                pos--;
            }

            return richTextbox.Text.Substring(0, richTextbox.SelectionStart);
        }
        public static string GetLastLine(RichTextBox richTextbox)
        {
            int charIndex = richTextbox.SelectionStart;
            int currentLineNumber = richTextbox.GetLineFromCharIndex(charIndex);

            // the carriage return hasn't happened yet... 
            //      so the 'previous' line is the current one.
            string previousLineText;
            if (richTextbox.Lines.Length <= currentLineNumber)
                previousLineText = richTextbox.Lines[richTextbox.Lines.Length - 1];
            else
                previousLineText = richTextbox.Lines[currentLineNumber];

            return previousLineText;
        }       
        public static string GetCurrentLine(RichTextBox richTextbox)
        {
            int charIndex = richTextbox.SelectionStart;
            int currentLineNumber = richTextbox.GetLineFromCharIndex(charIndex);

            if (currentLineNumber < richTextbox.Lines.Length)
            {
                return richTextbox.Lines[currentLineNumber];
            }
            else
            {
                return string.Empty;
            }
        }
        public static int GetCurrentLineStartIndex(RichTextBox richTextbox)
        {
            return richTextbox.GetFirstCharIndexOfCurrentLine();
        }
        #endregion
    }
}
