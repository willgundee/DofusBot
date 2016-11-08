using System;
using System.Collections.Generic;

namespace Gofus
{

    public class Sort
    {// TODO: changer les create de la bd pour add la table equipEntite ainsi que changer pomodifiable de sort en bool
        public List<Effet> LstEffets { get; set; }
        public Zone ZonePortee { get; set; }
        public Zone ZoneEffet { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public float Exprience { get; set; }
        public bool LigneDeVue { get; set; }
        public bool CelluleLibre { get; set; }
        public bool PorteeModifiable { get; set; }
        public int TauxDeRelance { get; set; }
        public int PointActionRequis { get; set; }
        public int NoImage { get; set; }

        private BDService bd = new BDService();
        /// <summary>
        /// Constructeur d'un sort
        /// </summary>
        /// <param name="Sorts"></param>
        public Sort(List<string> Sorts)
        {
            addEffet(Convert.ToInt32(Sorts[0]));
            addZonePo(Convert.ToInt32(Sorts[1]));
            addZoneEf(Convert.ToInt32(Sorts[2]));
            Nom = Sorts[3];
            Description = Sorts[4];
            Exprience = Convert.ToInt32(Sorts[5]);
            LigneDeVue = Convert.ToBoolean(Sorts[6]);
            CelluleLibre = Convert.ToBoolean(Sorts[7]);
            TauxDeRelance = Convert.ToInt32(Sorts[8]);
            PorteeModifiable = Convert.ToBoolean(Sorts[9]);
            PointActionRequis = Convert.ToInt32(Sorts[10]);
            NoImage = Convert.ToInt32(Sorts[11]);
        }
        /// <summary>
        /// Pour ajouter la zone de portée du sort
        /// </summary>
        /// <param name="idZone">  l'id de la zone</param>
        private void addZonePo(int idZone)
        {
            ZonePortee = new Zone(bd.selection("SELECT tz.nom,z.porteeMin,z.porteeMax FROM sorts s INNER JOIN Zones z ON z.idZone = s.idZonePortee INNER JOIN typesZones tz ON tz.idTypezone = z.idtypeZone WHERE idZonePortee = " + idZone)[0]);
        }
        /// <summary>
        /// Pour ajouter la zone d'effet du sort
        /// </summary>
        /// <param name="idZone">l'id de la zone</param>
        private void addZoneEf(int idZone)
        {
            ZoneEffet = new Zone(bd.selection("SELECT tz.nom,z.porteeMin,z.porteeMax FROM sorts s INNER JOIN Zones z ON z.idZone = s.idZoneEffet INNER JOIN typesZones tz ON tz.idTypezone = z.idtypeZone WHERE idZoneEffet = " + idZone)[0]);
        }
        //TODO : Pas encore pris en compte les envoutements
        /// <summary>
        /// Pour ajouter les effets du sorts
        /// </summary>
        /// <param name="idSort"> Le sort</param>
        private void addEffet(int idSort)
        {
            LstEffets = new List<Effet>();
            foreach (List<string> effet in bd.selection("SELECT e.nom,es.valeurMin,es.valeurMax FROM effetssorts es INNER JOIN Sorts s ON s.idSort = es.idSort INNER JOIN Effets e ON e.idEffet = es.idEffet WHERE s.idSort = "+idSort))
                LstEffets.Add(new Effet(effet));
        }
    }
}
