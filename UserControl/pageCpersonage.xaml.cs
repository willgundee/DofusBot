using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        /// <summary>
        /// Création de la page Création de personnage
        /// </summary>
        /// <param name="Playert">Le joueur connecté</param>
        public pageCpersonage(Joueur Playert)
        {
            InitializeComponent();
            this.Player = Playert;
        }
        /// <summary>
        /// Actions qui sont effectuées quand on clique sur l'image de l'une des 3 classes
        /// </summary>
        /// <param name="sender">les information de l,image</param>
        /// <param name="e"></param>
        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string Classe;
            //on prend le nom de l'image
            string choix = (sender as Image).Name;
            //avec le nom de l'image on change la grosseur de leur image pour que l'utilisateur sache quel classe il a choisi
            //et on initialise un variable classe  
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
            //avec la variable classe initiallisée dans le switch on va checher les information de la classe en BD
            Cl = new Classe(bd.selection("SELECT * FROM Classes WHERE Nom = '" + Classe + "'")[0]);
            //on affiche les informations de la classe choisie
            txtbDesc.Text = Cl.Description;
            txtbDesc.Foreground = new SolidColorBrush(Colors.Black);
            //pt afficher les sorts de la classe
        }
        /// <summary>
        /// Fonction pour valider la création d'un personnage
        /// </summary>
        /// <returns>retourne si les champs sont valides</returns>
        private bool valider()
        {
            bool valider = true;
            //change la couleur du champ s'il n'est par remplir
            if (txtNom.Text.ToString() == "")
            {
                lblNom.Content = "Nom du personnage (Champs Obligatoire)";
                lblNom.Foreground = new SolidColorBrush(Colors.Orange);
                valider = false;
            }
            else if (!checkUniqueNom())
            {
                //vérifie si le nom du personnage est déjà utilisé et change la couleur si oui
                lblNom.Content = "Nom du personnage (nom déjà utilisé)";
                lblNom.Foreground = new SolidColorBrush(Colors.Orange);
                valider = false;
            }
            else
            {   //si tout est valide on s'assure que le titre reste noir
                lblNom.Content = "Nom du personnage";
                lblNom.Foreground = new SolidColorBrush(Colors.Black);
            }
            //si la description est vide cela veut dire que la classe n'a pas été sélectionnée
            if (txtbDesc.Text == "")
            {
                txtbDesc.Text = "Pas de classe sélectionné";
                txtbDesc.Foreground = new SolidColorBrush(Colors.Orange);
                valider = false;
            }
            else
            {   //si tout est valide on s'assure que le titre reste noir
                txtbDesc.Foreground = new SolidColorBrush(Colors.Black);
            }
            return valider;
        }
        
        /// <summary>
        /// Vérifier si le nom est unique
        /// </summary>
        /// <returns>si le nom est utilisé ou pas</returns>
        private bool checkUniqueNom()
        {
            string nom = txtNom.Text;
            //vérife dans la base de donnée que le nom du personnage n'est pas utilisé
            if ((bd.selection("SELECT idEntite FROM Entites WHERE nom='" + nom + "'"))[0][0] != "rien")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Fontion qui sont effectué lorsque le joueur confirme qu'il veut créer se personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfimer_Click(object sender, RoutedEventArgs e)
        {

            if (valider())
            {// insert les informations rentrés par l'utilisateur en BD.
                bd.insertion("INSERT INTO Entites(idClasse, idScript,idJoueur, Nom, CapitalLibre) VALUES((SELECT idClasse FROM Classes WHERE nom ='" + Cl.Nom + "'),(SELECT idScript FROM JoueursScripts  s INNER JOIN Joueurs j ON  j.idJoueur = s.idJoueur WHERE j.NomUtilisateur='" + Player.NomUtilisateur + "' LIMIT 1),(SELECT idJoueur FROM Joueurs WHERE NomUtilisateur='" + Player.NomUtilisateur + "'),'" + txtNom.Text.ToString() + "', 5)");
            //on sélectionne l'id de l'entites qui vein de créer en BD.
                List<string> idEntite = bd.selection("SELECT idEntite FROM Entites WHERE nom = '" + txtNom.Text.ToString() + "'")[0];
                //on insére les statistiques du nouveux personnage en BD
                for (int i = 1; i < 6; i++)
                    bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "')," + i + ",0)");
                bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES ((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 6 ,100),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 7 ,120),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 8 ,0),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 9 ,6),((SELECT idEntite FROM Entites WHERE nom='" + txtNom.Text.ToString() + "'), 10 ,3);");
                for (int i = 11; i <= 38; i++)
                    bd.insertion("INSERT INTO statistiquesentites(idEntite,idTypeStatistique,valeur) VALUES (" + idEntite[0] + "," + i + ",0)");
                //on selectionne le nom du personnage en BD
                List<string> h = bd.selection("SELECT * FROM Entites WHERE nom ='" + txtNom.Text.ToString() + "'")[0];
                //on créer un nouve onglet avec le nom du personnage
                TabItem onglet = new TabItem();
                onglet.Header = txtNom.Text.ToString();// je donne un nom a l'onglet
                onglet.Content = new PagePerso(new Entite(h), Player);// je met son contenu pour les informations de l'entité
                onglet.IsSelected = true;
                // store la mainwindows pour pas avoir des lignes trop longues
                MainWindow w = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow);
                w.tCPerso.Items.RemoveAt(w.tCPerso.SelectedIndex);// j'enleve l'onglet +
                w.tCPerso.Items.Add(onglet);// j'ajoute son personnage

                if (w.tCPerso.Items.Count <= 4)// s'il y a plus de 5 personnages je ne lui laisse pas ajouter d'autres personnages
                {
                    TabItem newP = new TabItem();
                    newP.Header = "+";
                    newP.Content = new pageCpersonage(Player);
                    w.tCPerso.Items.Add(newP);
                }
                Player.LstEntites.Add(new Entite(h));
            }

        }
    }
}
