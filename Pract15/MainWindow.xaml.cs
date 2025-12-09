using Microsoft.IdentityModel.Tokens;
using Pract15.Models;
using Pract15.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pract15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string filterBrand{ get; set; } = null!;
        public string filterCategory { get; set; } = null!;

        public string filterPriceIdFrom { get; set; } = null!;
        public string filterPriceIdTo { get; set; } = null!;


        public Pract15Context db = DBService.Instance.Context;
        public ObservableCollection<Product> products { get; set; } = new();
        public ICollectionView productsView { get; set; }
        public string searchQuery { get; set; } = null!;

        public void LoadList(object sender, EventArgs e)
        {
            products.Clear();
            foreach (var form in db.Products.ToList())
                products.Add(form);
        }

        public MainWindow()
        {
            productsView = CollectionViewSource.GetDefaultView(products);
            productsView.Filter = FilterForms;
            InitializeComponent();
        }

        public bool FilterForms(object obj)
        {
            if (obj is not Product)
                return false;
            var product = (Product)obj;

            if (searchQuery != null && !product.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (!filterPriceIdFrom.IsNullOrEmpty() && Convert.ToInt32(filterPriceIdFrom) > product.Price)
                return false;
            if (!filterPriceIdTo.IsNullOrEmpty() && Convert.ToInt32(filterPriceIdTo) < product.Price)
                return false;
            if(searchQuery != null && !product.Brand.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (searchQuery != null && !product.Category.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                return false;
            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            productsView.Refresh();
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsView.SortDescriptions.Clear();
            var cb = (ComboBox)sender;
            var selected = (ComboBoxItem)cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Name":
                    productsView.SortDescriptions.Add(new SortDescription("Name",
                    ListSortDirection.Ascending));
                    break;
                case "Price":
                    productsView.SortDescriptions.Add(new SortDescription("Price",
                    ListSortDirection.Ascending));
                    break;
                case "Stock":
                    productsView.SortDescriptions.Add(new SortDescription("Stock",
                    ListSortDirection.Ascending));
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
    }
}