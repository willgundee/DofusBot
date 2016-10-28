using System.Linq;
using System.Windows;

namespace GofusSharp
{
    public class Debug
    {
        public static void log(object Value)
        {
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Value.ToString();
        }
    }
}
