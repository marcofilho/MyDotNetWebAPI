using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DevIODbContext context) : base(context)
        {
        }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await _dbContext.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(a => a.SupplierId == supplierId);
        }
    }
}
