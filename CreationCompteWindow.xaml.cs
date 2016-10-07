using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace test
{

    public partial class CreationCompteWindow: Window
    {
        private BDService bd = new BDService();
        public CreationCompteWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Authentification A = new Authentification();
            A.Show();
            this.Close(); 

        }
        public bool Valider()
        {
            Regex courriel = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");


            //TODO REGEX validation nom , mdp et email.


           if(txt_mdp.Password==txtConfirmation.Password)
            {
                return false;
            }
            if (txt_nom.Text.ToString().Length > 13 || txt_nom.Text.ToString().Length < 5)
            {

            }


            if (courriel.Match(txt_Courriel.Text.ToString()) != null)
            {

            }


            return true;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

            if(Valider()==true)
            {
                System.Windows.Forms.MessageBox.Show("Bientôt disponible !");

             //   bd.insertion("INSERT  INTO Joueurs(nomUtilisateur,couriel,motDePasse,argent,avatar) VALUES(" + txt_nom.Text + "," + txt_Courriel.Text + "," + txt_mdp.Password + ",0,0 )");

                /* Confirmation confirmation = new Confirmation();
                 creation.Show();
                 this.Close();*/
            }


        }
    }
}
