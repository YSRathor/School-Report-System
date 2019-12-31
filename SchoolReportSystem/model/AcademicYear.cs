using SchoolReportSystem.utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SchoolReportSystem.model
{
    /** 
     * <summary>This class defines a <c>AcademicYear</c> object.
     * 
     * Implements IEnumerable to allow Student objects inside the AcademicYear object to be iterated through.
     * Implements IComparable to allow AcademicYear objects to be compared.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class AcademicYear : IEnumerable, IComparable<AcademicYear>
    {
        // Custom Constructor.
        /**
         * <summary>This custom constructor is responsible for creating an AcademicYear object.</summary>
         * 
         * <param name="year">The school year number.</param>
         * 
         * <example>
         * <code>AcademicYear yr = new AcademicYear(7);</code>
         * This creates an AcademicYear object with the year as '7'.
         * </example>
         */
        public AcademicYear(int year)
        {
            if (year < 1 || year > 13)
            {
                // Throws an exception if the year number is less than 1 or greater than 13.
                throw new ArgumentException("Invalid year number!");
            }
            else
            {
                GetYearNo = year;
            }

            // Creates an empty list to be populated by students within a particular academic year.
            GetStudents = new List<Student>();

            // Creates a folder for the year that will contain reports of students that belong to a particular school year.
            Directory.CreateDirectory("C:/Users/Yash/Desktop/Student Results/Year " + GetYearNo + "/");
        }

        // Methods.
        /**
         * <summary>This method stores a valid Student object into the AcademicYear object.</summary>
         *
         * <param name="s">Student object to be added to the year.</param>
         *
         * <example>
         * <code>AcademicYear yr = new AcademicYear(12);
         * yr.AddStudentToYear(new Student("8126", "Edward", "Willis", 12, "Red"));</code>
         * A new student has been added to 'yr'.</example>
         */
        public void AddStudentToYear(Student s)
        {
            if (s != null && s.GetYear == GetYearNo)
            {
                // Throws an exception if the student already exists within the AcademicYear object.
                GetStudents.ForEach(student => { if (student.GetStudentID == s.GetStudentID) throw new ArgumentException("Student already exists!"); });

                GetStudents.Add(s);
            }
        }

        /**
        * <summary>This method removes a student at a particular index position, inside the AcademicYear object.</summary>
        *         
        * <example>
        * <code>AcademicYear yr = new AcademicYear(10);
        * yr.AddStudentToYear(new Student("5003", "Chris", "Archer", 10, "Green"));
        * yr.AddStudentToYear(new Student("4500", "Hayden", "Collins", 10, "Red"));
        * yr.AddStudentToYear(new Student("5210", "James", "Holder", 10, "Blue"));
        * yr.RemoveStudentFromYear(1);</code>
        * The student at index position 1 (Hayden Collins) will be removed.</example>
        */
        public void RemoveStudentFromYear(int i)
        {
            if (0 <= i && i < GetStudents.Count)
            {
                GetStudents.RemoveAt(i);
            }
        }

        /**
        * <summary>This method removes removes any duplicate students inside the AcademicYear object.</summary>
        *         
        * <example>
        * <code>AcademicYear yr = new AcademicYear(9);
        * yr.AddStudentToYear(new Student("2950", "Wendy", "Foakes", 9, "Mauve"));
        * yr.AddStudentToYear(new Student("2950", "Wendy", "Foakes", 9, "Mauve"));
        * yr.AddStudentToYear(new Student("4125", "Stewart", "Richards", 9, "Teal"));
        * yr.RemoveStudentFromYear(1);</code>
        * The second occurence of 'Wendy Foakes' will be removed as it already exists once.</example>
        */
        public void RemoveDuplicateStudents()
        {
            for (int i = 0; i < GetStudents.Count; i++)
            {
                for (int j = i + 1; j < GetStudents.Count; j++)
                {
                    if (GetStudent(i).GetStudentID == GetStudent(j).GetStudentID)
                    {
                        RemoveStudentFromYear(j);
                        j--;
                    }
                }
            }

            GetStudents.Sort(); // Sorts the subjects inside GetStudents into a natural order.
        }

        /** 
         * <summary>This method retrieves the year's number.</summary>
         * 
         * <example>
         * <code>AcademicYear yr = new AcademicYear(12);
         * yr.GetYearNo;</code>
         * This will return '12'.</example>
         * 
         * <returns>returns the value of GetYearNo.</returns>
         */
        public int GetYearNo { get; }

        /**
        * <summary>This method retrieves the Student object located at a given index.</summary>
        *         
        * <param name="index">The index position of the desired Student object.</param>        
        *         
        * <example>
        * <code>AcademicYear yr = new AcademicYear(13);
        * yr.AddStudentToYear(new Student("8156", "Colin", "Jacobs", 13, "Lilac"));
        * yr.AddStudentToYear(new Student("8601", "Harris", "Ford", 13, "Rose"));
        * yr.AddStudentToYear(new Student("9509", "Scott", "Parker", 13, "Gold"));
        * yr.GetStudent(2);</code>
        * The student at index position 2 (Scott Parker) will be returned.</example>
        * 
        * <returns>returns the student located at the index position specifed by the 'index' parameter.</returns>
        */
        public Student GetStudent(int index)
        {
            Student student = null;

            if (0 <= index && index < GetStudents.Count)
            {
                student = GetStudents[index];
            }

            if (student == null)
            {
                // Throws an exception if student is still equal to null.
                throw new ArgumentException("No student found at that index position!");
            }

            return student;
        }

        /**
        * <summary>This method retrieves the Student object matched by the 'ID' string.</summary>
        * 
        * <param name="ID">The ID string to be used to get the matching Student object.</param>
        *         
        * <example>
        * <code>AcademicYear yr = new AcademicYear(7);
        * yr.AddStudentToYear(new Student("0796", "Christine", "Lively", 7, "Indigo"));
        * yr.AddStudentToYear(new Student("1472", "Jessica", "Rhodes", 7, "Magenta"));
        * yr.AddStudentToYear(new Student("1140", "Frank", "London", 7, "Cyan"));
        * yr.GetStudentByID("0796");</code>
        * The student with the ID value '0796' (Christine Lively) will be returned.</example>
        * 
        * <returns>returns the student with the matching ID specifed by the 'ID' parameter.</returns>
        */
        public Student GetStudentById(string ID)
        {
            Student student = null;

            if (!string.IsNullOrWhiteSpace(ID))
            {
                GetStudents.ForEach(_ => { if (_.GetStudentID == ID) student = _; });
            }

            if (student == null)
            {
                // Throws an exception if student is still equal to null.
                throw new ArgumentException("No student with that ID number exists!");
            }

            return student;
        }

        /** 
         * <summary>This method retrieves all students within the year.</summary>
         * 
         * <example>
         * <code>AcademicYear yr = new AcademicYear(13);
         * yr.AddStudentToYear(new Student("8156", "Colin", "Jacobs", 13, "Lilac"));
         * yr.AddStudentToYear(new Student("8601", "Harris", "Ford", 13, "Rose"));
         * yr.AddStudentToYear(new Student("9509", "Scott", "Parker", 13, "Gold"));
         * yr.GetStudents;</code>
         * This will return {("8156", "Colin", "Jacobs", 13, "Lilac"), ("8601", "Harris", "Ford", 13, "Rose"), ("9509", "Scott", "Parker", 13, "Gold")}.</example>
         * 
         * <returns>returns the value of GetStudents.</returns>
         */
        public List<Student> GetStudents { get; }

        /** 
         * <summary>This method returns the IEnumerator for the AcademicYear class.</summary>
         * 
         * <returns>returns the iteration of the non-generic collection for an AcademicYear object.</returns>
         */
        public IEnumerator GetEnumerator() { return ((IEnumerable)GetStudents).GetEnumerator(); }

        /**
        * <summary>This method compares one AcademicYear object with another AcademicYear object.</summary>
        * 
        * <param name="other">A different AcademicYear object.</param>
        * 
        * <example>
        * <code>AcademicYear yr = new AcademicYear(7);
        * AcademicYear yr2 = new AcademicYear(7);
        * yr.CompareTo(yr2);</code>
        * This returns <c>0</c>, as the objects have the exact same year number.</example>
        * 
        * <returns>returns 0 if the objects are the same and -1 or 1 if they are different.</returns>
        */
        public int CompareTo(AcademicYear other)
        {
            int result = 0;

            if (other != null)
            {
                result = GetYearNo.CompareTo(other.GetYearNo);
            }

            return result;
        }

        /** 
         * <summary>This method overrides the default 'ToString()' representation of the AcademicYear class.</summary>
         * 
         * <example> 
         * <code>AcademicYear yr = new AcademicYear(10);
         * yr.ToString();</code></example>
         */
        public override string ToString()
        {
            string students = "";

            GetStudents.ForEach(_ => { students += _.ToString() + "\n"; });

            return "\n\nAcademic Year: " + GetYearNo + HelperMethods.DynamicSeparator("Academic Year: " + GetYearNo, "-") + "\n\n" + students.Trim();
        }
    }
}
