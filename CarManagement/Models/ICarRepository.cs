<<<<<<< HEAD
﻿using CarManagement.Data;

namespace CarManagement.Models
{
    public interface ICarRepository
    {
        public Car SearchCar(CarInformationSystemContext context, string? model);
        public bool AddCar(CarInformationSystemContext context,Car car);
        public bool ModifyCar(CarInformationSystemContext context, Car updatedCar, string model);
        public bool RemoveCar(CarInformationSystemContext context, string model);

        //public class CarView;
        public IEnumerable<object> GetCarsWithDetails(CarInformationSystemContext context, string manufacturerName);




    }
}
=======
﻿using CarManagement.Data;

namespace CarManagement.Models
{
    public interface ICarRepository
    {
        public Car SearchCar(CarInformationSystemContext context, string? model);
        public bool AddCar(CarInformationSystemContext context,Car car);
        public bool ModifyCar(CarInformationSystemContext context, Car updatedCar, string model);
        public bool RemoveCar(CarInformationSystemContext context, string model);

        //public class CarView;
        public IEnumerable<object> GetCarsWithDetails(CarInformationSystemContext context, string manufacturerName);




    }
}
>>>>>>> 88eac82cbc7e90fd6a90fc7c710ee8208f6cf91f
