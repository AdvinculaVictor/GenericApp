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
public class OrderDetailController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<PedidoDetalle> _logger;

    public OrderDetailController(ILogger<PedidoDetalle> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        this.unitOfWork = unitOfWork;
    }

        [HttpGet,Authorize(Roles="Reader,Writer")]
        public ActionResult<IEnumerable<PedidoDetalle>> GetAll()
        {
            return unitOfWork.PedidoDetalle.GetAll().ToList();
        }

        [HttpGet("{id:int}"), Authorize(Roles="Writer,Reader")]
        public ActionResult<PedidoDetalle> GetOrderDetail(int id)
        {
            try
            {
                var result = unitOfWork.PedidoDetalle.GetById(id);

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
    public ActionResult<PedidoDetalle> CreateOrderDetail(PedidoDetalle PedidoDetalle)
    {
        try
        {
            if (PedidoDetalle == null)
                return BadRequest();

            unitOfWork.PedidoDetalle.Add(PedidoDetalle);
            return Ok(unitOfWork.Save());

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new order detail record");
        }
    }    

    [HttpPut("{id:int}"),Authorize(Roles ="Writer")]
    public ActionResult<PedidoDetalle> UpdatePedidoDetalle(int id, PedidoDetalle PedidoDetalle)
    {
        try
        {
            if (id != PedidoDetalle.PedidoDetalleId)
                return BadRequest("Order Detail ID mismatch");

            var orderToUpdate = unitOfWork.PedidoDetalle.GetById(id);

            if (orderToUpdate == null)
                return NotFound($"Order Detail with Id = {id} not found");
            
            
            orderToUpdate.Cantidad = PedidoDetalle.Cantidad;
            orderToUpdate.PedidoId = PedidoDetalle.PedidoId;
            orderToUpdate.ProductoId = PedidoDetalle.ProductoId;

            unitOfWork.Save();

            return Ok(unitOfWork.PedidoDetalle.GetById(PedidoDetalle.PedidoDetalleId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error updating data {ex.Message}");
        }
    }    

    [HttpDelete("{id:int}"), Authorize(Roles ="Writer")]
    public ActionResult<PedidoDetalle> DeletePedidoDetalle(int id)
    {
        try
        {
            var PedidoDetalleToDelete = unitOfWork.PedidoDetalle.GetById(id);

            if (PedidoDetalleToDelete == null)
            {
                return NotFound($"PedidoDetalle with Id = {id} not found");
            }

            unitOfWork.PedidoDetalle.Remove(PedidoDetalleToDelete);
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
