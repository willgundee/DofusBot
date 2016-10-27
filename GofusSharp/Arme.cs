namespace GofusSharp
{
    public class Arme : Equipement
    {
        public enum typeArme { arc, baguette, baton, dague, faux, hache, marteau, outil, pelle, pioche, epee }
        public Effet[] TabEffets { get; internal set; }
        public Zone ZonePortee { get; internal set; }
        public Zone ZoneEffet { get; internal set; }
        public typeArme TypeArme { get; internal set; }
        public int CoutPA { get; set; }
        internal Arme(int IdEquipement, Statistique[] TabStatistiques, string Nom, type Type, Effet[] TabEffets, Zone ZonePortee, Zone ZoneEffet, typeArme TypeArme, int CoutPA) : base(IdEquipement, TabStatistiques, Nom, Type)
        {
            this.TabEffets = TabEffets;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
            this.TypeArme = TypeArme;
            this.CoutPA = CoutPA;
        }
    }
}
