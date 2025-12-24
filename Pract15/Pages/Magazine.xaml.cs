using Microsoft.EntityFrameworkCore;
using Pract15.Models;
using Pract15.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Pract15.Pages
{
    /// <summary>
    /// Логика взаимодействия для MagazinePage.xaml
    /// </summary>
    public partial class Magazine : Page
    {
        public string filterBrand { get; set; } = null!;
        public string filterCategory { get; set; } = null!;

        public string filterPriceIdFrom { get; set; } = null!;
        public string filterPriceIdTo { get; set; } = null!;


        public Pract15Context db = DBService.Instance.Context;
        public ObservableCollection<Product> products { get; set; } = new();

        

        public ICollectionView productsView { get; set; }
        public string searchQuery { get; set; } = null!;


        public Magazine(bool IsAdmin)
        {
            productsView = CollectionViewSource.GetDefaultView(products);
            productsView.Filter = FilterForms;
            InitializeComponent();

            if (IsAdmin == false)
                ButtonToAdminPage.Visibility = Visibility.Collapsed;
            else
                ButtonToAdminPage.Visibility = Visibility.Visible;
        }

        public void LoadList(object sender, EventArgs e)
        {
            products.Clear();
            foreach (var product in db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .ToList())
            {
                products.Add(product);
            }
        }

        public bool FilterForms(object obj)
        {
            if (obj is not Product)
                return false;
            var product = (Product)obj;

            if (searchQuery != null && !product.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!String.IsNullOrEmpty(filterPriceIdFrom) && Convert.ToInt32(filterPriceIdFrom) > product.Price)
                return false;
            if (!String.IsNullOrEmpty(filterPriceIdTo) && Convert.ToInt32(filterPriceIdTo) < product.Price)
                return false;

            if (!String.IsNullOrEmpty(filterBrand) && filterBrand != product.Brand.Name)
                return false;
            if (!String.IsNullOrEmpty(filterCategory) && filterCategory != product.Category.Name)
                return false;

            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            productsView.Refresh();
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cb) return;
            if (cb.SelectedItem is not ComboBoxItem item || item.Tag == null) return;

            productsView.SortDescriptions.Clear();

            switch (item.Tag.ToString())
            {
                case "Name":
                    productsView.SortDescriptions.Add(new SortDescription(nameof(Product.Name), ListSortDirection.Ascending));
                    break;

                case "Price":
                    var priceSort = PriceSort.SelectedIndex == 0
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
                    productsView.SortDescriptions.Add(new SortDescription(nameof(Product.Price), priceSort));
                    break;

                case "Stock":
                    var stockSort = StockSort.SelectedIndex == 0
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
                    productsView.SortDescriptions.Add(new SortDescription(nameof(Product.Stock), stockSort));
                    break;
            }
            productsView.Refresh();
        }

        private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsView.SortDescriptions.Clear();
            var cb = (ComboBox)sender;
            var selected = (ComboBoxItem)cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Category":
                    FilterPricePanel.Visibility = Visibility.Collapsed;
                    FilterBrandPanel.Visibility = Visibility.Collapsed;
                    FilterCategoryPanel.Visibility = Visibility.Visible;
                    break;
                case "Brand":
                    FilterPricePanel.Visibility = Visibility.Collapsed;
                    FilterBrandPanel.Visibility = Visibility.Visible;
                    FilterCategoryPanel.Visibility = Visibility.Collapsed;
                    break;
                case "Price":
                    FilterPricePanel.Visibility = Visibility.Visible;
                    FilterBrandPanel.Visibility = Visibility.Collapsed;
                    FilterCategoryPanel.Visibility = Visibility.Collapsed;
                    break;
            }
            productsView.Refresh();
        }

        private void ButtonToAdminPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
    }
}
