using System.Linq;

namespace SchoolReportSystem.model.validation
{

    /** 
     * <summary>This important class contains utility methods that validate some of the constructor arguments for the objects created by the <c>Subject</c> class.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class SubjectValidation
    {

        // As each method requires a tuple of type bool and string to be returned, this variable is global.
        private static (bool, string) isValidWithMessage = (false, "");

        /**
         * <summary>This method checks if the ID field of a Subject object is valid and returns a tuple of boolean and string, which depends on the validity.</summary>
         * 
         * <param name="ID">The subject ID to be validated.</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the subject ID and a helpful status message.</returns>
         */
        public static (bool, string) IsSubjectIDValid(string ID)
        {
            if (string.IsNullOrWhiteSpace(ID))
            {
                // This value is set to the global variable if the subject ID is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing subject ID!");
            }
            else
            {
                if (!ID.All(char.IsLetterOrDigit))
                {
                    // This value is set to the global variable if the subject ID contains an erroneous character that is not a letter or digit.
                    isValidWithMessage = (false, "Invalid subject ID, no special characters allowed!");
                }
                else
                {
                    if (ID.Count(char.IsDigit) != 1)
                    {
                        // This value is set to the global variable if the subject ID does not contain exactly 1 digit.
                        isValidWithMessage = (false, "Invalid subject ID, only a single digit is allowed!");
                    }
                    else
                    {
                        // This value is set to the global variable if the subject ID is perfectly valid.
                        isValidWithMessage = (true, "");
                    }
                }
            }

            return isValidWithMessage;
        }

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
         * <summary>This method checks if the subject grade is valid and returns a tuple of boolean and string, which depends on the validity.</summary>
         * 
         * <param name="grade">The subject grade string to be validated.</param>
         * <param name="type">The type of subject grade (actual/expected).</param>
         * 
         * <returns>A tuple of a boolean true/false dependent on the validity of the grade string and a helpful status message.</returns>
         */
        public static (bool, string) IsSubjectGradeValid(string grade, string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                // This value is set to the global variable if the type string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing subject type!");
            }
            else if (string.IsNullOrWhiteSpace(grade))
            {
                // This value is set to the global variable if the grade string is null, empty or only contains whitespaces.
                isValidWithMessage = (false, "Missing subject " + type + " grade!");
            }
            else
            {
                if (type != "actual" && type != "expected")
                {
                    // This value is set to the global variable if the type string is not equal to "actual" or "expected".
                    isValidWithMessage = (false, "Invalid subject type!");
                }
                else
                {
                    switch (grade)
                    {
                        case "A*":
                        case "A":
                        case "B":
                        case "C":
                        case "D":
                        case "E":
                        case "U":
                            // This value is set to the global variable if the grade string is perfectly valid.
                            isValidWithMessage = (true, "");
                            break;

                        default:
                            // This value is set to the global variable if the grade string is invalid for the subject.
                            isValidWithMessage = (false, "Invalid subject grade!");
                            break;
                    }
                }
            }

            return isValidWithMessage;
        }

    }
}
