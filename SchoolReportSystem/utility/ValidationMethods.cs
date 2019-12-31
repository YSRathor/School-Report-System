using System;
using System.Linq;

namespace SchoolReportSystem.utility
{
    /** 
     * <summary>This important class contains utility methods that validate some of the constructor arguments for the objects created by the <c>model</c> classes.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class ValidationMethods
    {

        // As each method requires a tuple of type bool and string to be returned, this variable is global.
        private static (bool, string) isValidWithMessage = (false, "");

        /**
         * <summary>This method checks if the subject name is valid and returns a tuple of boolean and string, with values that depend on the validity of the subject name.
         * As there a potential of different types of subject names depending on different conditions, each one is sectioned in a seperate part of the main IF statement.</summary>
         * 
         * <param name="name">The subject name to be validated.</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the subject name and a helpful status message.</returns>
         */
        public static (bool, string) IsSubjectNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                // This value is set to the global variable if the subject name is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing subject name!");
            }
            else if (name.Length == 10 && name.Contains(".") && name.Contains(" "))
            {
                bool notLetterSpaceOrDot = false;
                int spaceCount = 0;
                int dotCount = 0;
                int spaceIndex = 0;
                int dotIndex = 0;

                for (int i = 0; i < name.ToCharArray().Length; i++)
                {
                    if (name.ToCharArray()[i] == ' ')
                    {
                        spaceCount++;
                        spaceIndex = i;
                    }
                    else if (name.ToCharArray()[i] == '.')
                    {
                        dotCount++;
                        dotIndex = i;
                    }
                    else
                    {
                        if (!char.IsLetter(name.ToCharArray()[i]))
                        {
                            notLetterSpaceOrDot = true;
                        }
                    }
                }

                if (notLetterSpaceOrDot)
                {
                    // This value is set to the global variable if the subject name contains an erroneous character that is not a letter, space or a dot.
                    isValidWithMessage = (false, "Invalid subject name, no special or numeric characters allowed!");
                }
                else if (spaceIndex != dotIndex + 1 || spaceCount != 1 && dotCount != 1)
                {
                    // This value is set to the global variable if the subject name contains more than one space character or if the dot and space characters are in the incorrect position.
                    isValidWithMessage = (false, "Invalid subject name!");
                }
                else
                {
                    // This value is set to the global variable if the subject name is perfectly valid.
                    isValidWithMessage = (true, "");
                }
            }
            else if (name.Contains("&"))
            {
                int ampCount = 0;

                foreach (char c in name)
                {
                    if (c == '&')
                    {
                        ampCount++;
                    }
                }

                if (ampCount != 1)
                {
                    // This value is set to the global variable if the subject name contains more than one ampersand character.
                    isValidWithMessage = (false, "Invalid subject name!");
                }
                else
                {
                    // This value is set to the global variable if the subject name is perfectly valid.
                    isValidWithMessage = (true, "");
                }
            }
            else
            {
                bool notLetterOrDot = false;
                bool noDotNext = false;
                bool invalidDots = false;

                foreach (char c in name)
                {
                    if (c != '.')
                    {
                        noDotNext = false;

                        if (!char.IsLetter(c))
                        {
                            notLetterOrDot = true;
                        }
                    }
                    else
                    {
                        if (noDotNext)
                        {
                            invalidDots = true;
                        }
                        else
                        {
                            noDotNext = true;
                        }
                    }
                }

                if (notLetterOrDot)
                {
                    // This value is set to the global variable if the subject name contains an erroneous character that is not a space or a dot.
                    isValidWithMessage = (false, "Invalid subject name, no special or numeric characters allowed!");
                }
                else if (invalidDots)
                {
                    // This value is set to the global variable if the subject name contains an invalid arrangement of dots.
                    isValidWithMessage = (false, "Invalid subject name!");
                }
                else
                {
                    // This value is set to the global variable if the subject name is perfectly valid.
                    isValidWithMessage = (true, "");
                }
            }

            return isValidWithMessage;
        }

        /**
         * <summary>This method checks if the ID field of a Student object is valid and returns a tuple of boolean and string, which depends on the validity.</summary>
         * 
         * <param name="year">The student's academic year.</param>
         * <param name="ID">The student's ID.</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the ID string and a helpful status message.</returns>
         */
        public static (bool, string) IsIDValid(int year, string ID)
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
                        if (!CheckIDRange(year, ID))
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
         * <param name="ID">The student's ID.</param>
         * 
         * <returns>A boolean true/false is returned, which depends on the ID value being in range or not.</returns>
         */
        public static bool CheckIDRange(int year, string ID)
        {
            bool IsIDValid = false;

            if (!string.IsNullOrWhiteSpace(ID))
            {
                IsIDValid = year switch
                {
                    7 => 1 <= int.Parse(ID) && int.Parse(ID) <= 1400,
                    8 => 1401 <= int.Parse(ID) && int.Parse(ID) <= 2800,
                    9 => 2801 <= int.Parse(ID) && int.Parse(ID) <= 4200,
                    10 => 4201 <= int.Parse(ID) && int.Parse(ID) <= 5600,
                    11 => 5601 <= int.Parse(ID) && int.Parse(ID) <= 7000,
                    12 => 7001 <= int.Parse(ID) && int.Parse(ID) <= 8500,
                    13 => 8501 <= int.Parse(ID) && int.Parse(ID) <= 9999,
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
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type))
            {
                // This value is set to the global variable if the name string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing student " + type + "!");
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

            return isValidWithMessage;
        }

        /**
         * <summary>This method checks if the name field of a School object is valid and it returns a tuple of boolean and string, which depends on the validity.</summary>
         * 
         * <param name="name">The school name to be validated.</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the name string and a helpful status message.</returns>
         */
        public static (bool, string) IsSchoolNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                // This value is set to the global variable if the school name string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing value for school name!");
            }
            else
            {
                bool notLetterOrSpace = false;
                bool noSpaceNext = false;
                bool invalidSpaces = false;

                foreach (char c in name)
                {
                    if (c != ' ')
                    {
                        noSpaceNext = false;

                        if (!char.IsLetter(c))
                        {
                            notLetterOrSpace = true;
                        }
                    }
                    else
                    {
                        if (noSpaceNext)
                        {
                            invalidSpaces = true;
                        }
                        else
                        {
                            noSpaceNext = true;
                        }
                    }
                }

                if (notLetterOrSpace)
                {
                    // This value is set to the global variable if the name string contains an erroneous character that is not a letter or a space.
                    isValidWithMessage = (false, "Invalid school name, no special or numeric characters allowed!");
                }
                else if (invalidSpaces)
                {
                    // This value is set to the global variable if the name string contains an invalid arrangement of spaces.
                    isValidWithMessage = (false, "Invalid school name!");
                }
                else
                {
                    // This value is set to the global variable if the school name string is perfectly valid.
                    isValidWithMessage = (true, "");
                }
            }

            return isValidWithMessage;
        }

    }
}
