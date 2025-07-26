using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository,
                               IAddressRepository addressRepository,
                               INotificator notificator) : base(notificator)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier> GetById(Guid id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }


        public async Task<Supplier> GetSupplierAddress(Guid id)
        {
            return await _supplierRepository.GetSupplierAddress(id);
        }

        public async Task<Supplier> GetSupplierProductsAddress(Guid id)
        {
            return await _supplierRepository.GetSupplierProductsAddress(id);
        }

        public async Task<bool> Add(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier) &&
               !ExecuteValidation(new AddressValidation(), supplier.Address)) return false;

            if (_supplierRepository.FindAsync(s => s.Document == supplier.Document).Result.Any())
            {
                Notify("A supplier with this document already exists.");
                return false;
            }

            await _supplierRepository.AddAsync(supplier);
            return true;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return false;

            if (_supplierRepository.FindAsync(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any())
            {
                Notify("A supplier with this document already exists.");
                return false;
            }

            await _supplierRepository.UpdateAsync(supplier);
            return true;
        }

        public async Task<bool> Remove(Guid id)
        {
            if (_supplierRepository.GetSupplierProductsAddress(id).Result.Products.Any())
            {
                Notify("The supplier has registered products!");
                return false;
            }

            var address = await _addressRepository.GetAddressBySupplier(id);

            if (address != null)
            {
                await _addressRepository.RemoveAsync(address.Id);
            }

            await _supplierRepository.RemoveAsync(id);
            return true;
        }

        public async Task UpdateAddress(Address address)
        {
            if (ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.UpdateAsync(address);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }

    }
}
