using Domain.common;
using System.Runtime.ConstrainedExecution;

namespace Domain.entities
{
    public class Product : AuditableBaseEntity
    {
        public  string Name { get; set; }
        public  string StatusName { get; set; }
        public int  Stock { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount {  get; set; }
        public double FinalPrice { get; set; }

    }
}
