using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Résultat.xaml
    /// </summary>
    public partial class Resultat : Window
    {
        BDService bd = new BDService();
        private long IdPartie { get; set; }

        public Resultat(long idPartie)
        {
            InitializeComponent();
            IdPartie = idPartie;
            string selectPartie = "SELECT * FROM Parties WHERE idPartie = " + idPartie;
            List<string>[] lstPartieBd = bd.selection(selectPartie);
            if (lstPartieBd[0][0] != "rien")
            {
               double gain;
                double exp;
                foreach (List<string> p in lstPartieBd)
                {

                    List<Entite> lstAtt = new List<Entite>();
                    List<Entite> lstDef = new List<Entite>();
                    if (p[3] != "infoEntites")
                    {
                        List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(p[3]);
                        lstAtt = infoJson[0];
                        lstDef = infoJson[1];
                        if (p[4] == "True")
                        {
                            NomPersoG.Content = lstAtt[0].Nom;
                            NomPersoP.Content = lstDef[0].Nom;


                            BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstAtt[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                            imgG.Source = path;
                            path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstDef[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                            imgP.Source = path;


                            gain = lstDef[0].Niveau * 100;
                            lblKamasG.Content += " " + gain + "$";
                            lblKamasP.Content += " " + (gain / 10) + "$";
                        


                        }
                        else
                        {
                            NomPersoP.Content = lstAtt[0].Nom;
                            NomPersoG.Content = lstDef[0].Nom;

                            BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstDef[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                            imgG.Source = path;
                            path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstAtt[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                            imgP.Source = path;

                            gain = lstAtt[0].Niveau * 100;
                            lblKamasG.Content += " " + gain + "$";
                            lblKamasP.Content += " " + (gain / 10) + "$";
                        }
                        exp = lstDef[0].Niveau * lstAtt[0].Niveau * 32;
                        lblexpG.Content += " " + exp;
                        lblexpP.Content += " " + exp / 10;

                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Windows.Cast<Window>().Where(x => x is GofusSharp.Combat).FirstOrDefault(x => (x as GofusSharp.Combat).IdPartie == IdPartie) != null)
            {
                Application.Current.Windows.Cast<Window>().Where(x => x is GofusSharp.Combat).FirstOrDefault(x => (x as GofusSharp.Combat).IdPartie == IdPartie).Close();
            }
        }
    }
}
