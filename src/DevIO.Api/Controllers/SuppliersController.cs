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
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierService.GetAll());
            return Ok(suppliers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var suppliers = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierService.GetAll());
            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierService.Add(supplier);

            return CreatedAtAction(nameof(GetSuppliers), new { id = supplier.Id }, supplierDto);
        }
    }
}
