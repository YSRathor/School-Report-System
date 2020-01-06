using SchoolReportSystem.model.classes;
using SchoolReportSystem.utility;

namespace SchoolReportSystem.view.export
{

    /** 
     * <summary>This class is responsible for providing a main method, which allows data to be read from the <c>model</c> and stored into reports (text files).
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class ExportData
    {
        /**
         * <summary>This method effectively stores student data from the model classes into individual reports.</summary>
         * 
         * <param name="sl">The School object where the data will be retrieved from.</param>
         */
        public void StoreReportData(School sl)
        {

            // IF statement checks the School object supplied as a parameter is not null.
            if (sl != null)
            {
                // Iterates through each AcademicYear object within the School object.
                sl.GetYears.ForEach(year =>
                {
                    //Invokes the 'GenerateOutput' method from the ExportMethods utility class.
                    ExportMethods.GenerateOutput(sl, year);
                });
            }

        }
    }
}
