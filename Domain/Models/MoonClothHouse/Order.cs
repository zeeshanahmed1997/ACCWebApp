using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? StatusId { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string ShippingCity { get; set; } = null!;

    public string ShippingState { get; set; } = null!;

    public string ShippingZipCode { get; set; } = null!;

    public string ShippingPhoneNumber { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();

    public virtual OrderStatus? Status { get; set; }
}
