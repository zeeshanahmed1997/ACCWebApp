using System;
using System.Collections.Generic;

namespace Domain.Models.MoonClothHouse;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public string AddressType { get; set; } = null!;

    public string RecipientName { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual Customer? Customer { get; set; }
}
