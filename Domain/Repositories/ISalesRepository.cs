﻿using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ISalesRepository : IRepository<Sales>
    {
        // Task<IEnumerable<Product>> GetProductByLastName(string lastname);
    }
}
