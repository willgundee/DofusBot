using System.Drawing;
using System.Windows.Forms;

namespace Moonlight
{
    public class SSRichTextBox : RichTextBox
    {
        public SSRichTextBox()
        {
            Font = new Font(FontFamily.GenericMonospace, 10);
            EnableAutoDragDrop = false;
            DetectUrls = false;
            WordWrap = false;
            AutoWordSelection = true;
            ReadOnly = true;
            RightToLeft = RightToLeft.Yes;
        }
    }
}
