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
    class ProductTagsService
    {
        private readonly Pract15Context _db = DBService.Instance.Context;
        public ObservableCollection<ProductTag> ProductTags { get; set; } = new();
        public int Commit() => _db.SaveChanges();
        public void Add(ProductTag prTag)
        {
            foreach (var item in ProductTags)
            {
                if (prTag.Product == item.Product)
                {
                    MessageBox.Show("Этот продукт уже добавлен");
                    return;
                }
            }

            var _prodTag = new ProductTag
            {
                ProductId = prTag.ProductId,
                TagId = prTag.TagId,

                Product = prTag.Product,
                Tag = prTag.Tag,
            };
            _db.Add<ProductTag>(_prodTag);
            _db.SaveChanges();
        }

        public void GetAll(int? groupId = null)
        {
            if (groupId != null)
            {
                var users = _db.ProductTags
                    .Include(x => x.Product)
                    .ThenInclude(u => u.Category)
                    .ToList();

                ProductTags.Clear();

                foreach (var user in users)
                {
                    ProductTags.Add(user);
                }
            }
        }
        public void Remove(ProductTag InterestGroup)
        {
            _db.Remove<ProductTag>(InterestGroup);
            if (Commit() > 0)
                if (ProductTags.Contains(InterestGroup))
                    ProductTags.Remove(InterestGroup);
        }
    }
}
