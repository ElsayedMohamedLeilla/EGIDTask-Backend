using EGIDTask.Contract.BusinessLogic.Orders;
using EGIDTask.Models.Criteria.Orders;
using EGIDTask.Models.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;

namespace EGIDTask.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL _orderBL)
        {
            orderBL = _orderBL;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderModel model)
        {
            var result = await orderBL.Create(model);
            return Success(result, customMessage: "Done Create Order Successfully");
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetOrdersCriteria criteria)
        {
            if (criteria == null)
            {
                return BadRequest();
            }
            var ordersResponse = await orderBL.Get(criteria);

            return Success(ordersResponse.Orders, ordersResponse.TotalCount);
        }

    }
}