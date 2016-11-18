using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gofus
{
    public class Condition
    {
        public Statistique Stat { get; set; }
        public string Signe { get; set; }


        [JsonConstructor]
        public Condition(Statistique Stat, string Signe)
        {
            this.Stat = Stat;
            this.Signe = Signe;
        }

        /// <summary>
        /// Constructeur d'une conditions
        /// </summary>
        /// <param name="info">La requête</param>
        public Condition(List<string> info )
        {
            Stat = new Statistique(info,false);
            Signe = info[2];
        }
    }
}
