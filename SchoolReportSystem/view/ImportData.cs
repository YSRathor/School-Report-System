using SchoolReportSystem.model;
using SchoolReportSystem.utility;
using System;

namespace SchoolReportSystem.view
{
    /** 
     * <summary>This class is responsible for providing a main method, which allows data to be read from a raw text file and stored in the <c>model</c>.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class ImportData
    {

        /**
         * <summary>This method effectively stores the raw student data in the appropriate model classes.</summary>
         * 
         * <param name="sl">The School object where the data will be stored to.</param>
         */
        public void LoadData(School sl)
        {
            // IF statement checks that the School object parameter is not null and has at least 1 academic year oject stored inside it.
            if (sl != null && sl.GetYears.Count > 0)
            {
                // Stores each line of the studentData2.txt file as a strings into an array.
                string[] lines = System.IO.File.ReadAllLines("C:/Users/Yash/source/repos/SchoolGradesReport/view/studentData2.txt");

                // IF statement checks that there is at least 1 line of text within the file.
                if (lines.Length > 0)
                {
                    // Breaks up each string inside the array and iterates through each one.
                    foreach (string line in lines)
                    {
                        // Splits each line into an array with comma delimited values.
                        string[] elements = line.Split(",");

                        // IF statement checks the length of the non-empty array.
                        if (elements.Length < 11 || elements.Length > 33)
                        {
                            // An exception is thrown if the length of the array is less than 11 or greater than 33.
                            throw new ArgumentException("Invalid student data!");
                        }
                        else
                        {
                            // Passes the academic year of the student into 'GetYearByNo' method, which returns the matching Academic Year object and stores it into a new object.
                            AcademicYear ay = sl.GetYearByNo(int.Parse(elements[3]));

                            // Invokes the 'StoreDetails' method of the ImportMethods utility class.
                            ImportMethods.StoreDetails(ay, elements);
                            // Removes any duplicate students from the Academic Year object.
                            ay.RemoveDuplicateStudents();
                        }
                    }
                }
            }

        }

    }
}
