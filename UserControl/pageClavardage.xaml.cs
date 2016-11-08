﻿using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Clavardage.xaml
    /// </summary>



    public partial class pageClavardage : UserControl
    {

        public Chat chat;
        DispatcherTimer aTimer;
        public ChatWindow fenetreChat;
        public Thread trdEnvoie { get; private set; }
        public pageClavardage(string NomUtilisateur)
        {
            InitializeComponent();

            txtMessage.IsEnabled = false;

            btnQuitterSalle.IsEnabled = false;

            lblEtat.Content = "État : Non connecté à la salle";
            lblEtat.Foreground = new SolidColorBrush(Colors.Orange);

            
       

            this.chat = new Chat();
            chat.nomUtilisateur = NomUtilisateur;
            chat.getId();

            btnEnvoyerMessage.IsEnabled = false;


            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 1);

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            if (this != null)
            {

                ObservableCollection<string> messages = new ObservableCollection<string>();
                Thread trdRefresh = new Thread(() =>
                {

                    messages = chat.refreshChat();
                    System.Windows.Application.Current.Dispatcher.Invoke(new System.Action(() =>
                    {
                        txtboxHistorique.Text = "";
                        foreach (string m in messages)
                        {
                            txtboxHistorique.Text += m;
                        }
                    }));
                });
                trdRefresh.Start();
                Thread.Yield();

                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                aTimer.Stop();
            }
        }

        private void BtnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            string text = txtMessage.Text;
            trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
            trdEnvoie.Start();
            Thread.Yield();
        }
        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && aTimer.IsEnabled)
            {
                string text = txtMessage.Text;
                trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
                trdEnvoie.Start();
                Thread.Yield();
            }
        }




        private void txtMessage_TextChange(object sender, TextChangedEventArgs e)
        {


            if (txtMessage.Text.ToString() == "")
            {

                btnEnvoyerMessage.IsEnabled = false;

            }
            else
            {
                if (aTimer.IsEnabled)
                    btnEnvoyerMessage.IsEnabled = true;
            }
        }

        private void btnRejoindreSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Start();
            lblEtat.Content = "État : Connecter à la salle.";
            lblEtat.Foreground = new SolidColorBrush(Colors.ForestGreen);
            txtMessage.IsEnabled = true;
            btnRejoindreSalle.IsEnabled = false;
            btnQuitterSalle.IsEnabled = true;

        }

        private void btnQuitterSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            txtMessage.Text = "";
            txtboxHistorique.Text = "";
            lblEtat.Content = "État : Déconnecter.";
            lblEtat.Foreground = new SolidColorBrush(Colors.Orange);

            btnEnvoyerMessage.IsEnabled = false;
            txtMessage.IsEnabled = false;

            btnRejoindreSalle.IsEnabled = true;
            btnQuitterSalle.IsEnabled = false;

        }

        public void MainWindow_ChatWindowClosing(object sender, System.EventArgs e)
        {
            fenetreChat = null;
        }

        private void BtnModLess_Click(object sender, RoutedEventArgs e)
        {

            if (fenetreChat != null)
            {
                fenetreChat.Activate();
            }
            else
            {
                fenetreChat = new ChatWindow(chat.nomUtilisateur);
                fenetreChat.Closed += MainWindow_ChatWindowClosing;
                fenetreChat.Show();
            }
        }



    }
}
