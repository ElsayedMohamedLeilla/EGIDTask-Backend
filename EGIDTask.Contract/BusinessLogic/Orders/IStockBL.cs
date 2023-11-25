using EGIDTask.Models.Criteria.Orders;
using EGIDTask.Models.Response.Stocks;

namespace EGIDTask.Contract.BusinessLogic.Orders
{
    public interface IStockBL
    {
        Task<GetStocksResponse> Get(GetStocksCriteria model);
        Task<GetStocksForDropDownResponse> GetForDropDown(GetStocksCriteria model);
    }
}
