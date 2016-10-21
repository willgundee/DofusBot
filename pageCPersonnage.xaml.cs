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

namespace test
{
 
    /// <summary>
    /// Logique d'interaction pour pageCPersonnage.xaml
    /// </summary>
    public partial class pageCPersonnage : UserControl
    {
        public Classe Cl;

     public BDService bd = new BDService();
        public pageCPersonnage()
        {
            InitializeComponent();
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            string Classe;
            string choix = (sender as Image).Name;


            switch (choix)
            {
                case "ClasseCra": Classe= "Cra" ;
                    break;
                case "ClasseIop": Classe="Iop" ;
                    break;
                case "ClasseEcaflip": Classe="Ecaflip" ;
                    break;
                default:
                    Classe = null;
                    break;
            }

             Cl=new Classe( bd.selection("SELECT * FROM Classes WHERE Nom = '" + Classe+"'")[0]);
            txtbDesc.Text = Cl.Description;
        }
    }
}
