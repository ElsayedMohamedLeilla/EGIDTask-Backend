using EGIDTask.Contract.Repository.Orders;

namespace EGIDTask.Contract.Repository.RepositoryManager
{
    public interface IRepositoryManager
    {
        IStockRepository StockRepository { get; }
        IOrderRepository OrderRepository { get; }
    }
}