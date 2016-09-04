namespace GofusSharp
{
    public class Personage
    {
        int pointDeVie;
        int pointAction;
        int pointMouvement;
        int vitalite;
        int sagesse;
        int force;
        int intelligence;
        int chance;
        int agilite;

        public Personage()
        {

        }

        public static int addition(int numero1, int numero2)
        {
            return numero1 + numero2;
        }
    }
}//MessageBox.Show(FonctionUtilisateur.addition(1,1).ToString());