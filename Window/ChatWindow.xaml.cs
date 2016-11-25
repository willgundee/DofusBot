using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    /// 



    public partial class ChatWindow : Window
    {
        public pageClavardage pgCht;
        public ChatWindow(string user, string id)
        {
            InitializeComponent();
            pgCht = new pageClavardage(user, true, id);
            Dispatcher.Invoke(new Action(() => Contenu.Content = pgCht));
        }


    }
}

