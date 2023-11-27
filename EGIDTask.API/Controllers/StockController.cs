using EGIDTask.Contract.BusinessLogic.Orders;
using EGIDTask.Models.Criteria.Orders;
using Microsoft.AspNetCore.Mvc;

namespace EGIDTask.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : BaseController
    {
        private readonly IStockBL stockBL;

        public StockController(IStockBL _stockBL)
        {
            stockBL = _stockBL;
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetStocksCriteria criteria)
        {
            if (criteria == null)
            {
                return BadRequest();
            }
            var stocksResponse = await stockBL.Get(criteria);

            return Success(stocksResponse.Stocks, stocksResponse.TotalCount);
        }
        [HttpGet]
        public async Task<ActionResult> GetForDropDown([FromQuery] GetStocksCriteria criteria)
        {
            if (criteria == null)
            {
                return BadRequest();
            }
            var stocksResponse = await stockBL.GetForDropDown(criteria);

            return Success(stocksResponse.Stocks, stocksResponse.TotalCount);
        }
    }
}