using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pageGestion.xaml
    /// </summary>
    public partial class pageGestion : UserControl
    {

        private List<string> lstAvatars;
        private BDService bd;
        private Joueur Player;
        public FenetreRapport rapport;
        public int idJoueur { get; set; }

        public pageGestion(Joueur joueur, int id)
        {
            bd = new BDService();
            InitializeComponent();
            lstAvatars = new List<string>();
            GenererAvatars();
            Player = joueur;
            idJoueur = id;
            string URI = lstAvatars[Player.Avatar];
            iAvatar.Source = new BitmapImage(new Uri(URI));
            txt_AncienCourriel.Text = Player.Courriel;
            txt_nomUtilisateur.Text = Player.NomUtilisateur;
        }
        #region Marc_OngletGestionCompte

        void GenererAvatars()
        {

            for (int J = 1; J < 94; J++)
            {
                string ajout;
                if (J > 10)
                    ajout = "";
                else
                    ajout = "0";
                string path = "http://staticns.ankama.com/dofus/www/game/items/200/180" + ajout + J.ToString() + ".png";
                lstAvatars.Add(path);
            }
        }


        /// ***************************************************
        /// / ONGLET OPTIONS
        // ***************************************************
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            StringBuilder UpdSt = new StringBuilder();
            UpdSt.Append("UPDATE Joueurs SET ");
            /* Faire un update si toute est legit*/
            if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "" || txt_Courriel.Text != "")
            {
                /* Update */
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);

                if (txt_Courriel.Text != "")
                {
                    if (txt_Courriel.Text != txt_AncienCourriel.Text)
                    {
                        lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
                        UpdSt.Append("courriel = '" + txt_Courriel.Text + "'");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Votre nouveau courriel doit être différent de l'ancien", "Courriel");
                        lbl_Courriel.Foreground = new SolidColorBrush(Colors.Red);
                        valide = false;
                    }

                }
                if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "")
                {
                    string reqid = "SELECT motDePasse from Joueurs WHERE NomUtilisateur = '" + Player.NomUtilisateur + "';";
                    List<string>[] idResult = bd.selection(reqid);
                    string mdp = idResult[0][0];

                    if (txt_AncienMdp.Password == mdp)
                    {
                        UpdSt.Append(" , motDePasse = '" + txt_mdp.Password + "'");
                    }
                    else
                    {
                        lbl_AncienMdp.Foreground = new SolidColorBrush(Colors.Red);
                        System.Windows.Forms.MessageBox.Show("Votre Ancien mot de passe n'est pas valide", "Ancien mot de passe");
                        valide = false;
                    }


                }

            }
            else
            {
                if (txt_mdp.Password == "" && txtConfirmation.Password == "")
                {
                    /* Aucune modification effectué*/
                    lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                    lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
                    lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
                    valide = false;
                }
                else if (txt_mdp.Password != "")
                {
                    /* Erreur de confirmation*/
                    lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Red);
                    valide = false;
                    System.Windows.Forms.MessageBox.Show("Votre Confirmation doit être identique à votre nouveau mot de passe", "Confirmation");
                }
                else if (txt_mdp.Password == "" & txtConfirmation.Password != "")
                {
                    /* Mot de passe vide*/
                    lbl_Mdp.Foreground = new SolidColorBrush(Colors.Red);
                    valide = false;
                    System.Windows.Forms.MessageBox.Show("Votre Confirmation doit être identique à votre nouveau mot de passe", "Champs mot de passe vide");
                }
            }


            if (valide)
            {
                UpdSt.Append(" WHERE nomUtilisateur = '" + Player.NomUtilisateur + "';");
                string st = UpdSt.ToString();
                if (bd.Update(st))
                {
                    System.Windows.Forms.MessageBox.Show("Mise à jour avec succès de vos infos!!");
                }
                txt_AncienCourriel.Text = txt_Courriel.Text;
                Player.Courriel = txt_AncienCourriel.Text;
                txt_mdp.Password = "";
                txtConfirmation.Password = "";
                txt_Courriel.Text = "";
                lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
                lbl_AncienMdp.Foreground = new SolidColorBrush(Colors.Black);

            }


        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtConfirmation.Password = "";
            txt_mdp.Password = "";
            txt_Courriel.Text = "";

            lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
            lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            Authentification a = new Authentification();

            a.Show();
            if (Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(MainWindow)) != null)
                Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)).Close();
        }

        public void MainWindow_RapportClosing(object sender, System.EventArgs e)
        {
            rapport = null;
        }



        private void btnSuggestion_Click(object sender, RoutedEventArgs e)
        {
            if (rapport != null)
            {
                rapport.Activate();
            }
            else
            {
                rapport = new FenetreRapport(idJoueur);
                rapport.Closed += MainWindow_RapportClosing;
                rapport.ShowDialog();
            }

        }

        private void Change_Avatar(object sender, MouseButtonEventArgs e)
        {
            ChangerAvatar();
        }


        private void ChangerAvatar()
        {

            choixAvatar choisir = new choixAvatar(lstAvatars, Player.Avatar);
            choisir.ShowDialog();

            string URI = lstAvatars[choisir.idAvatar];
            iAvatar.Source = new BitmapImage(new Uri(URI));
            Player.Avatar = choisir.idAvatar;

            if (bd != null)
            {
                bool upd = bd.Update("UPDATE  Joueurs SET  Avatar =  " + Player.Avatar + " WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "';COMMIT");
            }

        }

        private void btnChange_Avatar(object sender, RoutedEventArgs e)
        {
            ChangerAvatar();
        }


        #endregion
    }

}
