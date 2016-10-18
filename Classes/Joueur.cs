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

        public Joueur(List<string> infoUser, List<string>[] items,List<string>[] personages,List<string>[] scripts)
        {
            NomUtilisateur = infoUser[1];
            Courriel = infoUser[2];
            Kamas = Convert.ToInt32(infoUser[4]);
            Avatar = Convert.ToInt32(infoUser[5]);
            foreach (List<string> item in items)
            {
                //Inventaire.Add(new Equipement())
            }
            foreach (List<string> perso in personages)
            {

            }
            foreach (List<string> script in scripts)
                LstScript.Add( new Script(script));


        }

    }
}
