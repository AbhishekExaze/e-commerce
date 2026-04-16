using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void Add(Product product)
    {
        context.Products.Add(product);
    }

    public void Delete(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetProductBrandsAsync()
    {
        return await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type,string? sort)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(p => p.Brand == brand);
        }

        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(p => p.Type == type);
        }

        if (!string.IsNullOrEmpty(sort))
        {
            query = sort.ToLower() switch
            {
                "price" => query.OrderBy(p => p.Price),
                "name" => query.OrderBy(p => p.Name),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query
            };
        }


        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetProductTypesAsync()
    {
        return await context.Products.Select(p => p.Type).Distinct().ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
