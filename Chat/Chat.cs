using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Gofus
{
    /// <summary>
    /// Classe chat
    /// Auteur : Marc-Antoine Lemieux
    /// </summary>
    public class Chat
    {
        // Nombre Maximum de messages qui seront afficher.
        public const int MAX_MESSAGES = 70;

        /// <summary>
        /// Liste de MessageText
        /// </summary>
        public ObservableCollection<MessageText> messages { get; private set; }

        
        public string id { get; set; }

         public string nomUtilisateur { get; set; }

        /// <summary>
        /// Contenu du chat en strings
        /// </summary>
        public ObservableCollection<string> contenuChat;


        /// <summary>
        /// Connexion à la BD pour insert
        /// </summary>
        
        BDService bdInsert;
        /// <summary>
        /// Connexion à la BD pour Select
        /// </summary>
        BDService bdSelect;

        public Chat(string u, string i)
        {
            nomUtilisateur = u;
            id = i;
            // Initialisation des propriétés
            contenuChat = new ObservableCollection<string>();
            messages = new ObservableCollection<MessageText>();

            bdSelect = new BDService();
            bdInsert = new BDService();
        }

        /// <summary>
        /// Permet de refresh le contenu du chat.
        /// </summary>
        /// <param name="date">Est-ce qu'il faut afficher la date.</param>
        /// <returns>ObservableCollection de string</returns>
        public ObservableCollection<string> refreshChat(bool date)
        {
            messages = new ObservableCollection<MessageText>();
            contenuChat = new ObservableCollection<string>();

            selectMessages();

            foreach (MessageText ms in messages)
            {
                contenuChat.Add(ms.formaterMessager(date));
            }
            return contenuChat;
        }

        /// <summary>
        /// Permet d'inserer un messagedans la BD.
        /// </summary>
        /// <param name="text">Texte à inserer</param>
        public void envoyerMessage(string text)
        {
            long envoie = 0;
            
            string inser = "INSERT INTO Messages(idJoueur,temps,contenu,uuid)VALUES(" + id + ",NOW(),'" +
                                text + "',UUID())";
            envoie = bdInsert.insertion(inser);
            if (envoie == -1)
            {
                System.Windows.MessageBox.Show("Une erreur s'est produite lors de l'envoie du message.");
            }

        }

        /// <summary>
        /// Permet de sélectionner des messages dans la base de donnée.
        /// </summary>
        private void selectMessages()
        {
            string reqMessages = "SELECT temps,contenu,nomUtilisateur FROM Messages INNER JOIN Joueurs ON Messages.idJoueur = Joueurs.idJoueur ORDER BY temps DESC LIMIT " + MAX_MESSAGES + " ;";
            DataSet result = bdSelect.selectionChat(reqMessages);
            if(result != null)
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
