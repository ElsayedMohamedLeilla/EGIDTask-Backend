namespace EGIDTask.Models.Dtos.Orders
{
    public class CreateOrderModel
    {
        public int StockId { get; set; }
        public string PersonName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
