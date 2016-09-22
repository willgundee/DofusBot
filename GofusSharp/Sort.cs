using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Sort
    {
        private int IdSort { get; }
        private Effet[] TabEffets { get; }
        private string Nom { get; }
        private bool LigneDeVue { get; }
        private bool PorteeModifiable { get;  }
        private bool CelluleLibre { get; }
        private Zone ZonePortee { get; }
        private Zone ZoneEffet { get; }
        private int TauxDeRelance { get; } //3 = cooldown 3 tour // -3 = 3 lancer par tour max //0 infinie lancer
        public Sort(int IdSort, Effet[] TabEffets, string Nom, bool LigneDeVue, bool PorteeModifiable, bool CelluleLibre, Zone ZonePortee, Zone ZoneEffet, int TauxDeRelance)
        {
            this.IdSort = IdSort;
            this.TabEffets = TabEffets;
            this.Nom = Nom;
            this.LigneDeVue = LigneDeVue;
            this.PorteeModifiable = PorteeModifiable;
            this.CelluleLibre = CelluleLibre;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
            this.TauxDeRelance = TauxDeRelance;
        }
    }
}
