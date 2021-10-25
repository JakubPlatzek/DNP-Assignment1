using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Assignment1.Data
{
    public interface IAdultsData
    {
        Task<IList<Adult>> GetAdults();
        Task AddAdult(Adult adult);
        Task RemoveAdult(int id);
        Task Update(Adult adult);
        Task<Adult> Get(int id);
    }
}