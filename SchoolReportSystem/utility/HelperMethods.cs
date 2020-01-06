using System;

namespace SchoolReportSystem.utility
{

    /** 
     * <summary>This class contains some useful utility methods that are used by other classes in the program.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class HelperMethods
    {

        /**
         * <summary>This utility method converts all strings into capital case, with the starting letter of each word being converted to upper case.</summary>
         * 
         * <param name="s">The string to have the first letter of each word capitalised.</param>
         * 
         * <returns>The string has the first letter of each word capitalised; any leading/trailing whitespaces are removed.</returns>
         */
        public static string CapitaliseFirstLetterOfEachWord(string s)
        {
            string formattedString = "";

            // IF statement checks that the string supplied as parameter is not null, empty or only whitespaces.
            if (!string.IsNullOrWhiteSpace(s))
            {
                char[] letters = s.ToCharArray(); // Converts string into an array of char.
                bool startOfWord = true;

                s = s.Trim();

                // IF statements checks if the string has any special characters at the start or end.
                if (letters[0] == '-' || letters[0] == '&' || letters[^1] == '-' || letters[^1] == '&')
                {
                    throw new ArgumentException("Invalid string, no special characters allowed at the start or end!");
                }
                else
                {
                    for (int i = 0; i < letters.Length; i++)
                    {
                        if (letters[i] == ' ')
                        {
                            startOfWord = true;
                            formattedString += " ";
                        }
                        else if (letters[i] == '-')
                        {
                            startOfWord = true;
                            formattedString += "-";
                        }
                        else if (letters[i] == '&')
                        {
                            startOfWord = true;
                            formattedString += "&";
                        }
                        else
                        {
                            if (!char.IsLetter(letters[i]))
                            {
                                // Exception is thrown if the string contains any disallowed characters.
                                throw new ArgumentException("Invalid string, no special characters allowed!");
                            }
                            else
                            {
                                if (startOfWord)
                                {
                                    formattedString += letters[i].ToString().ToUpper();
                                    startOfWord = false;
                                }
                                else
                                {
                                    formattedString += letters[i].ToString().ToLower();
                                }
                            }
                        }
                    }
                }
            }

            return formattedString.Trim();
        }

        /**
         * <summary>This utility method checks whether an array has any elements that are null, empty or only consist of whitespaces.</summary>
         * 
         * <param name="array">The array to have each element checked.</param>
         * <param name="error">The error message to display if an exception needs to be thrown</param>
         */
        public static void CheckArray(string[] array, string error)
        {
            foreach (string s in array)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    // An exception is thrown if the array contains any null, empty or whitespaces only elements.
                    throw new ArgumentException(error);
                }
            }
        }

        /**
         * <summary>This utility method returns a pattern of characters corresponding to the length of the supplied string.</summary>
         * 
         * <param name="s">The string which requires a pattern separator.</param>
         * <param name="pattern">The string pattern to be produced for the dynamic seperator.</param>
         * 
         * <returns>The dynamic seperator with the input pattern and the length equal to the input string.</returns>
         */
        public static string DynamicSeparator(string s, string pattern)
        {
            string separator = "";

            if (!string.IsNullOrWhiteSpace(s) && !string.IsNullOrWhiteSpace(pattern))
            {
                foreach (char c in s)
                {
                    separator += pattern;
                }
            }

            return "\n" + separator;
        }

    }
}
