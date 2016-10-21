namespace GofusSharp
{
    public class Case : System.ComponentModel.INotifyPropertyChanged
    {
        public enum type {vide, joueur, obstacle, piege }
        public int X { get; internal set; }
        public int Y { get; internal set; }
        private type _Contenu;
        public type Contenu
        {
            get { return _Contenu; }
            internal set
            {
                if (Equals(Contenu, value))
                    return;
                _Contenu = value;
                OnPropertyChanged("Case");
            }
        }
        internal Case(int X, int Y, type Contenu)
        {
            this.X = X;
            this.Y = Y;
            this.Contenu = Contenu;
        }

        #region INotifyPropertyChanged Implementation
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            var handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
