using EGIDTask.Contract.Repository.Orders;
using EGIDTask.Contract.Repository.RepositoryManager;
using EGIDTask.Data;
using EGIDTask.Data.UnitOfWork;
using EGIDTask.Repository.Orders;

namespace EGIDTask.Repository.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IUnitOfWork<ApplicationDBContext> unitOfWork;
        private IStockRepository stockRepository;
        private IOrderRepository orderRepository;

        public RepositoryManager(IUnitOfWork<ApplicationDBContext> _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IStockRepository StockRepository =>
         stockRepository ??= new StockRepository(unitOfWork);
        public IOrderRepository OrderRepository =>
         orderRepository ??= new OrderRepository(unitOfWork);
    }
}
