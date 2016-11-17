using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Authentification : Window
    {
        public BDService bdService = new BDService();
        public MediaPlayer music;

        public Authentification()
        {
            InitializeComponent();
            music = new MediaPlayer();
            PlaySound();
        }

        private void PlaySound()
        {                   
            music.Open(new Uri(@"..\..\Resources\Music.mp3", UriKind.RelativeOrAbsolute));
            music.Volume = 1;
            music.Play();
        }

        private bool valide(List<string>[] hh)
        {
            if (!(hh[0][3] == txtMDP.Password))
            {
                lblMDP.Foreground = new SolidColorBrush(Colors.Red);
                return false;            
            }          
            return true;
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            List<string>[] hh = bdService.selection("SELECT * FROM Joueurs WHERE nomUtilisateur ='" + txtNomU.Text + "'");
            lblMDP.Foreground = new SolidColorBrush(Colors.Black);
            lblNomU.Foreground = new SolidColorBrush(Colors.Black);
            if (hh[0][0] != "rien" && valide(hh) == true)
            {
                Mouse.SetCursor(Cursors.AppStarting);
                music.Stop();
                MainWindow perso = new MainWindow(Convert.ToInt32(hh[0][0]));
                perso.Show();
                this.Close();
            }
            if (hh[0][0] == "rien")
            {
                lblNomU.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void btnVisionner_Click(object sender, RoutedEventArgs e)
        {
            PageVisionneuse pgv = new PageVisionneuse();
            pgv.Show();
            this.Close();
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            // System.Windows.Forms.MessageBox.Show("Bientôt disponible !");
            créationUser creation = new créationUser();
            creation.Show();
            this.Close();
        }
        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {     
            if (e.Key == Key.Return)
            {
                List<string>[] hh = bdService.selection("SELECT * FROM Joueurs WHERE nomUtilisateur ='" + txtNomU.Text + "'");
                lblMDP.Foreground = new SolidColorBrush(Colors.Black);
                lblNomU.Foreground = new SolidColorBrush(Colors.Black);
                if (hh[0][0] != "rien" && valide(hh) == true)
                {
                    Mouse.SetCursor(Cursors.AppStarting);
                    music.Stop();
                    MainWindow perso = new MainWindow(Convert.ToInt32(hh[0][0]));
                    perso.Show();
                    this.Close();
                }
                if (hh[0][0] == "rien")
                {
                    lblNomU.Foreground = new SolidColorBrush(Colors.Red);
                }

            }

        }

    }
}

