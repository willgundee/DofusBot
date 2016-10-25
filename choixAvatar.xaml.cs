using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Interaction logic for choixAvatar.xaml
    /// </summary>
    /// 


    public partial class choixAvatar : Window
    {
        private ObservableCollection<BitmapImage> lstImage;

        public choixAvatar(List<string> lstAvatars, int idAvatar)
        {
            InitializeComponent();
            lstImage = new ObservableCollection<BitmapImage>();
            listBoxAvatars.ItemsSource = lstImage;
            foreach (string img in lstAvatars)
            {
                BitmapImage image = new BitmapImage(new Uri(img));

                lstImage.Add(image);

            }


        }
    }
}
