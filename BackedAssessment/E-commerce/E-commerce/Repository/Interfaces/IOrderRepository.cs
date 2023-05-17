using E_commerce.Models;

namespace E_commerce.Repository.Interfaces
{
    public interface IOrderRepository
    {
        int Create(Orders order);
    }
}
