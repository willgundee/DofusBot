using System.Text;

namespace Gofus
{
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
