using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Partie
    {
        public int seed { get; set; }

        public string Attaquant { get; set; }
        public string Defendant { get; set; }

        public DateTime datePartie;

        public Partie(string a , string d,DateTime date)
        {
            Attaquant = a;
            Defendant = d;
            datePartie = new DateTime();
            datePartie = date;
        }



    }
}
