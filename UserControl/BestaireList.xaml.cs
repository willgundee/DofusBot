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
        /// <summary>
        /// Constructeur de bestiaire
        /// </summary>
        /// <param name="bestiaire">liste de tous les monstres</param>
        public BestaireList(List<string> bestiaire)
        {
            InitializeComponent();
            Bestiaire = new Entite(bestiaire);
            //Image des monstres
            BitmapImage path = new BitmapImage(new Uri("../resources/" + Bestiaire.Nom + ".jpg", UriKind.Relative));
            ImgBest.Source = path;
            //nom des monstres
            lblNomBest.Content = Bestiaire.Nom;           
        }
    }
}
