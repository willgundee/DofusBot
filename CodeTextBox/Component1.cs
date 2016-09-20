using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Moonlight
{
    public class SynchronizedScrollRichTextBox : System.Windows.Forms.RichTextBox
    {
        public event vScrollEventHandler vScroll;
        public delegate void vScrollEventHandler(System.Windows.Forms.Message message);

        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x020A;

        protected override void WndProc(ref System.Windows.Forms.Message msg)
        {
            if (msg.Msg == WM_VSCROLL || msg.Msg == WM_MOUSEWHEEL)
            {
                if (vScroll != null)
                {
                    vScroll(msg);
                }
            }
            base.WndProc(ref msg);
        }

        public void PubWndProc(ref System.Windows.Forms.Message msg)
        {
            base.WndProc(ref msg);
        }
    }
}
