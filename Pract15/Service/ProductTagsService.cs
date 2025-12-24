using Microsoft.EntityFrameworkCore;
using Pract15.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pract15.Service
{
    public class ProductTagsService
    {
        private readonly Pract15Context _db = DBService.Instance.Context;
        public ObservableCollection<ProductTag> ProductTags { get; set; } = new();
        public int Commit() => _db.SaveChanges();
        public void Add(ProductTag prTag)
        {
            foreach (var item in ProductTags)
            {
                if (prTag.Tag == item.Tag)
                {
                    MessageBox.Show("Этот тег уже добавлен");
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
        public void Remove(ProductTag prTag)
        {
            _db.Remove<ProductTag>(prTag);
            if (Commit() > 0)
                if (ProductTags.Contains(prTag))
                    ProductTags.Remove(prTag);
        }

        public void RemoveAllByTagId(int tagId)
        {
            var toRemove = _db.ProductTags
                .Where(pt => pt.TagId == tagId)
                .ToList();
            if (!toRemove.Any()) return;

            _db.ProductTags.RemoveRange(toRemove);
            if (Commit() > 0)
            {
                foreach (var pt in toRemove)
                    ProductTags.Remove(pt);
            }
        }

        public void RemoveAllByProductId(int prodId)
        {
            var toRemove = _db.ProductTags
                .Where(pt => pt.ProductId == prodId)
                .ToList();
            if (!toRemove.Any()) return;

            _db.ProductTags.RemoveRange(toRemove);
            if (Commit() > 0)
            {
                foreach (var pt in toRemove)
                    ProductTags.Remove(pt);
            }
        }

        public void GetAllTagsForUser(int prodId)
        {
            var prodTags = _db.ProductTags
                               .Include(ug => ug.Product)
                               .Include(ug => ug.Tag)
                               .Where(ug => ug.ProductId == prodId)
                               .ToList();

            ProductTags.Clear();

            foreach (var pr in prodTags)
            {
                ProductTags.Add(pr);
            }
        }
    }
}
