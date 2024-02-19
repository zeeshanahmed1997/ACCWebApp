using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string? OrderId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal PaymentAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
