using SchoolReportSystem.model.validation;
using System;

namespace SchoolReportSystem.model.classes
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
         * <param name="ID">The ID of the subject.</param>
         * <param name="name">The name of the subject.</param>
         * <param name="actualGrade">The actual subject grade.</param>
         * <param name="expectedGrade">The expected subject grade.</param>
         * 
         * <example>
         * <code>Subject sub = new Subject("PHY4", "Physics", "B", "A*");</code>
         * This creates a Subject object with "PHY4" as the subject ID, "Physics" as the subject name, "B" as the actual grade and "A*" as the expected grade.</example>
         */
        public Subject(string ID, string name, string actualGrade, string expectedGrade)
        {
            if (!SubjectValidation.IsSubjectIDValid(ID).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the subject ID.
                throw new ArgumentException(SubjectValidation.IsSubjectIDValid(ID).Item2);
            }
            else
            {
                GetSubjectID = ID;
            }

            if (!SubjectValidation.IsSubjectNameValid(name).Item1)
            {
                // Throws an exception with a message dependent on what invalidates the subject name.
                throw new ArgumentException(SubjectValidation.IsSubjectNameValid(name).Item2);
            }
            else
            {
                GetSubjectName = name;
            }

            if (!SubjectValidation.IsSubjectGradeValid(actualGrade, "actual").Item1)
            {
                // Throws an exception with a message dependent on what invalidates the subject's actual grade.
                throw new ArgumentException(SubjectValidation.IsSubjectIDValid(ID).Item2);
            }
            else
            {
                GetActualGrade = actualGrade;
            }

            if (!SubjectValidation.IsSubjectGradeValid(expectedGrade, "expected").Item1)
            {
                // Throws an exception with a message dependent on what invalidates the subject's expected grade.
                throw new ArgumentException(SubjectValidation.IsSubjectIDValid(ID).Item2);
            }
            else
            {
                GetExpectedGrade = expectedGrade;
            }
        }

        // Methods.
        /** 
         * <summary>This method retrieves the subject ID.</summary>
         * 
         * <example>
         * <code>Subject sub = new Subject("PHY4", "Physics", "B", "A*");
         * sub.GetSubjectID;</code>
         * This will return "PHY4".</example>
         * 
         * <returns>Returns the value of GetSubjectID.</returns>
         */
        public string GetSubjectID { get; }

        /** 
         * <summary>This method retrieves the subject name.</summary>
         * 
         * <example>
         * <code>Subject sub = new Subject("PHY4", "Physics", "B", "A*");
         * sub.GetSubjectName;</code>
         * This will return "Physics".</example>
         * 
         * <returns>Returns the value of GetSubjectName.</returns>
         */
        public string GetSubjectName { get; }

        /** 
         * <summary>This method retrieves the subject's actual grade.</summary>
         * 
         * <example>
         * <code>Subject sub = new Subject("PHY4", "Physics", "B", "A*");
         * sub.GetActualGrade;</code>
         * This will return "B".</example>
         * 
         * <returns>Returns the value of GetActualGrade.</returns>
         */
        public string GetActualGrade { get; }

        /** 
         * <summary>This method retrieves the subject's expected grade.</summary>
         * 
         * <example>
         * <code>Subject sub = new Subject("PHY4", "Physics", "B", "A*");
         * sub.GetExpectedGrade;</code>
         * This will return "A*".</example>
         * 
         * <returns>Returns the value of GetExpectedGrade.</returns>
         */
        public string GetExpectedGrade { get; }

        /**
         * <summary>This method compares one Subject object with another Subject object.</summary>
         * 
         * <param name="other">A different Subject object.</param>
         * 
         * <example>
         * <code>Subject sub = new Subject("MAT3", "Maths", "A", "B");
         * Subject sub2 = new Subject("MAT3", "Maths", "A", "B");
         * sub.CompareTo(sub2);
         * </code>
         * This returns <c>0</c>, as the objects have the exact same subject ID.</example>
         * 
         * <returns>Returns 0 if the objects are the same and -1 or 1 if they are different.</returns>
         */
        public int CompareTo(Subject other)
        {
            int result = 0;

            if (other != null)
            {
                result = GetSubjectID.CompareTo(other.GetSubjectID);
            }

            return result;
        }

        /** 
         * <summary>This method overrides the default 'ToString()' representation of the Subject class.</summary>
         * 
         * <example> 
         * <code>Subject sub = new Subject("BIO3", "Biology", "C", "B");
         * sub.ToString();</code></example>
         * 
         * <returns>The string representation of the Subject object.</returns>
         */
        public override string ToString()
        {
            return GetSubjectName + ": " + GetActualGrade + " (" + GetExpectedGrade + ") <" + GetSubjectID + ">";
        }
    }
}
