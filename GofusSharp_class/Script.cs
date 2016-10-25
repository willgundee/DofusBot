namespace GofusSharp
{
    internal class Script
    {
        internal int IdScript { get; set; }
        internal string Texte { get; set; }
        internal Script(int IdScript, string Texte)
        {
            this.IdScript = IdScript;
            this.Texte = Texte;
        }
    }
}
