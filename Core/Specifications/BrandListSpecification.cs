using System;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecification : BaseSpecifications<Product, string>
{
    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}
