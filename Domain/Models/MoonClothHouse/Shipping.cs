using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Shipping
{
    public string ShippingId { get; set; } = null!;

    public string? OrderId { get; set; }

    public string CarrierName { get; set; } = null!;

    public string TrackingNumber { get; set; } = null!;

    public DateTime? EstimatedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    public virtual Order? Order { get; set; }
}
