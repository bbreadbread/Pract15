using System;
using System.Collections.Generic;

namespace Pract15.Models;

public class ProductTag : ObservableObject
{
    private double? _productId;
    private double? _tagId;
    private Product _product;
    private Tag _tag;

    public double? ProductId
    {
        get => _productId;
        set => SetProperty(ref _productId, value);
    }

    public double? TagId
    {
        get => _tagId;
        set => SetProperty(ref _tagId, value);
    }

    public Product Product
    {
        get => _product;
        set => SetProperty(ref _product, value);
    }

    public Tag Tag
    {
        get => _tag;
        set => SetProperty(ref _tag, value);
    }
}
