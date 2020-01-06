using SchoolReportSystem.model.classes;
using SchoolReportSystem.utility;
using System;

namespace SchoolReportSystem.view.import
{

    /** 
     * <summary>This class is responsible for providing a main method, which allows <c>Student</c> data to be read from a raw text file and stored in the <c>model</c>.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class ImportStudentData
    {

        /**
        * <summary>This method effectively stores the raw student data in the appropriate model classes.</summary>
        * 
        * <param name="sl">The School object where the data will be stored to.</param>
        */
        public void LoadStudentData(School sl)
        {
            // IF statement checks that the School object parameter is not null and has at least 1 AcademicYear object stored inside it.
            if (sl != null && sl.GetYears.Count > 0)
            {
                // Stores each line of the studentData3.txt file as a strings into an array.
                string[] lines = System.IO.File.ReadAllLines("C:/Users/Yash/source/repos/SchoolReportSystem/view/import/studentData3.txt");

                // IF statement checks that there is at least 1 line of text within the file.
                if (lines.Length > 0)
                {
                    // Breaks up each string inside the array and iterates through each one.
                    foreach (string line in lines)
                    {
                        // Splits each line into an array with comma delimited values.
                        string[] elements = line.Split(",");

                        // IF statement checks the length of the non-empty array.
                        if (elements.Length != 5)
                        {
                            // An exception is thrown if the length of the array not equal to 5.
                            throw new ArgumentException("Invalid student data!");
                        }
                        else
                        {
                            // An exception will be thrown if the 'elements' array has any missing data.
                            HelperMethods.CheckArray(elements, "Missing student data!");

                            // Passes the academic year of the student into 'GetYearByNo' method, which returns the matching Academic Year object and stores it into a new object.
                            AcademicYear ay = sl.GetYearByNo(int.Parse(elements[3]));

                            // Invokes the 'StoreStudentDetails' method of the ImportMethods utility class.
                            ImportMethods.StoreStudentDetails(ay, elements);

                            // Removes any duplicate students from the Academic Year object.
                            ay.RemoveDuplicateStudents();
                        }
                    }
                }
            }

        }

    }
}
