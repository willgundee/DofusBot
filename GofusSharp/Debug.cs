using System.Windows;

namespace GofusSharp
{
    public class Debug
    {
        static void log(object Value)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Combat))
                {
                    (window as Combat).tb_Log.Text += "\n" + Value.ToString();
                }
            }
        }
    }
}
