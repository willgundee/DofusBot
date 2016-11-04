
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

namespace Gofus
{

    /// <summary>
    /// Logique d'interaction pour pageCPersonnage.xaml
    /// </summary>
    public partial class pageCpersonage : UserControl
    {
        public Classe Cl = null;
        public Entite et;
        private Joueur Player;

        public BDService bd = new BDService();
        public pageCpersonage(Joueur Playert)
        {
            InitializeComponent();
            this.Player = Playert;
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {


            string Classe;
            string choix = (sender as Image).Name;

            switch (choix)
            {
                case "ClasseCra":
                    Classe = "Cra";
                    ClasseCra.Height = 150;
                    ClasseCra.Width = 150;
                    ClasseIop.Height = 125;
                    ClasseIop.Width = 125;
                    ClasseEcaflip.Height = 125;
                    ClasseEcaflip.Width = 125;
                    break;
                case "ClasseIop":
                    Classe = "Iop";
                    ClasseCra.Height = 125;
                    ClasseCra.Width = 125;
                    ClasseIop.Height = 150;
                    ClasseIop.Width = 150;
                    ClasseEcaflip.Height = 125;
                    ClasseEcaflip.Width = 125;
                    break;
                case "ClasseEcaflip":
                    Classe = "Ecaflip";
                    ClasseCra.Height = 125;
                    ClasseCra.Width = 125;
                    ClasseIop.Height = 125;
                    ClasseIop.Width = 125;
                    ClasseEcaflip.Height = 150;
                    ClasseEcaflip.Width = 150;
                    break;
                default:
                    Classe = null;
                    break;
            }

            Cl = new Classe(bd.selection("SELECT * FROM Classes WHERE Nom = '" + Classe + "'")[0]);
            txtbDesc.Text = Cl.Description;
            txtbDesc.Foreground = new SolidColorBrush(Colors.Black);
            //pt afficher les sorts de la classe
        }
        private bool valider()
        {
            bool valider = true;

            

            if (txtNom.Text.ToString() == "")
            {
                lblNom.Content = "Nom du personnage (Champs Obligatoire)";
                lblNom.Foreground = new SolidColorBrush(Colors.Orange);
                valider = false;
            }
            else if (!checkUniqueNom())
            {

                lblNom.Content = "Nom du personnage (nom déjà utilisé)";
                lblNom.Foreground = new SolidColorBrush(Colors.Orange);
                valider = false;

            }
            else
            {
                lblNom.Content = "Nom du personnage";
                lblNom.Foreground = new SolidColorBrush(Colors.Black);
            }




            if (txtbDesc.Text == "")
            {
                txtbDesc.Text = "Pas de classe sélectionné";
                txtbDesc.Foreground = new SolidColorBrush(Colors.Orange);
                valider = false;
            }
            else
            {
                txtbDesc.Foreground = new SolidColorBrush(Colors.Black);
            }
            return valider;
        }
        

        /// <summary>
        /// Vérifier si le nom est unique
        /// </summary>
        /// <returns></returns>
        private bool checkUniqueNom()
        {
            string nom = txtNom.Text;
            if ((bd.selection("SELECT idEntite FROM Entites WHERE nom='" + nom + "'"))[0][0] != "rien")
            {
                return false;
            }
            return true;
        }


        private void btnConfimer_Click(object sender, RoutedEventArgs e)
        {

            if (valider())
            {//TODO: revoir le link du script et bien linker la page que tu ajoute
                bd.insertion("INSERT INTO Entites(idClasse, idScript,idJoueur, Nom, CapitalLibre) VALUES((SELECT idClasse FROM Classes WHERE nom ='" + Cl.Nom + "'),(SELECT idScript FROM JoueursScripts  s INNER JOIN Joueurs j ON  j.idJoueur = s.idJoueur WHERE j.NomUtilisateur='" + Player.NomUtilisateur + "'),(SELECT idJoueur FROM Joueurs WHERE NomUtilisateur='" + Player.NomUtilisateur + "'),'" + txtNom.Text.ToString() + "', 5)");
                List<string> idEntite = bd.selection("SELECT idEntite FROM Entites WHERE nom = '" + txtNom.Text.ToString() + "'")[0];
                for (int i = 1; i < 6; i++)
                    bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "')," + i + ",0)");

                bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 6 ,100),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 7 ,120),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 8 ,0),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 9 ,6),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 10 ,3);");
                for (int i = 11; i <= 38; i++)
                    bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES (" + idEntite[0] + "," + i + ",0)");


                List<string> h = bd.selection("SELECT * FROM Entites WHERE nom ='" + txtNom.Text.ToString() + "'")[0];

                TabItem onglet = new TabItem();
                onglet.Header = txtNom.Text.ToString();// je donne un nom a l'onglet
                onglet.Content = new PagePerso(new Entite(h), Player);// je met son contenu pour les informations de l'entité
                onglet.IsSelected = true;

                MainWindow w = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow); // store la mainwondows pour pas avoir des ligne trop longue

                w.tCPerso.Items.RemoveAt(w.tCPerso.SelectedIndex);// j'enleve l'onglet +
                w.tCPerso.Items.Add(onglet);// j'ajoute son personnage

                if (w.tCPerso.Items.Count <= 4)// s'il y a plus de 5 personnages je ne lui laisse pas ajouter d'autres personnages
                {
                    TabItem newP = new TabItem();
                    newP.Header = "+";
                    newP.Content = new pageCpersonage(Player);
                    w.tCPerso.Items.Add(newP);
                }
            }

        }
    }
}
/*INSERT INTO Entites(idClasse, idScript, Nom, CapitalLibre) 
VALUES(SELECT idClasse FROM Classes WHERE nom ='Ecaflip'),2, 'pedro ', 5)*/
