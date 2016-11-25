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
        public ChatWindow(string user,string id)
        {
            InitializeComponent();
            Dispatcher.Invoke(new Action(() => Contenu.Content = new pageClavardage(user, true,id)));
        }
    }
}

