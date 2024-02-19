﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.MoonClothHouse
{
    public interface IMoonClothHouseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
