using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenericApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using GenericApp.Domain.Repositories;

namespace GenericApp.OrderApi.Controllers
{
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<Pedido> _logger;

    public OrderController(ILogger<Pedido> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        this.unitOfWork = unitOfWork;
    }

        [HttpGet,Authorize(Roles="Order.Readers,Order.Writers")]
        public ActionResult<IEnumerable<Pedido>> GetAll()
        {
            return unitOfWork.Pedido.GetAll().ToList();
        }

        [HttpGet("{id:int}"), Authorize(Roles="Writers,Readers")]
        public ActionResult<Pedido> GetOrder(int id)
        {
            try
            {
                var result = unitOfWork.Pedido.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

    [HttpPost, Authorize(Roles = "Writers")]
    public ActionResult<Pedido> CreateOrder(Pedido pedido)
    {
        try
        {
            if (pedido == null)
                return BadRequest();

            unitOfWork.Pedido.Add(pedido);
            return Ok(unitOfWork.Save());

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new order record");
        }
    }    

    [HttpPut("{id:int}"),Authorize(Roles ="Writers")]
    public ActionResult<Pedido> UpdatePedido(int id, Pedido pedido)
    {
        try
        {
            if (id != pedido.PedidoId)
                return BadRequest("Order ID mismatch");

            var orderToUpdate = unitOfWork.Pedido.GetById(id);

            if (orderToUpdate == null)
                return NotFound($"Order with Id = {id} not found");
            
            orderToUpdate.FechaPedido = pedido.FechaPedido;

            unitOfWork.Save();

            return Ok(unitOfWork.Pedido.GetById(pedido.PedidoId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error updating data {ex.Message}");
        }
    }    

    [HttpDelete("{id:int}"), Authorize(Roles ="Writers")]
    public ActionResult<Pedido> DeletePedido(int id)
    {
        try
        {
            var pedidoToDelete = unitOfWork.Pedido.GetById(id);

            if (pedidoToDelete == null)
            {
                return NotFound($"Pedido with Id = {id} not found");
            }

            unitOfWork.Pedido.Remove(pedidoToDelete);
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
