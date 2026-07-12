using Domain;

namespace Services;

public interface IInventoryService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    Product Add(Product product);
    Product? Update(int id, Product product);
    bool Delete(int id);
    IEnumerable<Product> GetByPrice(decimal threshold);
    IEnumerable<Product> GetBySupplier(int supplierId);
    IEnumerable<Product> SearchByName(string name);
}