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

        public Classe(List<string> classe, List<string>[] sorts)
        {
            Nom = classe[1];
            Description = classe[2];

           /* foreach (List<string> sort in sorts)
                ListeSort.Add(new Sort(sorts)));*/
        }
    }
}
