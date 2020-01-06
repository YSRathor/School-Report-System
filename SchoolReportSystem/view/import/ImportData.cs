using SchoolReportSystem.model.classes;

namespace SchoolReportSystem.view.import
{

    /** 
     * <summary>This class is responsible for providing a main method which executes both main methods from the <c>ImportStudentData</c> and <c>ImportSubjectData</c> classes.
     * 
     * <author>Author: Yashwant Rathor</author></summary>
     */

    public class ImportData
    {

        // Default Constructor.
        /**
         * <summary>The default constructor responsible for creating a DataClass object.</summary>
         */
        public ImportData()
        {
            GetImportStudentData = new ImportStudentData();
            GetImportSubjectData = new ImportSubjectData();
        }

        // Methods.
        /**
         * <summary>This method allows the ImportStudentData class to be accessed.</summary>
         * 
         * <returns>Returns the instance of the ImportStudentData class.</returns>
         */
        private ImportStudentData GetImportStudentData { get; }

        /**
         * <summary>This method allows the ImportSubjectData class to be accessed.</summary>
         * 
         * <returns>Returns the instance of the ImportSubjectData class.</returns>
         */
        private ImportSubjectData GetImportSubjectData { get; }

        /**
         * <summary>This method is responsible for loading all student and subject data into the model classes.</summary>
         * 
         * <param name="school">The School object to have all data stored inside it.</param>
         */
        public void LoadAllData(School school)
        {
            // Invokes the 'LoadStudentData' method for the School object.
            GetImportStudentData.LoadStudentData(school);

            // Invokes the 'LoadSubjectData' method for each AcademicYear object in school.
            school.GetYears.ForEach(year => { GetImportSubjectData.LoadSubjectData(year); });
        }

    }
}
