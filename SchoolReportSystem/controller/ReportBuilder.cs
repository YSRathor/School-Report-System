using SchoolReportSystem.model;
using SchoolReportSystem.utility;
using SchoolReportSystem.view;

namespace SchoolReportSystem.controller
{
    /** 
     * <summary>This class is responsible for creating instances of the <c>School</c>, <c>DataClass</c>, <c>ImportData</c> and <c>ExportData</c> classes.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class ReportBuilder
    {
        // Fields.
        private readonly School model;
        private readonly DataClass view;
        private readonly ImportData imD;
        private readonly ExportData exD;

        // Default Constructor.
        /**
         * <summary>The default constructor responsible for creating a ReportBuilder object.</summary>
         */
        public ReportBuilder()
        {
            model = new School(HelperMethods.CapitaliseFirstLetterOfEachWord("BrookLands High"), 7, 13);
            view = new DataClass();
            imD = view.GetImportData;
            exD = view.GetExportData;
        }

        // Main method.
        /**
         * <summary>This method effectively executes both import/export main methods for the system.</summary>
         */
        public void GenerateReports()
        {
            if (model != null)
            {
                // Invokes the 'LoadData' method from the ImportData class, in order to store the data to the model classes.
                imD.LoadData(model);
                // Invokes the 'StoreData' method from the ExportData class, in order to output the data into reports for each student.
                exD.StoreData(model);
            }
        }

    }
}
