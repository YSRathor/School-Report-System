using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model.validation;

namespace SchoolReportSystemTests.model.validation
{
    // This class is used to test all suitable methods belonging to the 'SchoolValidation' utility class.
    [TestClass()]
    public class SchoolValidationTests
    {
        [TestMethod()]
        public void IsSchoolNameValidTest()
        {
            Assert.AreEqual((false, "Missing value for school name!"), SchoolValidation.IsSchoolNameValid("  "));
            Assert.AreEqual((false, "Invalid school name, no special or numeric characters allowed!"), SchoolValidation.IsSchoolNameValid("Spring-well"));
            Assert.AreEqual((false, "Invalid school name!"), SchoolValidation.IsSchoolNameValid("Spring  well"));
            Assert.AreEqual((true, ""), SchoolValidation.IsSchoolNameValid("Springwell"));
        }
    }
}
