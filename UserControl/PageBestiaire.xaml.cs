using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour PageBestiaire.xaml
    /// </summary>
    public partial class PageBestiaire : UserControl
    {
        public BDService bd = new BDService();
        private ObservableCollection<BestaireList> lstBEAST;
        private ObservableCollection<BestaireDesc> lstDescription;
        /// <summary>
        /// Constructeur de la page générale de bestiaire 
        /// </summary>
        public PageBestiaire()
        {
            InitializeComponent();
            lstBEAST = new ObservableCollection<BestaireList>();
            lstDescription = new ObservableCollection<BestaireDesc>();
            lbxBestiaire.ItemsSource = lstBEAST;
            lbxDescBestiaire.ItemsSource = lstDescription;
            contenulxbBestiaire();
        }
        /// <summary>
        /// Fonction qui peuple la liste de monstres
        /// </summary>
        void contenulxbBestiaire()
        {
            int con;
            List<string>[] Type;
            //on s'assure que la liste est vidé
            lstBEAST.Clear();
            //on sélectionne tous les monstres dans la BD
            Type = bd.selection("SELECT * FROM Entites e  WHERE idJoueur = 103");
            con = Type.Count();

            Thread createBete = new Thread(() =>
            {
                Dispatcher.Invoke(new Action(() =>
                {//on donne une action a chaque élément (monstre) de la liste
                    for (int i = 0; i < con; i++)
                    {
                        BestaireList s = new BestaireList(Type[i]);
                        //lorsqu,on clique sur un monstre on ajoute ses informations dans la zone de description
                        s.MouseDown += lbxBestiaire_MouseDoubleClick;
                        lstBEAST.Add(s);
                    }
                }));
            });
            createBete.Start();
            Thread.Yield();
        }
        /// <summary>
        /// Fonction qui peuple la description des monstre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxBestiaire_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                string nom = (sender as BestaireList).lblNomBest.Content.ToString();               
                contenuLxbDesc(nom);
            }));
        }
        /// <summary>
        /// Fonction qui avec le nom vas chercher les informations du monstres sélectionné 
        /// </summary>
        /// <param name="nom">Nom du monstre</param>
        void contenuLxbDesc(string nom)
        {
            List<string>[] info;
            Dispatcher.Invoke(new Action(() =>
            {//on vide la liste 
                lstDescription.Clear();
            }));
            //on vas chercher les informations du monstre en BD
            info = bd.selection("SELECT * FROM Entites WHERE nom='" + nom + "'");
            Thread createBete = new Thread(() =>
            {
                //on crée un entité avec les info du monstre
                Entite ds = new Entite(info[0]);
                Dispatcher.Invoke(new Action(() =>
                {
                    //on passe ne paramètre l'entité pour crée la zone de description
                    BestaireDesc descS = new BestaireDesc(ds);
                    //on ajoute les information a la zone de description.
                    lstDescription.Add(descS);
                }));
            });
            createBete.Start();
            Thread.Yield();
        }
    }
}
