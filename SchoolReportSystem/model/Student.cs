using SchoolReportSystem.utility;
using System;
using System.Collections.Generic;

namespace SchoolReportSystem.model
{
    /** 
     * <summary>This class defines a <c>Student</c> object.
     * 
     * Implements IComparable to allow Student objects to be compared.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class Student : IComparable<Student>
    {
        // Custom Constructor.
        /**
         * <summary>This custom constructor is responsible for creating a Student object.</summary>
         * 
         * <param name="ID">A student's unique 4-digit ID number.</param>
         * <param name="f_name">A student's first name.</param>
         * <param name="l_name">A student's last name.</param>
         * <param name="year">A student's current school year.</param>
         * <param name="clName">A student's class name.</param>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");</code>
         * This creates a Student object with the ID value as '0007', first name as 'John', last name as 'Smith', school year as '9' and class name as 'Grey'.
         * </example>
         */
        public Student(string ID, string f_name, string l_name, int year, string clName)
        {
            if (!ValidationMethods.IsIDValid(year, ID).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the student ID.
                throw new ArgumentException(ValidationMethods.IsIDValid(year, ID).Item2);
            }
            else
            {
                GetStudentID = ID;
            }

            if (!ValidationMethods.IsStudentNameValid(f_name, "first name").Item1)
            {
                // Throws an exception with a message dependent on what invalidates the student's first name.
                throw new ArgumentException(ValidationMethods.IsStudentNameValid(f_name, "first name").Item2);
            }
            else
            {
                GetForename = f_name;
            }

            if (!ValidationMethods.IsStudentNameValid(l_name, "last name").Item1)
            {
                // Throws an exception with a message dependent on what invalidates the student's last name.
                throw new ArgumentException(ValidationMethods.IsStudentNameValid(l_name, "last name").Item2);
            }
            else
            {
                GetSurname = l_name;
            }

            if (year > 13 || year < 1)
            {
                // Throws an exception if 'year' is equal to a value less than 1 or greater than 13.
                throw new ArgumentException("Invalid academic year, only 1 - 13 are allowed!");
            }
            else
            {
                GetYear = year;
            }

            if (string.IsNullOrWhiteSpace(clName))
            {
                // Throws an exception if the 'clname' string is null, empty or only contains whitespaces.
                throw new ArgumentException("Missing student class name!");
            }
            else
            {
                bool notLetter = false;

                foreach (char c in clName)
                {
                    if (!char.IsLetter(c))
                    {
                        notLetter = true;
                    }
                }

                if (notLetter)
                {
                    // Throws an exception if the 'clname' string contains a disallowed character.
                    throw new ArgumentException("Invalid student class name, no special or numeric characters allowed!");
                }
                else
                {
                    GetClassName = clName;
                }
            }

            // Creates an empty list to be populated by subjects studied by the student.
            GetSubjects = new List<Subject>();

            // Sets the unique file path for the student.
            GetFilePath = "C:/Users/Yash/Desktop/Student Results/Year " + GetYear + "/" + GetFullName + ".txt";
        }

        // Methods.
        /**
         * <summary>This method stores a valid Subject object into the Student object.</summary>
         *
         * <param name="s">Subject object to be added to the student.</param>
         *
         * <example>
         * <code>Student sd = new Student("0021", "Jane", "Harper", 12, "Navy");
         * sd.AddSubjectToStudent(new Subject("Psychology", "C"));</code>
         * A new subject has been added to 'sd'.</example>
         */
        public void AddSubjectToStudent(Subject s)
        {
            if (s != null)
            {
                if (GetSubjects.Count + 1 > HelperMethods.CheckNoSubjectsForYear(GetYear))
                {
                    // Throws an exception if there will be an invalid number of subjects for a student (dependent on the year).
                    throw new ArgumentException("Invalid number of subjects for Year: " + GetYear + " student!");
                }
                else
                {
                    // Throws an exception if the subject already exists within the Student object.
                    GetSubjects.ForEach(subject => { if (subject.GetSubjectName == s.GetSubjectName) throw new ArgumentException("Subject already exists!"); });

                    GetSubjects.Add(s);
                }
            }
        }

        /**
         * <summary>This method removes any duplicate subjects inside the Student object.</summary>
         *         
         * <example>
         * <code>Student sd = new Student("5001", "Tom", "Curry", 10, "Purple");
         * sd.AddSubjectToStudent(new Subject("Geography", "A"));
         * sd.AddSubjectToStudent(new Subject("History", "B"));
         * sd.AddSubjectToStudent(new Subject("Geography", "A"));
         * sd.RemoveDuplicateSubjects();</code>
         * The second occurence of 'Geography' will be removed as it already exists once.
         * </example>
         */
        public void RemoveDuplicateSubjects()
        {
            for (int i = 0; i < GetSubjects.Count; i++)
            {
                for (int j = i + 1; j < GetSubjects.Count; j++)
                {
                    if (GetSubject(i).GetSubjectName == GetSubject(j).GetSubjectName)
                    {
                        RemoveSubject(j);
                        j--;
                    }
                }
            }

            GetSubjects.Sort(); // Sorts the subjects inside GetSubjects into a natural order.
        }

        /**
        * <summary>This method removes a subject at a particular index position, inside the Student object.</summary>
        *      
        * <param name="i">The index where the subject is to be removed from.</param>     
        *      
        * <example>
        * <code>Student sd = new Student("2608", "Bradley", "Wiley", 13, "Bronze");
        * sd.AddSubjectToStudent(new Subject("French", "B"));
        * sd.AddSubjectToStudent(new Subject("Chemistry", "A*"));
        * sd.AddSubjectToStudent(new Subject("English", "C"));
        * sd.RemoveSubject(2);</code>
        * The subject at index position 2 (English) will be removed.</example>
        */
        public void RemoveSubject(int i)
        {
            if (0 <= i && i < GetSubjects.Count)
            {
                GetSubjects.RemoveAt(i);
            }
        }

        /** 
         * <summary>This method retrieves the student's ID.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetStudentID;</code>
         * This will return "0007".</example>
         * 
         * <returns>returns the value of GetStudentID.</returns>
         */
        public string GetStudentID { get; }

        /** 
         * <summary>This method retrieves the student's first name.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetForename;</code>
         * This will return "John".</example>
         * 
         * <returns>returns the value of GetForename.</returns>
         */
        public string GetForename { get; }

        /** 
         * <summary>This method retrieves the student's last name.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetSurname;</code>
         * This will return "Smith".</example>
         * 
         * <returns>returns the value of GetSurname.</returns>
         */
        public string GetSurname { get; }

        /** 
         * <summary>This method retrieves the student's full name.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetFullName;</code>
         * This will return "John Smith".</example>
         * 
         * <returns>returns the concatenated value of GetForname and GetSurname</returns>
         */
        public string GetFullName { get { return GetForename + " " + GetSurname; } }

        /** 
         * <summary>This method retrieves the student's current school year.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetYear;</code>
         * This will return '9'.</example>
         * 
         * <returns>returns the value of GetYear.</returns>
         */
        public int GetYear { get; }

        /** 
         * <summary>This method retrieves the student's current class name.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetClassName;</code>
         * This will return "Grey".</example>
         * 
         * <returns>returns the value of GetClassName.</returns>
         */
        public string GetClassName { get; }

        /**
         * <summary>This method retrieves the Subject object located at a given index.</summary>
         *
         * <param name="index">The index position of the desired Subject object.</param>
         * 
         * <example>
         * <code>Student sd = new Student("2000", "Dean", "Ambrose", 8, "Crimson");
         * sd.AddSubjectToStudent(new Subject("Sociology", "B"));
         * sd.AddSubjectToStudent(new Subject("German", "B"));
         * sd.AddSubjectToStudent(new Subject("Maths", "A*"));
         * sd.GetSubject(0);</code>
         * The subject at index position 0 (Sociology) will be returned.</example>
         * 
         * <returns>returns the subject located at the index position specifed by the 'index' parameter.</returns>
         */
        public Subject GetSubject(int index)
        {

            Subject subject = null;

            if (0 <= index && index < GetSubjects.Count)
            {
                subject = GetSubjects[index];
            }

            if (subject == null)
            {
                // Throws an exception if subject is still equal to null.
                throw new ArgumentException("No subject found at that index position!");
            }

            return subject;
        }

        /** 
         * <summary>This method retrieves all subjects that the student studies.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("5451", "Dylan", "Thomas", 10, "Sapphire");
         * sd.AddSubjectToStudent(new Subject("French", "A"));
         * sd.AddSubjectToStudent(new Subject("Biology", "C"));
         * sd.AddSubjectToStudent(new Subject("Psychology", "B"));
         * sd.GetSubjects;</code>
         * This will return '{("French", "A"), ("Biology", "C"), ("Psychology", "B")}'.
         * </example>
         * 
         * <returns>returns the value of GetSubjects.</returns>
         */
        public List<Subject> GetSubjects { get; }

        /** 
         * <summary>This method retrieves the unique filepath to be used to store the student's report.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetFilePath;</code>
         * This will return "C:/Users/Yash/Desktop/Student Results/Year 9/John Smith.txt".</example>
         * 
         * <returns>returns the value of GetFilePath.</returns>
         */
        public string GetFilePath { get; }

        /**
        * <summary>This method compares one Student object with another Student object.</summary>
        * 
        * <param name="other">A different Student object.</param>
        * 
        * <example>
        * <code>Student sd = new Student("2000", "Dean", "Ambrose", 8, "Crimson");
        * Student sd2 = new Student("2000", "Dean", "Ambrose", 8, "Crimson");
        * sd.CompareTo(sd2);
        * </code>
        * This returns <c>0</c>, as the objects have the exact same student ID.</example>
        * 
        * <returns>returns 0 if the objects are the same and -1 or 1 if they are different.</returns>
        */
        public int CompareTo(Student other)
        {
            int result = 0;

            if (other != null)
            {
                result = GetStudentID.CompareTo(other.GetStudentID);
            }

            return result;
        }

        /** 
         * <summary>This method overrides the default 'ToString()' representation of the Student class.</summary>
         * 
         * <example> 
         * <code>Student sd = new Student("7142", "Ralph", "Dibney", 11, "Silver");
         * sd.ToString();</code></example>
         */
        public override string ToString()
        {
            string subjects = "";

            GetSubjects.ForEach(_ => { subjects += _.ToString() + "\n"; });

            return "\nID: #" + GetStudentID + "\nForename: " + GetForename + "\nSurname: " + GetSurname + "\nYear: " + GetYear + "\nClass: " + GetClassName
                + "\nSubjects:" + HelperMethods.DynamicSeparator("Subjects:", "+") + "\n" + subjects.Trim() + "\n";
        }
    }
}
