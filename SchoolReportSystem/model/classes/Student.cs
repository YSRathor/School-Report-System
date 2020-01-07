using SchoolReportSystem.model.validation;
using SchoolReportSystem.utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolReportSystem.model.classes
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
         * This creates a Student object with the ID value as "0007", first name as "John", last name as "Smith", school year as '9' and class name as "Grey".
         * </example>
         */
        public Student(string ID, string f_name, string l_name, int year, string clName)
        {
            if (!StudentValidation.IsStudentIDValid(year, ID).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the student ID.
                throw new ArgumentException(StudentValidation.IsStudentIDValid(year, ID).Item2);
            }
            else
            {
                GetStudentID = ID;
            }

            if (!StudentValidation.IsStudentNameValid(f_name, "first name").Item1)
            {
                // Throws an exception with a message dependent on what invalidates the student's first name.
                throw new ArgumentException(StudentValidation.IsStudentNameValid(f_name, "first name").Item2);
            }
            else
            {
                GetForename = f_name;
            }

            if (!StudentValidation.IsStudentNameValid(l_name, "last name").Item1)
            {
                // Throws an exception with a message dependent on what invalidates the student's last name.
                throw new ArgumentException(StudentValidation.IsStudentNameValid(l_name, "last name").Item2);
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
            GetFilePath = "C:/Users/Student Results/Year " + GetYear + "/" + GetFullName + ".txt";
        }

        // Methods.
        /**
         * <summary>This method stores a valid Subject object into the Student object.</summary>
         *
         * <param name="s">Subject object to be added to the student.</param>
         *
         * <example>
         * <code>Student sd = new Student("0021", "Jane", "Harper", 12, "Navy");
         * sd.AddSubjectToStudent(new Subject("PSY4", "Psychology", "C", "A"));</code>
         * A new subject has been added to 'sd'.</example>
         */
        public void AddSubjectToStudent(Subject s)
        {
            if (s != null)
            {
                if (GetSubjects.Count + 1 > ImportMethods.CheckNoSubjectsForYear(GetYear))
                {
                    // Throws an exception if there will be an invalid number of subjects for a student (dependent on the year).
                    throw new ArgumentException("Invalid number of subjects for Year: " + GetYear + " student!");
                }
                else
                {
                    // Throws an exception if the subject already exists within the Student object.
                    GetSubjects.ForEach(subject => { if (subject.GetSubjectID == s.GetSubjectID) { throw new ArgumentException("Subject already exists!"); } });

                    GetSubjects.Add(s);
                }
            }
        }

        /**
         * <summary>This method removes any duplicate subjects inside the Student object.</summary>
         *         
         * <example>
         * <code>Student sd = new Student("5001", "Tom", "Curry", 10, "Purple");
         * sd.AddSubjectToStudent(new Subject("GEO3", "Geography", "A", "B"));
         * sd.AddSubjectToStudent(new Subject("HIS3", "History", "B", "A*"));
         * sd.AddSubjectToStudent(new Subject("GEO3", "Geography", "A", "B"));
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
                    if (GetSubject(i).GetSubjectID == GetSubject(j).GetSubjectID)
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
        * sd.AddSubjectToStudent(new Subject("FRE3", "French", "B", "A"));
        * sd.AddSubjectToStudent(new Subject("CHM3", "Chemistry", "A*", "A"));
        * sd.AddSubjectToStudent(new Subject("ENG3", "English", "C", "B"));
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
         * <returns>Returns the value of GetStudentID.</returns>
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
         * <returns>Returns the value of GetForename.</returns>
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
         * <returns>Returns the value of GetSurname.</returns>
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
         * <returns>Returns the concatenated value of GetForname and GetSurname</returns>
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
         * <returns>Returns the value of GetYear.</returns>
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
         * <returns>Returns the value of GetClassName.</returns>
         */
        public string GetClassName { get; }

        /**
         * <summary>This method retrieves the Subject object located at a given index.</summary>
         *
         * <param name="index">The index position of the desired Subject object.</param>
         * 
         * <example>
         * <code>Student sd = new Student("2000", "Dean", "Ambrose", 8, "Crimson");
         * sd.AddSubjectToStudent(new Subject("SOC4", "Sociology", "A", "B"));
         * sd.AddSubjectToStudent(new Subject("GMN4", "German", "B", "B"));
         * sd.AddSubjectToStudent(new Subject("MAT4", "Maths", "A", "A*"));
         * sd.GetSubject(0);</code>
         * The subject at index position 0 (Sociology) will be returned.</example>
         * 
         * <returns>Returns the subject located at the index position specifed by the 'index' parameter.</returns>
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
         * sd.AddSubjectToStudent(new Subject("FRE3", "French", "A", "A"));
         * sd.AddSubjectToStudent(new Subject("BIO3", "Biology", "C", "B"));
         * sd.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "B"));
         * sd.GetSubjects;</code>
         * This will return '{("FRE3", "French", "A", "A"), ("BIO3", "Biology", "C", "B"), ("PSY3", "Psychology", "B", "B")}'.
         * </example>
         * 
         * <returns>Returns the value of GetSubjects.</returns>
         */
        public List<Subject> GetSubjects { get; }

        /** 
         * <summary>This method retrieves the unique filepath to be used to store the student's report.</summary>
         * 
         * <example>
         * <code>Student sd = new Student("0007", "John", "Smith", 9, "Grey");
         * sd.GetFilePath;</code>
         * This will return "C:/Users/Student Results/Year 9/John Smith.txt".</example>
         * 
         * <returns>Returns the value of GetFilePath.</returns>
         */
        public string GetFilePath { get; }


        /** 
         * <summary>This method retrieves the total number of subjects that the Student exceeds, meets or falls belows their expected grades.</summary>
         * 
         * <param name="type"></param>
         * 
         * <example>
         * <code>Student sd = new Student("3650", "Oliver", "Ricardo", 9, "Copper");
         * sd.AddSubjectToStudent(new Subject("MAT3", "Mathematics", "A*", "B"));
         * sd.AddSubjectToStudent(new Subject("BIO3", "Biology", "B", "B"));
         * sd.AddSubjectToStudent(new Subject("CHM3", "Chemistry", "A", "B"));
         * sd.AddSubjectToStudent(new Subject("PHY3", "Physics", "C", "B"));
         * sd.AddSubjectToStudent(new Subject("ENG3", "English", "B", "A"));</code>
         * </example>
         * 
         * <example>
         * <code>sd.GetTargetCount("above");</code>
         * This will return '2'.</example>
         * 
         * <example>
         * <code>sd.GetTargetCount("equal");</code>
         * This will return '1'.</example>
         * 
         * <example>
         * <code>sd.GetTargetCount("below");</code>
         * This will return '2'.</example>
         * 
         * <returns>The total number of subjects that satisfy the 'type' string parameter are returned.</returns>
         */
        public int GetTargetCount(string type)
        {
            int count = 0;

            if (!string.IsNullOrWhiteSpace(type))
            {
                if (type == "above")
                {
                    count = GetSubjects.Where(_ => !_.GetExpectedGrade.Contains("*") 
                    && (_.GetActualGrade.Contains("*") || _.GetActualGrade.CompareTo(_.GetExpectedGrade) == -1)).Count();
                }
                else if (type == "equal")
                {
                    count = GetSubjects.Where(_ => _.GetActualGrade.CompareTo(_.GetExpectedGrade) == 0).Count();
                }
                else if (type == "below")
                {
                    count = GetSubjects.Where(_ => !_.GetActualGrade.Contains("*") 
                    && (_.GetExpectedGrade.Contains("*") || _.GetActualGrade.CompareTo(_.GetExpectedGrade) == 1)).Count();
                }
            }

            return count;
        }

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
        * <returns>Returns 0 if the objects are the same and -1 or 1 if they are different.</returns>
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
         * 
         * <returns>The string representation of the Student object.</returns>
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
