# Event Calculator

## Overview
The Event Calculator is a C# application that demonstrates the use of events for mathematical operations and error handling. It includes a calculator class that raises events for each operation (Add, Subtract, Multiply, Divide) and for error situations, such as division by zero. The application also features subscriber classes that log operations, audit the number of operations performed, and handle errors.

## Project Structure
```
EventCalculator
├── src
│   ├── EventCalculator.cs
│   ├── CalculationLogger.cs
│   ├── CalculationAuditor.cs
│   ├── ErrorHandler.cs
│   └── Program.cs
├── EventCalculator.csproj
└── README.md
```

## Files Description
- **src/EventCalculator.cs**: Contains the `EventCalculator` class with methods for mathematical operations and event declarations for operation and error handling.
- **src/CalculationLogger.cs**: Implements the `CalculationLogger` class that subscribes to events from `EventCalculator` to log operations and errors to the console.
- **src/CalculationAuditor.cs**: Implements the `CalculationAuditor` class that tracks the count of operations performed and provides statistics.
- **src/ErrorHandler.cs**: Implements the `ErrorHandler` class that handles error events, particularly formatting messages for division by zero or other errors.
- **src/Program.cs**: The entry point of the application that creates instances of the calculator and subscriber classes, subscribes to events, and demonstrates operations and error handling.
- **EventCalculator.csproj**: The project file containing configuration settings and references for the Event Calculator application.

## How to Build and Run
1. Clone the repository or download the project files.
2. Open a terminal and navigate to the project directory.
3. Build the project using the command:
   ```
   dotnet build
   ```
4. Run the application using the command:
   ```
   dotnet run --project src/EventCalculator.csproj
   ```

## Features
- Supports basic mathematical operations with event-driven architecture.
- Logs all operations and errors to the console.
- Tracks the number of operations performed.
- Handles errors gracefully, providing user-friendly messages.

## Future Enhancements
- Extend the calculator to support more complex operations.
- Implement a graphical user interface (GUI) for better user interaction.
- Add unit tests for each component to ensure reliability and correctness.