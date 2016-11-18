using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class DocumentPropriete
    {
        public string type{ get; set; }
        public string nom { get; set; }

        public string description { get; set; }



        public DocumentPropriete(string t,string n,string d)
        {
            type = t;

            nom = n;

            description = d;

        }
    }
}
