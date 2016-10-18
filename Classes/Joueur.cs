using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Joueur
    {
        public List<Equipement> Inventaire { get; set; }
        public List<Script> LstScript { get; set; }
        public List<Entite> LstEntite { get; set; }
        public string NomUtilisateur { get; set; }
        public string Courriel { get; set; }
        public int Kamas { get; set; }
        public int Avatar { get; set; }

        public Joueur(List<string> infoUser)
        {

        }

    }
}
