using System;
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



            this.chat = new Chat();
            chat.nomUtilisateur = NomUtilisateur;
            chat.getId();




            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 1);

            aTimer.Start();
            Scroll.ScrollToEnd();
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
                    Application.Current.Dispatcher.Invoke(new Action(() =>
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
            trdEnvoie = new Thread(() =>
            {
                chat.envoyerMessage(text);
            });
            trdEnvoie.Start();
            Thread.Yield();
            Scroll.ScrollToEnd(); txtMessage.Text = "";

        }
        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && aTimer.IsEnabled)
            {
                string text = txtMessage.Text;
                trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
                trdEnvoie.Start();
                Thread.Yield();
                Scroll.ScrollToEnd(); txtMessage.Text = "";
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
