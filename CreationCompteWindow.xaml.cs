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


            //TODO REGEX validation nom , mdp et email.


           if(txt_mdp.Password==txtConfirmation.Password)
            {
                return false;
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
