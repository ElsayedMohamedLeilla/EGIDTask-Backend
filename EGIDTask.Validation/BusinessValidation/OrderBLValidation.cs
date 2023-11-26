using EGIDTask.Contract.BusinessValidation.Orders;
using EGIDTask.Contract.Repository.RepositoryManager;
using EGIDTask.Models.Dtos.Orders;
using EGIDTask.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EGIDTask.Validation.BusinessValidation
{

    public class OrderBLValidation : IOrderBLValidation
    {
        private readonly IRepositoryManager repositoryManager;
        public OrderBLValidation(IRepositoryManager _repositoryManager)
        {
            repositoryManager = _repositoryManager;
        }
        public async Task<bool> CreateOrderValidation(CreateOrderModel model)
        {
            var checkStock = await repositoryManager
                .StockRepository.Get(c => c.Id == model.StockId).AnyAsync();
            if (!checkStock)
            {
                throw new BusinessValidationException("Sorry Stock Not Found.");
            }


            return true;
        }
    }
}
