using SchoolReportSystem.model.classes;
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
        public static void StoreStudentDetails(AcademicYear ay, string[] array)
        {
            // IF statement checks that the AcademicYear object is not null, and if the array is not null or empty.
            if (ay != null && (array != null || array.Length > 0))
            {
                // A new Student object is created with valid parameters and stored within the AcademicYear object.
                ay.AddStudentToYear(new Student(array[0].Trim(),
                    HelperMethods.CapitaliseFirstLetterOfEachWord(array[1]),
                    HelperMethods.CapitaliseFirstLetterOfEachWord(array[2]),
                    int.Parse(array[3]),
                    CapitaliseFirstLetter(array[4], false)
                    ));
            }
        }

        /**
         * <summary>This method stores valid subject data into a Subject object, which is then stored within the Student object.</summary>
         * 
         * <param name="sd">The Student object to have a subject added to it.</param>
         * <param name="array">The array, representing items on a line of the raw data text file.</param>
         */
        public static void StoreSubjectDetails(Student sd, string[] array)
        {
            // IF statement checks that the AcademicYear object is not null, and if the array is not null or empty.
            if (sd != null && (array != null || array.Length > 0))
            {
                if (!CheckSubjectIDForStudent(sd.GetYear, CapitaliseAlphaNumeric(array[0])))
                {
                    // An exception is thrown if the subject ID is not valid for the student's current year.
                    throw new ArgumentException("Invalid subject for student!");
                }
                else
                {
                    // A new Subject object is created with valid parameters and stored within the Student object.
                    sd.AddSubjectToStudent(new Subject(CapitaliseAlphaNumeric(array[0]), ShortenSubjectName(array[1]), 
                        CapitaliseFirstLetter(array[2], true), CapitaliseFirstLetter(array[3], true)));
                }
            }
        }

        /**
         * <summary>This method checks whether the subject ID is applicable for a student, dependant of what year they are in.</summary>
         * 
         * <param name="year">The student's current year.</param>
         * <param name="subjectID">The subject's ID.</param>
         * 
         * <returns>A boolean true/false is returned, which depends on the subject ID value being applicable to the student's year or not.</returns>
         */
        public static bool CheckSubjectIDForStudent(int year, string subjectID)
        {
            bool IsIDValid = false;

            if (!string.IsNullOrWhiteSpace(subjectID))
            {
                IsIDValid = year switch
                {
                    7 => subjectID.Contains("3"),
                    8 => subjectID.Contains("3"),
                    9 => subjectID.Contains("4"),
                    10 => subjectID.Contains("4"),
                    11 => subjectID.Contains("4"),
                    12 => subjectID.Contains("5A"),
                    13 => subjectID.Contains("5B"),
                    _ => throw new ArgumentException("Invalid academic year!") // An exception is thrown if the year number is not a value between 7 and 13.
                };
            }

            return IsIDValid;
        }

        /**
         * <summary>This useful method returns the correct number of subjects for an academic year.</summary>
         * 
         * <param name="year">The school year.</param>
         * 
         * <returns>The total number of subjects for that year.</returns>
         */
        public static int CheckNoSubjectsForYear(int year)
        {
            int total = 0;

            switch (year)
            {
                case 7: case 8:
                    total = 14;
                    break;

                case 9: case 10: case 11:
                    total = 9;
                    break;

                case 12:
                    total = 4;
                    break;

                case 13:
                    total = 3;
                    break;

                default:
                    // Exception is thrown if the int supplied by the parameter is not a number between 7 and 13.
                    throw new ArgumentException("Invalid academic year!");
            }

            return total;
        }

        /**
         * <summary>This method returns a single word string into capital case by capitalising the first letter of the word.</summary>
         * 
         * <param name="s">The string to have the first letter capitalised.</param>
         * <param name="special">A true/false value dependant on whether there are special characters in the string or not.</param>
         * 
         * <returns>The string has the first letter capitalised and any leading/trailing whitespaces are removed.</returns>
         */
        public static string CapitaliseFirstLetter(string s, bool special)
        {
            string output = "";

            if (!string.IsNullOrWhiteSpace(s))
            {
                s = s.Trim();

                if (special)
                {
                    output = s.Substring(0, 1).ToUpper() + s.Substring(1);
                }
                else
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
            }

            return output.Trim();
        }

        /**
         * <summary>This method returns a single 'alphanumeric' string into capital case by capitalising each letter.</summary>
         * 
         * <param name="s">The alphanumeric string to have each letter capitalised.</param>
         * 
         * <returns>The alphanumeric string has each letter capitalised and any leading/trailing whitespaces are removed.</returns>
         */
        public static string CapitaliseAlphaNumeric(string s)
        {
            string output = "";

            if (!string.IsNullOrWhiteSpace(s))
            {
                s = s.Trim();

                foreach (char c in s)
                {
                    if (char.IsLetter(c))
                    {
                        output += c.ToString().ToUpper();
                    }
                    else if (char.IsDigit(c))
                    {
                        output += c.ToString();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid string, no special characters allowed!");
                    }
                }
            }

            return output;
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

            // An array of possible delimiters used to seperate words in a string.
            char[] delimiters = new char[] {' '};

            // IF statement checks the number of words in a string and depending on the result it will execute a particular capitalise method.
            if (subjectName.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length == 1)
            {
                subjectName = CapitaliseFirstLetter(subjectName, false);
            } 
            else
            {
                // If subject name comprises of multiple words, the 'CapitaliseFirstLetterOfEachWord' is used to convert the entire string into capital case.
                subjectName = HelperMethods.CapitaliseFirstLetterOfEachWord(subjectName);
            }

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
                                shortName += c + "&";
                            }
                        }

                        shortName = shortName[0..^1];
                    }
                    else if (!subjectName.All(c => char.IsLetter(c) || c == ' ')) 
                    {
                        throw new ArgumentException("Invalid subject name!");
                    }
                    else
                    {
                        foreach (char c in subjectName)
                        {
                            if (char.IsUpper(c))
                            {
                                shortName +=  c + ".";
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
