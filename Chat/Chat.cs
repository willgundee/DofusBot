
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace test
{

    public class Chat
    {
        public const int MAX_MESSAGES = 100;
        public ObservableCollection<MessageText> messages { get; private set; }
        public EventHandler Tick { get; internal set; }

        public ObservableCollection<string> contenuChat;

        BDService bdChat;

        public Chat()
        {
            contenuChat = new ObservableCollection<string>();
            messages = new ObservableCollection<MessageText>();
            bdChat = new BDService();
        }

        public void refreshChat()
        {

            messages = new ObservableCollection<MessageText>();
            contenuChat = new ObservableCollection<string>();

            selectMessages();

            foreach (MessageText ms in messages)
            {
                contenuChat.Add(ms.formaterMessager());
            }



            ((MainWindow)Application.Current.MainWindow).txtboxHistorique.Text = "";


            foreach (string st in contenuChat)
            {
                ((MainWindow)Application.Current.MainWindow).txtboxHistorique.Text += st;
            }



        }



        public void refreshChatModLess()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(ChatWindow))
                {
                    messages = new ObservableCollection<MessageText>();
                    contenuChat = new ObservableCollection<string>();

                    selectMessages();

                    foreach (MessageText ms in messages)
                    {
                        contenuChat.Add(ms.formaterMessager());
                    }



                     (window as ChatWindow).txtboxHistorique.Text = "";




                    foreach (string st in contenuChat)
                    {
                        (window as ChatWindow).txtboxHistorique.Text += st;
                    }

                }
            }


        }






        public long envoyerMessage()
        {
            string inser = "INSERT INTO Messages(idJoueur,temps,contenu)VALUES(17,NOW(),'" +
                ((MainWindow)Application.Current.MainWindow).txtMessage.Text.ToString() + "');";
            long test = bdChat.insertion(inser);

            return test;
        }



        public long envoyerMessageModLess()
        {
            long testeur = 0;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(ChatWindow))
                {
                    string inser = "INSERT INTO Messages(idJoueur,temps,contenu)VALUES(17,NOW(),'" +
                     (window as ChatWindow).txtMessage.Text.ToString() + "');";
                    testeur = bdChat.insertion(inser);
                }
            }
            return testeur;
        }

      


        private void selectMessages()
        {

            string reqMessages = "SELECT * FROM Messages ORDER BY Temps DESC LIMIT 75; ";
            List<string>[] result = bdChat.selection(reqMessages);

            foreach (List<string> message in result.Reverse())
            {
                if (message.Count > 1)
                {
                    string reqAuteur = "SELECT nomUtilisateur FROM Joueurs WHERE idJoueur = " + message[1] + ";";
                    List<string>[] nom = bdChat.selection(reqAuteur);
                    messages.Add(new MessageText(message[3], nom[0][0], message[2]));
                }

            }

        }


    }
}
