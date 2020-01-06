using SchoolReportSystem.model.classes;
using SchoolReportSystem.model.validation;
using SchoolReportSystem.utility;
using System;
using System.Linq;

namespace SchoolReportSystem.view.import
{

    /** 
     * <summary>This class is responsible for providing a main method, which allows <c>Subject</c> data to be read from a raw text file and stored in the <c>model</c>.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class ImportSubjectData
    {

        /**
        * <summary>This method effectively stores the raw subject data in the appropriate model classes.</summary>
        * 
        * <param name="ay">The AcademicYear object where the data will be stored to.</param>
        */
        public void LoadSubjectData(AcademicYear ay)
        {
            // IF statement checks that the AcademicYear object parameter is not null and has at least 1 Student obect stored inside it.
            if (ay != null && ay.GetStudents.Count > 0)
            {
                // Stores each line of the subjectData2.txt file as a strings into an array.
                string[] lines = System.IO.File.ReadAllLines("C:/Users/Yash/source/repos/SchoolReportSystem/view/import/subjectData2.txt");

                // IF statement checks that there is at least 1 line of text within the file.
                if (lines.Length > 0)
                {
                    // Creates an empty Student object.
                    Student sd = null;

                    // Breaks up each string inside the array and iterates through each one.
                    foreach (string line in lines)
                    {
                        // Splits each line into an array with comma delimited values.
                        string[] elements = line.Split(",");

                        // IF statement checks the length of the non-empty array.
                        if (elements.Length != 5)
                        {
                            // An exception is thrown if the length of the array not equal to 5.
                            throw new ArgumentException("Invalid subject data!");
                        }
                        else
                        {
                            // An exception will be thrown if the 'elements' array has any missing data.
                            HelperMethods.CheckArray(elements, "Missing subject data!");

                            if (!elements[4].All(char.IsDigit))
                            {
                                // An exception is thrown if the last element of the array contains anything other than digits.
                                throw new ArgumentException("Invalid student ID!");
                            }
                            else if (StudentValidation.CheckStudentIDRange(ay.GetYearNo, elements[4]))
                            {
                                // Passes the ID of the student into 'GetStudentByID' method, which returns the matching Student object and stores it into a new object.
                                sd = ay.GetStudentById(elements[4]);

                                // Invokes the 'StoreSubjectDetails' method of the ImportMethods utility class.
                                ImportMethods.StoreSubjectDetails(sd, elements);

                                // Removes any duplicate subjects stored within the Student object.
                                sd.RemoveDuplicateSubjects();
                            }
                        }
                    }
                }

                ay.GetStudents.ForEach(_ => 
                { 
                    if (_.GetSubjects.Count != ImportMethods.CheckNoSubjectsForYear(_.GetYear))
                        // Throws an exception if the student does not have the required number of subjects for their academic year.
                        throw new ArgumentException("Invalid number of subjects for a student in year " + _.GetYear); 
                });

            }
        }

    }
}
