using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pract15.Models;

public partial class Tag : ObservableObject
{
    private int _id;
    private string? _name;

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

    private ObservableCollection<ProductTag> _productTags;
    public ObservableCollection<ProductTag> ProductTags
    {
        get => _productTags;
        set => SetProperty(ref _productTags, value);
    }
}
