namespace EGIDTask.Models.Response.Stocks
{
    public class GetStocksForDropDownResponse
    {
        public List<GetStocksForDropDownResponseModel> Stocks { get; set; }
        public int TotalCount { get; set; }
    }
}
