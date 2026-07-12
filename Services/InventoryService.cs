using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.Include(p => p.Supplier).ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.Include(p => p.Supplier)
            .FirstOrDefault(p => p.Id == id);
    }

    public Product Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product? Update(int id, Product product)
    {
        var existing = _context.Products.Find(id);
        if (existing == null) return null;
        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        existing.SupplierId = product.SupplierId;
        _context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Product> GetByPrice(decimal price)
    {
        return _context.Products.Where(p => p.Price > price).ToList();
    }

    public IEnumerable<Product> GetBySupplier(int supplierId)
    {
        return _context.Products.Where(p => p.SupplierId == supplierId).ToList();
    }

    public IEnumerable<Product> SearchByName(string name)
    {
        return _context.Products.Where(p => p.Name.Contains(name)).ToList();
    }
}