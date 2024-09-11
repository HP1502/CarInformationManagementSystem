using System.Collections.Generic;
using System.Threading.Tasks;
using CarInformationManagmentSystem.Models.Entities;

namespace CarInformationManagmentSystem.Repositories
{
   public interface ICarTransmissionTypeRepository
    {
        Task<IEnumerable<CarTransmissionType>> GetAllAsync();
        Task<CarTransmissionType> GetByIdAsync(int id);
        Task AddAsync(CarTransmissionType entity);
        Task UpdateAsync(CarTransmissionType entity);
        Task DeleteAsync(int id);
    }
}
