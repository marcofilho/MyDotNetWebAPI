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

            return CustomResponse(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fileName = Guid.NewGuid() + "_" + productDto.Image;
            if (!UploadFile(productDto.ImageUpload, fileName))
            {
                return CustomResponse(productDto);
            }

            productDto.Image = fileName;
            await _productService.Add(_mapper.Map<Product>(productDto));

            return CustomResponse(productDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                NotifyError("The provided ID does not match the product ID.");
                return CustomResponse(ModelState);
            }

            var productDtoUpdate = await GetProduct(id);
            if (productDtoUpdate == null) return NotFound();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (productDto.ImageUpload != null)
            {
                var fileName = Guid.NewGuid() + "_" + productDto.Image;
                if (!UploadFile(productDto.ImageUpload, fileName))
                {
                    return CustomResponse(ModelState);
                }

                productDtoUpdate.Image = fileName;
            }


            productDtoUpdate.Name = productDto.Name;
            productDtoUpdate.Description = productDto.Description;
            productDtoUpdate.Price = productDto.Price;
            productDtoUpdate.Active = productDto.Active;

            await _productService.Update(_mapper.Map<Product>(productDtoUpdate));

            return CustomResponse(productDtoUpdate);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productDto = await GetProduct(id);
            if (productDto == null) return NotFound();

            await _productService.Remove(id);
            return CustomResponse();
        }

        private bool UploadFile(string file, string fileName)
        {
            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Please provide an image!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(file);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("This file already exists!");
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
