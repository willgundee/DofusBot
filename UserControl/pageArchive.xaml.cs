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

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pageArchive.xaml
    /// </summary>
    /// 
    

    public partial class pageArchive : UserControl
    {

        public BDService bd;
        public List<Partie> lstpartie;

        public int idJoueur { get; set; }
        public pageArchive(int id)
        {
            InitializeComponent();

            lstpartie = new List<Partie>();

            bd = new BDService();

            idJoueur = id;

            dgHistorique.ItemsSource = lstpartie;

            cboTypePartie.Items.Add("Mes Parties");
            cboTypePartie.Items.Add("Les partie de tout le monde");
        }



        private void loadParties(string type)
        {

            string selectid = "Select  idPartie,temps,seed From Parties LIMIT 70 ";
            List<string>[] lstPartieBd = bd.selection(selectid);



            lstpartie.Clear();

            foreach (List<string> p in lstPartieBd)
            {
                string selectPartici = "SELECT estAttaquant,idEntite FROM PartiesEntites WHERE idPartie = " + p[0];
                List<string>[] result = bd.selection(selectPartici);
                if (result[0][0] != "rien")
                {
                    int seed = Int32.Parse(p[2]);
                    string att = "";
                    string def = "";
                    int idatt = -1;
                    int iddef = -1;

                    foreach (List<string> particip in result)
                    {

                        if (particip[0] == "False")
                        {
                            string selectN = "SELECT nomUtilisateur FROM Joueurs WHERE idJoueur=" + particip[1].ToString();
                            List<string>[] selectNom = bd.selection(selectN);
                            def = selectNom[0][0];
                            iddef = Int32.Parse(particip[1]);
                        }
                        else
                        {
                            string selectN = "SELECT nomUtilisateur FROM Joueurs WHERE idJoueur=" + particip[1].ToString();
                            List<string>[] selectNom = bd.selection(selectN);
                            att = selectNom[0][0];
                            idatt = Int32.Parse(particip[1]);
                        }

                    }
                    if (type == "joueur" && idatt == idJoueur || type == "joueur" && iddef == idJoueur || type == "all")
                    {
                        lstpartie.Add(new Partie(att, def, p[1], seed));
                    }
                    else
                    {
                       
                    }
                    

                }

            }
        }


        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTypePartie.SelectedIndex == 0)
            {

                loadParties("joueur");
                dgHistorique.Items.Refresh();

            }
            else
            {
                loadParties("all");
                dgHistorique.Items.Refresh();
            }
        }


    }
}
