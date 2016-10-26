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
        public ObservableCollection<Equipement> LstEquipements { get; set; }
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
            addListStatsAllEquipement();
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
            foreach (List<string> stat in bd.selection(sta))
                LstStats.Add(new Statistique(stat));
        }
        /// <summary>
        /// ajout des équipements portée par l'entité
        /// </summary>
        /// <param name="idEntite">l'entité</param>
        private void addEquipements(int idEntite)
        {
            LstEquipements = new ObservableCollection<Equipement>();
            string equip = "SELECT * FROM equipements e INNER JOIN equipementsentites ee ON ee.idEquipement = e.idEquipement WHERE ee.idEntite =" + idEntite;
            List<string>[] items = bd.selection(equip);
            if (items[0][0] != "rien")
                foreach (List<string> item in items)
                    LstEquipements.Add(new Equipement(item, true, 0));
        }

        public void addListStatsAllEquipement()
        {
            List<Statistique> lstStatsItems = new List<Statistique>();
            foreach (Equipement item in LstEquipements)
                foreach (Statistique stat in item.LstStatistiques)
                    lstStatsItems.Add(stat);
            foreach (Statistique stats in lstStatsItems)
                switch (stats.Nom)
                {
                    case Statistique.element.vie:
                        LstStats.First(x => x.Nom == Statistique.element.vie).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.force:
                        LstStats.First(x => x.Nom == Statistique.element.force).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.intelligence:
                        LstStats.First(x => x.Nom == Statistique.element.intelligence).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.chance:
                        LstStats.First(x => x.Nom == Statistique.element.chance).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.agilite:
                        LstStats.First(x => x.Nom == Statistique.element.agilite).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.vitalite:
                        LstStats.First(x => x.Nom == Statistique.element.vie).Valeur += stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.vitalite).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.sagesse:
                        LstStats.First(x => x.Nom == Statistique.element.sagesse).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.PA:
                        LstStats.First(x => x.Nom == Statistique.element.PA).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.PM:
                        LstStats.First(x => x.Nom == Statistique.element.PM).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.portee:
                        LstStats.First(x => x.Nom == Statistique.element.portee).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.invocation:
                        LstStats.First(x => x.Nom == Statistique.element.invocation).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.initiative:
                        LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.fuite:
                        LstStats.First(x => x.Nom == Statistique.element.fuite).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_neutre:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_neutre).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_feu:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_feu).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_air:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_air).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_terre:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_terre).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_eau:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_eau).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_poussee:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_poussee).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.DMG_piege:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_piege).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_neutre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_neutre).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_feu:
                        LstStats.First(x => x.Nom == Statistique.element.RES_feu).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_air:
                        LstStats.First(x => x.Nom == Statistique.element.RES_air).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_terre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_terre).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_eau:
                        LstStats.First(x => x.Nom == Statistique.element.RES_eau).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_poussee:
                        LstStats.First(x => x.Nom == Statistique.element.RES_poussee).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_neutre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_neutre).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_feu:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_feu).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_air:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_air).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_terre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_terre).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_eau:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_eau).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.retrait_PA:
                        LstStats.First(x => x.Nom == Statistique.element.retrait_PA).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.retrait_PM:
                        LstStats.First(x => x.Nom == Statistique.element.retrait_PM).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.esquive_PA:
                        LstStats.First(x => x.Nom == Statistique.element.esquive_PA).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.esquive_PM:
                        LstStats.First(x => x.Nom == Statistique.element.esquive_PM).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.soin:
                        LstStats.First(x => x.Nom == Statistique.element.soin).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.renvoie_DMG:
                        LstStats.First(x => x.Nom == Statistique.element.renvoie_DMG).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.puissance:
                        LstStats.First(x => x.Nom == Statistique.element.puissance).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.puissance_piege:
                        LstStats.First(x => x.Nom == Statistique.element.puissance_piege).Valeur += stats.Valeur;
                        break;
                }
        }
    }
}
