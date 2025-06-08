using System;

namespace EventCalculator
{
    public class CalculationLogger
    {
        public void OnOperationPerformed(string operation, double operand1, double operand2, double result)
        {
            Console.WriteLine($"Operation: {operation}, Operands: {operand1}, {operand2}, Result: {result}");
        }

        public void OnErrorOccurred(string operation, string errorMessage)
        {
            Console.WriteLine($"Error in operation: {operation}, Message: {errorMessage}");
        }
    }
}