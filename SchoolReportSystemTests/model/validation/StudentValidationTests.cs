using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model.validation;

namespace SchoolReportSystemTests.model.validation
{
    // This class is used to test all suitable methods belonging to the 'StudentValidation' utility class.
    [TestClass()]
    public class StudentValidationTests
    {
        [TestMethod()]
        public void IsStudentIDValidTest()
        {
            Assert.AreEqual((false, "Missing student ID!"), StudentValidation.IsStudentIDValid(7, ""));
            Assert.AreEqual((false, "Invalid student ID, only numeric digits allowed!"), StudentValidation.IsStudentIDValid(7, "1A*2"));
            Assert.AreEqual((false, "Invalid student ID, no more than 4 digits allowed!"), StudentValidation.IsStudentIDValid(7, "13245"));
            Assert.AreEqual((false, "Invalid student ID for Year 7 student!"), StudentValidation.IsStudentIDValid(7, "1736"));
            Assert.AreEqual((true, ""), StudentValidation.IsStudentIDValid(7, "0050"));
        }

        [TestMethod()]
        public void CheckStudentIDRangeTest()
        {
            Assert.IsFalse(StudentValidation.CheckStudentIDRange(7, "2001"));
            Assert.IsTrue(StudentValidation.CheckStudentIDRange(8, "1412"));
            Assert.IsFalse(StudentValidation.CheckStudentIDRange(9, "8920"));
            Assert.IsTrue(StudentValidation.CheckStudentIDRange(10, "4500"));
            Assert.IsFalse(StudentValidation.CheckStudentIDRange(11, "0050"));
            Assert.IsTrue(StudentValidation.CheckStudentIDRange(12, "7056"));
            Assert.IsFalse(StudentValidation.CheckStudentIDRange(13, "5012"));
        }

        [TestMethod()]
        public void IsStudentNameValidTest()
        {
            Assert.AreEqual((false, "Missing student name type!"), StudentValidation.IsStudentNameValid("Carlos", null));
            Assert.AreEqual((false, "Missing student first name!"), StudentValidation.IsStudentNameValid(null, "first name"));
            Assert.AreEqual((false, "Invalid student name type!"), StudentValidation.IsStudentNameValid("Tyler", "forename"));
            Assert.AreEqual((false, "Invalid student first name, no special or numeric characters allowed!"), StudentValidation.IsStudentNameValid("Martin/James", "first name"));
            Assert.AreEqual((false, "Invalid student first name!"), StudentValidation.IsStudentNameValid("Ryan--Mark", "first name"));
            Assert.AreEqual((true, ""), StudentValidation.IsStudentNameValid("Stuart", "first name"));
        }
    }
}
