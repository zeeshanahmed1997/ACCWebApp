using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models.MoonClothHouse;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Address { get; set; } = null!;
    public string? Gender { get; set; }
    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [JsonIgnore]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [JsonIgnore]
    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
