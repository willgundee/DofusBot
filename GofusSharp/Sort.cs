namespace GofusSharp
{
    public class Sort
    {
        public int IdSort { get; internal set; }
        public Effet[] TabEffets { get; internal set; }
        public string Nom { get; internal set; }
        public bool LigneDeVue { get; internal set; }
        public bool PorteeModifiable { get; internal set; }
        public bool CelluleLibre { get; internal set; }
        public Zone ZonePortee { get; internal set; }
        public Zone ZoneEffet { get; internal set; }
        public int TauxDeRelance { get; internal set; } //3 = cooldown 3 tour // -3 = 3 lancer par tour max //0 infinie lancer
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
