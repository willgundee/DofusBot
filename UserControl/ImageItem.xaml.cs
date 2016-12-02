using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour ImageItem.xaml
    /// </summary>
    public partial class ImageItem : UserControl
    {
        public ImageItem(Equipement e, int quantite)
        {
            InitializeComponent();

            txtNom.Text = e.Nom;
            imgItem.Source = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + e.NoImg + ".png"));
            imgItem.Name = e.Nom.Replace(" ", "_");

            if (quantite != 0)
                txtNom.Text = quantite.ToString();
        }

    }
}
