using Microsoft.EntityFrameworkCore;
using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pract15.Service
{
    class ProductsService
    {
        private readonly Pract15Context _db = DBService.Instance.Context;
        public ObservableCollection<Product> Products { get; set; } = new();
        public int Commit() => _db.SaveChanges();

        public void Add(Product product)
        {
            foreach (var item in Products)
            {
                if (product.Id == item.Id)
                {
                    MessageBox.Show("Этот продукт уже существует");
                    return;
                }
            }

            var _groupUser = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Rating = product.Rating,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,

                Category = product.Category,
                Brand = product.Brand,

            };
            _db.Add<Product>(_groupUser);
            _db.SaveChanges();
        }

        public void GetAll(int? groupId = null)
        {
            if (groupId != null)
            {
                var users = _db.Products
                                   .Include(p => p.Category)
                                   .Include(u => u.Brand)
                                   .ToList();

                Products.Clear();

                foreach (var user in users)
                {
                    Products.Add(user);
                }
            }
        }

        public void GetAllBrand(string brand)
        {
            var userGroups = _db.Products
                                   .Include(u => u.Brand)
                                   .Where(t =>  t.Brand.Name == brand)
                                   .ToList();

            Products.Clear();

            foreach (var userGroup in userGroups)
            {
                Products.Add(userGroup);
            }
        }

        public void GetAllCategory(string category)
        {
            var prod = _db.Products
                                   .Include(u => u.Category)
                                   .Where(t => t.Category.Name == category)
                                   .ToList();

            Products.Clear();

            foreach (var p in prod)
            {
                Products.Add(p);
            }
        }

        public void Remove(Product prod)
        {
            _db.Remove<Product>(prod);
            if (Commit() > 0)
                if (Products.Contains(prod))
                    Products.Remove(prod);
        }
    }
}

