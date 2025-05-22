using CompShop.Windows;
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

namespace CompShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        Frame _frame;
        public MainMenuPage(Frame FrmMain)
        {
            InitializeComponent();

            _frame = FrmMain;
        }

        private void btnStore_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StorePage());
        }
        private void btnComponents_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnStoreComp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}