using GofusSharp;

namespace test
{
    class EnviroTest
    {
        //        EntiteInconnu ennemi = null;
        //foreach (EntiteInconnu entite in ListEntites)
        //{
        //    if (entite.Equipe != Perso.Equipe)
        //    {
        //        ennemi = entite;
        //        break;
        //    }
        //}
        //if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
        //{
        //    int result = 1;
        //    while (result != 0 && result != -1)
        //    {
        //        result = Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
        //    }
        //}
        //while(Perso.UtiliserSort(Sort.nom_sort.pression, ennemi))
        //{

        //}

        public void Execution(Terrain terrain, Entite Perso, Liste<EntiteInconnu> ListEntites)
        { 
            EntiteInconnu ennemi = Perso.EnemiLePlusProche(ListEntites);
            Sort Bavouille;
            Sort Mordillement;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    default:
                        break;
                }
            }
            if ()
        }
    }
}
