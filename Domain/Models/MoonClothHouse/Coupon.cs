using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Coupon
{
    public string CouponId { get; set; } = null!;

    public string CouponCode { get; set; } = null!;

    public decimal DiscountPercentage { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
