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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pinpon_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Oyuna_Basla_Click (Object Sender,RoutedEventArgs e){
            Game g1 =new Game ();
            this.Close();
            g1.Show();

        }
        public void Cikis_Click (Object Sender,RoutedEventArgs e){
            this.Close();

        }

    }
}
