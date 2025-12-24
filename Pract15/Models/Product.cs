using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pract15.Models;

public partial class Product : ObservableObject
{
    private int _id;
    private string? _name;

    private string? _description;
    private double? _price;
    private double? _stock;

    private double? _rating;
    private string? _createdAt;
    private int? _categoryId;

    private int? _brandId;
    private Brand? _brand;
    private Category? _category;

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

    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public double? Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public double? Stock
    {
        get => _stock;
        set => SetProperty(ref _stock, value);
    }

    public double? Rating
    {
        get => _rating;
        set => SetProperty(ref _rating, value);
    }

    public string? CreatedAt
    {
        get => _createdAt;
        set => SetProperty(ref _createdAt, value);
    }

    public int? CategoryId
    {
        get => _categoryId;
        set => SetProperty(ref _categoryId, value);
    }

    public int? BrandId
    {
        get => _brandId;
        set => SetProperty(ref _brandId, value);
    }

    public virtual Brand? Brand
    {
        get => _brand;
        set => SetProperty(ref _brand, value);
    }

    public virtual Category? Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    private ObservableCollection<ProductTag> _productTags;
    public ObservableCollection<ProductTag> ProductTags
    {
        get => _productTags;
        set => SetProperty(ref _productTags, value);
    }
}