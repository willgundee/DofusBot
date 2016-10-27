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
using System.Windows.Navigation;
using System.Windows.Shapes;
using test;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pageSort.xaml
    /// </summary>
    public partial class pageSort : UserControl
    {
       public BDService bd = new BDService();
       private ObservableCollection<SortList> lstSort;
        public pageSort()
        {
            InitializeComponent();
            lstSort = new ObservableCollection<SortList>();
            contenuCmbType();
        }

        void contenuCmbType()
        {
            List<string> type = new List<string>();
            type.Add("Tous");
            type.Add("Iop");
            type.Add("Cra");
            type.Add("Ecaflip");
        }

        private void lbxsort_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
