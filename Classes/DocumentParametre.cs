using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class DocumentParametre
    {
        public string nom { get; set; }

        public string description{get;set;}

        public bool estFacultatif { get; set; }


        public DocumentParametre(string n, string d, bool eF)
        {
            nom = n;

            description = d;

            estFacultatif = eF;

        }

    }
}
