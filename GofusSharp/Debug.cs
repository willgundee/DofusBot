using System.Linq;
using System.Windows;

namespace GofusSharp
{
    public class Debug
    {
        internal static Combat FCombat;
        public Debug()
        {

        }
        public static void Log(object Value)
        {
            if (!FCombat.Generation)
                FCombat.Dispatcher.Invoke(FCombat.DelLog, new object[] { "\n" + Value.ToString() });
        }
        public static int TourCourant()
        {
            return FCombat.CombatCourant.Tour;
        }
    }
}
