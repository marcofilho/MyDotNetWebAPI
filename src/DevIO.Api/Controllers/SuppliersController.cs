﻿using AutoMapper;
using DevIO.Api.Dtos;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/suppliers")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierService _supplierService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierService supplierService, IAddressService adressService, IMapper mapper, INotificator notificator) : base(notificator)
        {
            _supplierService = supplierService;
            _addressService = adressService;
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
        public async Task<IActionResult> Create(SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Add(_mapper.Map<Supplier>(supplierDto));

            return CustomResponse(supplierDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != supplierDto.Id)
            {
                NotifyError("The IDs informed do not match.");
                return CustomResponse(supplierDto);
            }

            await _supplierService.Update(_mapper.Map<Supplier>(supplierDto));

            return CustomResponse(supplierDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null) return NotFound();

            await _supplierService.Remove(supplier.Id);

            return CustomResponse();
        }

        [HttpGet("get-address/{id:guid}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var addressDto = _mapper.Map<AddressDto>(await _addressService.GetById(id));
            if (addressDto == null) return NotFound();

            return CustomResponse(addressDto);
        }

        [HttpPut("update-address/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressDto addressDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != addressDto.Id)
            {
                NotifyError("The IDs informed do not match.");
                return CustomResponse(addressDto);
            }

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressDto));

            return CustomResponse(addressDto);
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
