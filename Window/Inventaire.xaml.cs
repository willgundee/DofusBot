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
using System.Windows.Shapes;
using test;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Inventaire.xaml
    /// </summary>
    public partial class Inventaire : Window
    {
        public Joueur Player { get; set; }
        public Inventaire(Joueur Player)
        {
            InitializeComponent();
            this.Player = Player;

        }
    }
}
