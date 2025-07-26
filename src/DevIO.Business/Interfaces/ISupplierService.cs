using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        Task<bool> Add(Supplier supplier);
        Task<bool> Update(Supplier supplier);
        Task<bool> Remove(Guid id);
        Task<Supplier> GetById(Guid id);
        Task<IEnumerable<Supplier>> GetAll();

        Task UpdateAddress(Address address);

        Task<Supplier> GetSupplierAddress(Guid id);
        Task<Supplier> GetSupplierProductsAddress(Guid id);
    }
}
