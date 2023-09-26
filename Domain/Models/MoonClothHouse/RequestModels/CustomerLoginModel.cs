using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.MoonClothHouse.RequestModels
{
    public partial class CustomerLoginModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

    }
}
