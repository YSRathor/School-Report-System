using SchoolReportSystem.view.import;
using SchoolReportSystem.view.export;

namespace SchoolReportSystem.view
{
    /** 
     * <summary>This class is responsible for creating instances of the <c>ImportData</c> and <c>ExportData</c> classes.
     * It allows the two instances to be accessed by the <c>ReportBuilder</c> class for importing/exporting data to and from the system.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class DataClass
    {

        // Default Constructor.
        /**
         * <summary>The default constructor responsible for creating a DataClass object.</summary>
         */
        public DataClass()
        {
            GetImportData = new ImportData();
            GetExportData = new ExportData();
        }

        // Methods.
        /**
         * <summary>This method allows the ImportData class to be accessed.</summary>
         * 
         * <returns>Returns the instance of the ImportData class.</returns>
         */
        public ImportData GetImportData { get; }

        /**
         * <summary>This method allows the ExportData class to be accessed.</summary>
         * 
         * <returns>Returns the instance of the ExportData class.</returns>
         */
        public ExportData GetExportData { get; }

    }
}
