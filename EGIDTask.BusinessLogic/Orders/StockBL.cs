using EGIDTask.Contract.BusinessLogic.Orders;
using EGIDTask.Contract.Repository.RepositoryManager;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Helpers.Helpers;
using EGIDTask.Models.Criteria.Orders;
using EGIDTask.Models.Response.Stocks;
using Microsoft.EntityFrameworkCore;

namespace EGIDTask.BusinessLogic.Orders
{
    public class StockBL : IStockBL
    {
        private readonly IRepositoryManager repositoryManager;
        public StockBL(IRepositoryManager _repositoryManager)
        {
            repositoryManager = _repositoryManager;
        }
        public async Task<GetStocksResponse> Get(GetStocksCriteria criteria)
        {
            var stockRepository = repositoryManager.StockRepository;
            var query = stockRepository.GetAsQueryable(criteria);

            #region paging
            int skip = PagingHelper.Skip(criteria.PageNumber, criteria.PageSize);
            int take = PagingHelper.Take(criteria.PageSize);

            #region sorting
            var queryOrdered = stockRepository.OrderBy(query, nameof(Stock.Id), "desc");
            #endregion

            var queryPaged = criteria.PagingEnabled ? queryOrdered.Skip(skip).Take(take) : queryOrdered;

            #endregion

            #region Handle Response

            var stocksList = await queryPaged.Select(stock => new GetStockResponseModel
            {
                Id = stock.Id,
                Name = stock.Name,
                Price = stock.Price
            }).ToListAsync();

            return new GetStocksResponse
            {
                Stocks = stocksList,
                TotalCount = await query.CountAsync()
            };

            #endregion

        }
        public async Task<GetStocksForDropDownResponse> GetForDropDown(GetStocksCriteria criteria)
        {
            criteria.IsActive = true;
            var stockRepository = repositoryManager.StockRepository;
            var query = stockRepository.GetAsQueryable(criteria);

            #region paging

            int skip = PagingHelper.Skip(criteria.PageNumber, criteria.PageSize);
            int take = PagingHelper.Take(criteria.PageSize);

            #region sorting

            var queryOrdered = stockRepository.OrderBy(query, nameof(Stock.Id), "desc");

            #endregion

            var queryPaged = criteria.PagingEnabled ? queryOrdered.Skip(skip).Take(take) : queryOrdered;

            #endregion

            #region Handle Response

            var stocks = await queryPaged.Select(stock => new GetStocksForDropDownResponseModel
            {
                Id = stock.Id,
                Name = stock.Name
            }).ToListAsync();

            return new GetStocksForDropDownResponse
            {
                Stocks = stocks,
                TotalCount = await query.CountAsync()
            };

            #endregion

        }
    }
}

