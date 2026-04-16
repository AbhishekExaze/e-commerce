using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification: BaseSpecifications<Product>
{
    public ProductSpecification(ProductSpecParams productParams)
     : base(x => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (productParams.Brands.Count == 0 || productParams.Brands.Contains(x.Brand)) &&
            (productParams.Types.Count == 0 || productParams.Types.Contains(x.Type))
            )
    {
        ApplyPaging((productParams.PageIndex - 1) * productParams.PageSize, productParams.PageSize);
        if (!string.IsNullOrWhiteSpace(productParams.Sort))
        {
            switch (productParams.Sort.ToLower())
            {
                case "price":
                    AddOrderBy(p => p.Price);
                    break;
                case "name":
                    AddOrderBy(p => p.Name);
                    break;
                case "price_desc":
                    AddOrderByDescending(p => p.Price);
                    break;
                case "name_desc":
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}
