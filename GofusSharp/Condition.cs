namespace GofusSharp
{
    public class Condition
    {
        public string Type { get; private set; }
        public int Valeur { get; private set; }
        public Condition(string Type, int Valeur)
        {
            this.Type = Type;
            this.Valeur = Valeur;
        }
    }
}
