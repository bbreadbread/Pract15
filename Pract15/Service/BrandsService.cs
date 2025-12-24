using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract15.Service
{
    class BrandsService
    {
        private readonly Pract15Context _db = DBService.Instance.Context;
        public static ObservableCollection<Brand> Items { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public BrandsService()
        {
            GetAll();
        }
        public void Add(Brand cat)
        {
            var _cat = new Brand
            {
                Name = cat.Name,
            };
            _db.Add<Brand>(_cat);
            Commit();
            Items.Add(_cat);
        }
        public void GetAll()
        {
            var groups = _db.Brands.ToList();
            Items.Clear();
            foreach (var group in groups)
                Items.Add(group);
        }

        public void Remove(Brand prod)
        {
            _db.Remove<Brand>(prod);
            if (Commit() > 0)
                if (Items.Contains(prod))
                    Items.Remove(prod);
        }
    }
}
