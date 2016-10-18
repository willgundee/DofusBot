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

        public ObservableCollection<Statistique> ListeStat { get; set; }
        public Script ScriptEntite { get; set; }
        public Classe ClasseEntite { get; set; }
        public string Nom { get; set; }
        public int CapitalLibre { get; set; }
        public List<Equipement> ListeEquipement { get; set; }
        /// <summary>
        /// constructeur de base d'entite
        /// </summary>
        /// <param name="nom">nom de l,entite</param>
        /// <param name="script">scripte lié al'entite</param>
        /// <param name="ClasseE">Classe de l'entite</param>
        /// <param name="stats">de l'entite</param>
        public Entite(List<string> nom, List<string> script, List<string> ClasseE, List<string>[] stats)
        {
            Nom = nom[4];
            CapitalLibre = Convert.ToInt32(nom[5]);

            ScriptEntite = new Script(script);

          //  ClasseEntite = new Classe(ClasseE);

            ListeStat = new ObservableCollection<Statistique>();
            foreach (List<String> stat in stats )           
                ListeStat.Add(new Statistique(stat[0], Convert.ToInt32(stat[1])));
            
               

        }

       


    }
}
