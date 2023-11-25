namespace EGIDTask.Models.Response.Stocks
{
    public class GetStocksResponse
    {
        public List<GetStockResponseModel> Stocks { get; set; }
        public int TotalCount { get; set; }
    }
}
