using CarInformationManagmentSystem.Data;
using CarInformationManagmentSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarInformationManagmentSystem.Repositories
{
    [Authorize]
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly Context _context;

        public ManufacturerRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }


        public async Task<Manufacturer> GetByIdAsync(int id)
        {
            return await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task AddAsync(Manufacturer entity)
        {
            _context.Manufacturers.Add(entity);
            await _context.SaveChangesAsync();
        }


        [Authorize(Policy = "AdminOnly")]
        public async Task UpdateAsync(Manufacturer entity)
        {
            _context.Manufacturers.Update(entity);
            await _context.SaveChangesAsync();
        }


        [Authorize(Policy = "AdminOnly")]
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Manufacturers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
