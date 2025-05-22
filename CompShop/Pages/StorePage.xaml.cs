using CompShop.Models;
using Microsoft.EntityFrameworkCore;
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

namespace CompShop.Windows
{
    /// <summary>
    /// Логика взаимодействия для StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        CompShopContext _db = new CompShopContext();
        List<Store> _stores = new List<Store>();

        public StorePage()
        {
            InitializeComponent();
            UpdateStoreList();
        }
        private void UpdateStoreList()
        {
            _db.Stores.Load();
            _stores = _db.Stores.ToList();

            string request = txtBoxSearch.Text.
                                         Replace(" ", "").
                                         ToLower();

            List<Store> storeList = SearchStores(_stores, request);

            lViewStores.ItemsSource = storeList;

        }

        private List<Store> SearchStores(List<Store> stores, string request)
        {
            return stores.Where(s => (s.StoreName).
                                      ToLower().
                                      Contains(request)).
                                      ToList();
        }

        /// <summary>
        /// Изменение поскового запроса
        /// </summary>
        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateStoreList();
        }

        private void ShowAddOrEditWindow(Store store)
        {
            AddEditStoreWindow addEditStoreWindow = new AddEditStoreWindow(store);
            addEditStoreWindow.ShowDialog();

            UpdateStoreList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Store? editedStore = (sender as Button).DataContext as Store;

            ShowAddOrEditWindow(editedStore);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ShowAddOrEditWindow(null);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(
                    "Удалить запись?",
                    "Удаление записи",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                    );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Store? deletedStore = (sender as Button).DataContext as Store;
                    _db.Stores.Remove(deletedStore);

                    MessageBox.Show(
                        "Запись удалена",
                        "Информация",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                        );

                    _db.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show(
                        "Выберите запись для удаления",
                        "Предупреждение",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                        );
                }
            }
            UpdateStoreList();
        }
    }
}
