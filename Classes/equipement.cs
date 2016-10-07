﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Equipement
    {

        public List<Statistique> LstStatistiques { get; set; }
        public List<Effet> LstEffet { get; set; }
        public Zone ZonePortee { get; set; }
        public Zone ZoneEffet { get; set; }
        public string Nom { get; set; }
        public string Desc { get; set; }
        public int Prix { get; set; }
        public string NoImg { get; set; }
        public string Type { get; set; }

        public Dictionary<int, string> DictType = new Dictionary<int, string>()
        {
            {1, "Chapeau" },{2, "Cape"},{3, "Botte"},{4, "Ceinture"},{5, "Anneau"},{6, "Amulette"},{7, "Hache"},{8, "Pelle"},{9, "Baguette"},{10, "Épée"},{11, "Arc"},{12, "Bague"},{13, "Bâton"},{14, "Marteau"},{ 15, "Faux"}
        };
        /// <summary>
        /// constructeur d'un item de base
        /// </summary>
        /// <param name="item">ses infos</param>
        public Equipement(List<string> item)
        {
            Type = DictType[Convert.ToInt32(item[1])];
            NoImg = item[4];
            Prix = Convert.ToInt32(item[5]);
            Nom = item[6];
            Desc = item[7];
        }
        /// <summary>
        /// Constructeur d'un équipement
        /// </summary>
        /// <param name="item"> requete des info de l'equipement</param>
        /// <param name="stats">ces stats</param>
        public Equipement(List<string> item, List<string>[] stats)
        {
            Type = DictType[Convert.ToInt32(item[1])];
            NoImg = item[4];
            Prix = Convert.ToInt32(item[5]);
            Nom = item[6];
            Desc = item[7];
            LstStatistiques = new List<Statistique>();
            foreach (List<string> stat in stats)
                LstStatistiques.Add(new Statistique(stat[0], Convert.ToInt32(stat[1])));

        }
        /// <summary>
        /// constructeur d'une arme
        /// </summary>
        /// <param name="item">les info de l'arme</param>
        /// <param name="stats">ses stats</param>
        /// <param name="zonePorte">sa zone de portee</param>
        /// <param name="zoneEffet">sa zone d'effet</param>
        /// <param name="effets">ses effets</param>
        public Equipement(List<string> item, List<string>[] stats, List<string> zonePorte, List<string> zoneEffet, List<string>[] effets)
        {
            Type = DictType[Convert.ToInt32(item[1])];
            NoImg = item[4];
            Prix = Convert.ToInt32(item[5]);
            Nom = item[6];
            Desc = item[7];
            LstStatistiques = new List<Statistique>();
            foreach (List<string> stat in stats)
                LstStatistiques.Add(new Statistique(stat[0], Convert.ToInt32(stat[1])));
            ZonePortee = new Zone(zonePorte);
            ZoneEffet = new Zone(zoneEffet);
            LstEffet = new List<Effet>();
            foreach (List<string> effet in effets)
                LstEffet.Add( new Effet(effet));



        }

    }

}