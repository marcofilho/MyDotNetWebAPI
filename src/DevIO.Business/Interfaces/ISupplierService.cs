using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        Task Add(Supplier supplier);
        Task Update(Supplier supplier);
        Task Remove(Guid id);
        Task<Supplier> GetById(Guid id);
        Task<IEnumerable<Supplier>> GetAll();

        Task UpdateAddress(Address address);

        Task<Supplier> GetSupplierAddress(Guid id);
        Task<Supplier> GetSupplierProductsAddress(Guid id);
    }
}
