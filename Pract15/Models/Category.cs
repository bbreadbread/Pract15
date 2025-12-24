using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pract15.Models;

public partial class Category : ObservableObject
{
    private int _id;
    private string? _name;
    private ObservableCollection<Product> _products = new();

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public virtual ObservableCollection<Product> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }
}
