using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Classe
    {
        private int IdClasse { get; }
        private Sort[] TabSorts { get; }
        private string Nom { get; }
        public Classe(int IdClasse, Sort[] TabSorts, string Nom)
        {
            this.IdClasse = IdClasse;
            this.TabSorts = TabSorts;
            this.Nom = Nom;
        }
    }
}
