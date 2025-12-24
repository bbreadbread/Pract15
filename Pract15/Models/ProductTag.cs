using System;
using System.Collections.Generic;

namespace Pract15.Models;

public partial class ProductTag : ObservableObject
{
    private int _productId;
    private int _tagId;
    private Product _product = null!;
    private Tag _tag = null!;

    public int ProductId
    {
        get => _productId;
        set => SetProperty(ref _productId, value);
    }

    public int TagId
    {
        get => _tagId;
        set => SetProperty(ref _tagId, value);
    }

    public virtual Product Product
    {
        get => _product;
        set => SetProperty(ref _product, value);
    }

    public virtual Tag Tag
    {
        get => _tag;
        set => SetProperty(ref _tag, value);
    }
}
