using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class Utilisateur
    {
        public string nom { get; set; }
        public bool estAdmin { get; set; }

        public Utilisateur(string n,bool eA)
        {
            nom = n;
            estAdmin = eA;
        }
    }
}
