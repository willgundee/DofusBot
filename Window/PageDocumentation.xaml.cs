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

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour PageDocumentation.xaml
    /// </summary>
    public partial class PageDocumentation : Window
    {
        ObservableCollection<PageDoc> lstDoc= new ObservableCollection<PageDoc>();
        public PageDocumentation()
        {
            InitializeComponent();
            pgDocu.Children.Add(new PageDoc());
            //tabDoc.Content = new PageDoc(); 
        }
    }
}
