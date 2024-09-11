using CarInformationManagmentSystem.Models.Entities;  

namespace CarInformationManagmentSystem.Repositories
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetAllAsync();
        Task<Manufacturer> GetByIdAsync(int id);
        Task AddAsync(Manufacturer entity);
        Task UpdateAsync(Manufacturer entity);
        Task DeleteAsync(int id);
    }
}