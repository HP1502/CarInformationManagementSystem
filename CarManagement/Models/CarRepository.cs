using CarManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.Models
{
    public class CarRepository
    {
        public Car SearchCar(CarInformationSystemContext context, string? model)
        {
            if (model == null)
            {
                return null;
            }

            var car = context.CAR
                .FirstOrDefault(m => m.Model == model);
            if (car == null)
            {
                return null;
            }

            return car;
        }

        public bool AddCar(CarInformationSystemContext context, Car car)
        {
            if (context.CAR.Any(c => c.Id == car.Id))
            {
                return false;
            }
            context.CAR.Add(car);
            context.SaveChanges();
            return true;
        }

        public bool RemoveCar(CarInformationSystemContext context, string? model)
        {
            if (!context.CAR.Any(c => c.Model == model) || model == null)
            {
                return false;
            }
            context.CAR.Remove(context.CAR.First(c => c.Id == id));
            context.SaveChanges();
            return true;
        }

        public bool ModifyCar(CarInformationSystemContext context, Car updatedCar, string model)
        {
            if (updatedCar == null)
            {
                return false;
            }
            var car = context.CAR.First(c => c.Model == model);
            car = updatedCar;
            context.SaveChanges();
            return true;
        }

        public class CarView
        {
            public string ModelName { get; set; }
            public string ManufacturerName { get; set; }

            public string AirBag { get; set; }

            public string BootCapacity { get; set; }

            public string engine { get; set; }

            public int mileage { get; set; }

            public int seat { get; set; }

            public int bhp { get; set; }
        }
        public List<CarView> GetCarsWithDetails(CarInformationSystemContext context, string manufacturerName, string carType)
        {
            // Fetch cars with eager loading for related data
            var result = context.CAR.Join(
            context.Manufacturer,                            // Inner collection
            car => car.ManufacturerId,                        // Key selector from the Car (outer) table
            manufacturer => manufacturer.Id,                  // Key selector from the Manufacturer (inner) table
            (car, manufacturer) => new CarView                       // Result selector, projecting the output
            {
                ModelName = car.Model,
                ManufacturerName = manufacturer.Name,
                AirBag = car.AirBagDetails,
                BootCapacity = car.BootSpace,
                engine = car.Engine,
                mileage = car.Mileage,
                seat = car.Seat,
                bhp = car.BHP,
            }
            ).ToList();

            return result;
        }
}
