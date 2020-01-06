using System;
using System.Linq;

namespace SchoolReportSystem.model.validation
{

    /** 
     * <summary>This important class contains utility methods that validate some of the constructor arguments for the objects created by the <c>Student</c> class.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class StudentValidation
    {

        // As each method requires a tuple of type bool and string to be returned, this variable is global.
        private static (bool, string) isValidWithMessage = (false, "");

        /**
         * <summary>This method checks if the ID field of a Student object is valid and returns a tuple of boolean and string, which depends on the validity.</summary>
         * 
         * <param name="year">The student's academic year.</param>
         * <param name="ID">The student's ID.</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the ID string and a helpful status message.</returns>
         */
        public static (bool, string) IsStudentIDValid(int year, string ID)
        {
            if (string.IsNullOrWhiteSpace(ID))
            {
                // This value is set to the global variable if the ID string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing student ID!");
            }
            else
            {
                if (!ID.All(char.IsDigit))
                {
                    // This value is set to the global variable if the ID string contains any non-numeric characters.
                    isValidWithMessage = (false, "Invalid student ID, only numeric digits allowed!");
                }
                else
                {
                    if (ID.Length != 4)
                    {
                        // This value is set to the global variable if the ID string is not exactly four digits long.
                        isValidWithMessage = (false, "Invalid student ID, no more than 4 digits allowed!");
                    }
                    else
                    {
                        if (!CheckStudentIDRange(year, ID))
                        {
                            // This value is set to the global variable if the ID string is invalid for the student.
                            isValidWithMessage = (false, "Invalid student ID for Year " + year + " student!");
                        }
                        else
                        {
                            // This value is set to the global variable if the ID string is perfectly valid.
                            isValidWithMessage = (true, "");
                        }
                    }
                }
            }

            return isValidWithMessage;
        }

        /**
         * <summary>This method returns true/false depending on the whether digits of the ID string are valid for the year.</summary>
         * 
         * <param name="year">The student's academic year.</param>
         * <param name="studentID">The student's ID.</param>
         * 
         * <returns>A boolean true/false is returned, which depends on the ID value being in range or not.</returns>
         */
        public static bool CheckStudentIDRange(int year, string studentID)
        {
            bool IsIDValid = false;

            if (!string.IsNullOrWhiteSpace(studentID))
            {
                IsIDValid = year switch
                {
                    7 => 1 <= int.Parse(studentID) && int.Parse(studentID) <= 1400,
                    8 => 1401 <= int.Parse(studentID) && int.Parse(studentID) <= 2800,
                    9 => 2801 <= int.Parse(studentID) && int.Parse(studentID) <= 4200,
                    10 => 4201 <= int.Parse(studentID) && int.Parse(studentID) <= 5600,
                    11 => 5601 <= int.Parse(studentID) && int.Parse(studentID) <= 7000,
                    12 => 7001 <= int.Parse(studentID) && int.Parse(studentID) <= 8500,
                    13 => 8501 <= int.Parse(studentID) && int.Parse(studentID) <= 9999,
                    _ => throw new ArgumentException("Invalid academic year!"), // An exception is thrown if the year number is not a value between 7 and 13.
                };
            }

            return IsIDValid;
        }

        /**
         * <summary>This method checks if the forename or surname fields of the Student object are valid; it returns a tuple of boolean and string, which depends on the validity.</summary>
         * 
         * <param name="name">The name string to be validated.</param>
         * <param name="type">The type of name (forename/surname).</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the name string and a helpful status message dependent on the type string.</returns>
         */
        public static (bool, string) IsStudentNameValid(string name, string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                // This value is set to the global variable if the type string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing student name type!");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                // This value is set to the global variable if the name string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing student " + type + "!");
            }
            else
            {
                if (type != "first name" && type != "last name")
                {
                    // This value is set to the global variable if the type string is not equal to "first name" or "last name".
                    isValidWithMessage = (false, "Invalid student name type!");
                }
                else
                {
                    bool notLetterOrHyphen = false;
                    bool noHyphenNext = false;
                    bool invalidHyphens = false;
                    int hyphenCount = 0;

                    foreach (char c in name)
                    {
                        if (c != '-')
                        {
                            noHyphenNext = false;

                            if (!char.IsLetter(c))
                            {
                                notLetterOrHyphen = true;
                            }
                        }
                        else
                        {
                            if (noHyphenNext)
                            {
                                invalidHyphens = true;
                                hyphenCount++;
                            }
                            else
                            {
                                hyphenCount++;
                                noHyphenNext = true;
                            }
                        }
                    }

                    if (notLetterOrHyphen)
                    {
                        // This value is set to the global variable if the name string contains an erroneous character that is not a letter or a hyphen.
                        isValidWithMessage = (false, "Invalid student " + type + ", no special or numeric characters allowed!");
                    }
                    else if (hyphenCount > 2 || invalidHyphens)
                    {
                        // This value is set to the global variable if the name string contains more than 2 hyphens or an invalid arrangement of hyphens.
                        isValidWithMessage = (false, "Invalid student " + type + "!");
                    }
                    else
                    {
                        // This value is set to the global variable if the name string is perfectly valid.
                        isValidWithMessage = (true, "");
                    }
                }
            }

            return isValidWithMessage;
        }

    }
}
