﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace test
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
                lbl_nom.Content = "Nom d'utilisateur (Champs vide)";
                lbl_nom.Foreground = new SolidColorBrush(Colors.Red);
                Valide = false;
            }
            else
            {

                if (result[0][0] != "rien")
                {
                    lbl_nom.Content = "Nom d'utilisateur (Nom utilisé)";
                    lbl_nom.Foreground = new SolidColorBrush(Colors.Red);
                    Valide = false;
                }
                else
                {
                    if (txt_nom.Text.ToString().Length > 13 || txt_nom.Text.ToString().Length < 5)
                    {
                        lbl_nom.Content = "Nom d'utilisateur (Entre 5 et 13 Lettres/chiffres)";
                        lbl_nom.Foreground = new SolidColorBrush(Colors.Red);
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
                lbl_Confirmation.Content = "Confimation de mot de passe ( Doit être identique au champs de mot de passe ) ";
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Red);
                Valide = false;
            }
            else
            {
                lbl_Confirmation.Content = "Confimation de mot de passe";
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (txt_mdp.Password.Length < 5 || txt_mdp.Password.Length > 15)
            {
                lbl_Mdp.Content = "Mot de passe (Doit contenir entre 5 et 15 lettres/chiffres)";
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Red);
                Valide = false;
            }
            else
            {
                lbl_Mdp.Content = "Mot de passe";
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
            }



            if (txt_Courriel.Text.ToString() == "")
            {
                lbl_Courriel.Foreground = new SolidColorBrush(Colors.Red);
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

            if (Valider() == true)
            {

                var result = System.Windows.MessageBox.Show("Souhaitez-vous créer votre compte avec ces informations?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = (int)bd.insertion("INSERT INTO Joueurs(nomUtilisateur, courriel, motDePasse, argent, avatar) VALUES('" + txt_nom.Text.ToString() + "', '" + txt_Courriel.Text.ToString() + "', '" + txt_mdp.Password + "', 0, 0);");
                    MainWindow Main = new MainWindow(id);
                    System.Windows.Forms.MessageBox.Show("Vous êtes connecté ! ","Information");
                    Main.Show();
                    this.Close();

                }
                else
                {

                }

            }


        }
    }
}
