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
