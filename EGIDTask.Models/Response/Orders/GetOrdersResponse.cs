namespace EGIDTask.Models.Response.Orders
{
    public class GetOrdersResponse
    {
        public List<GetOrderResponseModel> Orders { get; set; }
        public int TotalCount { get; set; }
    }
}
