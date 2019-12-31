using SchoolReportSystem.utility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SchoolReportSystem.model
{

    /** 
     * <summary>This class defines a <c>School</c> object.
     * 
     * Implements IEnumerable to allow AcademicYear objects inside the School object to be iterated through.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class School : IEnumerable
    {
        // Custom Constructor.
        /**
         * <summary>This custom constructor is responsible for creating a School object.</summary>
         * 
         * <param name="name">The school name.</param>
         * <param name="year">The year number.</param>
         * 
         * <example>
         * <code>School sl = new School("Portsborough Secondary", 8);</code>
         * This creates a School object with the name as 'Portsborough Secondary' and  year as '8'.</example>
         */
        public School(string name, int year)
        {
            if (!ValidationMethods.IsSchoolNameValid(name).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the school's name.
                throw new ArgumentException(ValidationMethods.IsSchoolNameValid(name).Item2);
            }
            else
            {
                GetSchoolName = name;
            }

            if (year < 1 || year > 13)
            {
                // Throws an exception if the year number is less than 1 or greater than 13.
                throw new ArgumentException("Academic year is outside the valid range (1 - 13)!");
            }
            else
            {
                // Creates an empty list to be populated by years within a school.
                GetYears = new List<AcademicYear>();
                // Adds the AcademicYear object to the School object.
                AddYear(new AcademicYear(year));
            }
        }

        // Custom Constructor 2.
        /**
         * <summary>This alternate custom constructor is responsible for creating a School object.</summary>
         * 
         * <param name="name">The school name.</param>
         * <param name="first">The lowest school year.</param>
         * <param name="last">The highest school year</param>
         * 
         * <example>
         * <code>School sl = new School("Earlmount High", 7, 11);</code>
         * This creates a School object with name as 'Earlmount High', with years 7 to 11.</example>
         */
        public School(string name, int first, int last)
        {
            if (!ValidationMethods.IsSchoolNameValid(name).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the school's name.
                throw new ArgumentException(ValidationMethods.IsSchoolNameValid(name).Item2);
            }
            else
            {
                GetSchoolName = name;
            }

            if (first >= last)
            {
                // Throws an exception if the first academic year is greater than or equal to the last academic year.
                throw new ArgumentException("First academic year can not be greater than or equal to last academic year!");
            }
            else if (first < 1 || first > 12)
            {
                // Throws an exception if the first academic year is less than 1 or greater than 12.
                throw new ArgumentException("First academic year can not be less than 1 or greater than 12!");
            }
            else if (last < 2 || last > 13)
            {
                // Throws an exception if the last academic year is less than 2 or greater than 13.
                throw new ArgumentException("Last academic year can not be less than 2 or greater than 13!");
            }
            else
            {
                // Creates an empty list to be populated by years within a school.
                GetYears = new List<AcademicYear>();
                // Adds a range of years to the School object.
                AddYears(first, last);
            }
        }

        // Methods.
        /**
         * <summary>This method stores a range of valid AcademicYear objects into the School object, dependent on the 'min' and 'max' parameters.</summary>
         *
         * <param name="min">The minimum inclusive number value for the range.</param>
         * <param name="max">The maximum inclusive number value for the range.</param>
         *
         * <example>
         * <code>School sl = new School("Berkshire High", 7, 8);
         * sl.AddYears(9, 13);</code>
         * Years: 9, 10, 11, 12 and 13 have been added to 'sl'.</example>
         */
        public void AddYears(int min, int max)
        {
            if (0 < min && min < max && 1 < max && max < 14 && min != max)
            {
                for (int i = min; i <= max; i++)
                {
                    AddYear(new AcademicYear(i));
                }

                GetYears.Sort(); // Sorts the years inside GetYears into a natural order.
            }
        }

        /**
         * <summary>This method stores a valid AcademicYear object into the School object.</summary>
         *
         * <param name="y">AcademicYear object to be added to the school.</param>
         *
         * <example>
         * <code>School sl = new School("Nottingham Secondary", 13);
         * sl.AddYear(12);</code>
         * Year 12 has been added to 'sl'.</example>
         */
        public void AddYear(AcademicYear y)
        {
            if (y != null)
            {
                // Throws an exception if the year already exists within the School object
                GetYears.ForEach(_ => { if (_.GetYearNo == y.GetYearNo) throw new ArgumentException("Year already exists!"); });

                GetYears.Add(y);
            }
        }

        /** 
         * <summary>This method retrieves the school's name.</summary>
         * 
         * <example>
         * <code>School sl = new School("Earlmount High", 7, 11);
         * sl.GetSchoolName;</code>
         * This will return "Earlmount High".</example>
         * 
         * <returns>returns the value of GetSchoolName.</returns>
         */
        public string GetSchoolName { get; }

        /**
         * <summary>This method retrieves the AcademicYear object which is equal to 'yearNo'.</summary>
         *
         * <param name="yearNo"></param>
         *
         * <example>
         * <code>School sl = new School("Midwest College", 9, 11);
         * sl.AddYears(12, 13);</code>
         * sl.GetYearByNo(13);
         * Year 13 will be returned.</example>
         * 
         * <returns>returns the year with the matching year number specifed by the 'yearNo' parameter.</returns>
         */
        public AcademicYear GetYearByNo(int yearNo)
        {
            AcademicYear ay = null;

            GetYears.ForEach(year => { if (year.GetYearNo == yearNo) ay = year; });

            if (ay == null)
            {
                // Throws an exception if 'ay' is still equal to null.
                throw new ArgumentException("Year does not exist!");
            }

            return ay;
        }

        /** 
         * <summary>This method retrieves the school years.</summary>
         * 
         * <example>
         * <code>School sl = new School("Earlmount High", 7, 11);
         * sl.GetYears;</code>
         * This will return {(7), (8), (9), (10), (11), (12), (13)}.</example>
         * 
         * <returns>returns the value of GetYears.</returns>
         */
        public List<AcademicYear> GetYears { get; }

        /** 
         * <summary>This method returns the IEnumerator for the School class.</summary>
         * 
         * <returns>returns the iteration of the non-generic collection for a School object.</returns>
         */
        public IEnumerator GetEnumerator() { return ((IEnumerable)GetYears).GetEnumerator(); }

        /** 
         * <summary>This method overrides the default 'ToString()' representation of the School class.</summary>
         * 
         * <example>
         * <code>School sl = new School("Holloway High", 7, 13);
         * sl.ToString();</code></example>
         */
        public override string ToString()
        {
            string years = "";

            GetYears.ForEach(_ => { years += _.ToString() + "\n"; });

            return "School: " + GetSchoolName + HelperMethods.DynamicSeparator("School: " + GetSchoolName, "*") + "\n\n" + years.Trim();
        }
    }
}
