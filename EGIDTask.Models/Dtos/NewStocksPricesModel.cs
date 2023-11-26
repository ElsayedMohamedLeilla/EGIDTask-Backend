namespace EGIDTask.Models.Dtos
{
    public class NewStocksPricesModel
    {
        // not used as i will update all prices at once
        public int StockId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
