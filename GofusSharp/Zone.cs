namespace GofusSharp
{
    public class Zone
    {
        public enum type {cercle, ligne, carre, croix, T, X, demi_cercle, cone, tous}
        public type Type { get; private set; }
        public int PorteeMin { get; private set; }
        public int PorteeMax { get; private set; }
        public Zone(type Type, int PorteeMin, int PorteeMax)
        {
            this.Type = Type;
            this.PorteeMin = PorteeMin;
            this.PorteeMax = PorteeMax;
        }
    }
}
