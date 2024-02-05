using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Interface
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task DeleteById(int id);
        Task<bool> IsExists(string? key, int? value);
        Task<int> SaveChangesAsync();
    }
}
