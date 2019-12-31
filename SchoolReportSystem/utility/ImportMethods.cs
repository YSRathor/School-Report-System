using SchoolReportSystem.model;
using System;
using System.Linq;

namespace SchoolReportSystem.utility
{
    /** 
     * <summary>This class contains some useful utility methods to support the <c>ImportData</c> class.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class ImportMethods
    {

        /**
         * <summary>This method stores valid student data into a Student object, which is then stored within the AcademicYear object.</summary>
         * 
         * <param name="ay">The AcademicYear object to have a student added to it.</param>
         * <param name="array">The array, representing items on a line of the raw data text file.</param>
         */
        public static void StoreDetails(AcademicYear ay, string[] array)
        {
            // IF statement checks that the AcademicYear object is not null, and if the array is not null or empty.
            if (ay != null && (array != null || array.Length > 0))
            {
                // A new Student object is created with valid parameters and stored within the AcademicYear object.
                ay.AddStudentToYear(new Student(array[0],
                    HelperMethods.CapitaliseFirstLetterOfEachWord(array[1]),
                    HelperMethods.CapitaliseFirstLetterOfEachWord(array[2]),
                    int.Parse(array[3]),
                    CapitaliseFirstLetter(array[4])
                    ));

                // For loop is responsible for storing all Subjects to the Student object.
                for (int i = 5; i < array.Length; i += 2)
                {
                    ay.GetStudentById(array[0]).AddSubjectToStudent(new Subject(ShortenSubjectName(array[i]), array[i + 1]));
                }

                // Removes any duplicate subjects stored within the Student object.
                ay.GetStudentById(array[0]).RemoveDuplicateSubjects();

                // Executes the 'ValidateNoOfSubjects' method to see if the number of subjects is valid.
                ValidateNoOfSubjects(ay, array);
            }
        }

        /**
         * <summary>This method checks whether the number of subjects for the student is valid for the year that they are in.</summary>
         * 
         * <param name="ay">The AcademicYear object to have a student added to it.</param>
         * <param name="array">The array, representing items on a line of the raw data text file.</param>
         */
        public static void ValidateNoOfSubjects(AcademicYear ay, string[] array)
        {
            // IF statement checks that AcademicYear object from the parameter is not null and whether the array is not null or empty.
            if (ay != null && (array != null || array.Length > 0))
            {
                // Switch statement throws an exception if the number of subjects stored in the Student object does not match the requirement.
                switch (ay.GetYearNo)
                {
                    case 7:
                    case 8:
                        if (ay.GetStudentById(array[0]).GetSubjects.Count != 14)
                        {
                            throw new ArgumentException("Invalid number of subjects for student in year 7 or year 8!");
                        }
                        break;

                    case 9:
                    case 10:
                    case 11:
                        if (ay.GetStudentById(array[0]).GetSubjects.Count != 9)
                        {
                            throw new ArgumentException("Invalid number of subjects for student in year 9, year 10 or year 11!");
                        }
                        break;

                    case 12:
                        if (ay.GetStudentById(array[0]).GetSubjects.Count != 4)
                        {
                            throw new ArgumentException("Invalid number of subjects for student in year 12!");
                        }
                        break;

                    case 13:
                        if (ay.GetStudentById(array[0]).GetSubjects.Count != 3)
                        {
                            throw new ArgumentException("Invalid number of subjects for student in year 13!");
                        }
                        break;

                    default:
                        // An exception is thrown if the year number is not a value between 7 and 13.
                        throw new ArgumentException("Invalid Academic Year!");
                }
            }
        }

        /**
         * <summary>This method returns a single word string into capital case by capitalising the first letter of the word.</summary>
         * 
         * <param name="s">The string to have the first letter capitalised.</param>
         * 
         * <returns>The string has the first letter capitalised and any leading/trailing whitespaces are removed.</returns>
         */
        public static string CapitaliseFirstLetter(string s)
        {
            string output = "";

            if (!string.IsNullOrWhiteSpace(s))
            {
                if (!s.All(char.IsLetter))
                {
                    // An exception is thrown if the string contains any disallowed characters.
                    throw new ArgumentException("Invalid string, no special characters allowed!");
                }
                else
                {
                    output = s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
                }
            }

            return output.Trim();
        }

        /**
         * <summary>This method returns a shortened variant of the string representing the subject name.</summary>
         * 
         * <param name="subjectName">The subject name to be shortened.</param>
         * 
         * <returns>The subject name is abbreviated, depending on certain conditions.</returns>
         */
        public static string ShortenSubjectName(string subjectName)
        {
            string shortName = "";

            // As the subject name could comprise of multiple words, the 'CapitaliseFirstLetterOfEachWord' is used to convert the entire string into capital case.
            subjectName = HelperMethods.CapitaliseFirstLetterOfEachWord(subjectName);

            if (!string.IsNullOrWhiteSpace(subjectName))
            {
                if (subjectName.Contains(" "))
                {
                    if (subjectName.Contains("Mathematics"))
                    {
                        shortName = subjectName.Substring(0, 3) + ". Maths";
                    }
                    else if (subjectName.Contains("&"))
                    {
                        foreach (char c in subjectName)
                        {
                            if (char.IsUpper(c))
                            {
                                shortName = shortName + c + "&";
                            }
                        }

                        shortName = shortName[0..^1];
                    }
                    else
                    {
                        foreach (char c in subjectName)
                        {
                            if (char.IsUpper(c))
                            {
                                shortName = shortName + c + ".";
                            }
                        }

                        shortName = shortName[0..^1];
                    }
                }
                else
                {
                    if (subjectName.Contains("Mathematics"))
                    {
                        shortName = "Maths";
                    }
                    else
                    {
                        shortName = subjectName;
                    }
                }
            }

            return shortName;
        }

    }
}
