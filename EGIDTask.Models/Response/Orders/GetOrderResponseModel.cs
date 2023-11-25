namespace EGIDTask.Models.Response.Orders
{
    public class GetOrderResponseModel
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public string PersonName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
