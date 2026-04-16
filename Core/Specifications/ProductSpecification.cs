using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification: BaseSpecifications<Product>
{
    public ProductSpecification(string? brand, string? type, string? sort)
        : base(x => (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
                    (string.IsNullOrWhiteSpace(type) || x.Type == type))
    {
        if (!string.IsNullOrWhiteSpace(sort))
        {
            switch (sort.ToLower())
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
