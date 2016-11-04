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


namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pageSort.xaml
    /// </summary>
    public partial class pageSort : UserControl
    {
        private Sort sorts;
       public BDService bd = new BDService();
       private ObservableCollection<SortList> lstSort;
        private ObservableCollection<SortDesc> lstDescription;
        public pageSort()
        {
            InitializeComponent();
            lstSort = new ObservableCollection<SortList>();
            lstDescription = new ObservableCollection<SortDesc>();
            lbxsort.ItemsSource = lstSort;          
            lbxDescript.ItemsSource = lstDescription;     
            contenuCmbType();                   
        }

        void contenulxbSort()
        {
            int con;
           List<string>[] Type;
           
            lstSort.Clear();

            if (cmbType.SelectedIndex==0)
            {
                Type = bd.selection("SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=1 OR idClasse=2 OR idClasse=3");             
                con = Type.Count();
                for (int i = 0; i < con; i++)
                {
                    SortList s = new SortList(Type[i]);
                    s.MouseDown += lbxsort_MouseDoubleClick;
                lstSort.Add(s);            
                }
            }
            else
            {
                Type = bd.selection("SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=(SELECT idClasse FROM Classes WHERE nom='" + cmbType.SelectedValue+"')");
                con = Type.Count();

                for (int i = 0; i < con; i++)
                {
                    SortList s = new SortList(Type[i]);
                    s.MouseDown += lbxsort_MouseDoubleClick;                    
                    lstSort.Add(s);       
                }
            } 
        }

        void contenuLxbDesc(string nom)
        {
            List<string>[] info;

            lstDescription.Clear();

            info = bd.selection("SELECT * FROM Sorts WHERE nom='"+nom+"'");
            Sort ds = new Sort(info[0]);
            SortDesc descS = new SortDesc(ds);

            lstDescription.Add(descS);

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
            string nom = (sender as SortList).lblNomSort.Content.ToString();

            contenuLxbDesc(nom);
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            contenulxbSort();
        }
    }
}
