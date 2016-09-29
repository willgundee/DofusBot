namespace GofusSharp
{
    public class Terrain
    {
        public Case[][] TabCases { get; internal set; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
        }
        

        public int DistanceEntreCases(Case case1, Case case2)
        {
            return Math.Abs(case1.X - case2.X) + Math.Abs(case1.Y - case2.Y);
        }

        public ListeChainee<Case> CaseAvecObstacle()
        {
            ListeChainee<Case> caseAvecObstacle = new ListeChainee<Case>();
            foreach (Case[] tabcase in TabCases)
            {
                foreach (Case caseT in tabcase)
                {
                    if(caseT.Contenu == Case.type.obstacle)
                        caseAvecObstacle.AjouterFin(caseT);
                }
            }
            return caseAvecObstacle;
        }

        public ListeChainee<Case> CheminEntreCases(Case Depart, Case Destination)
        {

            ListeChainee<Case> open = new ListeChainee<Case>();//list of nodes
            ListeChainee<Case> closed = new ListeChainee<Case>();

            

            open.AjouterFin(Depart);//Add starting point

            while (open.Count > 0)
            {
                Case position = getBestNode();//Get node with lowest F value
                if (position == Destination)
                {
                    //Debug.Log("Goal reached");
                    //getPath(node);
                    break;
                }
                open.Enlever(position);//removeNode(node, open);
                closed.AjouterFin(position);

                ListeChainee<Case> neighbors = getNeighbors(node);
                foreach (Case n in neighbors)
                {
                    float g_score = node.G + 1;
                    float h_score = ManhattanDistance(n.position, goalNode.position);
                    float f_score = g_score + h_score;

                    if (isValueInList(n, closed) && f_score >= n.F)
                        continue;

                    if (!isValueInList(n, open) || f_score < n.F)
                    {
                        n.parent = node;
                        n.G = g_score;
                        n.H = h_score;
                        if (!isValueInList(n, open))
                        {
                            map_data[n.position.x, n.position.y] = 4;
                            open.Add(n);
                        }
                    }
                }
            }
            //ListeChainee<Case> chemin = new ListeChainee<Case>();
            //ListeChainee<Case> CulDeSac = new ListeChainee<Case>();
            //chemin.AjouterFin(Depart);
            //while (chemin.Last.Valeur != Destination)
            //{
            //    if (TrouverProchaineCase(chemin, CulDeSac, Destination) == null)
            //    {
            //        CulDeSac.AjouterFin(chemin.Last.Valeur);
            //        chemin.EnleverDernier();
            //    }
            //    else
            //    {
            //        chemin.AjouterFin(TrouverProchaineCase(chemin, CulDeSac, Destination));
            //    }
            //}
            //return chemin;
        }

        internal Case TrouverProchaineCase(ListeChainee<Case> Chemin, ListeChainee<Case> CulDeSac, Case Destination)
        {
            if (Math.Abs(Chemin.Last.Valeur.X - Destination.X) >= Math.Abs(Chemin.Last.Valeur.Y - Destination.Y))
            {
                if (TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y]))
                {
                    if (TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)]))
                    {
                        return null;
                    }
                    return TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)];
                }
                return TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y];
            }
            else
            {
                if (TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)]))
                {
                    if (TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y]))
                    {
                        return null;
                    }
                    return TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y];
                }
                return TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)];
            }
        }
    }
}
