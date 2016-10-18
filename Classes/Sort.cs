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
      



        public Sort(List<string> Sorts ,List<string>[] effets, List<string> zonePorte, List<string> zoneEffet)
        {
           
            Nom = Sorts[3];
            Description = Sorts[4];
            Exprience = Convert.ToInt32(Sorts[5]);
            LigneDeVue = Convert.ToBoolean(Sorts[6]);
            CelluleLibre= Convert.ToBoolean(Sorts[7]);
            PorteeModifiable= Convert.ToBoolean(Sorts[8]);
            TauxDeRelance = Convert.ToInt32(Sorts[9]);
            PointActionRequis = Convert.ToInt32(Sorts[10]);

            ZonePortee = new Zone(zonePorte);
            ZoneEffet = new Zone(zoneEffet);
            LstEffet = new List<Effet>();
            foreach (List<string> effet in effets)
                LstEffet.Add(new Effet(effet));

        }
    }
}
