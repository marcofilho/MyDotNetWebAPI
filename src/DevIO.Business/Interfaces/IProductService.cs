using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetAll();
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);

        Task<IEnumerable<Product>> GetProductsSuppliers(Guid id);
        Task<IEnumerable<Product>> GetProductsSuppliers();
        Task<Product> GetProductSupplier(Guid supplierId);
    }
}
