using SchoolReportSystem.model.classes;
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
                    case 7: case 8:
                        OutputInformation(sl, ay, "Key Stage 3");
                        break;

                    case 9: case 10: case 11:
                        OutputInformation(sl, ay, "GCSE");
                        break;

                    case 12:
                        OutputInformation(sl, ay, "AS (A Levels)");
                        break;

                    case 13:
                        OutputInformation(sl, ay, "A2 (A Levels)");
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

                    file.WriteLine("Academic Report".ToUpper());
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
                    sd.GetSubjects.ForEach(sub => { file.WriteLine(sub.GetSubjectName + ": " + FixSpacing(sd.GetSubjects, sub, "name")
                        + sub.GetActualGrade + FixSpacing(sd.GetSubjects, sub, "actualGrade") + " (" + sub.GetExpectedGrade + ")" + FixSpacing(sd.GetSubjects, sub, "expectedGrade") 
                        + " <" + sub.GetSubjectID + ">"); });

                    file.WriteLine("\nTarget Statistics:");
                    file.WriteLine(HelperMethods.DynamicSeparator("Target Statistics:", "-").Trim());
                    file.WriteLine("Above Target:   " + sd.GetTargetCount("above"));
                    file.WriteLine("Meeting Target: " + sd.GetTargetCount("equal"));
                    file.WriteLine("Below Target:   " + sd.GetTargetCount("below"));

                    file.WriteLine("\nFeedback:");
                    file.WriteLine(HelperMethods.DynamicSeparator("Feedback:", "-").Trim());
                    file.WriteLine(OutputFeedback(sd.GetFullName, sd.GetTargetCount("above"), sd.GetTargetCount("equal"), sd.GetTargetCount("below")));
                });
            }
        }

        /**
         * <summary>This method returns a message dependent on the performance of the student in terms of their target grades for each subject.</summary>
         * 
         * <param name="fullName">The student's full name.</param>
         * <param name="above">The number of subjects with grades that exceed the target</param>
         * <param name="equal">The number of subjects with grades that meet the target.</param>
         * <param name="below">The number of subjects with grades that lie below the target.</param>
         * 
         * <returns>A message that represents feedback on the overall performance of a student, in relation to their target grades.</returns>
         */
        public static string OutputFeedback(string fullName, int above, int equal, int below)
        {
            string message = "";

            int total = above + equal + below;

            if (above > equal && above > below)
            {
                double average = Math.Round(above / (double)total, 2) * 100;

                if (average == 100)
                {
                    message = fullName + " is outstanding, as they are exceeding their target grades for all of their subjects!";
                }
                else if (average < 100 && average > 50)
                {
                    message = fullName + " is performing excellently, as they are exceeding their target grades for the majority of their subjects!";
                }
                else
                {
                    message = fullName + " is performing well, as they are exceeding their target grades for some of their subjects!";
                }
            }
            else if (equal >= above && equal > below)
            {
                double average = Math.Round(equal / (double)total, 2) * 100;

                if (average == 100)
                {
                    message = fullName + " is performing really well, as they are meeting their target grades for all of their subjects!";
                }
                else if (average < 100 && average > 50)
                {
                    message = fullName + " is performing well, as they are meeting their target grades for most of their subjects!";
                }
                else
                {
                    message = fullName + " is performing okay, as they are meeting their target grades for some of their subjects!";
                }
            }
            else if (above == equal && equal == below)
            {
                message = fullName + " is performing adequately, however they could do more to achieve or exceed their target grades for more subjects!";
            }
            else
            {
                if (below == total)
                {
                    message = fullName + " is performing horribly, as they are below their target grades for all of their subjects!";
                }
                else if (below > above + equal)
                {
                    message = fullName + " is performing very poorly, as they are below their target grades for most of their subjects!";
                }
                else if (below == above + equal)
                {
                    message = fullName + " is performing rather poorly, as they are below their target grades for some of their subjects!";
                }
                else
                {
                    message = fullName + " is performing a bit poorly, as they are below their target grades for a few of their subjects!";
                }
            }

            return message;
        }

        /**
        * <summary>This method returns a string of whitespaces that depend on the size of the field type string for a student.</summary>
        * 
        * <param name="subjects">The list of subjects that need to be iterated through.</param>
        * <param name="s">The current subject which requires one of its fields to be spaced out neatly with additional whitespaces.</param>
        * <param name="type">The type of string that needs to whitespaces inserted next to it.</param>
        * 
        * <returns>The number of whitespaces required to space out the subject's field string.</returns>
        */
        public static string FixSpacing(List<Subject> subjects, Subject s, string type)
        {
            int difference = 0;
            int maxWordLength = 0;
            int currentWordLength = 0;

            if (subjects != null && subjects.Count > 0 && s != null)
            {
                if (!string.IsNullOrWhiteSpace(type))
                {
                    if (type == "name")
                    {
                        currentWordLength = s.GetSubjectName.Length;
                        subjects.ForEach(_ => { if (_.GetSubjectName.Length > maxWordLength) { maxWordLength = _.GetSubjectName.Length; } });
                    }
                    else if (type == "actualGrade")
                    {
                        currentWordLength = s.GetActualGrade.Length;
                        subjects.ForEach(_ => { if (_.GetActualGrade.Length > maxWordLength) { maxWordLength = _.GetActualGrade.Length; } });
                    }
                    else if (type == "expectedGrade")
                    {
                        currentWordLength = s.GetExpectedGrade.Length;
                        subjects.ForEach(_ => { if (_.GetExpectedGrade.Length > maxWordLength) { maxWordLength = _.GetExpectedGrade.Length; } });
                    }
                    else
                    {
                        throw new ArgumentException("Invalid type!");
                    }
                }

                difference = maxWordLength - currentWordLength;
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
