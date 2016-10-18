using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
   
    public class Classe
    {
        public List<Sort> LstSort { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        private BDService bd =new BDService();

        public Classe(List<string> classe)
        {
            addSort(Convert.ToInt32(classe[0]));
            Nom = classe[1];
            Description = classe[2];

        }

        private void addSort(int idClasse)
        {
            string Sor = "SELECT * FROM Sorts s INNER JOIN classesSorts  c ON s.idSort = c.idSort Where idClasse=" + idClasse ;
            foreach (List<string> sort in bd.selection(Sor))
            {
                LstSort.Add(new Sort(sort));
            } 

        }
    }
}
