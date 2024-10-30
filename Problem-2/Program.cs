using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Problem_2
{
    public class Solution
    {

        public static Dictionary<string, int> AverageAgeForEachCompany(List<Employee> employees)
        {
            var result = from emp in employees
                         group emp by emp.Company into g
                         select new
                         {
                             Company = g.Key,
                             AverageAge = (int)Math.Round(g.Average(x => x.Age)) // Casting to int to match the return type
                         };

            return result.OrderBy(x => x.Company).ToDictionary(x => x.Company, x => x.AverageAge);
        }


        public static Dictionary<string, int> CountOfEmployeesForEachCompany(List<Employee> employees)
        {
            var result = from emp in employees
                         group emp by emp.Company into g
                         select new
                         {
                             Company = g.Key,
                             Count = g.Count()
                         };
            return result.OrderBy(x => x.Company).ToDictionary(x => x.Company, x => x.Count);
        }

        public static Dictionary<string, Employee> OldestAgeForEachCompany(List<Employee> employees)
        {
            var result = from emp in employees
                         group emp by emp.Company into g
                         select new
                         {
                             Company = g.Key,
                             Employee = g.OrderByDescending(x => x.Age).First() // Get the oldest employee
                         };

            return result.OrderBy(x => x.Company) // Sort by company name
                         .ToDictionary(x => x.Company, x => x.Employee);
        }


        public static void Main()
        {
            int countOfEmployees = int.Parse(Console.ReadLine());

            var employees = new List<Employee>();

            for (int i = 0; i < countOfEmployees; i++)
            {
                string str = Console.ReadLine();
                string[] strArr = str.Split(' ');
                employees.Add(new Employee
                {
                    FirstName = strArr[0],
                    LastName = strArr[1],
                    Company = strArr[2],
                    Age = int.Parse(strArr[3])
                });
            }

            foreach (var emp in AverageAgeForEachCompany(employees))
            {
                Console.WriteLine($"The average age for company {emp.Key} is {emp.Value}");
            }

            foreach (var emp in CountOfEmployeesForEachCompany(employees))
            {
                Console.WriteLine($"The count of employees for company {emp.Key} is {emp.Value}");
            }

            foreach (var emp in OldestAgeForEachCompany(employees))
            {
                Console.WriteLine($"The oldest employee of company {emp.Key} is {emp.Value.FirstName} {emp.Value.LastName} having age {emp.Value.Age}");
            }
        }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }
    }
}