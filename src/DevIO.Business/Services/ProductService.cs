using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsSuppliers(Guid id)
        {
            return await _productRepository.GetProductsBySupplier(id);
        }

        public async Task<IEnumerable<Product>> GetProductsSuppliers()
        {
            return await _productRepository.GetProductsSuppliers();
        }

        public Task<Product> GetProductSupplier(Guid supplierId)
        {
            return _productRepository.GetProductSupplier(supplierId);
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.AddAsync(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.UpdateAsync(product);
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.RemoveAsync(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
