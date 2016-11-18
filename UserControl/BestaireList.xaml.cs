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
    /// Logique d'interaction pour BestaireList.xaml
    /// </summary>
    public partial class BestaireList : UserControl
    {
        private Entite Bestiaire; 
        public BestaireList(List<string> bestiaire)
        {
            InitializeComponent();

            Bestiaire = new Entite(bestiaire);
            int con = bestiaire.Count();

            BitmapImage path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/spells/55/sort_" + Bestiaire.Niveau + ".png"));

            ImgBest.Source = path;

            lblNomBest.Content = Bestiaire.Nom;

        }
    }
}
