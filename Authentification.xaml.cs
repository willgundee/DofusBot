﻿using System;
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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Authentification : Window
    {
       public  BDService bdService = new BDService();
        public Authentification()
        {
            InitializeComponent();
        }

  

        private bool valide(List<string>[] hh)
        {
            if (!(hh[0][3] == txtMDP.Password))
            {
                return false;
            }

            return true;
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {

            List<string>[] hh = bdService.selection("SELECT * FROM Joueurs WHERE nomUtilisateur ='" + txtNomU.Text + "'");
            if (hh[0][0] != "rien" && valide(hh) == true)
            {
                Mouse.SetCursor(Cursors.AppStarting);

                MainWindow perso = new MainWindow(Convert.ToInt32(hh[0][0]));
                perso.Show();
                this.Close();
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
            CreationCompteWindow creation = new CreationCompteWindow();
             creation.Show();
             this.Close();
        }
        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {
            List<string>[] hh = bdService.selection("SELECT * FROM Joueurs WHERE nomUtilisateur ='" + txtNomU.Text + "'");
            if (hh[0][0] != "rien" && valide(hh) == true)
            {
                Mouse.SetCursor(Cursors.AppStarting);

                MainWindow perso = new MainWindow(Convert.ToInt32(hh[0][0]));
                perso.Show();
                this.Close();
            }
        }

    }
}

