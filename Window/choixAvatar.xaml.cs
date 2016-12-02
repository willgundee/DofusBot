using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Gofus
{
    /// <summary>
    /// Interaction logic for choixAvatar.xaml
    /// </summary>
    /// 


    public partial class choixAvatar : Window
    {
        private ObservableCollection<Image> lstImage;

        public int idAvatar { get; set; }

        public choixAvatar(List<string> lstAvatars, int Avatar)
        {
            InitializeComponent();
            lstImage = new ObservableCollection<Image>();
            Dispatcher.Invoke(new Action(() => {     listBoxAvatars.ItemsSource = lstImage;
            foreach (string img in lstAvatars)
            {
                Image imge = new Image();
                imge.Width =80;
                imge.Height = 70;
                imge.Source = new BitmapImage(new Uri(img));
                lstImage.Add(imge);

            }
            listBoxAvatars.SelectedIndex = Avatar;
            idAvatar = Avatar; }));
       
        }

        private void btnChoisir_Click(object sender, RoutedEventArgs e)
        {
           
            idAvatar = listBoxAvatars.SelectedIndex;

            this.Close();

        }
    }
}
