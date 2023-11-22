using Domain.Interfaces.MoonClothHouse;
using Domain.Models.MoonClothHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.MoonClothHouse
{
    public interface IProductImageRepository : IMoonClothHouseRepository<ProductImage>
    {
        Task<ProductImage> GetByIdAsync(string id);
    }
}
