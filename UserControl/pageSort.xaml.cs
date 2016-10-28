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

        void contenulxbSort()
        {
            int con;
           List<string>[] Type;
                  
            /*
             -0=tous
             -1=Iop
             -2=Cra        
             -3=Ecaflip
             */
            if (cmbType.SelectedIndex==0)
            {
                Type = bd.selection("SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=1 OR idClasse=2 OR idClasse=3");

                con = Type.Count();
                for (int i = 0; i < con; i++)
                {

                lstSort.Add(new SortList(Type[i]));
                lbxsort.ItemsSource = lstSort;            
                }
            }
            else
            {
            //    string i = "SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=(SELECT idClasse FROM Classes WHERE nom='" + cmbType.SelectedValue + "')";
                Type = bd.selection("SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=(SELECT idClasse FROM Classes WHERE nom='" + cmbType.SelectedValue+"')");
                con = Type.Count();
                for (int i = 0; i < con; i++)
                {
                    lstSort.Add(new SortList(Type[i]));
                    lbxsort.ItemsSource = lstSort;
                }
            } 
        }


            void contenuCmbType()
        {
            List<string> type = new List<string>();
            type.Add("Tous");
            type.Add("Iop");
            type.Add("Cra");
            type.Add("Ecaflip");
            cmbType.ItemsSource = type;
        }

        private void lbxsort_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            contenulxbSort();
        }
    }
}
