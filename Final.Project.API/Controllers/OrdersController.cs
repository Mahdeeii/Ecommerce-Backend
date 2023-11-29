using Final.Project.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersManager _ordersManager;

        public OrdersController(IOrdersManager ordersManager)
        {
            _ordersManager = ordersManager;
        }

        #region Make new order

        [HttpGet]
        [Route("MakeNewOrder/{addressId}")]
        public ActionResult MakeNewOrder(int addressId)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            _ordersManager.AddNewOrder(userIdFromToken, addressId);


            return Ok("order Added Successfully");
        }

        #endregion

        #region Get all Orders Dashboard
        [HttpGet]
        [Route("Dashboard/GetAllOrders")]
        public ActionResult<IEnumerable<OrderReadDto>> GetAllOrders()
        {
            IEnumerable<OrderReadDto> orderReadDtos = _ordersManager.GetAllOrders();
            if (orderReadDtos is null)
            {
                return BadRequest();
            }

            return Ok(orderReadDtos);
        }

        #endregion

        #region Get order details Dashboard

        [HttpGet]
        [Route("Dashboard/GetOrderDetails/{Id}")]
        public ActionResult<OrderDetailsDto> GetOrderDetails(int Id)
        {
            OrderDetailsDto orderDetailsDto = _ordersManager.GetOrderDetails(Id);
            if (orderDetailsDto is null)
            {
                return BadRequest();
            }

            return Ok(orderDetailsDto);
        }

        #endregion

        #region Edit Order Dashboard

        [HttpPut]
        [Route("Dashboard/EditOrder")]
        public ActionResult Edit(OrderEditDto orderEditDto)
        {
            bool isEdited = _ordersManager.UpdateOrder(orderEditDto);

            return isEdited ? NoContent() : BadRequest();
        }

        #endregion

        #region Delete Order Dashboard

        [HttpDelete]
        [Route("Dashboard/DeleteOrder/{Id}")]
        public ActionResult Delete(int Id)
        {
            bool isDeleted = _ordersManager.DeleteOrder(Id);

            return isDeleted ? NoContent() : BadRequest();
        }

        #endregion

    }
}