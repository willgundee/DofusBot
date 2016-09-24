namespace GofusSharp
{
    public class ListeChainee<T>
    {
        public Noeud<T> First { get; set; }
        public Noeud<T> Last { get; set; }
        public int Count { get; set; }
        public ListeChainee()
        {
            this.First = First;
            if (First == null)
            {
                Count = 0;
                Last = First;
                return;
            }
            Noeud<T> NoeudCourant = First;
            while (NoeudCourant.Next != null)
            {

            }
            SetCount();
            Last = NoeudCourant;
        }

        public void AjouterDebut(T Valeur)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
            Count++;
            if (First == null)
            {
                First = NouveauNoeud;
                Last = NouveauNoeud;
                return;
            }
            NouveauNoeud.Next = First;
            First = NouveauNoeud;
            First.Next.Previous = First;
        }

        public void AjouterFin(T Valeur)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
            Count++;
            if (First == null)
            {
                First = NouveauNoeud;
                Last = NouveauNoeud;
                return;
            }
            NouveauNoeud.Previous = Last;
            Last = NouveauNoeud;
            Last.Previous.Next = Last;
        }

        public void AjouterAvant(T Valeur, int Position)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
            Count++;
            if (First == null)
            {
                First = NouveauNoeud;
                Last = NouveauNoeud;
                return;
            }
            Noeud<T> NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            NouveauNoeud.Next = NoeudCourant;
            NouveauNoeud.Previous = NoeudCourant.Previous;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NouveauNoeud;
            if (NoeudCourant == First)
                First = NouveauNoeud;
            NoeudCourant.Previous = NouveauNoeud;
        }
        public void AjouterApres(T Valeur, int Position)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
            Count++;
            if (First == null)
            {
                First = NouveauNoeud;
                Last = NouveauNoeud;
                return;
            }
            Noeud<T> NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            NouveauNoeud.Next = NoeudCourant.Next;
            NouveauNoeud.Previous = NoeudCourant;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NouveauNoeud;
            if (NoeudCourant == Last)
                Last = NouveauNoeud;
            NoeudCourant.Next = NouveauNoeud;
        }
        public void EnleverA(int Position)
        {
            if (First == null)
                return;
            Count--;
            Noeud<T> NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NoeudCourant.Next;
            if (NoeudCourant == First)
                First = NoeudCourant.Next;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NoeudCourant.Previous;
            if (NoeudCourant == Last)
                Last = NoeudCourant.Previous;
            NoeudCourant = null;
        }

        public void Enlever(T Valeur)
        {
            if (First == null)
                return;
            Noeud<T> NoeudCourant = First;
            while (!NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (!NoeudCourant.Valeur.Equals(Valeur))
                return;
            Count--;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NoeudCourant.Next;
            if (NoeudCourant == First)
                First = NoeudCourant.Next;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NoeudCourant.Previous;
            if (NoeudCourant == Last)
                Last = NoeudCourant.Previous;
            NoeudCourant = null;
        }

        public void EnleverDernier(T Valeur)
        {
            if (Last == null)
                return;
            Noeud<T> NoeudCourant = Last;
            while (!NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != First)
                NoeudCourant = NoeudCourant.Previous;
            if (!NoeudCourant.Valeur.Equals(Valeur))
                return;
            Count--;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NoeudCourant.Next;
            if (NoeudCourant == First)
                First = NoeudCourant.Next;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NoeudCourant.Previous;
            if (NoeudCourant == Last)
                Last = NoeudCourant.Previous;
            NoeudCourant = null;
        }

        public void EnleverPremier()
        {
            Count--;
            First.Next.Previous = First.Previous;
            if (First.Previous != null)
                First.Previous.Next = First.Next;
            First = First.Next;
        }

        public void EnleverDernier()
        {
            Count--;
            Last.Previous.Next = Last.Next;
            if (Last.Next != null)
                Last.Next.Previous = Last.Previous;
            Last = Last.Previous;
        }

        public Noeud<T> Trouver(T Valeur)
        {
            if (First == null)
                return null;
            Noeud<T> NoeudCourant = First;
            while (!NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (!NoeudCourant.Valeur.Equals(Valeur))
                return null;
            return NoeudCourant;
        }
        public Noeud<T> TrouverDernier(T Valeur)
        {
            if (Last == null)
                return null;
            Noeud<T> NoeudCourant = Last;
            while (!NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != First)
                NoeudCourant = NoeudCourant.Previous;
            if (!NoeudCourant.Valeur.Equals(Valeur))
                return null;
            return NoeudCourant;
        }

        public bool Contient(T Valeur)
        {
            if (First == null)
                return false;
            Noeud<T> NoeudCourant = First;
            while (!NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (!NoeudCourant.Valeur.Equals(Valeur))
                return false;
            return true;
        }

        public void Vider()
        {
            Vider(First);
        }
        private void Vider(Noeud<T> NoeudADetruire)
        {
            if (NoeudADetruire != Last)
                Vider(NoeudADetruire.Next);
            NoeudADetruire = null;
        }

        private void SetCount()
        {
            if (First == null)
                Count = 0;
            Noeud<T> NoeudCourant = First;
            int compteur = 1;
            while (NoeudCourant.Next != null)
                compteur++;
            Count = compteur;
        }
    }
}
