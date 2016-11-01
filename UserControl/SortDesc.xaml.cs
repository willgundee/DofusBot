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
using test;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour SortDesc.xaml
    /// </summary>
    public partial class SortDesc : UserControl
    {
        public SortDesc(Sort s)
        {
            InitializeComponent();
           
            

            BitmapImage path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/spells/55/sort_" + s.NoImage + ".png"));

            Imgsort.Source = path;
            lblNom.Content = s.Nom;
            lblDescription.Content = s.Description;
        }
    }
}
