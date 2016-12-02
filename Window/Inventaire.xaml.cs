using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Inventaire.xaml
    /// </summary>
    public partial class PageInventaire : Window
    {
        public Window _dragdropWindow = null;// la window qui contient l'image drag
        ObservableCollection<ImageItem> LstInventaire;
        ObservableCollection<DescItem> LstDesc;
        ListBox dragSource = null;
        public Joueur Player { get; set; }
        private BDService bd = new BDService();
        private MainWindow w;

        public PageInventaire(Joueur Player)
        {
            InitializeComponent();
            LstInventaire = new ObservableCollection<ImageItem>();
            LstDesc = new ObservableCollection<DescItem>();
            w = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow);
            this.Player = Player;
            List<string> type = new List<string>();
            type.Add("Tous");
            foreach (List<string> typeNom in bd.selection("SELECT nom FROM typesEquipements"))
                type.Add(typeNom[0]);//rempli la liste du trie
            cboTrieInventaire.ItemsSource = type;
            cboTrieInventaire.SelectedIndex = 0;
            lbxInventaire.ItemsSource = LstInventaire;// lie l'inventaire du joueur à la page inventaire
            lblArgent.Content = Player.Kamas;

        }
        /// <summary>
        /// le Drag du Drag&Drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxInventaire_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PagePerso pp = (w.tCPerso.SelectedContent as PagePerso);

            ListBox parent = (ListBox)sender;
            dragSource = parent;
            ImageItem data = (ImageItem)GetDataFromListBox(dragSource, e.GetPosition(parent));

            #region drop pas icitte !
            pp.imgCapeInv.AllowDrop = false;
            pp.imgChapeauInv.AllowDrop = false;
            pp.imgBotteInv.AllowDrop = false;
            pp.imgAnneau1Inv.AllowDrop = false;
            pp.imgAnneau2Inv.AllowDrop = false;
            pp.imgAmuletteInv.AllowDrop = false;
            pp.imgArmeInv.AllowDrop = false;
            #endregion // enleve le droit de drop sur chaque image

            SolidColorBrush color = Brushes.Orange;

            if (data != null)
            {
                Equipement itemDrag = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(data.imgItem.Source.ToString())); // crée l'item qui est drag
                switch (itemDrag.Type) // dépendant de son type je lui propose ou il peut l'équiper
                {
                    case "Cape":
                        pp.imgCapeInv.AllowDrop = true;
                        pp.borderCape.BorderBrush = color;
                        break;
                    case "Chapeau":
                        pp.imgChapeauInv.AllowDrop = true;
                        pp.borderCoiffe.BorderBrush = color;
                        break;
                    case "Botte":
                        pp.imgBotteInv.AllowDrop = true;
                        pp.borderBotte.BorderBrush = color;
                        break;
                    case "Ceinture":
                        pp.imgCeintureInv.AllowDrop = true;
                        pp.borderCeinture.BorderBrush = color;
                        break;
                    case "Anneau":
                        pp.imgAnneau1Inv.AllowDrop = true;
                        pp.imgAnneau2Inv.AllowDrop = true;
                        pp.borderAno1.BorderBrush = color;
                        pp.borderAno2.BorderBrush = color;
                        break;
                    case "Amulette":
                        pp.imgAmuletteInv.AllowDrop = true;
                        pp.borderAmu.BorderBrush = color;
                        break;
                    default://arme
                        pp.imgArmeInv.AllowDrop = true;
                        pp.borderCac.BorderBrush = color;
                        break;
                }
                //image qui suit le curseur http://stackoverflow.com/questions/3129443/wpf-4-drag-and-drop-with-visual-element-as-cursor
                DataObject dragData = new DataObject("image", data);
                CreateDragDropWindow(data.imgItem);
                var effet = DragDrop.DoDragDrop(parent, dragData, DragDropEffects.Move); // effectue le drag N drop
                if (effet == DragDropEffects.None)
                {//drop fail
                    if (this._dragdropWindow != null)
                    {
                        this._dragdropWindow.Close();
                        this._dragdropWindow = null;
                    }
                    #region Pouf Transparent !
                    pp.borderCape.BorderBrush = Brushes.Transparent;
                    pp.borderCoiffe.BorderBrush = Brushes.Transparent;
                    pp.borderBotte.BorderBrush = Brushes.Transparent;
                    pp.borderCeinture.BorderBrush = Brushes.Transparent;
                    pp.borderAno1.BorderBrush = Brushes.Transparent;
                    pp.borderAno2.BorderBrush = Brushes.Transparent;
                    pp.borderAmu.BorderBrush = Brushes.Transparent;
                    pp.borderCac.BorderBrush = Brushes.Transparent;
                    #endregion //remet les contour transparent
                }
            }
        }

        /// <summary>
        /// suis la position du curseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            // update the position of the visual feedback item
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            this._dragdropWindow.Left = w32Mouse.X;
            this._dragdropWindow.Top = w32Mouse.Y;
        }
        /// <summary>
        /// crée l'image qui suis le curseur une window
        /// </summary>
        /// <param name="dragElement"></param>
        private void CreateDragDropWindow(Visual dragElement)
        {
            this._dragdropWindow = new Window();

            #region window design
            _dragdropWindow.WindowStyle = WindowStyle.None;
            _dragdropWindow.AllowsTransparency = true;
            _dragdropWindow.AllowDrop = false;
            _dragdropWindow.Background = null;
            _dragdropWindow.IsHitTestVisible = false;
            _dragdropWindow.SizeToContent = SizeToContent.WidthAndHeight;
            _dragdropWindow.Topmost = true;
            _dragdropWindow.ShowInTaskbar = false;
            #endregion  // change le style de la window pour que seulement l'image soit visible

            Rectangle r = new Rectangle();
            r.Width = ((FrameworkElement)dragElement).ActualWidth;
            r.Height = ((FrameworkElement)dragElement).ActualHeight;
            r.Fill = new VisualBrush(dragElement);
            this._dragdropWindow.Content = r;

            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            this._dragdropWindow.Left = w32Mouse.X;
            this._dragdropWindow.Top = w32Mouse.Y;
            this._dragdropWindow.Show();
            //affiche la window
        }

        /// <summary>
        /// renvoi l'information de ta position dans la listbox
        /// </summary>
        /// <param name="source"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private static object GetDataFromListBox(ListBox source, Point point)
        {// renvoie l'information a une position dans la listBox
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    if (element == source)
                        return null;
                }
                if (data != DependencyProperty.UnsetValue)
                    return data;
            }
            return null;
        }
        private void cboTrieInventaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {// lorsque l'inventaire est triée
            PagePerso pp = (w.tCPerso.SelectedContent as PagePerso);

            if (((ComboBox)sender).SelectedIndex != -1)
            {
                string type = ((ComboBox)sender).SelectedValue.ToString();

                LstInventaire.Clear();

                foreach (Equipement item in Player.Inventaire)
                    if (type == "Tous" && item.Quantite - item.QuantiteEquipe != 0)// si le trie est tous
                    {
                        ImageItem i = new ImageItem(item, item.Quantite - item.QuantiteEquipe); // crée un image et un label de quantité
                        i.PreviewMouseDoubleClick += image_desc;
                        i.imgItem.PreviewMouseRightButtonDown += imgInv_PreviewMouseRightButtonDown;
                        LstInventaire.Add(i);
                    }
                    else
                    {
                        if (item.Type == type && item.Quantite - item.QuantiteEquipe != 0)
                        {
                            ImageItem i = new ImageItem(item, item.Quantite - item.QuantiteEquipe);
                            i.PreviewMouseDoubleClick += image_desc;
                            i.imgItem.PreviewMouseRightButtonDown += imgInv_PreviewMouseRightButtonDown;
                            LstInventaire.Add(i);
                        }
                    }
                if (LstInventaire.Count <= 4 * 4)// s'il a - de 16 articles
                    lbxInventaire.Style = (Style)FindResource("RowFix");// je met le style pour que les images s'affiche bien
                else
                    lbxInventaire.Style = (Style)FindResource("RowOverflow");
            }
        }
        #region Truc pour invoquer des squelettes
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]

        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        #endregion
        /// <summary>
        /// lors d'un double click d'un image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_desc(object sender, MouseButtonEventArgs e)
        {
            string i = convertPathToNoItem((sender as ImageItem).imgItem.Source.ToString());

            if (i != "vide")
            {// sur un double click j'affiche les info de l'article
                (w.tCPerso.SelectedContent as PagePerso).itmCtrlDesc.Items.Clear();
                (w.tCPerso.SelectedContent as PagePerso).itmCtrlDesc.Items.Add(new DescItem(Player.Inventaire.First(x => x.NoImg == i)));
            }
        }
        /// <summary>
        /// Regénere la liste de l'inventaire
        /// </summary>
        public void refreshInv()
        {
            int i = cboTrieInventaire.SelectedIndex;
            cboTrieInventaire.SelectedIndex = -1;
            cboTrieInventaire.SelectedIndex = i;
        }
        /// <summary>
        /// converti le path pour avoir juste le numero de l'article
        /// </summary>
        /// <param name="path"> le path de l'image</param>
        /// <returns></returns>
        private string convertPathToNoItem(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path.Split('/').Last());
        }

        /// <summary>
        /// Lorsqu'on effecte un clic droit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgInv_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = FindResource("cmClick") as ContextMenu;// je cherche le context menu dans le XAML
            cm.PlacementTarget = sender as Button;
            cm.DataContext = sender as Image;
            cm.IsOpen = true;// j'ouvre le menu contextuel
        }
        /// <summary>
        /// l'interaction du bouton vendre du menu contextuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickVendre(object sender, RoutedEventArgs e)
        {// il vend l'article et l'enleve de son inventaire
            Image item = ((sender as MenuItem).Parent as ContextMenu).DataContext as Image;
            Equipement equiper = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(item.Source.ToString()));
            float k = equiper.Prix * (float)0.8;
            MessageBoxResult m = MessageBox.Show("Voulez vous vraiment vendre l'objet : " + equiper.Nom + ". Au cout de " + (int)k + " Kamas ?", "Achat", MessageBoxButton.YesNo, MessageBoxImage.Information);// affichage d'un message box te demandant situ veut vendre ceci
            if (m == MessageBoxResult.Yes)
            {
                //enleve l'article
                Player.Inventaire.First(x => x.Nom == equiper.Nom).Quantite--;
                if (Player.Inventaire.First(x => x.Nom == equiper.Nom).Quantite == 0) // update ou delete 
                    bd.delete("DELETE FROM JoueursEquipements WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "')AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "')");
                else
                    bd.Update("UPDATE JoueursEquipements SET quantite= " + Player.Inventaire.First(x => x.Nom == equiper.Nom).Quantite.ToString() + " WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "');COMMIT;");

                Player.Kamas += (int)k;
                //update l'argent
                bd.Update("UPDATE  Joueurs SET  argent =  " + Player.Kamas.ToString() + " WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "';COMMIT;");
                refreshInv();
                lblArgent.Content = Player.Kamas;
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow).lblKamas.Content = Player.Kamas;
            }
        }
    }
}
