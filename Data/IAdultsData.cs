using System.Collections.Generic;
using Models;

namespace Assignment1.Data
{
    public interface IAdultsData
    {
        IList<Adult> GetAdults();
        void AddAdult(Adult adult);
        void RemoveAdult(string firstName, string lastName);
        void Update(Adult adult);
        Adult Get(int id);
    }
}