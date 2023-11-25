using EGIDTask.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EGIDTask.Domain.Entities.Orders
{
    [Table(nameof(Order) + "s")]
    public class Order : BaseEntity
    {
        public int StockId { get; set; }
        [ForeignKey(nameof(StockId))]
        public virtual Stock Stock { get; set; }
        public string PersonName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}