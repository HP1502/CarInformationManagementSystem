using CarInformationManagmentSystem.Data;
using CarInformationManagmentSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarInformationManagmentSystem.Repositories
{
    [Authorize]
    public class CarRepository : ICarRepository
    {
        private readonly Context _context;

        public CarRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            try {
                return await _context.Cars
                .Include(c => c.Manufacturer)
                .Include(c => c.Type)
                .Include(c => c.Transmission)
                .ToListAsync();
            }
            catch
            {
                return new List<Car>();
            }
            
        }

        public async Task<Car> GetByModelAsync(string Model)
        {
            try
            {
                return await _context.Cars
                .Include(c => c.Manufacturer)
                .Include(c => c.Type)
                .Include(c => c.Transmission)
                .FirstOrDefaultAsync(c => c.Model == Model);
            }
            catch
            {
                return null;
            }
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<bool> AddAsync(Car entity)
        {
            try
            {
                _context.Cars.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<bool> UpdateAsync(Car car)
        {

            var existingCar = await _context.Cars.FindAsync(car.Id);
            if (existingCar == null) return false;
            try
            {
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<bool> DeleteAsync(string Model)
        {
            try
            {
                var car = await _context.Cars
                    .FirstOrDefaultAsync(c => c.Model == Model);

                if (car == null)
                {
                    return false;
                }
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
