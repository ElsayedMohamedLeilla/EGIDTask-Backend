using AutoMapper;
using EGIDTask.Contract.BusinessLogic.Orders;
using EGIDTask.Contract.BusinessValidation.Orders;
using EGIDTask.Contract.Repository.RepositoryManager;
using EGIDTask.Data;
using EGIDTask.Data.UnitOfWork;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Helpers.Helpers;
using EGIDTask.Models.Criteria.Orders;
using EGIDTask.Models.Dtos.Orders;
using EGIDTask.Models.Response.Orders;
using Microsoft.EntityFrameworkCore;

namespace EGIDTask.BusinessLogic.Orders
{
    public class OrderBL : IOrderBL
    {
        private readonly IUnitOfWork<ApplicationDBContext> unitOfWork;
        private readonly IOrderBLValidation orderBLValidation;
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        public OrderBL(IUnitOfWork<ApplicationDBContext> _unitOfWork,
            IRepositoryManager _repositoryManager,
            IMapper _mapper,
           IOrderBLValidation _orderBLValidation)
        {
            unitOfWork = _unitOfWork;
            repositoryManager = _repositoryManager;
            orderBLValidation = _orderBLValidation;
            mapper = _mapper;
        }
        public async Task<int> Create(CreateOrderModel model)
        {
            #region Business Validation

            await orderBLValidation.CreateOrderValidation(model);

            #endregion

            unitOfWork.CreateTransaction();

            #region Insert Order

            var order = mapper.Map<Order>(model);
            repositoryManager.OrderRepository.Insert(order);
            await unitOfWork.SaveAsync();

            #endregion

            #region Handle Response

            await unitOfWork.CommitAsync();
            return order.Id;

            #endregion

        }
        public async Task<GetOrdersResponse> Get(GetOrdersCriteria criteria)
        {
            var orderRepository = repositoryManager.OrderRepository;
            var query = orderRepository.GetAsQueryable(criteria);

            #region paging
            int skip = PagingHelper.Skip(criteria.PageNumber, criteria.PageSize);
            int take = PagingHelper.Take(criteria.PageSize);

            #region sorting
            var queryOrdered = orderRepository.OrderBy(query, nameof(Order.Id), "desc");
            #endregion

            var queryPaged = criteria.PagingEnabled ? queryOrdered.Skip(skip).Take(take) : queryOrdered;
            #endregion

            #region Handle Response

            var ordersList = await queryPaged.Select(order => new GetOrderResponseModel
            {
                Id = order.Id,
                PersonName = order.PersonName,
                StockName = order.Stock.Name,
                Quantity = order.Quantity,
                Price = order.Price
            }).ToListAsync();
            return new GetOrdersResponse
            {
                Orders = ordersList,
                TotalCount = await query.CountAsync()
            };
            #endregion

        }
    }
}

