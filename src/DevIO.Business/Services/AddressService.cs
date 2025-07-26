using DevIO.Business.Interfaces;
using DevIO.Business.Models;

namespace DevIO.Business.Services
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository, INotificator notificator) : base(notificator)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await _addressRepository.GetAddressBySupplier(supplierId);
        }

        public async Task<Address> GetById(Guid id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
        }
    }
}
