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

namespace test
{
    /// <summary>
    /// Logique d'interaction pour ImageItem.xaml
    /// </summary>
    public partial class ImageItem : UserControl
    {
        public ImageItem(Equipement e)
        {
            InitializeComponent();
            //231
            //589
            txtNom.Text = e.Nom;
            imgItem.Source = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + e.NoImg + ".png"));
            imgItem.Name = e.Nom.Replace(" ", "_");
            
        }
    }
}
