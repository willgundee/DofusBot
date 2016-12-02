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
    /// Logique d'interaction pour BestaireDesc.xaml
    /// </summary>
    public partial class BestaireDesc : UserControl
    {
        public BDService bd = new BDService();
        List<Statistique> st = new List<Statistique>();

        /// <summary>
        /// Constructeur de la description de bestiaire
        /// </summary>
        /// <param name="e"> l'entité choisie</param>
        public BestaireDesc(Entite e)
        {
            string vieMin = "0";
            string vieMax = "0";
            string PA = "0";
            string PM = "0";

            //on vas chercher tous ses statistiques
            foreach (Statistique item in e.LstStats)
            {
                //on sélectionne les statistiques principales vie, PM(Point de mouvement) et PA(Point d'action)
                if (item.Nom == Statistique.element.vie)
                {
                    vieMin = item.ValeurMin.ToString();
                    vieMax = item.ValeurMax.ToString();
                }
                if (item.Nom == Statistique.element.PM)
                    PM = item.ValeurMin.ToString();
                if (item.Nom == Statistique.element.PA)
                    PA = item.ValeurMin.ToString();
            }
            InitializeComponent();
            //on s'assure qu'un entité a été sélectionné
            if (e.Nom != "")
            {//nom du monstre
                lblNomBes.Text = e.Nom.ToString();
                //image du monstre
                BitmapImage path = new BitmapImage(new Uri("../resources/" + e.Nom + ".jpg", UriKind.Relative));
                Imgbestai.Source = path;
                //les statistiques principales 
                lstCar.Text += "CARACTÉRISTIQUES : " + Environment.NewLine + Environment.NewLine;
                //vie
                lstCar.Text += "Point de vie entre " + vieMin + " et " + vieMax + Environment.NewLine;
                //PA
                lstCar.Text += "Point d'action : " + PA + Environment.NewLine;
                //PM
                lstCar.Text += "Point de mouvement : " + PM + Environment.NewLine;

                //la liste des résistances de monstres
                lstRes.Text += "RÉSISTANCES : " + Environment.NewLine + Environment.NewLine;
                foreach (Statistique item in e.LstStats)
                {
                    if (item.Nom != Statistique.element.vie && item.Nom != Statistique.element.PM && item.Nom != Statistique.element.PA && item.Nom != Statistique.element.experience)
                    {
                        lstRes.Text += item.Nom.ToString() + " : " + Environment.NewLine + " Entre " + item.ValeurMin + " et " + item.ValeurMax + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
        }
    }
}
