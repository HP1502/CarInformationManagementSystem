using System.Collections.Generic;
using System.Threading.Tasks;
using CarInformationManagmentSystem.Models.Entities;

namespace CarInformationManagmentSystem.Repositories
{
    public interface ICarTypeRepository
    {
        Task<IEnumerable<CarType>> GetAllAsync();
        Task<CarType> GetByIdAsync(int id);
        Task AddAsync(CarType entity);
        Task UpdateAsync(CarType entity);
        Task DeleteAsync(int id);
    }
}
