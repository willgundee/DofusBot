using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Gofus
{

    public partial class CreationCompteWindow : Window
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

            bool Valide = true;

            string req = "SELECT * FROM Joueurs WHERE nomUtilisateur = '" + txt_nom.Text.ToString() + "';";
            List<string>[] result = bd.selection(req);


            #region nomUtilisateurValidations
            if (txt_nom.Text.ToString() == "")
            {
                lbl_nom.Content = "Nom d'utilisateur (Champs Obligatoire)";
                lbl_nom.Foreground = new SolidColorBrush(Colors.Orange);
                Valide = false;
            }
            else
            {

                if (result[0][0] != "rien")
                {
                    lbl_nom.Content = "Nom d'utilisateur (Nom déjà utilisé)";
                    lbl_nom.Foreground = new SolidColorBrush(Colors.Orange);
                    Valide = false;
                }
                else
                {
                    if (txt_nom.Text.ToString().Length > 13 || txt_nom.Text.ToString().Length < 5)
                    {
                        lbl_nom.Content = "Nom d'utilisateur (Entre 5 et 13 Lettres/chiffres)";
                        lbl_nom.Foreground = new SolidColorBrush(Colors.Orange);
                        Valide = false;
                    }
                    else
                    {
                        lbl_nom.Content = "Nom d'utilisateur";
                        lbl_nom.Foreground = new SolidColorBrush(Colors.Black);
                    }

                }

            }
            #endregion



            if (txt_mdp.Password != txtConfirmation.Password)
            {
                lbl_Confirmation.Content = "Confimation de mot de passe ( Doit être identique au mot de passe ) ";
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Orange);
                Valide = false;
            }
            else
            {
                lbl_Confirmation.Content = "Confimation de mot de passe";
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (txt_mdp.Password.Length < 5 || txt_mdp.Password.Length > 15)
            {
                lbl_Mdp.Content = "Mot de passe (Entre 5 et 15 Lettres/chiffres)";
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Orange);
                Valide = false;
            }
            else
            {
                lbl_Mdp.Content = "Mot de passe";
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
            }



            if (txt_Courriel.Text.ToString() == "")
            {
                lbl_Courriel.Foreground = new SolidColorBrush(Colors.Orange);
                lbl_Courriel.Content = "Courriel (Champs Obligatoire)";
                Valide = false;
            }
            else
            {
                lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
            }


            // Regex courriel = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");


            //TODO REGEX validation nom , mdp et email.


            // if(txt_mdp.Password==txtConfirmation.Password)
            // {
            //  return false;
            // }



            // if (courriel.Match(txt_Courriel.Text.ToString()) != null)
            // {

            //  }


            return Valide;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

            if (Valider())
            {
                var result = System.Windows.MessageBox.Show("Souhaitez-vous créer votre compte avec ces informations?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = (int)bd.insertion("INSERT INTO Joueurs(nomUtilisateur, courriel, motDePasse, argent, avatar) VALUES('" + txt_nom.Text.ToString() + "', '" + txt_Courriel.Text.ToString() + "', '" + txt_mdp.Password + "', 3000, 0);");
                    int ascript = (int)bd.insertion("INSERT INTO Scripts(contenu,nom, uuid) VALUES('','Script1',UUID())");
                    int joueurscript = (int)bd.insertion("INSERT INTO JoueursScripts(idJoueur,idScript) VALUES("+ id.ToString() + ","+ ascript.ToString() +")");

                    MainWindow Main = new MainWindow(id);


                    System.Windows.Forms.MessageBox.Show("Vous êtes connecté ! ","Information");
                    Main.Show();
                    this.Close();
                }
            

            }


        }
    }
}
