using SchoolReportSystem.model;
using System;
using System.Collections.Generic;

namespace SchoolReportSystem.utility
{
    /** 
     * <summary>This class contains some useful utility methods used to support the <c>ExportData</c> class.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public static class ExportMethods
    {
        /**
         * <summary>This method executes the 'OuputInformation' method with differing parameters, depending on the year number of the AcademicYear object.</summary>
         * 
         * <param name="sl">The School object that needs to be outputted.</param>
         * <param name="ay">The AcademicYear object that will have student reports built from.</param>
         */
        public static void GenerateOutput(School sl, AcademicYear ay)
        {
            // IF statement checks that the School object is not null or empty and whether the AcademicYear object is not null or empty.
            if ((sl != null || sl.GetYears.Count > 0) && (ay != null || ay.GetStudents.Count > 0))
            {
                switch (ay.GetYearNo)
                {
                    case 7:
                    case 8:
                        OutputInformation(sl, ay, "Key Stage 3");
                        break;

                    case 9:
                    case 10:
                    case 11:
                        OutputInformation(sl, ay, "Key Stage 4");
                        break;

                    case 12:
                    case 13:
                        OutputInformation(sl, ay, "Key Stage 5");
                        break;

                    default:
                        // An exception is thrown if the year number is not a value between 7 and 13.
                        throw new ArgumentException("Invalid Academic Year!");
                }
            }
        }

        /**
         * <summary>This method writes the report data for each student to their own text file.</summary>
         * 
         * <param name="sl">The School object that needs to be outputted.</param>
         * <param name="ay">The AcademicYear object that will have student reports built from.</param>
         * <param name="qual">The current adademic qualification that the student is enrolled on.</param>
         */
        public static void OutputInformation(School sl, AcademicYear ay, string qual)
        {
            // IF statement checks that the School object is not null or empty and whether the AcademicYear object is not null or empty.
            // In addition, the qual string parameter must not be null, empty or full of whitespaces.
            if ((sl != null || sl.GetYears.Count > 0) && (ay != null || ay.GetStudents.Count > 0) && !string.IsNullOrWhiteSpace(qual))
            {
                // Iterates through each Student object contained within the AcademicYear object.
                ay.GetStudents.ForEach(sd =>
                {
                    // Initialises the Stream Writer object to write to the unique path created for each Student object.
                    using System.IO.StreamWriter file = new System.IO.StreamWriter(sd.GetFilePath);

                    file.WriteLine("Academic Record".ToUpper());
                    file.WriteLine(HelperMethods.DynamicSeparator("Academic Record", "*").Trim());

                    file.WriteLine("\nSchool Information:");
                    file.WriteLine(HelperMethods.DynamicSeparator("School Information:", "-").Trim());
                    file.WriteLine("School Name:   " + sl.GetSchoolName + " School");
                    file.WriteLine("Academic Year: " + sd.GetYear);
                    file.WriteLine("Qualification: " + qual);
                    file.WriteLine("Class Name:    " + sd.GetClassName);
                    file.WriteLine("Report Date:   " + GetCurrentDate());
                    file.WriteLine("Report Time:   " + GetCurrentTime());

                    file.WriteLine("\nStudent Details:");
                    file.WriteLine(HelperMethods.DynamicSeparator("Student Details:", "-").Trim());
                    file.WriteLine("Student ID: #" + sd.GetStudentID);
                    file.WriteLine("Forename:   " + sd.GetForename);
                    file.WriteLine("Surname:    " + sd.GetSurname);

                    file.WriteLine("\nSubject Grades:");
                    file.WriteLine(HelperMethods.DynamicSeparator("Subject Grades:", "-").Trim());
                    sd.GetSubjects.ForEach(sub => { file.WriteLine(sub.GetSubjectName + ": " + FixSpacing(sd.GetSubjects, sub) + sub.GetSubjectGrade); });
                });
            }
        }

        /**
         * <summary>This method returns a string of whitespaces that depend on the size of the longest subject name for a student.</summary>
         * 
         * <param name="subjects">The list of subjects that need to be iterated through.</param>
         * <param name="s">The current subject which requires its name to spaced out neatly with additional whitespaces.</param>
         * 
         * <returns>The number of whitespaces required to space out the subject's name.</returns>
         */
        public static string FixSpacing(List<Subject> subjects, Subject s)
        {
            int difference = 0;

            if (subjects != null && subjects.Count > 0 && s != null)
            {
                int maxWordLength = 0;
                int subjectNameLength = s.GetSubjectName.Length;

                subjects.ForEach(_ => { if (_.GetSubjectName.Length > maxWordLength) maxWordLength = _.GetSubjectName.Length; });

                difference = maxWordLength - subjectNameLength;
            }

            return AddSpaces(difference);
        }

        /**
         * <summary>This method simply returns a string of whitespaces with a quantity solely dependant on the int 'no' parameter.</summary>
         * 
         * <param name="no">The number of whitespace characters required.</param>
         * 
         * <returns>Whitespaces with a quantity equal to the 'no' parameter.</returns>
         */
        public static string AddSpaces(int no)
        {
            string spaces = "";

            if (no >= 0)
            {
                for (int i = 0; i < no; i++)
                {
                    spaces += " ";
                }
            }

            return spaces;
        }

        /**
         * <summary>This method returns the current date in the 'DD/MM/YYYY' format.</summary>
         * 
         * <returns>The current date in the specified string format.</returns>
         */
        public static string GetCurrentDate()
        {
            return DateTime.Today.ToString("dd/MM/yyyy");
        }

        /**
         * <summary>This method returns the current time in the 'HH:MM:SS' format.</summary>
         * 
         * <returns>The current time in the specified string format.</returns>
         */
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

    }
}
