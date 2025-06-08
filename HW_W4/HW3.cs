using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesLinQ.Homework
{
    // Data models for the homework
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public double Grade { get; set; } // 0-4.0 scale
        public string Semester { get; set; }
        public string Instructor { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    /// <summary>
    /// Homework 3: LINQ Data Processor
    /// 
    /// This is the most challenging homework requiring students to:
    /// 1. Use complex LINQ operations with multiple data sources
    /// 2. Implement custom extension methods
    /// 3. Create generic delegates and expressions
    /// 4. Handle advanced scenarios like pivot operations, statistical analysis
    /// 5. Implement caching and performance optimization
    /// 
    /// Advanced Requirements:
    /// - Custom LINQ extension methods
    /// - Expression trees and dynamic queries
    /// - Performance comparison between different approaches
    /// - Statistical calculations and reporting
    /// - Data transformation and pivot operations
    /// </summary>
    public class LinqDataProcessor
    {
        private List<Student> _students;

        public LinqDataProcessor()
        {
            _students = GenerateSampleData();
        }

        // BASIC REQUIREMENTS (using techniques from sample codes)
        
        public void BasicQueries()
        {
            // TODO: Implement basic LINQ queries similar to 6_8_LinQObject
            
            // 1. Find all students with GPA > 3.5
            var highGpaStudents = _students.Where(s => s.GPA > 3.5);
            Console.WriteLine("Students with GPA > 3.5:");
            foreach (var s in highGpaStudents)
                Console.WriteLine($"  {s.Name} ({s.Major}) - GPA: {s.GPA}");

            // 2. Group students by major
            var studentsByMajor = _students.GroupBy(s => s.Major);
            Console.WriteLine("\nStudents grouped by major:");
            foreach (var group in studentsByMajor)
            {
                Console.WriteLine($"  Major: {group.Key}");
                foreach (var s in group)
                    Console.WriteLine($"    {s.Name}");
            }

            // 3. Calculate average GPA per major
            var avgGpaPerMajor = _students
                .GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, AvgGPA = g.Average(s => s.GPA) });
            Console.WriteLine("\nAverage GPA per major:");
            foreach (var item in avgGpaPerMajor)
                Console.WriteLine($"  {item.Major}: {item.AvgGPA:F2}");

            // 4. Find students enrolled in specific courses
            var courseCode = "CS101";
            var studentsInCourse = _students
                .Where(s => s.Courses.Any(c => c.Code == courseCode));
            Console.WriteLine($"\nStudents enrolled in course {courseCode}:");
            foreach (var s in studentsInCourse)
                Console.WriteLine($"  {s.Name}");

            // 5. Sort students by enrollment date
            var sortedByEnrollment = _students.OrderBy(s => s.EnrollmentDate);
            Console.WriteLine("\nStudents sorted by enrollment date:");
            foreach (var s in sortedByEnrollment)
                Console.WriteLine($"  {s.Name}: {s.EnrollmentDate:yyyy-MM-dd}");
            
            Console.WriteLine("=== BASIC LINQ QUERIES ===");
            // Implementation needed by students
        }

        // Challenge 1: Create custom extension methods
        public void CustomExtensionMethods()
        {
            Console.WriteLine("=== CUSTOM EXTENSION METHODS ===");
            
            // TODO: Implement extension methods
            // 1. CreateAverageGPAByMajor() extension method
            // 2. FilterByAgeRange(int min, int max) extension method  
            // 3. ToGradeReport() extension method that creates formatted output
            // 4. CalculateStatistics() extension method
            
            // Example usage (students need to implement the extensions):
            // var highPerformers = _students.FilterByAgeRange(20, 25).Where(s => s.GPA > 3.5);
            // var gradeReport = _students.ToGradeReport();
            // var stats = _students.CalculateStatistics();

            // 1. Average GPA by major
            var avgGpaByMajor = _students.AverageGPAByMajor();
            Console.WriteLine("Average GPA by major (using extension):");
            foreach (var kv in avgGpaByMajor)
                Console.WriteLine($"  {kv.Key}: {kv.Value:F2}");

            // 2. Filter students by age range
            var ageFiltered = _students.FilterByAgeRange(20, 22);
            Console.WriteLine("\nStudents aged 20-22:");
            foreach (var s in ageFiltered)
                Console.WriteLine($"  {s.Name} ({s.Age} years old)");

            // 3. Grade report for each student
            Console.WriteLine("\nGrade reports:");
            foreach (var s in _students)
                Console.WriteLine(s.ToGradeReport());

            // 4. Statistics
            var statistics = _students.CalculateStatistics();
            Console.WriteLine("\nStudent statistics:");
            Console.WriteLine($"  Mean GPA: {statistics.MeanGPA:F2}");
            Console.WriteLine($"  Median GPA: {statistics.MedianGPA:F2}");
            Console.WriteLine($"  StdDev GPA: {statistics.StandardDeviationGPA:F2}");
        }

        // Challenge 2: Dynamic queries using Expression Trees
        public void DynamicQueries()
        {
            Console.WriteLine("=== DYNAMIC QUERIES ===");
            
            // TODO: Research Expression Trees
            // Implement a method that builds LINQ queries dynamically based on user input
            // Example: BuildDynamicFilter("GPA", ">", "3.5")
            // This requires understanding of Expression<Func<T, bool>>
            
            // Students should implement:
            // 1. Dynamic filtering based on property name and value
            // 2. Dynamic sorting by any property
            // 3. Dynamic grouping operations

            // Example: Dynamic filter - find students with GPA > 3.5
            var filter = BuildDynamicFilter<Student>("GPA", ">", "3.5");
            var filtered = _students.AsQueryable().Where(filter);
            Console.WriteLine("Dynamic filter: Students with GPA > 3.5");
            foreach (var s in filtered)
                Console.WriteLine($"  {s.Name} ({s.GPA})");

            // Example: Dynamic sort - sort by Name descending
            var sorted = _students.AsQueryable().OrderByDynamic("Name", false);
            Console.WriteLine("\nDynamic sort: Students by Name descending");
            foreach (var s in sorted)
                Console.WriteLine($"  {s.Name}");

            // Example: Dynamic group by Major
            var grouped = _students.AsQueryable().GroupByDynamic("Major");
            Console.WriteLine("\nDynamic group by: Students by Major");
            foreach (var group in grouped)
            {
                Console.WriteLine($"  Major: {group.Key}");
                foreach (var s in group)
                    Console.WriteLine($"    {s.Name}");
            }
        }

        // Challenge 3: Statistical Analysis with Complex Aggregations
        public void StatisticalAnalysis()
        {
            Console.WriteLine("=== STATISTICAL ANALYSIS ===");
            
            // TODO: Implement complex statistical calculations
            // 1. Calculate median GPA (requires custom logic)
            // 2. Calculate standard deviation of GPAs
            // 3. Find correlation between age and GPA
            // 4. Identify outliers using statistical methods
            // 5. Create percentile rankings
            
            // This requires research into statistical formulas and advanced LINQ usage

            var gpas = _students.Select(s => s.GPA).OrderBy(g => g).ToList();
            int count = gpas.Count;
            double medianGpa = (count % 2 == 1)
                ? gpas[count / 2]
                : (gpas[(count / 2) - 1] + gpas[count / 2]) / 2.0;

            double meanGpa = gpas.Average();
            double stdDevGpa = Math.Sqrt(gpas.Sum(g => Math.Pow(g - meanGpa, 2)) / count);

            // Correlation between Age and GPA
            var ages = _students.Select(s => (double)s.Age).ToList();
            double meanAge = ages.Average();
            double covariance = _students.Sum(s => (s.Age - meanAge) * (s.GPA - meanGpa)) / count;
            double stdDevAge = Math.Sqrt(ages.Sum(a => Math.Pow(a - meanAge, 2)) / count);
            double correlation = (stdDevAge == 0 || stdDevGpa == 0) ? 0 : covariance / (stdDevAge * stdDevGpa);

            // Outliers: GPAs more than 2 std dev from mean
            var outliers = _students.Where(s => Math.Abs(s.GPA - meanGpa) > 2 * stdDevGpa);

            // Percentile ranking for each student
            var studentPercentiles = _students
                .Select(s => new
                {
                    s.Name,
                    s.GPA,
                    Percentile = 100.0 * gpas.Count(g => g <= s.GPA) / count
                });

            Console.WriteLine($"Median GPA: {medianGpa:F2}");
            Console.WriteLine($"Standard Deviation of GPA: {stdDevGpa:F2}");
            Console.WriteLine($"Correlation (Age, GPA): {correlation:F2}");

            Console.WriteLine("Outlier students (GPA > 2 std dev from mean):");
            foreach (var s in outliers)
                Console.WriteLine($"  {s.Name} (GPA: {s.GPA})");

            Console.WriteLine("Student GPA Percentiles:");
            foreach (var s in studentPercentiles)
                Console.WriteLine($"  {s.Name}: {s.GPA} ({s.Percentile:F1} percentile)");
        }

        // Challenge 4: Data Pivot Operations
        public void PivotOperations()
        {
            Console.WriteLine("=== PIVOT OPERATIONS ===");
            
            // TODO: Research pivot table concepts
            // Create pivot tables showing:
            // 1. Students by Major vs GPA ranges (3.0-3.5, 3.5-4.0, etc.)
            // 2. Course enrollment by semester and major
            // 3. Grade distribution across instructors
            
            // This requires understanding of GroupBy with multiple keys and complex projections

            // 1. Students by Major vs GPA ranges (3.0-3.5, 3.5-4.0, etc.)
            var gpaRanges = new[]
            {
                new { Min = 0.0, Max = 3.0, Label = "Below 3.0" },
                new { Min = 3.0, Max = 3.5, Label = "3.0-3.5" },
                new { Min = 3.5, Max = 4.0, Label = "3.5-4.0" }
            };

            var pivot1 = _students
                .GroupBy(s => new
                {
                    s.Major,
                    GPARange = gpaRanges.First(r => s.GPA >= r.Min && s.GPA < r.Max).Label
                })
                .Select(g => new
                {
                    g.Key.Major,
                    g.Key.GPARange,
                    Count = g.Count()
                })
                .OrderBy(x => x.Major).ThenBy(x => x.GPARange);

            Console.WriteLine("Pivot: Students by Major vs GPA Range");
            foreach (var row in pivot1)
                Console.WriteLine($"  Major: {row.Major}, GPA Range: {row.GPARange}, Count: {row.Count}");

            // 2. Course enrollment by semester and major
            var pivot2 = _students
                .SelectMany(s => s.Courses.Select(c => new { s.Major, c.Semester, s.Name }))
                .GroupBy(x => new { x.Major, x.Semester })
                .Select(g => new
                {
                    g.Key.Major,
                    g.Key.Semester,
                    StudentCount = g.Select(x => x.Name).Distinct().Count()
                })
                .OrderBy(x => x.Major).ThenBy(x => x.Semester);

            Console.WriteLine("\nPivot: Course enrollment by Semester and Major");
            foreach (var row in pivot2)
                Console.WriteLine($"  Major: {row.Major}, Semester: {row.Semester}, Students: {row.StudentCount}");

            // 3. Grade distribution across instructors
            var pivot3 = _students
                .SelectMany(s => s.Courses.Select(c => new { c.Instructor, c.Grade }))
                .GroupBy(x => x.Instructor)
                .Select(g => new
                {
                    Instructor = g.Key,
                    GradeCounts = g.GroupBy(x => Math.Floor(x.Grade * 2) / 2) // e.g., 3.0, 3.5, 4.0
                        .ToDictionary(
                            gg => gg.Key,
                            gg => gg.Count()
                        )
                });

            Console.WriteLine("\nPivot: Grade distribution across Instructors");
            foreach (var row in pivot3)
            {
                Console.WriteLine($"  Instructor: {row.Instructor}");
                foreach (var kv in row.GradeCounts.OrderBy(x => x.Key))
                    Console.WriteLine($"    Grade: {kv.Key:F1}, Count: {kv.Value}");
            }
        }

        // Sample data generator
        private List<Student> GenerateSampleData()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1, Name = "Alice Johnson", Age = 20, Major = "Computer Science", 
                    GPA = 3.8, EnrollmentDate = new DateTime(2022, 9, 1),
                    Email = "alice.j@university.edu",
                    Address = new Address { City = "Seattle", State = "WA", ZipCode = "98101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS101", Name = "Intro to Programming", Credits = 3, Grade = 3.7, Semester = "Fall 2022", Instructor = "Dr. Smith" },
                        new Course { Code = "MATH201", Name = "Calculus II", Credits = 4, Grade = 3.9, Semester = "Fall 2022", Instructor = "Prof. Johnson" }
                    }
                },
                new Student
                {
                    Id = 2, Name = "Bob Wilson", Age = 22, Major = "Mathematics", 
                    GPA = 3.2, EnrollmentDate = new DateTime(2021, 9, 1),
                    Email = "bob.w@university.edu",
                    Address = new Address { City = "Portland", State = "OR", ZipCode = "97201" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "MATH301", Name = "Linear Algebra", Credits = 3, Grade = 3.3, Semester = "Spring 2023", Instructor = "Dr. Brown" },
                        new Course { Code = "STAT101", Name = "Statistics", Credits = 3, Grade = 3.1, Semester = "Spring 2023", Instructor = "Prof. Davis" }
                    }
                },
                // Add more sample students...
                new Student
                {
                    Id = 3, Name = "Carol Davis", Age = 19, Major = "Computer Science", 
                    GPA = 3.9, EnrollmentDate = new DateTime(2023, 9, 1),
                    Email = "carol.d@university.edu",
                    Address = new Address { City = "San Francisco", State = "CA", ZipCode = "94101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS102", Name = "Data Structures", Credits = 4, Grade = 4.0, Semester = "Fall 2023", Instructor = "Dr. Smith" },
                        new Course { Code = "CS201", Name = "Algorithms", Credits = 3, Grade = 3.8, Semester = "Fall 2023", Instructor = "Prof. Lee" }
                    }
                }
            };
        }
    }

    // TODO: Implement these extension methods
    public static class StudentExtensions
    {
        // Challenge: Implement custom extension methods
        // public static IEnumerable<Student> FilterByAgeRange(this IEnumerable<Student> students, int minAge, int maxAge)
        // public static Dictionary<string, double> AverageGPAByMajor(this IEnumerable<Student> students)
        // public static string ToGradeReport(this Student student)
        // public static StudentStatistics CalculateStatistics(this IEnumerable<Student> students)
    }

    // TODO: Define this class for statistical operations
    public class StudentStatistics
    {
        // Properties for mean, median, mode, standard deviation, etc.
    }

    public class LinqDataProcessor
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 3: LINQ DATA PROCESSOR ===");
            Console.WriteLine();
            
            Console.WriteLine("BASIC REQUIREMENTS:");
            Console.WriteLine("1. Implement basic LINQ queries (filtering, grouping, sorting)");
            Console.WriteLine("2. Use SelectMany for course data");
            Console.WriteLine("3. Calculate averages and aggregations");
            Console.WriteLine();
            
            Console.WriteLine("ADVANCED REQUIREMENTS:");
            Console.WriteLine("1. Create custom LINQ extension methods");
            Console.WriteLine("2. Implement dynamic queries using Expression Trees");
            Console.WriteLine("3. Perform statistical analysis (median, std dev, correlation)");
            Console.WriteLine("4. Create pivot table operations");
            Console.WriteLine();

            LinqDataProcessor processor = new LinqDataProcessor();
            
            // Students should implement all these methods
            processor.BasicQueries();
            processor.CustomExtensionMethods();
            processor.DynamicQueries();
            processor.StatisticalAnalysis();
            processor.PivotOperations();

            Console.ReadKey();
        }
    }
}