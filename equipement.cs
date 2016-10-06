using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Equipement
    {

        public Statistique[] TabStatistiques { get; set; }
        public string Nom { get; set; }
        public string Desc { get; set; }
        public int Prix { get; set; }
        public string NoImg { get; set; }
        public string Type { get; set; }
        public Dictionary<int, string> DictType = new Dictionary<int, string>()
        {
            {0, "Chapeau" },{1, "Cape"},{2, "Botte"},{3, "Ceinture"},{4, "Anneau"},{5, "Amulette"},{6, "Hache"},{7, "Pelle"},{8, "Baguette"},{9, "Épée"},{10, "Arc"},{11, "Bague"},{12, "Bâton"},{13, "Marteau"},{ 14, "Faux"}
        };
        //	4	14	12	13	7023	5000	Marteau du bouftou	Le manuel de l'utilisateur explique que ce surprenant marteau est capable de libérer une énergie
        public Equipement(List<string> item)
        {
            Type = DictType[Convert.ToInt32(item[1])];
            //2 idZone portee
            //3 idZone effet
            NoImg = item[4];
            Prix = Convert.ToInt32(item[5]);
            Nom = item[6];
            Desc = item[7];
        }
    }

}
