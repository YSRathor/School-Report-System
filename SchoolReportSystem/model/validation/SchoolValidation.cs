namespace SchoolReportSystem.model.validation
{

    /** 
     * <summary>This important class contains utility methods that validate some of the constructor arguments for the objects created by the <c>School</c> class.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class SchoolValidation
    {

        // As each method requires a tuple of type bool and string to be returned, this variable is global.
        private static (bool, string) isValidWithMessage = (false, "");

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
