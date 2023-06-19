using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public int ClothingId { get; set; }
        public decimal price { get; set; }

        [ForeignKey("ClothingId")]
        public virtual Clothing Clothing { get; set; }
    }

}

