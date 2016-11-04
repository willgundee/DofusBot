using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string formaterMessager()
        {
            StringBuilder textFinal = new StringBuilder();
            textFinal.Append(TempsEnvois);
            textFinal.Append(" - " + auteur + " dit : " + message + "\n");
            return textFinal.ToString();
        }
    }
}
