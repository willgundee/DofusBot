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

        public BestaireDesc(Entite e)
        {
            string vieMin = "0";
            string vieMax = "0";
            string PA = "0";
            string PM = "0";

            foreach (Statistique item in e.LstStats)
            {
                if (item.Nom == Statistique.element.vie)
                {
                    vieMin = item.ValeurMin.ToString();
                    vieMax = item.ValeurMax.ToString();
                }
                if (item.Nom == Statistique.element.PM)
                    PM = item.Valeur.ToString();
                if (item.Nom == Statistique.element.PA)
                    PA = item.Valeur.ToString();


            }
            InitializeComponent();
            lblNomBes.Text = e.Nom.ToString();
            BitmapImage path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/monsters/200/134.w200h.png"));

            Imgbestai.Source = path;
            lstCar.Text += "CARACTÉRISTIQUES : " + Environment.NewLine + Environment.NewLine;
            lstCar.Text += "Point de vie entre " + vieMin + " et " + vieMax + Environment.NewLine;
            lstCar.Text += "Point d'action : " + PA + Environment.NewLine;
            lstCar.Text += "Point de mouvement : " + PM + Environment.NewLine;


            lstRes.Text += "RÉSISTANCES : " + Environment.NewLine + Environment.NewLine;
            foreach (Statistique item in e.LstStats)
            {
                if (item.Nom != Statistique.element.vie && item.Nom != Statistique.element.PM && item.Nom != Statistique.element.PA && item.Nom != Statistique.element.experience)
                {
                    lstRes.Text += item.Nom.ToString() +" : "+ Environment.NewLine + " Entre "+ item.ValeurMin+" et "+ item.ValeurMax+Environment.NewLine + Environment.NewLine;
                }
            }
        }
    }
}
