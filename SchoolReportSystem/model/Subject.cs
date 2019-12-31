using SchoolReportSystem.utility;
using System;

namespace SchoolReportSystem.model
{
    /** 
     * <summary>This class defines a <c>Subject</c> object.
     * 
     * Implements IComparable to allow Subject objects to be compared.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class Subject : IComparable<Subject>
    {
        // Custom Constructor.
        /**
         * <summary>This custom constructor method is responsible for creating a Subject object.</summary>
         * 
         * <param name="name">The name of the subject.</param>
         * <param name="grade">The subject grade.</param>
         * 
         * <example>
         * <code>Subject sub = new Subject("Physics", "A*");</code>
         * This creates a Subject object with 'Physics' as the subject name, and 'A*' as the subject grade.</example>
         */
        public Subject(string name, string grade)
        {
            if (!ValidationMethods.IsSubjectNameValid(name).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the subject name.
                throw new ArgumentException(ValidationMethods.IsSubjectNameValid(name).Item2);
            }
            else
            {
                GetSubjectName = name;
            }

            if (string.IsNullOrWhiteSpace(grade))
            {
                // Throws an exception if the grade string is null, empty or only contains whitespaces.
                throw new ArgumentException("Invalid or empty value for subject grade!");
            }
            else
            {
                switch (grade)
                {
                    case "A*": case "A": case "B": case "C": case "D": case "E": case "U":
                        GetSubjectGrade = grade;
                        break;

                    default:
                        // Throws an exception if the grade string is invalid.
                        throw new ArgumentException("Invalid subject grade!");
                }
            }
        }

        // Methods.
        /** 
         * <summary>This method retrieves the subject name.</summary>
         * 
         * <example>
         * <code>Subject sub = new Subject("Physics", "A*");
         * sub.GetSubjectName;</code>
         * This will return "Physics".</example>
         * 
         * <returns>returns the value of GetSubjectName.</returns>
         */
        public string GetSubjectName { get; }

        /** 
         * <summary>This method retrieves the subject grade.</summary>
         * 
         * <example>
         * <code>Subject sub = new Subject("Physics", "A*");
         * sub.GetSubjectName;</code>
         * This will return "A*".</example>
         * 
         * <returns>returns the value of GetSubjectGrade.</returns>
         */
        public string GetSubjectGrade { get; }

        /**
         * <summary>This method compares one Subject object with another Subject object.</summary>
         * 
         * <param name="other">A different Subject object.</param>
         * 
         * <example>
         * <code>Subject sub = new Subject("Maths", "B");
         * Subject sub2 = new Subject("Maths", "B");
         * sub.CompareTo(sub2);
         * </code>
         * This returns <c>0</c>, as the objects have the exact same subject name.</example>
         * 
         * <returns>returns 0 if the objects are the same and -1 or 1 if they are different.</returns>
         */
        public int CompareTo(Subject other)
        {
            int result = 0;

            if (other != null)
            {
                result = GetSubjectName.CompareTo(other.GetSubjectName);
            }

            return result;
        }

        /** 
         * <summary>This method overrides the default 'ToString()' representation of the Subject class.</summary>
         * 
         * <example> 
         * <code>Subject sub = new Subject("Biology", "B");
         * sub.ToString();</code></example>
         */
        public override string ToString()
        {
            return GetSubjectName + ": " + GetSubjectGrade;
        }
    }
}
