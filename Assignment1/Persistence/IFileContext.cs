using System.Collections.Generic;

namespace Assignment1.Persistence
{
    public interface IFileContext
    {
        IList<T> ReadData<T>(string s);
        void SaveChanges();
        
    }
}