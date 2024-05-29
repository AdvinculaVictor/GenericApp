using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenericApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using GenericApp.Domain.Repositories;

namespace GenericApp.CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Customer : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Microsoft.Extensions.Logging.ILogger<Cliente> _logger;

        public Customer(Microsoft.Extensions.Logging.ILogger<Cliente> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet,Authorize(Roles="Reader,Writer")]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            return unitOfWork.Cliente.GetAll().ToList();
        }

        [HttpGet("{id:int}"), Authorize(Roles="Writer,Reader")]
        public ActionResult<Cliente> GetCustomer(int id)
        {
            try
            {
                var result = unitOfWork.Cliente.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

    [HttpPost, Authorize(Roles = "Writer")]
    public ActionResult<Cliente> CreateCustomer(Cliente cliente)
    {
        try
        {
            if (cliente == null)
                return BadRequest();

            unitOfWork.Cliente.Add(cliente);
            return Ok(unitOfWork.Save());

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new customer record");
        }
    }    

    [HttpPut("{id:int}"),Authorize(Roles ="Writer")]
    public ActionResult<Cliente> UpdateCustomer(int id, Cliente cliente)
    {
        try
        {
            if (id != cliente.ClienteId)
                return BadRequest("Customer ID mismatch");

            var customerToUpdate = unitOfWork.Cliente.GetById(id);

            if (customerToUpdate == null)
                return NotFound($"Customer with Id = {id} not found");
            
            customerToUpdate.Calle = cliente.Calle;
            customerToUpdate.Colonia = cliente.Colonia;
            customerToUpdate.CP = cliente.CP;
            customerToUpdate.Email = cliente.Email;
            customerToUpdate.Estado = cliente.Estado;
            customerToUpdate.Municipio = cliente.Municipio;
            customerToUpdate.RazonSocial = cliente.RazonSocial;
            customerToUpdate.RFC = cliente.RFC;
            customerToUpdate.Telefono = cliente.Telefono;

            unitOfWork.Save();

            return Ok(unitOfWork.Cliente.GetById(cliente.ClienteId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error updating data {ex.Message}");
        }
    }    

[HttpDelete("{id:int}"), Authorize(Roles ="Writer")]
    public ActionResult<Cliente> DeleteCliente(int id)
    {
        try
        {
            var customerToDelete = unitOfWork.Cliente.GetById(id);

            if (customerToDelete == null)
            {
                return NotFound($"Customer with Id = {id} not found");
            }

            unitOfWork.Cliente.Remove(customerToDelete);
            return Ok(unitOfWork.Save());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }

    }
}