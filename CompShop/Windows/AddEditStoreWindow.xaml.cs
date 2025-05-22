using CompShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompShop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditStoreWindow.xaml
    /// </summary>
    public partial class AddEditStoreWindow : Window
    {
        CompShopContext _db = new CompShopContext();

        Store _currentStore = new Store(),
             _store;


        static string _workingDirectoryPath = Directory.GetParent(
                                                Directory.GetParent(
                                                     Directory.GetParent(
                                                         Environment.CurrentDirectory).
                                                                           FullName).
                                                                        FullName).
                                                                    FullName;

        public AddEditStoreWindow(Store selectedStore)

        {
            InitializeComponent();
            _store = selectedStore;

            if (selectedStore != null)
            {
                _currentStore = selectedStore;
                txtBoxStoreID.IsEnabled = false;
            }
            else
            {
                Title = "Добавление магазина";
            }
            DataContext = _currentStore;
        }


        private void txtBoxStoreID_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateText(txtBoxStoreID);
        }

        private void txtBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateText(txtBoxStoreID, @"^[А-ЯA-ZЁ][a-яa-zё]+");
        }

        private void txtBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateText(txtBoxStoreID, @"^[А-ЯA-ZЁ][a-яa-zё]+");
        }

        private void txtBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateText(txtBoxPhone);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            #region № магазина
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentStore.StoreId)) ||
                string.IsNullOrEmpty(Convert.ToString(_currentStore.StoreId)))
            {
                errors.AppendLine("Введите корректный № магазина");
            }
            else if (_db.Stores.Local.Select(actor => actor.StoreId).ToList().
                                        Contains(_currentStore.StoreId))
            {
                errors.AppendLine("Магазин с таким № уже существует!");
            }
            #endregion

            #region Адрес
            if (string.IsNullOrWhiteSpace(_currentStore.Address) ||
               string.IsNullOrEmpty(_currentStore.Address))
            {
                errors.AppendLine("Введите корректный адрес");
            }
            #endregion

            #region Название
            if (string.IsNullOrWhiteSpace(_currentStore.StoreName) ||
               string.IsNullOrEmpty(_currentStore.StoreName))
            {
                errors.AppendLine("Введите корректное название");
            }

            else if (!Regex.IsMatch(_currentStore.StoreName, @"^[А-ЯA-ZЁ][a-яa-zё]+"))
            {
                errors.AppendLine("Название должно начинаться с заглавной буквы");
            }
            #endregion

            #region Номер телефона
            if (_currentStore.Phone != null)
            {
                _currentStore.Phone = Regex.Replace(_currentStore.Phone, @"\D", "");
            }

            if (string.IsNullOrWhiteSpace(_currentStore.Phone) ||
                string.IsNullOrEmpty(_currentStore.Phone))
            {
                errors.AppendLine("Введите корректный номер!");
            }
            else if (_db.Stores.Local.Select(actor => actor.Phone).ToList().Contains(_currentStore.Phone))
            {
                errors.AppendLine("Студент с таким номером уже существует!");
            }
            #endregion


            if (errors.Length > 0)
            {
                MessageBox.Show(
                    errors.ToString(),
                    "Заполните поля",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );

                return;
            }

            if (_store != _currentStore)
            {
                _db.Stores.Add(_currentStore);
            }
            else
            {
                _db.Entry(_currentStore).State = EntityState.Modified;
            }

            try
            {

                _db.SaveChanges();

                MessageBox.Show(
                "Информация сохранена",
                "Сохранение",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );

                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message.ToString(),
                    "Системная ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
            }
        }

        private static void ValidateText(TextBox textBox, string regexPattern)
        {
            if (Regex.IsMatch(textBox.Text, regexPattern))
            {
                textBox.BorderBrush = Brushes.Green;
            }
            else
            {
                textBox.BorderBrush = Brushes.Red;
            }
        }

        private static void ValidateText(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text) &&
                !string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Green;
            }
            else
            {
                textBox.BorderBrush = Brushes.Red;
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
