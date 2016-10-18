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

        public ObservableCollection<Statistique> LstStat { get; set; }
        public List<Equipement> LstEquipement { get; set; }
        public Script ScriptEntite { get; set; }
        public Classe ClasseEntite { get; set; }
        public string Nom { get; set; }
        public int CapitalLibre { get; set; }

        private BDService bd = new BDService();

        /// <summary>
        /// constructeur de base d'entite
        /// </summary>
        /// <param name="nom">nom de l,entite</param>
        /// <param name="script">scripte lié al'entite</param>
        /// <param name="ClasseE">Classe de l'entite</param>
        /// <param name="stats">de l'entite</param>
        public Entite(List<string> infoEntite)
        {
            addClasse(Convert.ToInt16(infoEntite[0]));
            addScript(Convert.ToInt16(infoEntite[0]));
            addStats(Convert.ToInt16(infoEntite[0]));
            Nom = infoEntite[4];
            CapitalLibre = Convert.ToInt32(infoEntite[5]);
        }
        private void addClasse(int idEntite)
        {
            ClasseEntite = new Classe(bd.selection("SELECT * FROM Classes c INNER JOIN Entites e ON c.idCLasse = e.idClasse WHERE e.idEntite =" + idEntite)[0]);
        }
        private void addScript(int idEntite)
        {
            ScriptEntite = new Script(bd.selection("SELECT * FROM Scripts s INNER JOIN Entites e ON e.idScript = s.idScript WHERE idEntite =" + idEntite)[0]);
        }
        private void addStats(int idEntite)
        {
            string sta = "SELECT t.nom,se.valeur FROM statistiquesentites se INNER JOIN Entites e ON e.idEntite = se.idEntite INNER JOIN TypesStatistiques t ON se.idTypeStatistique = t.idtypestatistique WHERE e.idEntite =" + idEntite;
            LstStat = new ObservableCollection<Statistique>();
            foreach (List<string> stat  in bd.selection(sta))
                LstStat.Add(new Statistique(stat[0], Convert.ToInt16(stat[1])));
        }
    }
}
