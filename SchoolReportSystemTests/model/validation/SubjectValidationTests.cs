using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model.validation;

namespace SchoolReportSystemTests.model.validation
{
    // This class is used to test all suitable methods belonging to the 'SubjectValidation' utility class.
    [TestClass()]
    public class SubjectValidationTests
    {
        [TestMethod()]
        public void IsSubjectIDValidTest()
        {
            Assert.AreEqual((false, "Missing subject ID!"), SubjectValidation.IsSubjectIDValid(""));
            Assert.AreEqual((false, "Invalid subject ID, no special characters allowed!"), SubjectValidation.IsSubjectIDValid("MAT*3"));
            Assert.AreEqual((false, "Invalid subject ID, only a single digit is allowed!"), SubjectValidation.IsSubjectIDValid("PSY43"));
            Assert.AreEqual((true, ""), SubjectValidation.IsSubjectIDValid("BIO5B"));
        }

        [TestMethod()]
        public void IsSubjectNameValidTest()
        {
            Assert.AreEqual((false, "Missing subject name!"), SubjectValidation.IsSubjectNameValid(""));
            Assert.AreEqual((false, "Invalid subject name, no special or numeric characters allowed!"), SubjectValidation.IsSubjectNameValid("M*aths"));
            Assert.AreEqual((false, "Invalid subject name!"), SubjectValidation.IsSubjectNameValid("Design && Technology"));
            Assert.AreEqual((true, ""), SubjectValidation.IsSubjectNameValid("Physics"));
        }

        [TestMethod()]
        public void IsSubjectGradeValidTest()
        {
            Assert.AreEqual((false, "Missing subject type!"), SubjectValidation.IsSubjectGradeValid("B", ""));
            Assert.AreEqual((false, "Missing subject actual grade!"), SubjectValidation.IsSubjectGradeValid("", "actual"));
            Assert.AreEqual((false, "Invalid subject type!"), SubjectValidation.IsSubjectGradeValid("A", "attainment"));
            Assert.AreEqual((false, "Invalid subject grade!"), SubjectValidation.IsSubjectGradeValid("F", "actual"));
            Assert.AreEqual((true, ""), SubjectValidation.IsSubjectGradeValid("A", "actual"));
        }
    }
}
