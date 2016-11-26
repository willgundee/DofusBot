using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class DocumentMethod
    {

        public string type { get; set; }

        public string nom { get; set; }

        public string description { get; set; }

        public List<DocumentParametre> lstParam;

        public DocumentMethod(string t, string n,string d ,List<DocumentParametre> lp)
        {
            lstParam = new List<DocumentParametre>();
            type = t;

            nom = n;

            description = d;

        }

    }
}
