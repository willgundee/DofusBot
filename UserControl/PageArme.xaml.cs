using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour PageArme.xaml
    /// </summary>
    public partial class PageArme : UserControl
    {
        public PageArme()
        {
            InitializeComponent();
            txtZoneEEpe.Text = "Sur la case ciblée seulement";
            txtZonePEpe.Text = "Portée de 1";

            txtZonePArc.Text = "Une portée de plus d'une case variable";
            txtZoneEArc.Text = "Sur la case ciblée seulement";

            txtZoneEBaguette.Text = "Une portée de plus d'une case variable";
            txtZonePBaguette.Text = "Sur la case ciblée";

            txtZoneEBaton.Text = "Une portée sur les cases adjacentes du personnage";
            txtZonePBaton.Text = "Sur une ligne horizontale";

            txtZoneEDague.Text = "Une portée sur les cases adjacentes du personnage";
            txtZonePDague.Text = "Sur la case ciblée";

            txtZoneEFaux.Text = "Une portée sur les cases adjacentes du personnage";
            txtZonePFaux.Text = "Sur la case ciblée";

            txtZoneEHache.Text = "Une portée tous les cases adjacentes du personnage";
            txtZonePHache.Text = "Sur la case ciblée";

            txtZoneEMarteau.Text = "Une portée sur en X sur les cases adjacentes du personnage";
            txtZonePMarteau.Text = "Sur la case en T ";

            txtZoneEPelle.Text = "Une portée sur en X sur les cases adjacentes du personnage";
            txtZonePPelle.Text = "Sur les cases en ligne ";

        }
    }
}
