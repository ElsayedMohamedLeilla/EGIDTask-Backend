using EGIDTask.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EGIDTask.Domain.Entities.Orders
{
    [Table(nameof(Stock) + "s")]
    public class Stock : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; }
    }
}