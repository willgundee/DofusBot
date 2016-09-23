namespace GofusSharp
{
    public class Zone
    {
        public enum type {cercle, ligne, carre, croix, T, X, demi_cercle, cone, tous}
        public type Type { get; internal set; }
        public int PorteeMin { get; internal set; }
        public int PorteeMax { get; internal set; }
        public Zone(type Type, int PorteeMin, int PorteeMax)
        {
            this.Type = Type;
            this.PorteeMin = PorteeMin;
            this.PorteeMax = PorteeMax;
        }
    }
}
