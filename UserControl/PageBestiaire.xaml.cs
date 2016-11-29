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
    /// Logique d'interaction pour PageBestiaire.xaml
    /// </summary>
    public partial class PageBestiaire : UserControl
    {
        public BDService bd = new BDService();
        private ObservableCollection<BestaireList> lstBEAST;
        private ObservableCollection<BestaireDesc> lstDescription;
        public PageBestiaire()
        {
            InitializeComponent();
            lstBEAST = new ObservableCollection<BestaireList>();
            lstDescription = new ObservableCollection<BestaireDesc>();
            lbxBestiaire.ItemsSource = lstBEAST;
            lbxDescBestiaire.ItemsSource = lstDescription;         
            contenulxbBestiaire();
        }

        void contenulxbBestiaire()
        {
            int con;
            List<string>[] Type;

            lstBEAST.Clear();
                Type = bd.selection("SELECT * FROM Entites e  WHERE idJoueur = 103");
                con = Type.Count();
                for (int i = 0; i < con; i++)
                {
                    BestaireList s = new BestaireList(Type[i]);
                    s.MouseDown += lbxBestiaire_MouseDoubleClick;
                lstBEAST.Add(s);
                }
        }

        private void lbxBestiaire_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string nom = (sender as BestaireList).lblNomBest.Content.ToString();

            contenuLxbDesc(nom);
        }
        void contenuLxbDesc(string nom)
        {
            List<string>[] info;

            lstDescription.Clear();

            info = bd.selection("SELECT * FROM Entites WHERE nom='" + nom + "'");
            Entite ds = new Entite(info[0]);
            BestaireDesc descS = new BestaireDesc(ds);

            lstDescription.Add(descS);

        }
    }
}
