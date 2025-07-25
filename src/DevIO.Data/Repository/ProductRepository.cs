using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DevIODbContext dbContext) : base(dbContext) { }

        public async Task<Product> GetProductSupplier(Guid id)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsSuppliers()
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Include(s => s.Supplier)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            return await FindAsync(p => p.SupplierId == supplierId);
        }
    }
}
