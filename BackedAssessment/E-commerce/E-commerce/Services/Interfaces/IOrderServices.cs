using E_commerce.Models;

namespace E_commerce.Services.Interfaces
{
    public interface IOrderServices
    {
        int Create(OrderRequest orderRequest);
    }

}
