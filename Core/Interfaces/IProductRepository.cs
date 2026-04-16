using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type,string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetProductBrandsAsync();
    Task<IReadOnlyList<string>> GetProductTypesAsync();
    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}
