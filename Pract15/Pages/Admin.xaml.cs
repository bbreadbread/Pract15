using Azure;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private ProductsService _productsService = new();
        public Product _product = new();
        public Product _originalProduct = new();

        private CategoriesService _categoriesService = new();
        public Category _category = new();
        public Category _originalCategory = new();

        private BrandsService _brandService = new();
        public Brand _brand = new();
        public Brand _originalBrand = new();

        private TagsService _tagService = new();
        public Tag _tag = new();
        public Tag _originalTag = new();

        private ProductTagsService _productTagsService = new();

        private bool IsProducts = false;
        private bool IsCategories = false;
        private bool IsTags = false;
        private bool IsBrands = false;

        public Pract15Context db = DBService.Instance.Context;
        public ObservableCollection<object> Items { get; set; } = new();
        public Object? selectedItem { get; set; } = null;
        public string searchQuery { get; set; } = null!;

        public Admin()
        {
            InitializeComponent();
        }
        private void LoadList(object sender, EventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Title = "Админская панель";
            }

            Items.Clear();

            if (IsProducts)
                foreach (var p in db.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category))
                    Items.Add(p);
            else if (IsCategories)
                foreach (var c in db.Categories)
                    Items.Add(c);
            else if (IsTags)
                foreach (var t in db.Tags)
                    Items.Add(t);
            else if (IsBrands)
                foreach (var b in db.Brands)
                    Items.Add(b);
        }

        public bool FilterForms(object obj)
        {
            if (obj is not Product)
                return false;
            var product = (Product)obj;

            if (searchQuery != null && !product.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                return false;

            return true;
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductPanel.DataContext = _product;

            IsProducts = true;
            IsCategories = false;
            IsBrands = false;
            IsTags = false;
            LoadList(sender, e);

            ProductPanel.Visibility = Visibility.Visible;
            BrandPanel.Visibility = Visibility.Collapsed;
            TagPanel.Visibility = Visibility.Collapsed;
            CategoryPanel.Visibility = Visibility.Collapsed;
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            CategoryPanel.DataContext = _category;

            IsProducts = false;
            IsCategories = true;
            IsBrands = false;
            IsTags = false;
            LoadList(sender, e);

            ProductPanel.Visibility = Visibility.Collapsed;
            BrandPanel.Visibility = Visibility.Collapsed;
            TagPanel.Visibility = Visibility.Collapsed;
            CategoryPanel.Visibility = Visibility.Visible;
        }

        private void Tag_Click(object sender, RoutedEventArgs e)
        {
            TagPanel.DataContext = _tag;

            IsProducts = false;
            IsCategories = false;
            IsBrands = false;
            IsTags = true;
            LoadList(sender, e);

            ProductPanel.Visibility = Visibility.Collapsed;
            BrandPanel.Visibility = Visibility.Collapsed;
            TagPanel.Visibility = Visibility.Visible;
            CategoryPanel.Visibility = Visibility.Collapsed;
        }

        private void Brand_Click(object sender, RoutedEventArgs e)
        {
            BrandPanel.DataContext = _brand;

            IsProducts = false;
            IsCategories = false;
            IsBrands = true;
            IsTags = false;
            LoadList(sender, e);

            ProductPanel.Visibility = Visibility.Collapsed;
            BrandPanel.Visibility = Visibility.Visible;
            TagPanel.Visibility = Visibility.Collapsed;
            CategoryPanel.Visibility = Visibility.Collapsed;
        }

        private void ButtonToMagazinePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Magazine(true));
        }

        private void FormsList_SelectDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsProducts)
            {
                p = (Product)selectedItem;
                _originalProduct = (Product)selectedItem;
                _product = new Product
                {
                    Id = _originalProduct.Id,
                    Name = _originalProduct.Name,
                    Description = _originalProduct.Description,
                    Price = _originalProduct.Price,
                    Stock = _originalProduct.Stock,
                    Rating = _originalProduct.Rating,
                    CreatedAt = _originalProduct.CreatedAt,
                    CategoryId = _originalProduct.CategoryId,
                    BrandId = _originalProduct.BrandId,
                    Category = _originalProduct.Category,
                    Brand = _originalProduct.Brand,
                };
                ProductPanel.DataContext = _product;
            }
            else if (IsCategories)
            {
                _originalCategory = (Category)selectedItem;
                _category = new Category
                {
                    Id = _originalCategory.Id,
                    Name = _originalCategory.Name,
                };
                CategoryPanel.DataContext = _category;
            }
            else if (IsBrands)
            {
                _originalBrand = (Brand)selectedItem;
                _brand = new Brand
                {
                    Id = _originalBrand.Id,
                    Name = _originalBrand.Name,
                };
                BrandPanel.DataContext = _brand;
            }
            else if (IsTags)
            {
                _originalTag = (Tag)selectedItem;
                _tag = new Tag
                {
                    Id = _originalTag.Id,
                    Name = _originalTag.Name,
                };
                TagPanel.DataContext = _tag;
            }
            else
            {
                MessageBox.Show("Хрень");
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

            if (IsProducts)
            {
                ProductPanel.DataContext = _product;
                bool hasError = Validation.GetHasError(tbName) ||
                Validation.GetHasError(tbDescription) ||
                Validation.GetHasError(tbPrice) ||
                Validation.GetHasError(tbStock);

                if (!hasError)
                {
                    _originalProduct.Name = _product.Name;
                    _originalProduct.Description = _product.Description;
                    _originalProduct.Price = _product.Price;
                    _originalProduct.Stock = _product.Stock;
                    _originalProduct.BrandId = _product.BrandId;
                    _originalProduct.CategoryId = _product.CategoryId;
                    _productsService.Commit();
                }
            }
            else if (IsCategories)
            {
                CategoryPanel.DataContext = _category;
                if (!Validation.GetHasError(tbNameCat))
                {
                    _originalCategory.Name = _category.Name;
                    _categoriesService.Commit();
                }
            }
            else if (IsBrands)
            {
                BrandPanel.DataContext = _brand;
                if (!Validation.GetHasError(tbNameBrand))
                {
                    _originalBrand.Name = _brand.Name;
                    _brandService.Commit();
                }
            }
            else if (IsTags)
            {
                TagPanel.DataContext = _tag;
                if (!Validation.GetHasError(tbNameTag))
                {
                    _originalTag.Name = _tag.Name;
                    _tagService.Commit();
                }
            }

            LoadList(sender, e);

        }

        private void AddClick(object sender, RoutedEventArgs e)
        {

            if (IsProducts)
            {
                if (string.IsNullOrWhiteSpace(_product.Name) ||
            string.IsNullOrWhiteSpace(_product.Description) ||
            _product.Price <= 0 || _product.CategoryId == 0 || _product.BrandId == 0)
                {
                    MessageBox.Show("Заполните все обязательные поля продукта");
                    return;
                }
                if(_product == null)
                    {
                        MessageBox.Show("Заполните все поля");
                    }

                bool hasError = Validation.GetHasError(tbName) ||
            Validation.GetHasError(tbDescription) ||
            Validation.GetHasError(tbPrice) ||
            Validation.GetHasError(tbStock);

                if (!hasError)
                {
                    ProductPanel.DataContext = _product;

                   
                    cbCat.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
                    cbBrand.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();

                    _product.CreatedAt = DateTime.Now.Date;
                    _product.Rating = 0.0;

                    _productsService.Add(_product);
                }
            }
            else if (IsCategories)
            {
                if (_category == null)
                {
                    MessageBox.Show("Нельзя добавить пустую категорию");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_category.Name))
                {
                    MessageBox.Show("Введите название категории");
                    return;
                }

                if (!Validation.GetHasError(tbNameCat))
                {
                    CategoryPanel.DataContext = _category;
                    _categoriesService.Add(_category);
                }
            }
            else if (IsBrands)
            {
                if (_brand == null)
                {
                    MessageBox.Show("Нельзя добавить пустой бренд");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_brand.Name))
                {
                    MessageBox.Show("Введите название бренда");
                    return;
                }

                if (!Validation.GetHasError(tbNameBrand))
                {
                    BrandPanel.DataContext = _brand;
                    _brandService.Add(_brand);
                }
            }
            else if (IsTags)
            {
                if (_tag == null)
                {
                    MessageBox.Show("Нельзя добавить пустой тег");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_tag.Name))
                {
                    MessageBox.Show("Введите название тега");
                    return;
                }

                if (!Validation.GetHasError(tbNameTag))
                {
                    TagPanel.DataContext = _tag;
                    _tagService.Add(_tag);
                }
            }
            else
            {
                MessageBox.Show("Выберите тип элемента для добавления");
                return;
            }

            LoadList(sender, e);
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (selectedItem != null)
            {
                if (MessageBox.Show("Вы действительно хотите продолжить удаление?",
                    "Вы уверены?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (IsProducts)
                    {
                        _originalProduct = (Product)selectedItem;

                        _productTagsService.RemoveAllByProductId(_originalProduct.Id);

                        _productsService.Remove(_originalProduct);
                    }
                    else if (IsCategories)
                    {
                        _originalCategory = (Category)selectedItem;
                        if (_productsService.IsExistCategory(_originalCategory.Id) == false)
                            _categoriesService.Remove(_originalCategory);
                        else MessageBox.Show("Отказано в удалении. Существуют записи с данной категорией");
                    }
                    else if (IsBrands)
                    {
                        _originalBrand = (Brand)selectedItem;
                        if (_productsService.IsExistBrand(_originalBrand.Id) == false)
                            _brandService.Remove(_originalBrand);
                        else MessageBox.Show("Отказано в удалении. Существуют записи с данным брендом");
                    }
                    else if (IsTags)
                    {
                        _originalTag = (Tag)selectedItem;

                        if (_productsService.IsExistTag(_originalTag.Id) == false)
                            _tagService.Remove(_originalTag);
                        else MessageBox.Show("Отказано в удалении. Существуют записи с данным тегом");
                    }
                    LoadList(sender, e);
                }
            }
            else MessageBox.Show("Не выбран предмет удаления");
        }
        Product p;
        private void EditTagsClick(object sender, RoutedEventArgs e)
        {
            if (p == null) { return; }
            NavigationService.Navigate(new EditTagsPage(p));
        }

    }
}
