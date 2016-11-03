using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
using test;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Inventaire.xaml
    /// </summary>
    public partial class Inventaire : Window
    {
        public Window _dragdropWindow = null;
        ObservableCollection<ImageItem> LstInventaire;
        ObservableCollection<DescItem> LstDesc;
        ListBox dragSource = null;
        public Joueur Player { get; set; }
        private BDService bd = new BDService();
        private MainWindow w;
        //TODO: REFONTE INVENTAIRE  ajout bouton onclick dans page perso qui pop l'inventaire coté ,double clic désequipe drag drop de l'inventaire à fenetre mick, unselected tab close l'inventaire si pas fait 

        public Inventaire(Joueur Player)
        {

            InitializeComponent();
            LstInventaire = new ObservableCollection<ImageItem>();
            w = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow);
            this.Player = Player;
            List<string> type = new List<string>();
            type.Add("Tous");
            foreach (List<string> typeNom in bd.selection("SELECT nom FROM typesEquipements"))
                type.Add(typeNom[0]);
            cboTrieInventaire.ItemsSource = type;
            cboTrieInventaire.SelectedIndex = 0;
            lbxInventaire.ItemsSource = LstInventaire;
            (w.tCPerso.SelectedContent as PagePerso).itmCtrlDesc.ItemsSource = LstDesc;

        }
        /// <summary>
        /// le Drag du Drag&Drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxInventaire_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PagePerso pp = (w.tCPerso.SelectedContent as PagePerso);

            // DataContext = this;
            System.Windows.Controls.ListBox parent = (System.Windows.Controls.ListBox)sender;
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
            #endregion

            SolidColorBrush color = Brushes.Orange;
            if (data != null)
            {
                Equipement itemDrag = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(data.imgItem.Source.ToString()));
                switch (itemDrag.Type)
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
                System.Windows.DataObject dragData = new System.Windows.DataObject("image", data);
                CreateDragDropWindow(data.imgItem);
                var effet = DragDrop.DoDragDrop(parent, dragData, System.Windows.DragDropEffects.Move);
                if (effet == System.Windows.DragDropEffects.None)
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
                    #endregion
                }
            }
        }

        /// <summary>
        /// suis la position du curseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_GiveFeedback(object sender, System.Windows.GiveFeedbackEventArgs e)
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
            _dragdropWindow.WindowStyle = WindowStyle.None;
            _dragdropWindow.AllowsTransparency = true;
            _dragdropWindow.AllowDrop = false;
            _dragdropWindow.Background = null;
            _dragdropWindow.IsHitTestVisible = false;
            _dragdropWindow.SizeToContent = SizeToContent.WidthAndHeight;
            _dragdropWindow.Topmost = true;
            _dragdropWindow.ShowInTaskbar = false;

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
        }

        /// <summary>
        /// renvoi l'information de ta position dans la listbox
        /// </summary>
        /// <param name="source"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private static object GetDataFromListBox(System.Windows.Controls.ListBox source, Point point)
        {
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
        {
            PagePerso pp = (w.tCPerso.SelectedContent as PagePerso);

            if (((System.Windows.Controls.ComboBox)sender).SelectedIndex != -1)
            {
                string type = ((System.Windows.Controls.ComboBox)sender).SelectedValue.ToString();

                LstInventaire.Clear();

                foreach (Equipement item in Player.Inventaire)
                    if (type == "Tous" && item.Quantite - item.QuantiteEquipe != 0)
                    {
                        ImageItem i = new ImageItem(item, false, item.Quantite - item.QuantiteEquipe);
                        i.PreviewMouseDoubleClick += image_desc;
                        LstInventaire.Add(i);
                    }
                    else
                    {
                        if (item.Type == type && item.Quantite - item.QuantiteEquipe != 0)
                        {
                            ImageItem i = new ImageItem(item, false, item.Quantite - item.QuantiteEquipe);
                            i.PreviewMouseDoubleClick += image_desc;

                            LstInventaire.Add(i);
                        }
                    }
                if (LstInventaire.Count <= 3 * 6)
                    lbxInventaire.Style = (Style)FindResource("RowFix");
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

        private void image_desc(object sender, MouseButtonEventArgs e)
        {
            LstDesc.Clear();
            string nom = (((ImageItem)sender).imgItem).Name.Replace("_", " ");
            LstDesc.Add(new DescItem(new Equipement(bd.selection("SELECT * FROM Equipements WHERE nom ='" + nom + "'")[0], true, 0)));
        }

        private void TabItem_Selected_Inventaire(object sender, RoutedEventArgs e)
        {//TODO pas sur selected item
            refreshInv();
        }

        public void refreshInv()
        {
            int i = cboTrieInventaire.SelectedIndex;
            cboTrieInventaire.SelectedIndex = -1;
            cboTrieInventaire.SelectedIndex = i;
        }

        private string convertPathToNoItem(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path.Split('/').Last());
        }

        /// <summary>
        /// pour avoir la description d'un item qui est équiper sur soi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgInv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string nomEntite = (w.tCPerso.SelectedContent as TabItem).Header.ToString();

            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                LstDesc.Clear();
                if (convertPathToNoItem((sender as Image).Source.ToString()) != "vide")
                    LstDesc.Add(new DescItem(new Equipement(bd.selection("SELECT * FROM Equipements WHERE noImage =" + Convert.ToInt32(convertPathToNoItem((sender as Image).Source.ToString())))[0], true, 0)));
                lbxInventaire.SelectedIndex = -1;
            }
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 1)
            {
                Image data = (Image)sender;
                Equipement itemDrag = null;
                if (convertPathToNoItem(data.Source.ToString()) != "vide")
                    itemDrag = Player.LstEntites.First(x => x.Nom == nomEntite).LstEquipements.First(x => x.NoImg == convertPathToNoItem(data.Source.ToString()));
                lbxInventaire.AllowDrop = true;
                System.Windows.DataObject dragData = new System.Windows.DataObject("image", data);
                CreateDragDropWindow(data);
                var effet = DragDrop.DoDragDrop(data, dragData, System.Windows.DragDropEffects.Move);
                if (effet == System.Windows.DragDropEffects.None)
                {//drop fail
                    if (this._dragdropWindow != null)
                    {
                        this._dragdropWindow.Close();
                        this._dragdropWindow = null;
                    }
                }

                //System.Windows.Forms.MessageBox.Show(e.ClickCount.ToString());
            }

        }


    }
}
