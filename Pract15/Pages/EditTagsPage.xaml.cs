using Pract15.Models;
using Pract15.Service;
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

namespace Pract15.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditTagsPage.xaml
    /// </summary>
    public partial class EditTagsPage : Page
    {
        public ProductTagsService productTagsService { get; set; } = new();
        public ProductTag? productTag { get; set; } = new();
        public TagsService tagsService { get; set; } = new();
        public Tag? tag { get; set; } = new();
        
        private Product? _selectedProduct;
         public EditTagsPage(Product? product = null)
        {
            InitializeComponent();
           _selectedProduct = product;
            productTagsService.GetAllTagsForUser(_selectedProduct.Id);
        }

        private void add_group_user(object sender, RoutedEventArgs e)
        {
            if (tag != null && _selectedProduct != null)
            {
                var userInterestGroup = new ProductTag
                {
                    Product = _selectedProduct,
                    Tag = tag,
                    ProductId = _selectedProduct.Id,
                    TagId = tag.Id
                };

                productTagsService.Add(userInterestGroup);

                productTagsService.GetAllTagsForUser(_selectedProduct.Id);
            }
        }

        private void remove_group_user(object sender, RoutedEventArgs e)
        {
            if (productTag != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить группу интереса?",
                "Удалить группу?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    productTagsService.Remove(productTag);
                }
            }
            else
            {
                MessageBox.Show("Выберите группу интереса для удаления", "Выберите группу интереса",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Title = "Редактирование тегов";
            }
        }
    }
}
