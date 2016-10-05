namespace GofusSharp
{
    public class ListeChainee<T> : System.Collections.Generic.IEnumerable<T>
    {
        public Noeud<T> First { get; set; }
        public Noeud<T> Last { get; set; }
        public int Count {
            get
            {
                this.Count = 0;
                if (First == null)
                    return Count;
                foreach (T valeur in this)
                    Count++;
                return Count;
            }
            set { }
        }

        public ListeChainee()
        {
            this.First = First;
            if (First == null)
            {
                Last = First;
                return;
            }
            Noeud<T> NoeudCourant = First;
            while (NoeudCourant.Next != null)
            {

            }
            Last = NoeudCourant;
        }

        public void AjouterDebut(Noeud<T> noeud)
        {
            if (First == null)
            {
                First = noeud;
                Last = noeud;
                return;
            }
            noeud.Next = First;
            First = noeud;
            First.Next.Previous = First;
        }

        public void AjouterDebut(T Valeur)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
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

        public void AjouterFin(Noeud<T> noeud)
        {
            if (First == null)
            {
                First = noeud;
                Last = noeud;
                return;
            }
            noeud.Previous = Last;
            Last = noeud;
            Last.Previous.Next = Last;
        }

        public void AjouterFin(T Valeur)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
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

        public void AjouterAvant(Noeud<T> noeud, int Position)
        {
            if (First == null)
            {
                First = noeud;
                Last = noeud;
                return;
            }
            Noeud<T> NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            noeud.Next = NoeudCourant;
            noeud.Previous = NoeudCourant.Previous;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = noeud;
            if (NoeudCourant == First)
                First = noeud;
            NoeudCourant.Previous = noeud;
        }

        public void AjouterAvant(T Valeur, int Position)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
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
        public void AjouterApres(Noeud<T> noeud, int Position)
        {
            if (First == null)
            {
                First = noeud;
                Last = noeud;
                return;
            }
            Noeud<T> NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            noeud.Next = NoeudCourant.Next;
            noeud.Previous = NoeudCourant;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = noeud;
            if (NoeudCourant == Last)
                Last = noeud;
            NoeudCourant.Next = noeud;
        }
        public void AjouterApres(T Valeur, int Position)
        {
            Noeud<T> NouveauNoeud = new Noeud<T>(Valeur);
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

        public void Enlever(Noeud<T> noeud)
        {
            if (First == null)
                return;
            Noeud<T> NoeudCourant = First;
            while (NoeudCourant != noeud && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant != noeud)
                return;
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

        public void EnleverDernier(Noeud<T> noeud)
        {
            if (Last == null)
                return;
            Noeud<T> NoeudCourant = Last;
            while (NoeudCourant != noeud && NoeudCourant != First)
                NoeudCourant = NoeudCourant.Previous;
            if (NoeudCourant != noeud)
                return;
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
            First.Next.Previous = First.Previous;
            if (First.Previous != null)
                First.Previous.Next = First.Next;
            First = First.Next;
        }

        public void EnleverDernier()
        {
            Last.Previous.Next = Last.Next;
            if (Last.Next != null)
                Last.Next.Previous = Last.Previous;
            Last = Last.Previous;
        }

        public Noeud<T> Trouver(Noeud<T> noeud)
        {
            if (First == null)
                return null;
            Noeud<T> NoeudCourant = First;
            while (NoeudCourant != noeud && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant != noeud)
                return null;
            return NoeudCourant;
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

        public Noeud<T> TrouverDernier(Noeud<T> noeud)
        {
            if (Last == null)
                return null;
            Noeud<T> NoeudCourant = Last;
            while (NoeudCourant != noeud && NoeudCourant != First)
                NoeudCourant = NoeudCourant.Previous;
            if (NoeudCourant != noeud)
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
        public Noeud<T> TrouverA(int Position)
        {
            if (First == null)
                return null;
            Noeud<T> NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            return NoeudCourant;
        }
        public int TrouverPosition(T Valeur)
        {
            if (First == null)
                return -1;
            Noeud<T> NoeudCourant = First;
            int i;
            for (i = 0; !NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Valeur.Equals(Valeur))
                return i;
            return -1;
        }
        public int TrouverPosition(Noeud<T> noeud)
        {
            if (First == null)
                return -1;
            Noeud<T> NoeudCourant = First;
            int i;
            for (i = 0; NoeudCourant != noeud && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant == noeud)
                return i;
            return -1;
        }
        public bool Contient(Noeud<T> noeud)
        {
            if (First == null)
                return false;
            Noeud<T> NoeudCourant = First;
            while (NoeudCourant != noeud && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant != noeud)
                return false;
            return true;
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



        // These make myLinkedList<T> implement IEnumerable<T> allowing
        // a LinkedList to be used in a foreach statement.
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            Noeud<T> node = First;
            if (First == null)
                yield break;
            while (node != Last.Next)
            {
                yield return node.Valeur;
                node = node.Next;
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
