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
    public class TagsService
    {
        private readonly Pract15Context _db = DBService.Instance.Context;
        public static ObservableCollection<Tag> Items { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public TagsService()
        {
            GetAll();
        }
        public void Add(Tag cat)
        {
            var _cat = new Tag
            {
                Name = cat.Name,
            };
            _db.Add<Tag>(_cat);
            Commit();
            Items.Add(_cat);
        }

        public void GetAll()
        {
            var groups = _db.Tags.ToList();
            Items.Clear();
            foreach (var group in groups)
                Items.Add(group);
        }

        public void Remove(Tag prod)
        {
            _db.Remove<Tag>(prod);
            if (Commit() > 0)
                if (Items.Contains(prod))
                    Items.Remove(prod);
        }

    }
}
