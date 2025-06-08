using System;

namespace EventCalculator
{
    public class CalculationAuditor
    {
        private int _operationCount = 0;

        public void OnOperationPerformed(string operation, double operand1, double operand2, double result)
        {
            _operationCount++;
        }

        public void DisplayStatistics()
        {
            Console.WriteLine($"Total operations performed: {_operationCount}");
        }
    }
}