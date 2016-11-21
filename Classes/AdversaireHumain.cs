using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class AdversaireHumain : Adversaire
    {
        public string proprietaire { get; set; }

        public AdversaireHumain(string n, string l,string p) : base(n,l)
        {
            proprietaire = p;
        }
    }

}
