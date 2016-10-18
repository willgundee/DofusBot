using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
   
    public class Classe
    {
        public List<Sort> ListeSort { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public Classe(List<string> Race, List<string>[] sorts)
        {
            Nom = Race[1];
            Description = Race[2];

        }
    }
}
