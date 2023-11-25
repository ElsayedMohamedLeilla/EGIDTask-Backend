using EGIDTask.Contract.Repository.Orders;
using EGIDTask.Data;
using EGIDTask.Data.UnitOfWork;
using EGIDTask.Helpers.Helpers;
using Glamatek.Real_Time.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace EGIDTask.BackgroundJobs
{
    public class UpdateStockPricesBackgroundJob : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private IServiceScopeFactory serviceScopeFactory;
        private readonly IHubContext<SignalRHub, ISignalRHubClient> hubContext;
        private string Schedule => "*/10 * * * * *"; // runs every 10 seconds
        public UpdateStockPricesBackgroundJob(IServiceScopeFactory _serviceScopeFactory, IHubContext<SignalRHub, ISignalRHubClient> _hubContext)
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            serviceScopeFactory = _serviceScopeFactory;
            hubContext = _hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (DateTime.UtcNow > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            Console.WriteLine("Update Stocks Prices " + DateTime.UtcNow.ToString("F"));
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork<ApplicationDBContext>>();
                var getallStocks = await stockRepository
                    .GetWithTracking(s => s.IsActive)
                    .ToListAsync();

                foreach (var stock in getallStocks)
                {
                    var randomNumber = RandomHelper.RandomNumberBetween(1, 100);
                    stock.Price = randomNumber;
                }

                await unitOfWork.SaveAsync();

                await hubContext.Clients.Group("EGIDTask-SignalR")
                    .NewStocksPrices(new Models.Dtos.NewStocksPricesModel());
            }
        }
    }
}
