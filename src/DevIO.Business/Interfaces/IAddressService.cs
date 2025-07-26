using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IAddressService : IDisposable
    {
        Task<Address> GetById(Guid id);
        Task<Address> GetAddressBySupplier(Guid supplierId);
    }
}
