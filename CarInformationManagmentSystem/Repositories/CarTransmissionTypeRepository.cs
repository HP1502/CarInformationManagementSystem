using CarInformationManagmentSystem.Data;
using CarInformationManagmentSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarInformationManagmentSystem.Repositories
{
    [Authorize]
    public class CarTransmissionTypeRepository : ICarTransmissionTypeRepository
    {
        private readonly Context _context;

        public CarTransmissionTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarTransmissionType>> GetAllAsync()
        {
            return await _context.CarTransmissionTypes.ToListAsync();
        }

        public async Task<CarTransmissionType> GetByIdAsync(int id)
        {
            return await _context.CarTransmissionTypes
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task AddAsync(CarTransmissionType entity)
        {
            _context.CarTransmissionTypes.Add(entity);
            await _context.SaveChangesAsync();
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task UpdateAsync(CarTransmissionType entity)
        {
            _context.CarTransmissionTypes.Update(entity);
            await _context.SaveChangesAsync();
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.CarTransmissionTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
