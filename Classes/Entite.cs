using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gofus
{

    public class Entite
    {
        public ObservableCollection<Statistique> LstStats { get; set; }
        public ObservableCollection<Equipement> LstEquipements { get; set; }
        public Script ScriptEntite { get; set; }
        public Classe ClasseEntite { get; set; }
        public string Nom { get; set; }
        public int CapitalLibre { get; set; }
        public int Niveau { get; set; }
        public int IdEntite { get; set; }
        public bool EstPersonnage { get; set; }

        private BDService bd = new BDService();

        /// <summary>
        /// Constructeur d'un entité
        /// </summary>
        /// <param name="infoEntite">La requête</param>
        public Entite(List<string> infoEntite)
        {
            IdEntite = Convert.ToInt16(infoEntite[0]);
            addClasse(Convert.ToInt16(infoEntite[0]));
            addScript(Convert.ToInt16(infoEntite[0]));
            addStats(Convert.ToInt16(infoEntite[0]));
            addEquipements(Convert.ToInt16(infoEntite[0]));
            Nom = infoEntite[4];
            Niveau = LstStats.First(x => x.Nom == Statistique.element.experience).toLevel();
            EstPersonnage = true;
            if (infoEntite[3] == "")
            {
                BalanceStatsMob();
                EstPersonnage = false;
            }
            else
            CapitalLibre = Convert.ToInt32(infoEntite[5]);


            //addListStatsAllEquipement();
        }

        private void BalanceStatsMob()
        {
            //Round aussi Valeur = Convert.ToDouble(((max-min)/diff*lvl)+min);
            double lvlMin = LstStats[0].toLevel(LstStats.First(x => x.Nom == Statistique.element.experience).ValeurMin);
            double lvlMax = LstStats[0].toLevel(LstStats.First(x => x.Nom == Statistique.element.experience).ValeurMax);
            double diff = lvlMax - lvlMin;


            foreach (Statistique stats in LstStats)
                stats.Valeur = Convert.ToDouble(Math.Round(((stats.ValeurMax - stats.ValeurMin) / diff) * (Niveau - lvlMin)) + stats.ValeurMin);

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
            string sta = "SELECT * FROM statistiquesentites se INNER JOIN Entites e ON e.idEntite = se.idEntite INNER JOIN TypesStatistiques t ON se.idTypeStatistique = t.idtypestatistique WHERE e.idEntite =" + idEntite;
            foreach (List<string> stat in bd.selection(sta))
                LstStats.Add(new Statistique(stat, true));
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
                    ajouterEquipement(new Equipement(item, true, 0));

        }
        public bool peutEquiper(Equipement equip)
        {
            Statistique statItem = null;
            Statistique statActuel = null;
            Condition condItem = null;
            bool possible = false;
            StringBuilder requis = new StringBuilder();
            foreach (Condition cond in equip.LstConditions)
            {
                switch (cond.Stat.Nom)
                {
                    case Statistique.element.experience:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.experience);
                        break;
                    case Statistique.element.vie:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.vie);
                        break;
                    case Statistique.element.force:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.force);
                        break;
                    case Statistique.element.agilite:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.agilite);
                        break;
                    case Statistique.element.intelligence:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.intelligence);
                        break;
                    case Statistique.element.chance:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.chance);
                        break;
                    case Statistique.element.vitalite:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.vitalite);
                        break;
                    case Statistique.element.sagesse:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.sagesse);
                        break;
                    case Statistique.element.PA:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.PA);
                        break;
                    case Statistique.element.PM:
                        condItem = cond;
                        statItem = equip.LstStatistiques.FirstOrDefault(x => x.Nom == Statistique.element.PM);
                        break;
                }

                switch (condItem.Signe)
                {
                    case "<":
                        statActuel = LstStats.First(x => x.Nom == condItem.Stat.Nom);
                        if (statActuel.Valeur + (statItem != null ? statItem.Valeur : 0) < condItem.Stat.Valeur)
                            possible = true;
                        else
                            requis.Append("Vous avez : " + statActuel.Valeur.ToString() + " " + statActuel.NomSimple + " et il vous faut moins de " + condItem.Stat.Valeur.ToString() + " " + condItem.Stat.NomSimple + Environment.NewLine);
                        break;
                    case "<=":
                        statActuel = LstStats.First(x => x.Nom == condItem.Stat.Nom);
                        if (statActuel.Valeur + (statItem != null ? statItem.Valeur : 0) <= condItem.Stat.Valeur)
                            possible = true;
                        else
                            requis.Append("Vous avez : " + statActuel.Valeur.ToString() + " " + statActuel.NomSimple + " et il vous faut au plus " + condItem.Stat.Valeur.ToString() + " " + condItem.Stat.NomSimple + Environment.NewLine);
                        break;
                    case ">":
                        statActuel = LstStats.First(x => x.Nom == condItem.Stat.Nom);
                        if (statActuel.Valeur + (statItem != null ? statItem.Valeur : 0) > condItem.Stat.Valeur)
                            possible = true;
                        else
                            requis.Append("Vous avez : " + statActuel.Valeur.ToString() + " " + statActuel.NomSimple + " et il vous faut plus de " + condItem.Stat.Valeur.ToString() + " " + condItem.Stat.NomSimple + Environment.NewLine);
                        break;
                    case ">=":
                        statActuel = LstStats.First(x => x.Nom == condItem.Stat.Nom);
                        if (statActuel.Valeur + (statItem != null ? statItem.Valeur : 0) >= condItem.Stat.Valeur)
                            possible = true;
                        else
                            if (condItem.Stat.Nom == Statistique.element.experience)
                            requis.Append("Vous êtes niveau : " + statActuel.toLevel().ToString() + " et vous devez être au moins niveau : " + condItem.Stat.toLevel().ToString() + Environment.NewLine);
                        else
                            requis.Append("Vous avez : " + statActuel.Valeur.ToString() + " " + statActuel.NomSimple + " et il vous faut au moins " + condItem.Stat.Valeur.ToString() + " " + condItem.Stat.NomSimple + Environment.NewLine);
                        break;
                    case "=":
                        statActuel = LstStats.First(x => x.Nom == condItem.Stat.Nom);
                        if (statActuel.Valeur + (statItem != null ? statItem.Valeur : 0) == condItem.Stat.Valeur)
                            possible = true;
                        else
                            requis.Append("Vous avez : " + statActuel.Valeur.ToString() + " " + statActuel.NomSimple + " et il vous faut exactement " + condItem.Stat.Valeur.ToString() + " " + condItem.Stat.NomSimple + Environment.NewLine);
                        break;
                }
            }
            if (!possible)
                System.Windows.Forms.MessageBox.Show(requis.ToString());
            return possible;
        }
        public void ajouterEquipement(Equipement aAjouter)
        {
            LstEquipements.Add(aAjouter);
            ajoutStats(aAjouter);
        }
        public void enleverItem(Equipement aEnlever)
        {
            LstEquipements.Remove(aEnlever);
            enleveStats(aEnlever);
        }
        private void enleveStats(Equipement item)
        {
            foreach (Statistique stats in item.LstStatistiques)
                switch (stats.Nom)
                {
                    case Statistique.element.vie:
                        LstStats.First(x => x.Nom == Statistique.element.vie).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.force:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur -= stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.force).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.intelligence:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur -= stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.intelligence).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.chance:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur -= stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.chance).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.agilite:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur -= stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.agilite).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.vitalite:
                        LstStats.First(x => x.Nom == Statistique.element.vie).Valeur -= stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.vitalite).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.sagesse:
                        LstStats.First(x => x.Nom == Statistique.element.sagesse).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.PA:
                        LstStats.First(x => x.Nom == Statistique.element.PA).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.PM:
                        LstStats.First(x => x.Nom == Statistique.element.PM).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.portee:
                        LstStats.First(x => x.Nom == Statistique.element.portee).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.invocation:
                        LstStats.First(x => x.Nom == Statistique.element.invocation).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.initiative:
                        LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.fuite:
                        LstStats.First(x => x.Nom == Statistique.element.fuite).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_neutre:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_neutre).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_feu:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_feu).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_air:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_air).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_terre:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_terre).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_eau:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_eau).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_poussee:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_poussee).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.DMG_piege:
                        LstStats.First(x => x.Nom == Statistique.element.DMG_piege).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_neutre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_neutre).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_feu:
                        LstStats.First(x => x.Nom == Statistique.element.RES_feu).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_air:
                        LstStats.First(x => x.Nom == Statistique.element.RES_air).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_terre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_terre).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_eau:
                        LstStats.First(x => x.Nom == Statistique.element.RES_eau).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_poussee:
                        LstStats.First(x => x.Nom == Statistique.element.RES_poussee).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_neutre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_neutre).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_feu:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_feu).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_air:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_air).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_terre:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_terre).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.RES_Pourcent_eau:
                        LstStats.First(x => x.Nom == Statistique.element.RES_Pourcent_eau).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.retrait_PA:
                        LstStats.First(x => x.Nom == Statistique.element.retrait_PA).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.retrait_PM:
                        LstStats.First(x => x.Nom == Statistique.element.retrait_PM).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.esquive_PA:
                        LstStats.First(x => x.Nom == Statistique.element.esquive_PA).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.esquive_PM:
                        LstStats.First(x => x.Nom == Statistique.element.esquive_PM).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.soin:
                        LstStats.First(x => x.Nom == Statistique.element.soin).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.renvoie_DMG:
                        LstStats.First(x => x.Nom == Statistique.element.renvoie_DMG).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.puissance:
                        LstStats.First(x => x.Nom == Statistique.element.puissance).Valeur -= stats.Valeur;
                        break;
                    case Statistique.element.puissance_piege:
                        LstStats.First(x => x.Nom == Statistique.element.puissance_piege).Valeur -= stats.Valeur;
                        break;
                }
        }

        private void ajoutStats(Equipement item)
        {
            foreach (Statistique stats in item.LstStatistiques)
                switch (stats.Nom)
                {
                    case Statistique.element.vie:
                        LstStats.First(x => x.Nom == Statistique.element.vie).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.force:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur += stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.force).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.intelligence:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur += stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.intelligence).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.chance:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur += stats.Valeur;
                        LstStats.First(x => x.Nom == Statistique.element.chance).Valeur += stats.Valeur;
                        break;
                    case Statistique.element.agilite:
                        if (stats.Valeur > 0)
                            LstStats.First(x => x.Nom == Statistique.element.initiative).Valeur += stats.Valeur;
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
