using Domain.Interfaces;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.MoonClothHouse
{
    public interface ICustomerRepository : IMoonClothHouseRepository<Customer>
    {
        // Task<IEnumerable<Product>> GetProductByLastName(string lastname);
    }
}
