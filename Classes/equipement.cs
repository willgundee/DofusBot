using System;
using System.Collections.Generic;

namespace Gofus
{
    public class Equipement
    {
        public List<Statistique> LstStatistiques { get; set; }
        public List<Condition> LstConditions { get; set; }
        public List<Effet> LstEffets { get; set; }
        public Zone ZonePortee { get; set; }
        public Zone ZoneEffet { get; set; }
        public string Nom { get; set; }
        public string Desc { get; set; }
        public int Prix { get; set; }
        public string NoImg { get; set; }
        public string Type { get; set; }
        public bool EstArme { get; set; }
        public int Quantite { get; set; }
        public int QuantiteEquipe { get; set; }
        public int Pa { get; set; }

        private BDService bd = new BDService();


        private Dictionary<int, string> DictType = new Dictionary<int, string>()
        {
            {1, "Chapeau" },{2, "Cape"},{3, "Botte"},{4, "Ceinture"},{5, "Anneau"},{6, "Amulette"},{7, "Hache"},{8, "Pelle"},{9, "Baguette"},{10, "Épée"},{11, "Arc"},{12, "Dague"},{13, "Bâton"},{14, "Marteau"},{ 15, "Faux"}
        };
        /// <summary>
        /// constructeur d'un équipement ou arme
        /// </summary>
        /// <param name="item">La requête</param>
        /// <param name="complet">si tu a besoin des stats/condition etc</param>
        /// <param name="idFacultatifJoueur">0 si pas besoin, pour link l'inventaire du joueur lui add des quantité</param>
        public Equipement(List<string> item, bool complet, int idFacultatifJoueur)
        {

            if (item[2] == "")// si l'idZone est null soit vide c'est une arme
                EstArme = false;
            else//donc je lui ajoute ses infos
            {
                EstArme = true;
                if (complet)
                {
                    addEffets(Convert.ToInt32(item[0]));
                    addZoneEf(Convert.ToInt32(item[3]));
                    addZonePo(Convert.ToInt32(item[2]));
                    Pa = Convert.ToInt32(item[8]);
                }
            }
            Type = DictType[Convert.ToInt32(item[1])];
            NoImg = item[4];
            Prix = Convert.ToInt32(item[5]);
            Nom = item[6];
            Desc = item[7];
            if (complet)
            {
                addStats(Convert.ToInt32(item[0]));
                addConditions(Convert.ToInt32(item[0]));
            }
            if (idFacultatifJoueur != 0)
                addQuantite(Convert.ToInt32(item[0]), idFacultatifJoueur);
        }
        private void addQuantite(int idEquipement, int idJoueur)
        {
            List<string>[] rep = bd.selection("SELECT quantite,quantiteEquipe FROM JoueursEquipements WHERE idJoueur = " + idJoueur.ToString() + " AND idEquipement = " + idEquipement.ToString());
            Quantite = Convert.ToInt32(rep[0][0]);
            QuantiteEquipe = Convert.ToInt32(rep[0][1]);
        }
        /// <summary>
        /// ajout des statistiques des items
        /// </summary>
        /// <param name="idEquipement">l'item</param>
        private void addStats(int idEquipement)
        {
            LstStatistiques = new List<Statistique>();
            string sta = "SELECT t.nom,se.valeur FROM StatistiquesEquipements se INNER JOIN Equipements e ON e.idEquipement = se.idEquipement INNER JOIN TypesStatistiques t ON se.idTypeStatistique = t.idtypestatistique WHERE e.idEquipement =" + idEquipement;
            foreach (List<string> stat in bd.selection(sta))
                LstStatistiques.Add(new Statistique(stat,false));
        }
        /// <summary>
        /// ajout de la zone de portée de l'arme
        /// </summary>
        /// <param name="idZone">la zone</param>
        private void addZonePo(int idZone)
        {
            ZonePortee = new Zone(bd.selection("SELECT tz.nom,z.porteeMin,z.porteeMax FROM Equipements e INNER JOIN Zones z ON z.idZone = e.idZonePorte INNER JOIN typesZones tz ON tz.idTypezone = z.idtypeZone WHERE e.idZonePorte =" + idZone)[0]);
        }
        /// <summary>
        /// ajout de la zone d'effet
        /// </summary>
        /// <param name="idZone">la zone </param>
        private void addZoneEf(int idZone)
        {
            ZoneEffet = new Zone(bd.selection("SELECT tz.nom,z.porteeMin,z.porteeMax FROM Equipements e INNER JOIN Zones z ON z.idZone = e.idZoneEffet INNER JOIN typesZones tz ON tz.idTypezone = z.idtypeZone WHERE e.idZoneEffet=" + idZone)[0]);
        }
        /// <summary>
        /// ajout des effets d'un arme
        /// </summary>
        /// <param name="idEquipement">l'arme</param>
        private void addEffets(int idEquipement)
        {
            LstEffets = new List<Effet>();
            foreach (List<string> effet in bd.selection("SELECT ef.nom,ee.valeurMin,ee.valeurMax,ee.nbTour,ee.idTypeStatistique FROM EffetsEquipements ee INNER JOIN Equipements e ON e.idEquipement = ee.idEquipement INNER JOIN Effets ef ON ef.idEffet = ee.idEffet WHERE e.idEquipement =" + idEquipement))
                LstEffets.Add(new Effet(effet));
        }
        /// <summary>
        /// ajout des conditions des items
        /// </summary>
        /// <param name="idItem">l'item</param>
        private void addConditions(int idItem)
        {
            LstConditions = new List<Condition>();
            string cond = "SELECT t.nom,ce.valeur,c.signe FROM Conditions c INNER JOIN ConditionsEquipements ce On ce.idCondition = c.idCondition INNER JOIN TypesStatistiques t ON t.idTypeStatistique = c.idTypeStatistique WHERE idEquipement =" + idItem;
            List<string>[] conditions = bd.selection(cond);
            foreach (List<string> condition in conditions)
                LstConditions.Add(new Condition(condition));
        }
    }

}
