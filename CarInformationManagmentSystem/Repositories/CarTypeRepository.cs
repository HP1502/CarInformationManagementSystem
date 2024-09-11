using CarInformationManagmentSystem.Data;
using CarInformationManagmentSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarInformationManagmentSystem.Repositories
{
    [Authorize]
    public class CarTypeRepository : ICarTypeRepository
    {
        private readonly Context _context;

        public CarTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarType>> GetAllAsync()
        {
            return await _context.CarTypes.ToListAsync();
        }

        public async Task<CarType> GetByIdAsync(int id)
        {
            return await _context.CarTypes
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task AddAsync(CarType entity)
        {
            _context.CarTypes.Add(entity);
            await _context.SaveChangesAsync();
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task UpdateAsync(CarType entity)
        {
            _context.CarTypes.Update(entity);
            await _context.SaveChangesAsync();
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.CarTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
