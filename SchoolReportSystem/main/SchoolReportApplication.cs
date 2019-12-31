using SchoolReportSystem.controller;

namespace SchoolReportSystem.main
{
    /** 
     * <summary>This class is responsible for running the entire program.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class SchoolReportApplication
    {

        // Main method.
        /**
         * <summary>This method runs the entire program.</summary>
         */
        public static void Main(string[] args)
        {
            ReportBuilder rb = new ReportBuilder(); // Creates a new instance of the ReportBuilder class.
            rb.GenerateReports(); // Executes the 'GenerateReports' method from the ReportBuilder class.
        }

    }
}
