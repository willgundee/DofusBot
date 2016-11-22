using System.Linq;
using System.Windows;

namespace GofusSharp
{
    public class Debug
    {
        private static Combat FCombat = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat);
        public static void Log(object Value)
        {
            if (!FCombat.Generation)
                FCombat.tb_Log.Text += "\n" + Value.ToString();
        }
        public static int TourCourant()
        {
            return (!FCombat.Generation? FCombat.CombatCourant.Tour : FCombat.CombatGeneration.Tour);
        }
    }
}
