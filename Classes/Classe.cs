using System;
using System.Collections.Generic;

namespace Gofus
{


    public class Classe
    {
        public List<Sort> LstSorts { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        private BDService bd =new BDService();
        /// <summary>
        /// Constructeur d'une classe
        /// </summary>
        /// <param name="classe"></param>
        public Classe(List<string> classe)
        {
            addSorts(Convert.ToInt32(classe[0]));
            Nom = classe[1];
            Description = classe[2];
        }
        /// <summary>
        /// ajout des sorts de la classes
        /// </summary>
        /// <param name="idClasse"></param>
        private void addSorts(int idClasse)
        {
            LstSorts = new List<Sort>();
            string Sor = "SELECT * FROM Sorts s INNER JOIN classesSorts  c ON s.idSort = c.idSort Where idClasse=" + idClasse ;
            foreach (List<string> sort in bd.selection(Sor))
                LstSorts.Add(new Sort(sort));
        }
    }
}
