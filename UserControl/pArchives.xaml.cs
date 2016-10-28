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
    /// Logique d'interaction pour pArchives.xaml
    /// </summary>
    public partial class pArchives : UserControl
    {

        BDService BD;

        public ObservableCollection<Partie> lstPartiePerso;
        public ObservableCollection<Partie> lstPartieAll;
        public pArchives()
        {
            InitializeComponent();

            lstPartieAll = new ObservableCollection<Partie>();
            lstPartiePerso = new ObservableCollection<Partie>();

            BD = new BDService();

            cboTypePartie.Items.Add("Mes Parties");
            cboTypePartie.Items.Add("Les partie de tout le monde");

        }



        private void loadParties()
        {
            string selectid = "Select  idPartie From Parties LIMIT 70 ";
            List<string>[] lstPartie = BD.selection(selectid);

            foreach (List<string>  p in lstPartie)
            {
                string selectPartici = "SELECT estAttaquant,idEntite FROM PartiesEntites WHERE idPartie = " + p[0];
                List<string>[] result = BD.selection(selectPartici);

                string att = "";
                string def = "";

                foreach (List<string> particip in result)
                {
                    if (particip[0] == "false")
                    {
                        def = particip[1];
                    }
                    else if (particip[0] == "True") 
                    {
                        att = particip[1];
                    }
                    
                }
                lstPartieAll.Add(new Partie(att, def));
               
            }


        }
   

        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTypePartie.SelectedIndex == 0)
            {
                loadParties();
                dgHistorique.ItemsSource = lstPartiePerso;
            }
            else
            {
                loadParties();
                dgHistorique.ItemsSource = lstPartieAll;
            }
        }

        private void btnVisionner_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
