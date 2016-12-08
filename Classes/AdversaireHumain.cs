using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{

    /// <summary>
    /// Classe pour les adversaires humains.
    /// </summary>
    public class AdversaireHumain : Adversaire
    {
        public string proprietaire { get; set; }

        public int trueLevel { get; set; }

        public AdversaireHumain(string n, int l,string p) : base(n,l,l)
        {
            proprietaire = p;
            trueLevel = l;
        }
    }

}
