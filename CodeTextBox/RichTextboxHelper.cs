using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Moonlight
{
    internal class RichTextboxHelper
    {
        #region Methods
        public static string GetLastWord(RichTextBox richTextbox)
        {
            string Text = new TextRange(richTextbox.Document.ContentStart, richTextbox.Document.ContentEnd).Text;
            return Text.Split(' ').Last();
        }
        public static string GetLastLine(RichTextBox richTextbox)
        {
            string Text = new TextRange(richTextbox.Document.ContentStart, richTextbox.Document.ContentEnd).Text;
            return Text.Split('\n').Last();
        }
        public static string GetCurrentLine(RichTextBox richTextbox)
        {
            string Text = new TextRange(richTextbox.Document.ContentStart, richTextbox.CaretPosition).Text;
            int nbLigne = Text.Split('\n').Count();
            Text = new TextRange(richTextbox.Document.ContentStart, richTextbox.Document.ContentEnd).Text;
            return Text.Split('\n').ElementAt(nbLigne - 1);
        }
        public static TextPointer GetTextPointerCurrentLine(RichTextBox richTextbox)
        {
            return richTextbox.CaretPosition.GetLineStartPosition(0);
        }
        public static int GetCurrentLineStartIndex(RichTextBox richTextbox)
        {
            string Text = new TextRange(richTextbox.Document.ContentStart, richTextbox.CaretPosition).Text;
            return Text.LastIndexOf('\n');
        }
        #endregion
    }
}
