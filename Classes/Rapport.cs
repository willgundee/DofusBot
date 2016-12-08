using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    /// <summary>
  /// Auteur : Marc-Antoine Lemieux
  /// Classe Rapport
  /// Contient les infos d'un rapport.
    /// </summary>

    public class Rapport
    {
        public string msg { get; set; }

        public string temps { get; set; }

        public string titre { get; set; }

        public string type { get; set; }

        public string id { get; set; }

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="M">Message</param>
        /// <param name="T">Le temps</param>
        /// <param name="Titre">Le titre</param>
        /// <param name="ty">le type de rapport</param>
        /// <param name="idt">l'id du Rapport</param>
        public Rapport(string M,string T,string Titre, string ty,string idt)
        {
            msg = M;
            temps = T;
            titre = Titre;
            type = ty;
            id = idt;
        }


      


    }
}
