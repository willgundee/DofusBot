namespace GofusSharp
{
    public class Zone
    {
        public string Type { get; private set; }
        public int PorteeMin { get; private set; }
        public int PorteeMax { get; private set; }
        public Zone(string Type, int PorteeMin, int PorteeMax)
        {
            this.Type = Type;
            this.PorteeMin = PorteeMin;
            this.PorteeMax = PorteeMax;
        }
    }
}
