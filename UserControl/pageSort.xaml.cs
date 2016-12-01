using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;


namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pageSort.xaml
    /// </summary>
    public partial class pageSort : UserControl
    {
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

            if (cmbType.SelectedIndex == 0)
            {
                Type = bd.selection("SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=1 OR idClasse=2 OR idClasse=3");
                con = Type.Count();
                Thread createSorts = new Thread(() =>
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        for (int i = 0; i < con; i++)
                        {
                            SortList s = new SortList(Type[i]);
                            s.MouseDown += lbxsort_MouseDoubleClick;
                            lstSort.Add(s);
                        }
                    }));
                });
                createSorts.Start();
                Thread.Yield();
            }
            else
            {
                Type = bd.selection("SELECT * FROM Sorts s INNER JOIN ClassesSorts cs ON s.idSort =cs.idSort WHERE idClasse=(SELECT idClasse FROM Classes WHERE nom='" + cmbType.SelectedValue + "')");
                con = Type.Count();
                Thread createSorts = new Thread(() =>
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        for (int i = 0; i < con; i++)
                        {
                            SortList s = new SortList(Type[i]);
                            s.MouseDown += lbxsort_MouseDoubleClick;
                            lstSort.Add(s);
                        }
                    }));
                });
                createSorts.Start();
                Thread.Yield();
            }
        }

        void contenuLxbDesc(string nom)
        {
            List<string>[] info;
            Dispatcher.Invoke(new Action(() =>
            {
                lstDescription.Clear();
            }));
            info = bd.selection("SELECT * FROM Sorts WHERE nom='" + nom + "'");
            Thread createSorts = new Thread(() =>
            {
                Sort ds = new Sort(info[0]);

                Dispatcher.Invoke(new Action(() =>
                {
                    SortDesc descS = new SortDesc(ds);
                    lstDescription.Add(descS);
                }));
            });

            createSorts.Start();
            Thread.Yield();



        }


        void contenuCmbType()
        {
            List<string> type = new List<string>();
            Dispatcher.Invoke(new Action(() =>
            {

                type.Add("Tous");
                type.Add("Iop");
                type.Add("Cra");
                type.Add("Ecaflip");
                cmbType.ItemsSource = type;
            }));

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
