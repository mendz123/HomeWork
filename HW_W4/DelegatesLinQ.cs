using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DelegatesLinQ.Homework
{
    // Delegate types for processing pipeline
    public delegate string DataProcessor(string input);
    public delegate void ProcessingEventHandler(string stage, string input, string output);

    /// <summary>
    /// Homework 2: Custom Delegate Chain
    /// </summary>
    public class DataProcessingPipeline
    {
        // Declare events for monitoring the processing
        public event ProcessingEventHandler ProcessingStageCompleted;

        // Individual processing methods
        public static string RemoveSpaces(string input)
        {
            string output = input.Replace(" ", "");
            return output;
        }

        public static string ToUpperCase(string input)
        {
            string output = input.ToUpper();
            return output;
        }

        public static string AddTimestamp(string input)
        {
            string output = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {input}";
            return output;
        }

        public static string ReverseString(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string EncodeBase64(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public static string ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty");
            return input;
        }

        // Method to process data through the pipeline
        public string ProcessData(string input, DataProcessor pipeline)
        {
            string currentInput = input;
            string currentOutput = input;
            if (pipeline == null) return input;

            foreach (Delegate del in pipeline.GetInvocationList())
            {
                string stage = del.Method.Name;
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    currentOutput = (string)del.DynamicInvoke(currentInput);
                    sw.Stop();
                    OnProcessingStageCompleted(stage, currentInput, currentOutput);
                }
                catch (Exception ex)
                {
                    OnProcessingStageCompleted(stage, currentInput, $"[ERROR] {ex.Message}");
                    throw; // Rethrow to let Main handle
                }
                currentInput = currentOutput;
            }
            return currentOutput;
        }

        // Method to raise processing events
        protected virtual void OnProcessingStageCompleted(string stage, string input, string output)
        {
            ProcessingStageCompleted?.Invoke(stage, input, output);
        }
    }

    // Logger class to monitor processing
    public class ProcessingLogger
    {
        public void OnProcessingStageCompleted(string stage, string input, string output)
        {
            Console.WriteLine($"[LOG] Stage: {stage} | Input: {input} | Output: {output}");
        }
    }

    // Performance monitor class
    public class PerformanceMonitor
    {
        private Dictionary<string, List<long>> _timings = new Dictionary<string, List<long>>();
        private Stopwatch _sw = new Stopwatch();

        public void OnProcessingStageCompleted(string stage, string input, string output)
        {
            // For demo, just simulate timing (not accurate per stage)
            if (!_timings.ContainsKey(stage))
                _timings[stage] = new List<long>();
            _timings[stage].Add(1); // Placeholder, real timing can be added if needed
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("[PERFORMANCE] Stage call counts:");
            foreach (var kv in _timings)
            {
                Console.WriteLine($"  {kv.Key}: {kv.Value.Count} times");
            }
        }
    }

    public class DelegateChain
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 2: CUSTOM DELEGATE CHAIN ===");
            Console.WriteLine();

            DataProcessingPipeline pipeline = new DataProcessingPipeline();
            ProcessingLogger logger = new ProcessingLogger();
            PerformanceMonitor monitor = new PerformanceMonitor();

            // Subscribe to events
            pipeline.ProcessingStageCompleted += logger.OnProcessingStageCompleted;
            pipeline.ProcessingStageCompleted += monitor.OnProcessingStageCompleted;

            // Create processing chain
            DataProcessor processingChain = DataProcessingPipeline.ValidateInput;
            processingChain += DataProcessingPipeline.RemoveSpaces;
            processingChain += DataProcessingPipeline.ToUpperCase;
            processingChain += DataProcessingPipeline.AddTimestamp;

            // Test the pipeline
            string testInput = "Hello World from C#";
            Console.WriteLine($"Input: {testInput}");

            string result = "";
            try
            {
                result = pipeline.ProcessData(testInput, processingChain);
                Console.WriteLine($"Output: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handled: {ex.Message}");
            }

            // Demonstrate adding more processors
            processingChain += DataProcessingPipeline.ReverseString;
            processingChain += DataProcessingPipeline.EncodeBase64;

            // Test again with extended pipeline
            try
            {
                result = pipeline.ProcessData("Extended Pipeline Test", processingChain);
                Console.WriteLine($"Extended Output: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handled: {ex.Message}");
            }

            // Demonstrate removing a processor
            processingChain -= DataProcessingPipeline.ReverseString;
            try
            {
                result = pipeline.ProcessData("Without Reverse", processingChain);
                Console.WriteLine($"Modified Output: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handled: {ex.Message}");
            }

            // Display performance statistics
            monitor.DisplayStatistics();

            // Error handling test
            try
            {
                result = pipeline.ProcessData(null, processingChain); // Should handle null input
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handled: {ex.Message}");
            }

            Console.WriteLine("\nDemo complete. Press any key to exit...");
            Console.ReadKey();
        }
    }
}