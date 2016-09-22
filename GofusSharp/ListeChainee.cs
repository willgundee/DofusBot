﻿namespace GofusSharp
{
    public class ListeChainee
    {
        public Noeud First { get; private set; }
        public Noeud Last { get; private set; }
        public int Count { get; private set; }
        public ListeChainee(Noeud First)
        {
            this.First = First;
            if (First == null)
            {
                Count = 0;
                Last = First;
                return;
            }
            Noeud NoeudCourant = First;
            while (NoeudCourant.Next != null)
            {

            }
            SetCount();
            Last = NoeudCourant;
        }

        public void AjouterPremier(Noeud NouveauNoeud)
        {
            Count++;
            NouveauNoeud.Next = First;
            First = NouveauNoeud;
            First.Next.Previous = First;
        }

        public void AjouterDernier(Noeud NouveauNoeud)
        {
            Count++;
            NouveauNoeud.Previous = Last;
            Last = NouveauNoeud;
            Last.Previous.Next = Last;
        }

        public void AjouterAvant(Noeud NouveauNoeud, int Position)
        {
            Count++;
            if (First == null)
            {
                First = NouveauNoeud;
                Last = NouveauNoeud;
                return;
            }
            Noeud NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant.Next != null; i++)
                NoeudCourant = NoeudCourant.Next;
            NouveauNoeud.Next = NoeudCourant;
            NouveauNoeud.Previous = NoeudCourant.Previous;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NouveauNoeud;
            else
                First = NouveauNoeud;
            NoeudCourant.Previous = NouveauNoeud;
        }
        public void AjouterApres(Noeud NouveauNoeud, int Position)
        {
            Count++;
            if (First == null)
            {
                First = NouveauNoeud;
                Last = NouveauNoeud;
                return;
            }
            Noeud NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant.Next != null; i++)
                NoeudCourant = NoeudCourant.Next;
            NouveauNoeud.Next = NoeudCourant.Next;
            NouveauNoeud.Previous = NoeudCourant;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NouveauNoeud;
            else
                Last = NouveauNoeud;
            NoeudCourant.Next = NouveauNoeud;
        }
        public void EnleverA(int Position)
        {
            if (First == null)
                return;
            Count--;
            Noeud NoeudCourant = First;
            for (int i = 0; i < Position && NoeudCourant.Next != null; i++)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NoeudCourant.Next;
            else
                First = NoeudCourant.Next;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NoeudCourant.Previous;
            else
                Last = NoeudCourant.Previous;
            NoeudCourant = null;
        }

        public void Enlever(object Valeur)
        {
            if (First == null)
                return;
            Noeud NoeudCourant = First;
            while (NoeudCourant.Valeur != Valeur && NoeudCourant.Next != null)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Valeur != Valeur)
                return;
            Count--;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NoeudCourant.Next;
            else
                First = NoeudCourant.Next;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NoeudCourant.Previous;
            else
                Last = NoeudCourant.Previous;
            NoeudCourant = null;
        }

        public void EnleverDernier(object Valeur)
        {
            if (Last == null)
                return;
            Noeud NoeudCourant = Last;
            while (NoeudCourant.Valeur != Valeur && NoeudCourant.Previous != null)
                NoeudCourant = NoeudCourant.Previous;
            if (NoeudCourant.Valeur != Valeur)
                return;
            Count--;
            if (NoeudCourant.Previous != null)
                NoeudCourant.Previous.Next = NoeudCourant.Next;
            else
                First = NoeudCourant.Next;
            if (NoeudCourant.Next != null)
                NoeudCourant.Next.Previous = NoeudCourant.Previous;
            else
                Last = NoeudCourant.Previous;
            NoeudCourant = null;
        }

        public void EnleverPremier()
        {
            Count--;
            First = First.Next;
            First.Previous = null;
        }

        public void EnleverDernier()
        {
            Count--;
            Last = Last.Previous;
            Last.Next = null;
        }

        public Noeud Trouver(object Valeur)
        {
            if (First == null)
                return null;
            Noeud NoeudCourant = First;
            while (NoeudCourant.Valeur != Valeur && NoeudCourant.Next != null)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Valeur != Valeur)
                return null;
            return NoeudCourant;
        }
        public Noeud TrouverDernier(object Valeur)
        {
            if (Last == null)
                return null;
            Noeud NoeudCourant = Last;
            while (NoeudCourant.Valeur != Valeur && NoeudCourant.Previous != null)
                NoeudCourant = NoeudCourant.Previous;
            if (NoeudCourant.Valeur != Valeur)
                return null;
            return NoeudCourant;
        }

        public bool Contient(object Valeur)
        {
            if (First == null)
                return false;
            Noeud NoeudCourant = First;
            while (NoeudCourant.Valeur != Valeur && NoeudCourant.Next != null)
                NoeudCourant = NoeudCourant.Next;
            if (NoeudCourant.Valeur != Valeur)
                return false;
            return true;
        }

        public void Vider()
        {
            Vider(First);
        }
        private void Vider(Noeud NoeudADetruire)
        {
            if (NoeudADetruire.Next != null)
                Vider(NoeudADetruire.Next);
            NoeudADetruire = null;
        }

        private void SetCount()
        {
            if (First == null)
                Count = 0;
            Noeud NoeudCourant = First;
            int compteur = 1;
            while (NoeudCourant.Next != null)
                compteur++;
            Count = compteur;
        }
    }
}
