﻿using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Threading;

namespace Gofus
{
    /// <summary>
    /// Auteur : Marc-Antoine Lemieux
    /// Fonctions de la page clavardage.
    /// </summary>
    public partial class pageClavardage : UserControl
    {
        // Classe Chat
        public Chat chat;
        // Timer async
        public DispatcherTimer aTimer;
        // Fenêtre de chat modless
        public ChatWindow fenetreChat;
        public Thread trdEnvoie { get; private set; }
        public pageClavardage(string NomUtilisateur, bool IsWindow,string id)
        {
            InitializeComponent();
            if (IsWindow)
                Dispatcher.Invoke(new Action(() => btnModLess.Visibility = Visibility.Hidden));
            // Initialisation des informations requises pour le chat.
            this.chat = new Chat(NomUtilisateur,id);
            // Initialisation du DispatcherTimer
            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 2);
            aTimer.Start();
            Dispatcher.Invoke(new Action(() => Scroll.ScrollToEnd()));
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this != null)
            {
                bool afficheTemps;
                // Vérification : faut-il afficher le temps.
                if (ckBox.IsChecked == false)
                {
                    afficheTemps = false;
                }
                else
                {
                    afficheTemps = true;
                }
                // Reset des messages du chat.
                ObservableCollection<string> messages = new ObservableCollection<string>();
                // Création d'un Thread pour refresh le chat en background.
                Thread trdRefresh = new Thread(() =>
                {
                    // Selection des messages
                    messages = chat.refreshChat(afficheTemps);
                    //Dispatcher pour modifier les controls de façon async.
                    Dispatcher.Invoke(new Action(() =>
                    {
                        // Modfication de la txtboxHistorique
                        txtboxHistorique.Text = "";
                        StringBuilder Contenu = new StringBuilder();
                        foreach (string m in messages)
                        {
                            Contenu.Append(m);
                        }
                        txtboxHistorique.Text = Contenu.ToString();
                    }));
                });
                // Démarrage du Thread.
                trdRefresh.Start();
                Thread.Yield();
                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                aTimer.Stop();
            }
        }
        /// <summary>
        /// Fonction du BtnEnvoyer click.
        /// Envois un message à la BD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            string text = "";
            Dispatcher.Invoke(new Action(() =>
            {
                text = txtMessage.Text;
            }));
            text = text.Replace("'", "\\'");
            trdEnvoie = new Thread(() =>
            {
                chat.envoyerMessage(text);
            });
            trdEnvoie.Start();
            Thread.Yield();
            Dispatcher.Invoke(new Action(() =>
            {
                Scroll.ScrollToEnd();
                txtMessage.Text = "";
            }));
          
        }

        /// <summary>
        /// Permet d'envoyer un message à l'aide de la touche entrée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && aTimer.IsEnabled)
            {
                if (txtMessage.Text != "")
                {
                    string text = txtMessage.Text;
                    text = text.Replace("'", @"\'");
                    trdEnvoie = new Thread(() => chat.envoyerMessage(text));
                    trdEnvoie.Start();
                    Thread.Yield();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        Scroll.ScrollToEnd(); txtMessage.Text = "";
                    }));
                }
            }
        }

        /// <summary>
        /// Permet d'activer le bouton envoyer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMessage_TextChange(object sender, TextChangedEventArgs e)
        {
            if (txtMessage.Text.ToString() == "")
            {
                Dispatcher.Invoke(new Action(() => btnEnvoyerMessage.IsEnabled = false));
            }
            else
            {
                if (aTimer.IsEnabled)
                    Dispatcher.Invoke(new Action(() => btnEnvoyerMessage.IsEnabled = true));
            }
        }
        public void MainWindow_ChatWindowClosing(object sender, System.EventArgs e)
        {
            fenetreChat = null;
        }

        /// <summary>
        /// Ouvrir en mode fenêtre la fenêtre de clavardage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModLess_Click(object sender, RoutedEventArgs e)
        {
            if (fenetreChat != null)
            {
                fenetreChat.Activate();
            }
            else
            {
                fenetreChat = new ChatWindow(chat.nomUtilisateur,chat.id);
                fenetreChat.Closed += MainWindow_ChatWindowClosing;
                fenetreChat.Show();
            }
        }
    }
}
