namespace GofusSharp
{
    public class Script
    {
        public int IdScript { get; private set; }
        public string Texte { get; private set; }
        public Script(int IdScript, string Texte)
        {
            this.IdScript = IdScript;
            this.Texte = Texte;
        }
    }
}
