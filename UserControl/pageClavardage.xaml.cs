using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        DispatcherTimer aTimer;
        // Fenêtre de chat modless
        public ChatWindow fenetreChat;
        public Thread trdEnvoie { get; private set; }
        public pageClavardage(string NomUtilisateur)
        {
            InitializeComponent();
            // Initialisation des informations requises pour le chat.
            this.chat = new Chat();
            chat.nomUtilisateur = NomUtilisateur;
            chat.getId();
            // Initialisation du DispatcherTimer
            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 2);
            aTimer.Start();
            Scroll.ScrollToEnd();
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
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        // Modfication de la txtboxHistorique
                        txtboxHistorique.Text = "";
                        StringBuilder Content = new StringBuilder();
                        foreach (string m in messages)
                        {
                            Content.Append(m);
                        }
                        txtboxHistorique.Text = Content.ToString();
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
            string text = txtMessage.Text;
            text = text.Replace("'", "\\'");
            trdEnvoie = new Thread(() =>
            {
                chat.envoyerMessage(text);
            });
            trdEnvoie.Start();
            Thread.Yield();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Scroll.ScrollToEnd();
                txtMessage.Text = "";
            }));
        }
        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && aTimer.IsEnabled)
            {
                if (txtMessage.Text != "")
                {
                    string text = txtMessage.Text;
                    text = text.Replace("'", @"\'");
                    trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
                    trdEnvoie.Start();
                    Thread.Yield();
                    Scroll.ScrollToEnd(); txtMessage.Text = "";
                }

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
