using AutoMapper;
using DevIO.Api.DTOs;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/suppliers")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierService supplierService, IMapper mapper, INotificator notificator) : base(notificator)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierService.GetAll());
            return Ok(suppliers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var suppliers = await GetSupplierProductsAddress(id);
            if (suppliers == null) return NotFound();

            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var supplier = _mapper.Map<Supplier>(supplierDto);
            var result = await _supplierService.Add(supplier);

            if (!result) return BadRequest();

            return Ok(supplier);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, SupplierDto supplierDto)
        {
            if (id != supplierDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var supplier = _mapper.Map<Supplier>(supplierDto);
            var result = await _supplierService.Update(supplier);

            if (!result) return BadRequest();

            return Ok(supplier);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var supplier = await GetSupplierAddress(id);
            if (supplier == null) return NotFound();

            var result = await _supplierService.Remove(supplier.Id);

            if (!result) return BadRequest();

            return Ok(supplier);
        }

        private async Task<SupplierDto> GetSupplierProductsAddress(Guid id)
        {
            var supplier = await _supplierService.GetSupplierProductsAddress(id);
            if (supplier == null) return null;
            
            return _mapper.Map<SupplierDto>(supplier);
        }

        private async Task<SupplierDto> GetSupplierAddress(Guid id)
        {
            var supplier = await _supplierService.GetSupplierAddress(id);
            if (supplier == null) return null;
            
            return _mapper.Map<SupplierDto>(supplier);
        }
    }
}
