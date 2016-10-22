
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test
{

    /// <summary>
    /// Logique d'interaction pour pageCPersonnage.xaml
    /// </summary>
    public partial class pageCpersonage : UserControl
    {
        public Classe Cl =null;
        public Entite et;
        private Joueur Player;

        public BDService bd = new BDService();
        public pageCpersonage(Joueur Player)
        {
            InitializeComponent();
            this.Player = Player;
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            string Classe;
            string choix = (sender as Image).Name;

            
            switch (choix)
            {
                case "ClasseCra":
                    Classe = "Cra";
                    break;
                case "ClasseIop":
                    Classe = "Iop";
                    break;
                case "ClasseEcaflip":
                    Classe = "Ecaflip";
                    break;
                default:
                    Classe = null;
                    break;
            }

            Cl = new Classe(bd.selection("SELECT * FROM Classes WHERE Nom = '" + Classe + "'")[0]);
            txtbDesc.Text = Cl.Description;
            //pt afficher les sorts de la classe
        }
        private bool valider()
        {
            bool valider = true;
            if (Cl == null || txtNom.Text.ToString() == "")
            {
                txtNom.Text = "Nom d'utilisateur";

                txtNom.Foreground = new SolidColorBrush(Colors.Red);
                valider = false;
            }
            else
            {
                txtNom.Text = "Nom d'utilisateur";
                txtNom.Foreground = new SolidColorBrush(Colors.Black);
            }
            return valider;
        }
        private void btnConfimer_Click(object sender, RoutedEventArgs e)
        {

            if(!valider())
            { }

            bd.insertion("INSERT INTO Entites(idClasse, idScript,idJoueur, Nom, CapitalLibre) VALUES((SELECT idClasse FROM Classes WHERE nom ='" + Cl.Nom + "'),2,(SELECT idJoueur FROM Joueurs WHERE NomUtilisateur='"+Player.NomUtilisateur+"'),'" + txtNom.Text.ToString() + "', 5)");

            for (int i = 1; i < 6 ; i++)
            {

            bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='"+ txtNom.Text.ToString() + "'),"+i+",0)");
            }
            bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 6 ,100)");
            bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 7 ,120)");
            bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 8 ,0)");
            bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 9 ,6)");
            bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 10 ,3)");
            for (int i = 11; i <= 30; i++)
            {
                bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "')," + i + ",0)");
            }
                      
        }
    }
}
/*INSERT INTO Entites(idClasse, idScript, Nom, CapitalLibre) 
VALUES(SELECT idClasse FROM Classes WHERE nom ='Ecaflip'),2, 'pedro ', 5)*/
