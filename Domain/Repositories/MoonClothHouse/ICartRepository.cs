using System;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models.MoonClothHouse;

namespace Domain.Repositories.MoonClothHouse
{
	public interface ICartRepository: IMoonClothHouseRepository<Cart>
	{
        Task<Cart> GetByIdAsync(string id);
    }
}

