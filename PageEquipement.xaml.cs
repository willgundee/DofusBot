using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Logique d'interaction pour PageEquipement.xaml
    /// </summary>
    public partial class PageEquipement : Window
    {
        ObservableCollection<Image> listImg;
        public BDService bd = new BDService();
        private string TypeEQ;
        public PageEquipement(string TypeEquipement, string NomJoueur, string emplacement)
        {
            TypeEQ = emplacement;
            bool valide;
            listImg = new ObservableCollection<Image>();
            InitializeComponent();
            valide = afficherEquipementDispo(TypeEquipement, NomJoueur);
            foreach (Window Page in Application.Current.Windows)
            {
                if (Page.GetType() == typeof(MainWindow))
                {
                    (Page as MainWindow).pgperso.First().validePg = valide;
                }
            }
            lbxItem.ItemsSource = listImg;


        }

        private bool afficherEquipementDispo(string TypeEquipement, string NomJoueur )
        {

            bool valide = false;
            List<string>[] NoImg = null;
            if (TypeEquipement == "Arme")
            {
                NoImg = bd.selection("SELECT e.noImage, je.quantite, je.quantiteEquipe FROM Equipements e INNER JOIN JoueursEquipements je ON je.idEquipement = e.idEquipement  INNER JOIN Joueurs j ON j.idJoueur = je.idJoueur WHERE idZonePorte IS NOT NULL AND j.nomUtilisateur = '" + NomJoueur + "'");
            }
            else
            {
                NoImg = bd.selection("SELECT e.noImage, je.quantite, je.quantiteEquipe FROM equipements e INNER JOIN JoueursEquipements je ON je.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement INNER JOIN Joueurs j ON j.idJoueur = je.idJoueur WHERE j.nomUtilisateur = '" + NomJoueur + "'AND t.nom = '" + TypeEquipement + "'");
            }
            if (NoImg[0][0] == "rien")
            {
                return valide;
            }
            listImg.Clear();
            foreach (List<string> item in NoImg)
            {
                for (int i = 0; i < Convert.ToInt32(item[1]) - Convert.ToInt32(item[2]); i++)
                {              
                    listImg.Add( CreateImg(item[0]));
                }
                valide = true;
            }
            if(listImg.Count==0)
            {
                return false;
            }

            return valide;
        }

        private Image CreateImg(string Noimg)
        {
            Image img = new Image();
            ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + Noimg + ".png"));
            img.Width = 100;
            img.Height = 100;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.MouseUp += image_MouseUp;
            img.Source = path;
            return img;
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Window Page in Application.Current.Windows)
            {
                if (Page.GetType() == typeof(MainWindow))
                {     
                    switch (TypeEQ)
                    {
                        case "tête":
                            (Page as MainWindow).pgperso.First().imageCasque.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "dos":
                            (Page as MainWindow).pgperso.First().imageCape.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "arme":
                            (Page as MainWindow).pgperso.First().imageArme.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "hanche":
                            (Page as MainWindow).pgperso.First().imageCeinture.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "ano1":
                            (Page as MainWindow).pgperso.First().imageAnneau1.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "ano2":
                            (Page as MainWindow).pgperso.First().imageAnneau2.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "pied":
                            (Page as MainWindow).pgperso.First().imageBotte.Source = (sender as Image).Source;
                            Close();
                            break;
                        case "cou":
                            (Page as MainWindow).pgperso.First().imageAmulette.Source = (sender as Image).Source;
                            Close();
                            break;
                    }
                }
            }
        }
    }
}

