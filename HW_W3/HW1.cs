using System;

namespace DesignPatterns.Homework
{
    // This homework is based on the Factory Method Pattern
    // You will implement a factory method pattern for creating different types of vehicles

    // Base Product interface
    public interface IVehicle
    {
        void Drive();
        void DisplayInfo();
    }

    // Concrete Products
    public class Car : IVehicle
    {
        public string Model { get; private set; }
        public int Year { get; private set; }

        public Car(string model, int year)
        {
            Model = model;
            Year = year;
        }

        public void Drive()
        {
            Console.WriteLine($"Driving {Model} car on the road");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Car: {Model}, Year: {Year}");
        }
    }

    public class Motorcycle : IVehicle
    {
        public string Brand { get; private set; }
        public int EngineCapacity { get; private set; }

        public Motorcycle(string brand, int engineCapacity)
        {
            Brand = brand;
            EngineCapacity = engineCapacity;
        }

        public void Drive()
        {
            Console.WriteLine($"Riding {Brand} motorcycle with {EngineCapacity}cc engine");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Motorcycle: {Brand}, Engine: {EngineCapacity}cc");
        }
    }

    // TODO: Implement a new concrete product class 'Truck' that implements IVehicle
    // The Truck should have properties for LoadCapacity (in tons) and FuelType
    // Implement the Drive() and DisplayInfo() methods accordingly
    public class Truck : IVehicle
    {
        // Thuộc tính sức chứa tải trọng (tấn)
        public double LoadCapacity { get; set; }

        // Thuộc tính loại nhiên liệu
        public string FuelType { get; set; }

        // Hàm khởi tạo để thiết lập giá trị ban đầu
        public Truck(double loadCapacity, string fuelType)
        {
            LoadCapacity = loadCapacity;
            FuelType = fuelType;
        }

        // Cài đặt phương thức Drive() từ IVehicle
        public void Drive()
        {
            // In ra thông báo khi xe tải di chuyển
            Console.WriteLine("The truck is driving.");
        }

        // Cài đặt phương thức DisplayInfo() từ IVehicle
        public void DisplayInfo()
        {
            // Hiển thị thông tin chi tiết về xe tải
            Console.WriteLine($"Truck - Load Capacity: {LoadCapacity} tons, Fuel Type: {FuelType}");
        }
    }
    // Abstract Creator
    public abstract class VehicleFactory
    {
        // Factory Method
        public abstract IVehicle CreateVehicle();

        // Operation that uses the factory method
        public void OrderVehicle()
        {
            IVehicle vehicle = CreateVehicle();

            Console.WriteLine("Ordering a new vehicle...");
            vehicle.DisplayInfo();
            vehicle.Drive();
            Console.WriteLine("Vehicle delivered!\n");
        }
    }

    // Concrete Creators
    public class CarFactory : VehicleFactory
    {
        private string _model;
        private int _year;

        public CarFactory(string model, int year)
        {
            _model = model;
            _year = year;
        }

        public override IVehicle CreateVehicle()
        {
            return new Car(_model, _year);
        }
    }

    // TODO: Implement a concrete creator class 'MotorcycleFactory' that extends VehicleFactory
    // It should have fields for brand and engineCapacity
    // Override the CreateVehicle() method to return a new Motorcycle
    // Concrete Creator for Motorcycle
    public class MotorcycleFactory : VehicleFactory
    {
        private string _brand;
        private int _engineCapacity;

        public MotorcycleFactory(string brand, int engineCapacity)
        {
            _brand = brand;
            _engineCapacity = engineCapacity;
        }

        public override IVehicle CreateVehicle()
        {
            return new Motorcycle(_brand, _engineCapacity);
        }
    }

    // TODO: Implement a concrete creator class 'TruckFactory' that extends VehicleFactory
    // It should have fields for loadCapacity and fuelType
    // Override the CreateVehicle() method to return a new Truck

    // Concrete Creator for Truck
    // Lớp TruckFactory kế thừa từ VehicleFactory và chịu trách nhiệm tạo đối tượng Truck
    public class TruckFactory : VehicleFactory
    {
        // Trường lưu thông tin sức chứa tải trọng (tấn)
        private double _loadCapacity;
        // Trường lưu thông tin loại nhiên liệu
        private string _fuelType;

        // Hàm khởi tạo nhận vào sức chứa tải trọng và loại nhiên liệu
        public TruckFactory(double loadCapacity, string fuelType)
        {
            _loadCapacity = loadCapacity;
            _fuelType = fuelType;
        }

        // Ghi đè phương thức CreateVehicle để trả về đối tượng Truck mới
        public override IVehicle CreateVehicle()
        {
            return new Truck(_loadCapacity, _fuelType);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Factory Method Pattern Homework\n");

            // Create a car using factory
            VehicleFactory carFactory = new CarFactory("Tesla Model 3", 2023);
            carFactory.OrderVehicle();

            // TODO: Create a motorcycle using the MotorcycleFactory

            // Sử dụng thương hiệu "Harley Davidson" và dung tích động cơ 1450cc để tạo xe máy
            VehicleFactory motorcycleFactory = new MotorcycleFactory("Harley Davidson", 1450);
            // Đặt hàng và hiển thị thông tin xe máy
            motorcycleFactory.OrderVehicle();

            // Tạo một xe tải với sức chứa 10 tấn và loại nhiên liệu "Diesel"
            VehicleFactory truckFactory = new TruckFactory(10, "Diesel");
            // Đặt hàng và hiển thị thông tin xe tải
            truckFactory.OrderVehicle();

            // TODO: Create a truck using the TruckFactory
            // Use load capacity 10 tons and fuel type "Diesel"
            VehicleFactory anotherTruckFactory = new TruckFactory(10, "Diesel");
            anotherTruckFactory.OrderVehicle();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
