using System;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models.MoonClothHouse;

namespace Domain.Repositories.MoonClothHouse
{
    public interface ICartItemRepository : IMoonClothHouseRepository<CartItem>
    {
        Task<CartItem> GetByIdAsync(string id);
    }
}

