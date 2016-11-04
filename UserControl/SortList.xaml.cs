﻿using System;
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
