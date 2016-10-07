using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace test
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    /// 

    public partial class ChatWindow : Window
    {
        public Chat chat;
        DispatcherTimer aTimer;
        public ChatWindow()
        {
            InitializeComponent();
            this.chat = new Chat();
            btnEnvoyerMessage.IsEnabled = false;
            txtboxHistorique.Text += " ";
            aTimer = new System.Windows.Threading.DispatcherTimer();
            aTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 2);



        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (this != null)
            {
                chat.refreshChatModLess();

                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                aTimer.Stop();
            }
        }

 


        private void btn_Envoyer_Click(object sender, RoutedEventArgs e)
        {
            long envois = chat.envoyerMessageModLess();
            if (envois != -1)
            {
                chat.refreshChatModLess();
                Scroll.ScrollToEnd();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'envoie du message.");
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
                if(aTimer.IsEnabled)
                btnEnvoyerMessage.IsEnabled = true;
            }
        }

        private void btnRejoindreSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Start();
            txtMessage.IsEnabled = true;
        }

        private void btnQuitterSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            txtMessage.Text = "";
            txtboxHistorique.Text = "";
            btnEnvoyerMessage.IsEnabled = false;
            txtMessage.IsEnabled = false;
            this.Close();
        }
    }
}

