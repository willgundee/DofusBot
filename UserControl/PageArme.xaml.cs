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
            txtZonePEpe.Text = "Portée de 1 case, seulement les case qui entoure ton personnage";
            txtZoneEEpe.Text = "Sur la case ciblée seulement";

            txtZonePArc.Text = "Portée généralement supérieur à 2 et inférieur à 10";
            txtZoneEArc.Text = "Sur la case ciblée seulement";

            txtZonePBaguette.Text = "Portée généralement supérieur à 2 et inférieur à 8";
            txtZoneEBaguette.Text = "Sur la case ciblée seulement";

            txtZonePBaton.Text = "Portée de 1 case, seulement les case qui entoure ton personnage";
            txtZoneEBaton.Text = "Une ligne horizontale de 1 portée à partir du point d'impact";

            txtZonePDague.Text = "Portée de 1 case, seulement les case qui entoure ton personnage";
            txtZoneEDague.Text = "Sur la case ciblée seulement";

            txtZonePFaux.Text = "Portée de 1 case, seulement les case qui entoure ton personnage";
            txtZoneEFaux.Text = "Sur la case ciblée seulement";

            txtZonePHache.Text = "Toutes les cases autour du personnage, même les diagonales";
            txtZoneEHache.Text = "Sur la case ciblée seulement";

            txtZonePMarteau.Text = "Portée de 1 case, seulement les case qui entoure ton personnage";
            txtZoneEMarteau.Text = "Sur une zone en T de 1 de portée à partir du point d'impact";

            txtZonePPelle.Text = "Portée de 1 case, seulement les case qui entoure ton personnage";
            txtZoneEPelle.Text = "Sur une ligne verticale de 1 de portée à partir du point d'impact";

        }
    }
}
