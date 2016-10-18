using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Condition
    {
        public Statistique Stat { get; set; }
        public string Signe { get; set; }

        public Condition(List<string> info )
        {
            Stat = new Statistique(info[8], Convert.ToInt32(info[6]));
            Signe = info[2];
        }
    }
}
