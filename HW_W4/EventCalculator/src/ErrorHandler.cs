using System;

namespace EventCalculator
{
    public class ErrorHandler
    {
        public void OnErrorOccurred(string operation, string errorMessage)
        {
            Console.WriteLine($"Error occurred during {operation}: {errorMessage}");
        }
    }
}