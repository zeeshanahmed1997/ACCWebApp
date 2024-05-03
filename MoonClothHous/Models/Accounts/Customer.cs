using Domain.Models.MoonClothHouse;

namespace MoonClothHous.Models.Accounts
{
    public partial class Customer
    {
        public string CustomerId { get; set; } = null!;



        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
