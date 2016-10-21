
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
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    messages = new ObservableCollection<MessageText>();
                    contenuChat = new ObservableCollection<string>();

                    selectMessages();

                    foreach (MessageText ms in messages)
                    {
                        contenuChat.Add(ms.formaterMessager());
                    }



                        (window as MainWindow).txtboxHistorique.Text = "";


                    foreach (string st in contenuChat)
                    {

                        (window as MainWindow).txtboxHistorique.Text += st;
                    }

                }
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
            long test = 0;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    string inser = "INSERT INTO Messages(idJoueur,temps,contenu,uuid)VALUES(9,NOW(),'" +
                (window as MainWindow).txtMessage.Text.ToString() + "',UUID());";
                    test = bdChat.insertion(inser);

                }
            }
            return test;
        }



        public long envoyerMessageModLess()
        {
            long testeur = 0;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(ChatWindow))
                {
                    if ((window as ChatWindow).txtMessage.Text.ToString() != "")
                    {
                        string inser = "INSERT INTO Messages(idJoueur,temps,contenu,uuid)VALUES(9,NOW(),'" +
                                            (window as ChatWindow).txtMessage.Text.ToString() + "',UUID());";
                        testeur = bdChat.insertion(inser);
                        return testeur;
                    }

                }
            }
            return testeur;
        }




        private void selectMessages()
        {

            string reqMessages = "SELECT * FROM Messages ORDER BY temps DESC LIMIT 75; ";
            List<string>[] result = bdChat.selection(reqMessages);

            foreach (List<string> message in result.Reverse())
            {
                if (message.Count > 1)
                {
                    string reqAuteur = "SELECT nomUtilisateur FROM Joueurs WHERE idJoueur = " + message[2] + ";";
                    List<string>[] nom = bdChat.selection(reqAuteur);
                    messages.Add(new MessageText(message[4], nom[0][0], message[3]));
                }

            }

        }


    }
}
