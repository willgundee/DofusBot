using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Joueur
    {
        public ObservableCollection<Equipement> Inventaire { get; set; }
        public ObservableCollection<Script> LstScripts { get; set; }
        public ObservableCollection<Entite> LstEntites { get; set; }
        public string NomUtilisateur { get; set; }
        public string Courriel { get; set; }
        public int Kamas { get; set; }
        public int Avatar { get; set; }

        private BDService bd = new BDService();

        /// <summary>
        /// Constructeur d'un joueur
        /// </summary>
        /// <param name="infoUser">La requête</param>
        public Joueur(List<string> infoUser)
        {
            NomUtilisateur = infoUser[1];
            Courriel = infoUser[2];
            Kamas = Convert.ToInt32(infoUser[4]);
            Avatar = Convert.ToInt32(infoUser[5]);

            addInventaire(Convert.ToInt32(infoUser[0]));
            addScripts(Convert.ToInt32(infoUser[0]));
            addInventaire(Convert.ToInt32(infoUser[0]));
            addPerso(Convert.ToInt32(infoUser[0]));


        }
        /// <summary>
        /// Ajouter les équipements que le joueurs possèdes
        /// </summary>
        /// <param name="idJoueur">l'id du joueur</param>
        private void addInventaire(int idJoueur)
        {
            Inventaire = new ObservableCollection<Equipement>();
            string equip = "SELECT * FROM equipements e INNER JOIN joueursequipements je ON e.idEquipement = je.idequipement WHERE idJoueur =" + idJoueur;
            List<string>[] items = bd.selection(equip);
            if (items[0][0] != "rien")
                foreach (List<string> item in items)
                    Inventaire.Add(new Equipement(item,true,idJoueur));
        }
        /// <summary>
        /// Pour ajouter les scripts du joueur
        /// </summary>
        /// <param name="idJoueur">Le joueur</param>
        private void addScripts(int idJoueur)
        {
            LstScripts = new ObservableCollection<Script>();
            string script = "SELECT idJoueur,contenu,nom,uuid FROM JoueursScripts j INNER JOIN Scripts s ON s.idScript = j.idScript WHERE idJoueur =" + idJoueur;
            List<string>[] scripts = bd.selection(script);
            if (scripts[0][0] != "rien")//TODO: enlever ensuite
                foreach (List<string> src in scripts)
                    LstScripts.Add(new Script(src));
        }
        /// <summary>
        /// Pour ajouter les personnages du joueur
        /// </summary>
        /// <param name="idJoueur">Le joueur</param>
        public void addPerso(int idJoueur)
        {
            LstEntites = new ObservableCollection<Entite>();
            string entit = "SELECT * FROM entites e INNER JOIN Joueurs j ON j.idJoueur = e.idJoueur WHERE j.idJoueur =" + idJoueur;
            List<string>[] entites = bd.selection(entit);
            if (entites[0][0] != "rien")
                foreach (List<string> ent in entites)
                    LstEntites.Add(new Entite(ent));
        }

    }
}
