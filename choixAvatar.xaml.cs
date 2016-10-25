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
        private ObservableCollection<Image> lstImage;

        public choixAvatar(List<string> lstAvatars, int idAvatar)
        {
            InitializeComponent();
            lstImage = new ObservableCollection<Image>();
            listBoxAvatars.ItemsSource = lstImage;
            foreach (string img in lstAvatars)
            {
                Image imge = new Image();
                imge.Width =80;
                imge.Height = 70;
                imge.Source = new BitmapImage(new Uri(img));
                lstImage.Add(imge);

            }


        }
    }
}
