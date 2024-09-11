using System.Collections.Generic;
using System.Threading.Tasks;
using CarInformationManagmentSystem.Models.Entities;

namespace CarInformationManagmentSystem.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByModelAsync(string Model);
        Task<bool> AddAsync(Car entity);
        Task<bool> UpdateAsync(Car entity);
        Task<bool> DeleteAsync(string Model);
    }
}
