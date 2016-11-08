using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour SortList.xaml
    /// </summary>
    public partial class SortList : UserControl
    {
        private Sort Sorts ;
        public SortList(List<string> sorts)
        {
            InitializeComponent();

            Sorts = new Sort(sorts);
            int con = sorts.Count();

            BitmapImage path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/spells/55/sort_" + Sorts.NoImage + ".png"));

                ImgSort.Source = path;
               
                 lblNomSort.Content = sorts[3] ;                        
        }        
    }
}
