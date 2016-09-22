namespace GofusSharp
{
    public class Sort
    {
        public int IdSort { get; private set; }
        public Effet[] TabEffets { get; private set; }
        public string Nom { get; private set; }
        public bool LigneDeVue { get; private set; }
        public bool PorteeModifiable { get; private set; }
        public bool CelluleLibre { get; private set; }
        public Zone ZonePortee { get; private set; }
        public Zone ZoneEffet { get; private set; }
        public int TauxDeRelance { get; private set; } //3 = cooldown 3 tour // -3 = 3 lancer par tour max //0 infinie lancer
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
