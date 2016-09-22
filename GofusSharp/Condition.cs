namespace GofusSharp
{
    public class Condition
    {
        public string Type { get; }
        public int Valeur { get; }
        public Condition(string Type, int Valeur)
        {
            this.Type = Type;
            this.Valeur = Valeur;
        }
    }
}
