namespace GofusSharp
{
    public class Script
    {
        public int IdScript { get; internal set; }
        public string Texte { get; internal set; }
        public Script(int IdScript, string Texte)
        {
            this.IdScript = IdScript;
            this.Texte = Texte;
        }
    }
}
