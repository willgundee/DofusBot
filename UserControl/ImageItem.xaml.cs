using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Logique d'interaction pour ImageItem.xaml
    /// </summary>
    public partial class ImageItem : UserControl
    {
        public ImageItem(Equipement e, bool monochrome, int quantite)
        {
            InitializeComponent();
            //231
            //589
            txtNom.Text = e.Nom;
            imgItem.Source = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + e.NoImg + ".png"));
            imgItem.Name = e.Nom.Replace(" ", "_");

            if (monochrome)
            {
                // Aider de http://www.c-sharpcorner.com/uploadfile/mahesh/grayscale-image-in-wpf/;
                // Create a BitmapImage and sets its DecodePixelWidth and DecodePixelHeight
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.UriSource = new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + e.NoImg + ".png");
                bmpImage.EndInit();

                // Create a new image using FormatConvertedBitmap and set DestinationFormat to GrayScale
                FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
                grayBitmap.BeginInit();
                grayBitmap.Source = bmpImage;
                grayBitmap.DestinationFormat = PixelFormats.Gray8;
                grayBitmap.EndInit();

                // Set Source property of Image
                imgItem.Source = grayBitmap;
            }
            if (quantite != 0)
            {
                txtNom.Text = quantite.ToString();

                /*Grid.SetRow(imgItem, 1);
                Grid.SetRowSpan(imgItem, 3);*/
            }
           /* else
            {
                Grid.SetRow(imgItem, 2);
                Grid.SetRowSpan(txtNom, 2);
            }*/
        }

    }
}
