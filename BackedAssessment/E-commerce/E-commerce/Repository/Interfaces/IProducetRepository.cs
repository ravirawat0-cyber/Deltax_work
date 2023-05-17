using E_commerce.Models;
using System.Collections;
using System.Collections.Generic;

namespace E_commerce.Repository.Interfaces
{
    public interface IProducetRepository
    {
        IEnumerable<Products> GetAll();
    }
}
