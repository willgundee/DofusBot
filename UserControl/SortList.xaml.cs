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
    /// Logique d'interaction pour SortList.xaml
    /// </summary>
    public partial class SortList : UserControl
    {
        public SortList(List<string> sorts)
        {
            InitializeComponent();

            int con = sorts.Count();

     
   
                BitmapImage path = new BitmapImage();
                path.BeginInit();
                path.UriSource = new Uri("../resources/Cra.png", UriKind.Relative);
                path.EndInit();
                ImgSort.Source = path;
               
                 lblNomSort.Content = sorts[3] ;              
            

        }



    }
}
