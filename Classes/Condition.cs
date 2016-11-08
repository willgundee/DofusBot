using System.Collections.Generic;

namespace Gofus
{
    public class Condition
    {
        public Statistique Stat { get; set; }
        public string Signe { get; set; }

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
