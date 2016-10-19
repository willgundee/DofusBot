using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    
    public class Entite
    {

        public ObservableCollection<Statistique> LstStats { get; set; }
        public List<Equipement> LstEquipements { get; set; }
        public Script ScriptEntite { get; set; }
        public Classe ClasseEntite { get; set; }
        public string Nom { get; set; }
        public int CapitalLibre { get; set; }

        private BDService bd = new BDService();
        
        /// <summary>
        /// Constructeur d'un entité
        /// </summary>
        /// <param name="infoEntite">La requête</param>
        public Entite(List<string> infoEntite)
        {
            addClasse(Convert.ToInt16(infoEntite[0]));
            addScript(Convert.ToInt16(infoEntite[0]));
            addStats(Convert.ToInt16(infoEntite[0]));
            addEquipements(Convert.ToInt16(infoEntite[0]));
            Nom = infoEntite[4];
            CapitalLibre = Convert.ToInt32(infoEntite[5]);
        }
        /// <summary>
        /// ajout de la classe de l'entité
        /// </summary>
        /// <param name="idEntite">l'entité</param>
        private void addClasse(int idEntite)
        {
            ClasseEntite = new Classe(bd.selection("SELECT * FROM Classes c INNER JOIN Entites e ON c.idCLasse = e.idClasse WHERE e.idEntite =" + idEntite)[0]);
        }
        /// <summary>
        /// ajout du script que l'entité utilise
        /// </summary>
        /// <param name="idEntite">l'entité</param>
        private void addScript(int idEntite)
        {
            ScriptEntite = new Script(bd.selection("SELECT * FROM Scripts s INNER JOIN Entites e ON e.idScript = s.idScript WHERE idEntite =" + idEntite)[0]);
        }
        /// <summary>
        /// ajout des stats de l'entités
        /// </summary>
        /// <param name="idEntite">l'entité</param>
        private void addStats(int idEntite)
        {
            LstStats = new ObservableCollection<Statistique>();
            string sta = "SELECT t.nom,se.valeur FROM statistiquesentites se INNER JOIN Entites e ON e.idEntite = se.idEntite INNER JOIN TypesStatistiques t ON se.idTypeStatistique = t.idtypestatistique WHERE e.idEntite =" + idEntite;
            foreach (List<string> stat  in bd.selection(sta))
                LstStats.Add(new Statistique(stat));
        }
        /// <summary>
        /// ajout des équipements portée par l'entité
        /// </summary>
        /// <param name="idEntite">l'entité</param>
        private void addEquipements(int idEntite)
        {
            LstEquipements = new List<Equipement>();
            string equip = "SELECT * FROM equipements e INNER JOIN equipementsentites ee ON ee.idEquipement = e.idEquipement WHERE ee.idEntite =" + idEntite;
            List<string>[] items = bd.selection(equip);
            foreach (List<string> item in items)
                LstEquipements.Add(new Equipement(item));
        }
    }
}
