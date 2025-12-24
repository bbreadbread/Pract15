using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract15.Service
{
    class CategoriesService
    {
        private readonly Pract15Context _db = DBService.Instance.Context;
        public static ObservableCollection<Category> Items { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public CategoriesService()
        {
            GetAll();
        }

        public void Add(Category cat)
        {
            var _cat = new Category
            {
                Name = cat.Name,
            };
            _db.Add<Category>(_cat);
            Commit();
            Items.Add(_cat);
        }

        public void GetAll()
        {
            var groups = _db.Categories.ToList();
            Items.Clear();
            foreach (var group in groups)
                Items.Add(group);
        }

        public void Remove(Category prod)
        {
            _db.Remove<Category>(prod);
            if (Commit() > 0)
                if (Items.Contains(prod))
                    Items.Remove(prod);
        }
    }
}
