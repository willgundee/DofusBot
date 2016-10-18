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

        private BDService bd = new BDService();


        public Joueur(List<string> infoUser)
        {
            NomUtilisateur = infoUser[1];
            Courriel = infoUser[2];
            Kamas = Convert.ToInt32(infoUser[4]);
            Avatar = Convert.ToInt32(infoUser[5]);

            addInventaire(Convert.ToInt32(infoUser[0]));
            addScripts(Convert.ToInt32(infoUser[0]));
            addInventaire(Convert.ToInt32(infoUser[0]));


        }
        private void addInventaire(int idJoueur)
        {
            string equip = "SELECT * FROM joueursequipements je INNER JOIN equipements e ON e.idEquipement = je.idequipement WHERE idJoueur =" + idJoueur;
            List<string>[] items = bd.selection(equip);
            foreach (List<string> item in items)
            {
                Inventaire.Add(new Equipement(item));
            }
        }
        private void addScripts(int idJoueur)
        {
            string script = "SELECT idJoueur,contenu,nom FROM JoueursScripts j INNER JOIN Scripts s ON s.idScript = j.idScript WHERE idJoueur =" + idJoueur;
            List<string>[] scripts = bd.selection(script);
            foreach (List<string> src in scripts)
            {
                LstScript.Add(new Script(src));
            }

        }
        private void addPerso(int idJoueur)
        {
            string entit = "SELECT * FROM Entites WHERE idJoueur =" + idJoueur;
            List<string>[] entites = bd.selection(entit);
           /* foreach (List<string> ent in entites)
            {
               // LstEntite.Add(new Entite())
            }*/

        }

    }
}
