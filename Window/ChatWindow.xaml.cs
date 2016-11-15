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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    /// 

    public partial class ChatWindow : Window
    {
        public Chat chat;
        DispatcherTimer aTimer;
        Thread trdEnvoie;



        public ChatWindow(string user)
        {
            InitializeComponent();
            this.chat = new Chat();
            btnEnvoyerMessage.IsEnabled = false;
            txtboxHistorique.Text += " ";
            aTimer = new System.Windows.Threading.DispatcherTimer();
            aTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 1);
            chat.nomUtilisateur = user;
            chat.getId();

            aTimer.Start();
            Scroll.ScrollToEnd();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }



        private void OnKeyDowntxtMessage(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && aTimer.IsEnabled)
            {
                string text = txtMessage.Text;
                trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
                trdEnvoie.Start();
                Thread.Yield();
                Scroll.ScrollToEnd();
                txtMessage.Text = "";
            }
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
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




        private void btn_Envoyer_Click(object sender, RoutedEventArgs e)
        {
            string text = txtMessage.Text;
            trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
            trdEnvoie.Start();
            Thread.Yield();
            Scroll.ScrollToEnd(); txtMessage.Text = "";
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
      
        private void btnFermer_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            this.Close();

        }
    }
}

