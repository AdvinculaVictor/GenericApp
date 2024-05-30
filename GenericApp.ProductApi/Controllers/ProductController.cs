using Microsoft.AspNetCore.Mvc;
using GenericApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using GenericApp.Domain.Repositories;

namespace GenericApp.ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Product : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<Product> _logger;

        public Product(ILogger<Product> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet,Authorize(Roles="Readers,Writers")]
        public ActionResult<IEnumerable<Producto>> GetAll()
        {
            return unitOfWork.Producto.GetAll().ToList();
        }

        [HttpGet("{id:int}"), Authorize(Roles="Readers,Writers")]
        public ActionResult<Producto> GetProduct(int id)
        {
            try
            {
                var result = unitOfWork.Producto.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

    [HttpPost, Authorize(Roles="Writers")]
    public ActionResult<Producto> CreateProduct(Producto producto)
    {
        try
        {
            if (producto == null)
                return BadRequest();

            unitOfWork.Producto.Add(producto);
            return Ok(unitOfWork.Save());

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new product record");
        }
    }    

    [HttpPut("{id:int}"),Authorize(Roles ="Writers")]
    public ActionResult<Producto> UpdateProduct(int id, Producto producto)
    {
        try
        {
            if (id != producto.ProductoId)
                return BadRequest("Product ID mismatch");

            var productoToUpdate = unitOfWork.Producto.GetById(id);

            if (productoToUpdate == null)
                return NotFound($"Product with Id = {id} not found");

            productoToUpdate.Descripcion = producto.Descripcion;
            productoToUpdate.Marca = producto.Marca;
            productoToUpdate.Precio = producto.Precio;
            productoToUpdate.Stock = producto.Stock;

            // unitOfWork.Producto.Update(producto);

            unitOfWork.Save();

            return Ok(unitOfWork.Producto.GetById(producto.ProductoId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error updating data {ex.Message}");
        }
    }    

[HttpDelete("{id:int}"), Authorize(Roles ="Writers")]
    public ActionResult<Producto> DeleteProduct(int id)
    {
        try
        {
            var productToDelete = unitOfWork.Producto.GetById(id);

            if (productToDelete == null)
            {
                return NotFound($"Product with Id = {id} not found");
            }

            unitOfWork.Producto.Remove(productToDelete);
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