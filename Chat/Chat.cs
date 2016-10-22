
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace test
{

    public class Chat
    {
        public const int MAX_MESSAGES = 70;

        
        public ObservableCollection<MessageText> messages { get; private set; }

        private string id;

        public string nomUtilisateur { get; set; }


        public ObservableCollection<string> contenuChat;

        BDService bdInsert;

        BDService bdSelect;

        public Chat()
        {
            contenuChat = new ObservableCollection<string>();
            messages = new ObservableCollection<MessageText>();

            bdSelect = new BDService();
            bdInsert = new BDService();
        }

        public void getId()
        {
            string reqid = "SELECT idJoueur from Joueurs WHERE NomUtilisateur = '" + nomUtilisateur + "';";
            List<string>[] idResult = bdSelect.selection(reqid);
            id = idResult[0][0];
        }

        public ObservableCollection<string> refreshChat()
        {
            messages = new ObservableCollection<MessageText>();
            contenuChat = new ObservableCollection<string>();

            selectMessages();

            foreach (MessageText ms in messages)
            {
                contenuChat.Add(ms.formaterMessager());
            }
            return contenuChat;
        }

        public void envoyerMessage(string text)
        {
            long envoie = 0;
            string inser = "INSERT INTO Messages(idJoueur,temps,contenu,uuid)VALUES(" + id.ToString() + ",NOW(),'" +
                                text + "',UUID())";
            envoie = bdSelect.insertion(inser);
            if (envoie == -1)
            {
                System.Windows.MessageBox.Show("Une erreur s'est produite lors de l'envoie du message.");
            }

        }
        private void selectMessages()
        {
            string reqMessages = "SELECT temps,contenu,nomUtilisateur FROM Messages INNER JOIN Joueurs ON Messages.idJoueur = Joueurs.idJoueur ORDER BY temps DESC LIMIT " + MAX_MESSAGES + " ;";
            DataSet result = bdSelect.selectionChat(reqMessages);

            foreach (DataTable table in result.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    messages.Insert(0, new MessageText(row[1].ToString(), row[2].ToString(), row[0].ToString()));
                }
            }

        }
    }
}
