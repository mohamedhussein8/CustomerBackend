using Api.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CustomerController(IUnitOfWork _unitOfWork,IMapper mapper)
        {
            this.unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var customerSpecs = new CustomerWithAddressSpecs();
            var result = await unitOfWork.Repository<Customer>().GetAllAsync(customerSpecs);
            if(result is null)
            {
                return BadRequest();
            }
            var customers = result.Select(e => mapper.Map<CustomerDTO>(e)).ToList();
            return Ok(customers);
        }

        [HttpGet ("{id:int}", Name = "getOneRoute")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customerSpecs = new CustomerWithAddressSpecs();
            var result = await unitOfWork.Repository<Customer>().GetByIdAsync(id, customerSpecs);
            var customer =   mapper.Map<CustomerDTO>(result);
            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = mapper.Map<CustomerDTO, Customer>(customerDTO);
                    await unitOfWork.Repository<Customer>().AddAsync(customer);
                    await unitOfWork.Commit();
                    string url = Url.Link("getOneRoute", new { id = customer.Id });
                    return Created(url, customer);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
      
            }
            StringBuilder errors = new StringBuilder();
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Append(error.ErrorMessage);
                }
            }
            return BadRequest(errors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var customerSpecs = new CustomerWithAddressSpecs();
                var checkCustomer = await unitOfWork.Repository<Customer>().GetByIdAsync(id, customerSpecs);

                if (checkCustomer is null)
                {
                    return NotFound();
                }
                else
                {
                    customerDTO.Id = id;
                    var customerUpdated = mapper.Map<CustomerDTO, Customer>(customerDTO);


                    foreach(var addres in checkCustomer.Addresses)
                    {
                        await unitOfWork.Repository<Addresses>().DeleteAsync(addres.Id);   
                    }
                    foreach (var address in customerDTO.Addresses)
                    {
                        Addresses addresses = new Addresses();
                             addresses.CustomerId = customerUpdated.Id;
                             addresses.Address = address;
                        await unitOfWork.Repository<Addresses>().AddAsync(addresses);
                        await unitOfWork.Commit();
                    }
                    await unitOfWork.Repository<Customer>().UpdateAsync(id, customerUpdated);
                    await unitOfWork.Commit();
                    return NoContent();
                }
            }
            StringBuilder errors = new StringBuilder();
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Append(error.ErrorMessage);
                }
            }
            return BadRequest(errors);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var checkCustomer = await unitOfWork.Repository<Customer>().DeleteAsync(id);

            if (checkCustomer is null)
            {
                return NotFound();
            }
            await unitOfWork.Commit();

            return Ok();
        }
    }
}
