﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using test;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pArchives.xaml
    /// </summary>
    public partial class pArchives : UserControl
    {

        public ObservableCollection<Partie> lstPartiePerso;
        public ObservableCollection<Partie> lstPartieAll;
        public pArchives()
        {
            InitializeComponent();

            lstPartieAll = new ObservableCollection<Partie>();
            lstPartiePerso = new ObservableCollection<Partie>();

            cboTypePartie.Items.Add("Mes Parties");
            cboTypePartie.Items.Add("Les partie de tout le monde");

        }



        private void loadParties(string typePartie)
        {
            string select = "Select * From Partieentites ";

        }
   

        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTypePartie.SelectedIndex == 0)
            {
                dgHistorique.ItemsSource = lstPartiePerso;
            }
            else
            {
                dgHistorique.ItemsSource = lstPartieAll;
            }
        }
    }
}
