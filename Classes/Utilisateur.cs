using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    // Utilisateur pour la page d'administration
    public class Utilisateur
    {
        public string nom { get; set; }

        public bool estAdmin { get; set; }

        /// <summary>
        /// Constructeur d'Utilisateur
        /// </summary>
        /// <param name="n">Nom de l'utilisateur</param>
        /// <param name="eA">Est-il admin?</param>
        public Utilisateur(string n,bool eA)
        {
            nom = n;
            estAdmin = eA;
        }
    }
}
