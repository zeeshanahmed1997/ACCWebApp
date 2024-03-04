using System;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models.MoonClothHouse;

namespace Domain.Repositories.MoonClothHouse
{
    public interface IProductRepository : IMoonClothHouseRepository<Product>
    {
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetProductsByIdAsync(string id);
    }
}

