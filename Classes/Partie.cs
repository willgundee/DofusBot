using System;

namespace Gofus
{

    /// <summary>
    /// Classe pour les parties qui seront affichés dans la datagrid.
    /// </summary>
    public class Partie
    {
        //public int seed { get; set; }


        public string Attaquant { get; set; }
        public string Defendant { get; set; }

        public string Date { get; set; }

        // Date en format DateTime.
        public DateTime trueDate { get; set; }

        public string Gagnant { get; set; }


        public int seed { get; set; }
        public int IdPartie { get; set; }

        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        /// <param name="a">Le nom de l'attaquant</param>
        /// <param name="d"> Le nom du défendant</param>
        /// <param name="dt">La date de la partie</param>
        /// <param name="sd">la seed du random</param>
        /// <param name="ga"> Le nom du gagnant</param>
        /// <param name="id">L'id de la partie.</param>
        public Partie(string a, string d, string dt, int sd, string ga, int id)
        {
            Attaquant = a;
            Defendant = d;
            seed = sd;
            Date = dt;
            trueDate = new DateTime();
            trueDate = DateTime.ParseExact(dt, "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
           
            Gagnant = ga;
            IdPartie = id;
        }



    }
}
