using System;
using System.Collections.Generic;

namespace DesignPatterns.Homework
{
    // This homework is based on the Observer Pattern (https://learn.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern)
    // You will implement a weather monitoring system using the Observer pattern

    #region Observer Pattern Interfaces

    // The subject interface that all weather stations must implement
    public interface IWeatherStation
    {
        // Methods to manage observers
        void RegisterObserver(IWeatherObserver observer);
        void RemoveObserver(IWeatherObserver observer);
        void NotifyObservers();
        
        // Weather data properties
        float Temperature { get; }
        float Humidity { get; }
        float Pressure { get; }
    }

    // The observer interface that all display devices must implement
    public interface IWeatherObserver
    {
        void Update(float temperature, float humidity, float pressure);
    }

    #endregion

    #region Weather Station Implementation

    // Concrete implementation of a weather station
    public class WeatherStation : IWeatherStation
    {
        // List to store all registered observers
        private List<IWeatherObserver> _observers;
        
        // Weather data
        private float _temperature;
        private float _humidity;
        private float _pressure;
        
        // Constructor
        public WeatherStation()
        {
            _observers = new List<IWeatherObserver>();
        }
        
        // Methods to register and remove observers
        public void RegisterObserver(IWeatherObserver observer)
        {
            // TODO: Implement RegisterObserver method
            // 1. Add the observer to the _observers list
            // 2. Print a message indicating that an observer has been registered
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine("Observer registered: " + observer.GetType().Name);
            }
        }
        
        public void RemoveObserver(IWeatherObserver observer)
        {
            // TODO: Implement RemoveObserver method
            // 1. Remove the observer from the _observers list
            // 2. Print a message indicating that an observer has been removed
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine("Observer removed: " + observer.GetType().Name);
            }
        }
        
        // Method to notify all observers when weather data changes
        public void NotifyObservers()
        {
            // TODO: Implement NotifyObservers method
            // 1. Loop through each observer in the _observers list
            // 2. Call the Update method on each observer, passing the current weather data
            // 3. Print a message indicating that observers are being notified
            Console.WriteLine("Notifying observers...");
            foreach (var observer in _observers)
            {
                observer.Update(_temperature, _humidity, _pressure);
            }
        }
        
        // Properties to access weather data
        public float Temperature => _temperature;
        public float Humidity => _humidity;
        public float Pressure => _pressure;
        
        // Method to update weather data and notify observers
        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            Console.WriteLine("\n--- Weather Station: Weather measurements updated ---");
            
            // Update weather data
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
            
            Console.WriteLine($"Temperature: {_temperature}°C");
            Console.WriteLine($"Humidity: {_humidity}%");
            Console.WriteLine($"Pressure: {_pressure} hPa");
            
            // Notify observers of the change
            NotifyObservers();
        }
    }

    #endregion

    #region Observer Implementations

    // TODO: Implement CurrentConditionsDisplay class that implements IWeatherObserver
    // 1. The class should have temperature, humidity, and pressure fields
    // 2. Implement the Update method to update these fields when notified
    // 3. Implement a Display method to show the current conditions
    // 4. The constructor should accept an IWeatherStation and register itself with that station
       // Implementation of CurrentConditionsDisplay class
    public class CurrentConditionsDisplay : IWeatherObserver
    {
        private float _temperature;
        private float _humidity;
        private float _pressure;
        private IWeatherStation _weatherStation;

        public CurrentConditionsDisplay(IWeatherStation weatherStation)
        {
            _weatherStation = weatherStation;
            _weatherStation.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
        }

        public void Display()
        {
            Console.WriteLine("Current conditions:");
            Console.WriteLine($"Temperature: {_temperature}°C");
            Console.WriteLine($"Humidity: {_humidity}%");
            Console.WriteLine($"Pressure: {_pressure} hPa");
        }
    }
    
    // Implementation of StatisticsDisplay class
    public class StatisticsDisplay : IWeatherObserver
    {
        private float _minTemp = float.MaxValue;
        private float _maxTemp = float.MinValue;
        private float _tempSum = 0f;
        private int _numReadings = 0;
        private IWeatherStation _weatherStation;

        public StatisticsDisplay(IWeatherStation weatherStation)
        {
            _weatherStation = weatherStation;
            _weatherStation.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            _tempSum += temperature;
            _numReadings++;

            if (temperature < _minTemp)
                _minTemp = temperature;
            if (temperature > _maxTemp)
                _maxTemp = temperature;
        }

        public void Display()
        {
            float avgTemp = _numReadings > 0 ? _tempSum / _numReadings : 0f;
            Console.WriteLine("Statistics:");
            Console.WriteLine($"Avg/Max/Min temperature = {avgTemp:F1}°C / {_maxTemp:F1}°C / {_minTemp:F1}°C");
        }
    }

    // Implementation of ForecastDisplay class
    // Lớp này quan sát trạm thời tiết và cung cấp dự báo dựa trên sự thay đổi áp suất
    public class ForecastDisplay : IWeatherObserver
    {
        // Lưu giá trị áp suất được ghi nhận lần trước
        private float _lastPressure;
        // Lưu giá trị áp suất hiện tại
        private float _currentPressure;
        // Lưu thông điệp dự báo
        private string _forecast;
        // Tham chiếu đến trạm thời tiết (subject)
        private IWeatherStation _weatherStation;

        // Hàm khởi tạo: tự đăng ký làm observer với trạm thời tiết
        public ForecastDisplay(IWeatherStation weatherStation)
        {
            _weatherStation = weatherStation;
            _weatherStation.RegisterObserver(this);
            _currentPressure = 0f;
            _lastPressure = 0f;
            _forecast = "Chưa có dự báo";
        }

        // Phương thức Update: được subject gọi khi dữ liệu thời tiết thay đổi
        public void Update(float temperature, float humidity, float pressure)
        {
            // Lưu áp suất trước đó trước khi cập nhật
            _lastPressure = _currentPressure;
            _currentPressure = pressure;

            // Xác định dự báo dựa trên xu hướng áp suất
            if (_lastPressure == 0f)
            {
                // Không có dữ liệu trước đó để so sánh
                _forecast = "Chưa có dự báo";
            }
            else if (_currentPressure > _lastPressure)
            {
                // Áp suất tăng: thời tiết đang cải thiện
                _forecast = "Thời tiết đang cải thiện";
            }
            else if (_currentPressure < _lastPressure)
            {
                // Áp suất giảm: có khả năng trời mát hơn, có mưa
                _forecast = "Trời mát hơn, có thể có mưa";
            }
            else
            {
                // Áp suất không đổi
                _forecast = "Thời tiết không đổi";
            }
        }

        // Phương thức Display: in dự báo ra màn hình
        public void Display()
        {
            Console.WriteLine("Dự báo:");
            Console.WriteLine(_forecast);
        }
    }

    #endregion

    #region Application

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observer Pattern Homework - Weather Station\n");

            try
            {
                // Create the weather station (subject)
                WeatherStation weatherStation = new WeatherStation();

                // Create display devices (observers)
                Console.WriteLine("Creating display devices...");

                // TODO: Uncomment these lines after implementing the required classes
                // CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherStation);
                // StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherStation);
                // ForecastDisplay forecastDisplay = new ForecastDisplay(weatherStation);
                // Create display devices (observers)
                CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherStation);
                StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherStation);
                ForecastDisplay forecastDisplay = new ForecastDisplay(weatherStation);

                // Simulate weather changes
                Console.WriteLine("\nSimulating weather changes...");

                // Initial weather
                weatherStation.SetMeasurements(25.2f, 65.3f, 1013.1f);

                // Display information from all displays
                Console.WriteLine("\n--- Displaying Information ---");
                // currentDisplay.Display();
                // statisticsDisplay.Display();
                // forecastDisplay.Display();

                // Weather change 1
                weatherStation.SetMeasurements(28.5f, 70.2f, 1012.5f);

                // Display updated information
                Console.WriteLine("\n--- Displaying Updated Information ---");
                // currentDisplay.Display();
                // statisticsDisplay.Display();
                // forecastDisplay.Display();

                // Weather change 2
                weatherStation.SetMeasurements(22.1f, 90.7f, 1009.2f);

                // Display updated information again
                Console.WriteLine("\n--- Displaying Final Information ---");
                // currentDisplay.Display();
                // statisticsDisplay.Display();
                // forecastDisplay.Display();

                // Test removing an observer
                Console.WriteLine("\nRemoving CurrentConditionsDisplay...");
                // weatherStation.RemoveObserver(currentDisplay);

                // Weather change after removing an observer
                weatherStation.SetMeasurements(24.5f, 80.1f, 1010.3f);

                // Display information without the removed observer
                Console.WriteLine("\n--- Displaying Information After Removal ---");
                // statisticsDisplay.Display();
                // forecastDisplay.Display();

                Console.WriteLine("\nObserver Pattern demonstration complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    #endregion
}
