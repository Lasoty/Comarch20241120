using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczeniaTesty
{
    public class CalculatorService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Subtract(int x, int y)
        {
            return x - y;
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }
        
        public float Dividy(int x, int y)
        {
            return x / (float)y;
        }

        public string ConcatenateString(string string1, string string2)
        {
            return $"{string1} {string2}";
        }

        public DateTime CalculateBirthDay(int age, int birthDay, int birthMonth)
        {
            Validate(age, birthDay, birthMonth);

            DateTime today = DateTime.Today;
            int birthYear = today.Year - age;

            try
            {
                DateTime birthDate = new DateTime(birthYear, birthMonth, birthDay);
                if (birthDate > today)
                {
                    birthDate = birthDate.AddYears(-1);
                }
                return birthDate;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException("Invalid date combination");
            }
        }

        private static void Validate(int age, int birthDay, int birthMonth)
        {
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative");
            }

            if (birthDay < 1 || birthDay > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Invalid day of the month");
            }

            if (birthMonth < 1 || birthMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(birthMonth), "Invalid month");
            }
        }
    }
}
