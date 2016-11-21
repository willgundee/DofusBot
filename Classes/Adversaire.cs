using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class Adversaire
    {
        public string nom { get; set; }
        public string level { get; set; }
        public Adversaire (string n , string l)
        {
            nom = n;
            level = l;
        }
    }
}
