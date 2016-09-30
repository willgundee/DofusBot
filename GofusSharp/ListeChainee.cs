using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GofusSharp
{
    public class ListeChainee<T> : IEnumerable<T>
    {
        public Noeud<T> First { get; set; }
        public Noeud<T> Last { get; set; }
        public int Count { get { SetCount(); return Count; } private set { Count = value; } }

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

        public void AjouterDebut(Noeud<T> noeud)
        {
            Count++;
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

        public void AjouterFin(Noeud<T> noeud)
        {
            Count++;
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

        public void AjouterAvant(Noeud<T> noeud, int Position)
        {
            Count++;
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
        public void AjouterApres(Noeud<T> noeud, int Position)
        {
            Count++;
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

        public void Enlever(Noeud<T> noeud)
        {
            if (First == null)
                return;
            Noeud<T> NoeudCourant = First;
            while (NoeudCourant != noeud && NoeudCourant != Last)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant != noeud)
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

        public void EnleverDernier(Noeud<T> noeud)
        {
            if (Last == null)
                return;
            Noeud<T> NoeudCourant = Last;
            while (NoeudCourant != noeud && NoeudCourant != First)
                NoeudCourant = NoeudCourant.Previous;
            if (NoeudCourant != noeud)
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
        public int? TrouverPosition(T Valeur)
        {
            if (First == null)
                return null;
            Noeud<T> NoeudCourant = First;
            int i;
            for (i = 0; !NoeudCourant.Valeur.Equals(Valeur) && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Valeur.Equals(Valeur))
                return i;
            return null;
        }
        public int? TrouverPosition(Noeud<T> noeud)
        {
            if (First == null)
                return null;
            Noeud<T> NoeudCourant = First;
            int i;
            for (i = 0; NoeudCourant != noeud && NoeudCourant != Last; i++)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant == noeud)
                return i;
            return null;
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



// These make myLinkedList<T> implement IEnumerable<T> allowing
// a LinkedList to be used in a foreach statement.
        public IEnumerator<T> GetEnumerator()
        {
            return new myLinkedListIterator<T>(First);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class myLinkedListIterator<T> : IEnumerator<T>
        {
            object IEnumerator.Current { get { return Current; } }
            private Noeud<T> current;
            public virtual T Current
            {
                get
                {
                    return current.Valeur;
                }
            }
            private Noeud<T> front;

            public myLinkedListIterator(Noeud<T> f)
            {
                front = f;
                current = front;
            }

            public bool MoveNext()
            {
                if (current.Next != null)
                {
                    current = current.Next;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                current = front;
            }

            public void Dispose()
            {
                throw new Exception("Unsupported Operation");
            }
        }
    }
}
