using System;

namespace Gofus
{
    public class Partie
    {
        //public int seed { get; set; }



        public string Attaquant { get; set; }
        public string Defendant { get; set; }

        public string Date { get; set; }

        public DateTime trueDate { get; set; }

        public string Gagnant { get; set; }


        private int seed { get; set; }
        public Partie(string a, string d, string dt, int sd, string ga)
        {
            Attaquant = a;
            Defendant = d;
            seed = sd;
            Date = dt;
            trueDate = new DateTime();
            trueDate = DateTime.ParseExact(dt, "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture);
           
            Gagnant = ga;
        }



    }
}
