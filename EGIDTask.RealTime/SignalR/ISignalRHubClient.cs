using EGIDTask.Models.Dtos;

namespace Glamatek.Real_Time.SignalR
{
    public interface ISignalRHubClient
    {
        Task NewStocksPrices(NewStocksPricesModel model);
    }
}
