using System.Text;

namespace Gofus
{
    /// <summary>
    /// Classe MessageText : Contient les informations d'un message.   /// Auteur : Marc-Antoine Lemieux
    /// </summary>
    public class MessageText
    {
        public string message { get; set; }
        public string auteur{ get; set; }
        public string TempsEnvois { get; set; }
        public MessageText(string msg,string atr,string tmp)
        {
            message = msg;
            auteur = atr;
            TempsEnvois = tmp;
            
   

        }

        /// <summary>
        /// Premet de formater un message pour qu'il soit prêt à être afficher.
        /// </summary>
        /// <param name="date">Est-ce qu'il faut afficher la date.</param>
        /// <returns>string</returns>
        public string formaterMessager(bool date)
        {
            StringBuilder textFinal = new StringBuilder();
            if (date)
            {
                textFinal.Append(TempsEnvois);
                textFinal.Append(" - ");
            }
            
            textFinal.Append(auteur + " dit : " + message + "\n");
            return textFinal.ToString();
        }
    }
}
