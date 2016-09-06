namespace GofusSharp
{
    //csc /target:library /out:GofusSharp.dll FonctionUtilisateur.cs
    public class Personage
    {
        int PV;
        int PA;

        public Personage(int pv, int pa)
        {
            PV = pv;
            PA = pa;
        }

        public static int addition(int numero1, int numero2)
        {
            return numero1 + numero2;
        }
    }
}//Personage pTest = new Personage(5,6);MessageBox.Show(Personage.addition(pTest.PV,pTest.PA).ToString());