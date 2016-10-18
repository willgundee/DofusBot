using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{

    public class Sort
    {
        public List<Effet> LstEffet { get; set; }
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

        private BDService bd = new BDService();




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
            PorteeModifiable = Convert.ToBoolean(Sorts[8]);
            TauxDeRelance = Convert.ToInt32(Sorts[9]);
            PointActionRequis = Convert.ToInt32(Sorts[10]);
        }
        private void addZonePo(int idZone)
        {
            ZonePortee = new Zone(bd.selection("SELECT tz.nom,z.porteeMin,z.porteeMax FROM sorts s INNER JOIN Zones z ON z.idZone = s.idZonePortee INNER JOIN typesZones tz ON tz.idTypezone = z.idtypeZone WHERE idZonePortee = " + idZone)[0]);
        }
        private void addZoneEf(int idZone)
        {
            ZoneEffet = new Zone(bd.selection("SELECT tz.nom,z.porteeMin,z.porteeMax FROM sorts s INNER JOIN Zones z ON z.idZone = s.idZoneEffet INNER JOIN typesZones tz ON tz.idTypezone = z.idtypeZone WHERE idZoneEffet = " + idZone)[0]);
        }

        private void addEffet(int idSort)
        {
            foreach (List<string> effet in bd.selection("SELECT e.nom,es.valeurMin,es.valeurMax FROM effetssorts es INNER JOIN Sorts s ON s.idSort = es.idSort INNER JOIN Effets e ON e.idEffet = es.idEffet WHERE s.idSort = "+idSort))
                LstEffet.Add(new Effet(effet));
        }
    }
}
