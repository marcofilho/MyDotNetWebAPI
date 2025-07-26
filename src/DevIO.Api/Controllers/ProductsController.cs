using AutoMapper;
using DevIO.Api.Dtos;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : MainController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper, INotificator notificator) : base(notificator)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productsDto = await GetProductsSuppliers();
            return CustomResponse(productsDto);

        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productDto = await GetProduct(id);
            if (productDto == null) return NotFound();

            return CustomResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fileName = Guid.NewGuid() + "_" + productDto.Image;

            if (!UploadFile(productDto.ImageUpload, fileName))
            {
                return CustomResponse();
            }

            productDto.Image = fileName;
            await _productService.Add(_mapper.Map<Product>(productDto));

            return CustomResponse(productDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productDto = await GetProduct(id);
            if (productDto == null) return NotFound();

            await _productService.Remove(id);
            return CustomResponse();
        }

        private bool UploadFile(string file, string imageName)
        {
            var imageDataByteArray = Convert.FromBase64String(file);

            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Please provide an image to the product!");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("An image with this name already exists!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            return true;
        }

        private async Task<IEnumerable<ProductDto>> GetProductsSuppliers()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productService.GetProductsSuppliers());
        }

        private async Task<ProductDto> GetProduct(Guid id)
        {
            return _mapper.Map<ProductDto>(await _productService.GetProductSupplier(id));
        }
    }
}
