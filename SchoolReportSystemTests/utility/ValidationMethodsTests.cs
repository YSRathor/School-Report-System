using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.utility;
using Xunit.Sdk;

namespace SchoolReportSystemTests.utility
{
    // This class is used to test all suitable methods belonging to the 'ValidationMethods' utility class.
    [TestClass()]
    public class ValidationMethodsTests
    {
        [TestMethod()]
        public void IsSubjectNameValidTest()
        {
            Assert.AreEqual((false, "Missing subject name!"), ValidationMethods.IsSubjectNameValid(""));
            Assert.AreEqual((false, "Invalid subject name, no special or numeric characters allowed!"), ValidationMethods.IsSubjectNameValid("M*aths"));
            Assert.AreEqual((false, "Invalid subject name!"), ValidationMethods.IsSubjectNameValid("Design && Technology"));
            Assert.AreEqual((true, ""), ValidationMethods.IsSubjectNameValid("Physics"));
        }

        [TestMethod()]
        public void IsIDValidTest()
        {
            Assert.AreEqual((false, "Missing student ID!"), ValidationMethods.IsIDValid(7, ""));
            Assert.AreEqual((false, "Invalid student ID, only numeric digits allowed!"), ValidationMethods.IsIDValid(7, "1A*2"));
            Assert.AreEqual((false, "Invalid student ID, no more than 4 digits allowed!"), ValidationMethods.IsIDValid(7, "13245"));
            Assert.AreEqual((false, "Invalid student ID for Year 7 student!"), ValidationMethods.IsIDValid(7, "1736"));
            Assert.AreEqual((true, ""), ValidationMethods.IsIDValid(7, "0050"));
        }

        [TestMethod()]
        public void CheckIDRangeTest()
        {
            Assert.IsFalse(ValidationMethods.CheckIDRange(7, "2001"));
            Assert.IsTrue(ValidationMethods.CheckIDRange(8, "1412"));
            Assert.IsFalse(ValidationMethods.CheckIDRange(9, "8920"));
            Assert.IsTrue(ValidationMethods.CheckIDRange(10, "4500"));
            Assert.IsFalse(ValidationMethods.CheckIDRange(11, "0050"));
            Assert.IsTrue(ValidationMethods.CheckIDRange(12, "7056"));
            Assert.IsFalse(ValidationMethods.CheckIDRange(13, "5012"));
        }

        [TestMethod()]
        public void IsStudentNameValidTest()
        {
            Assert.AreEqual((false, "Missing student first name!"), ValidationMethods.IsStudentNameValid(null, "first name"));
            Assert.AreEqual((false, "Invalid student first name, no special or numeric characters allowed!"), ValidationMethods.IsStudentNameValid("Martin/James", "first name"));
            Assert.AreEqual((false, "Invalid student first name!"), ValidationMethods.IsStudentNameValid("Ryan--Mark", "first name"));
            Assert.AreEqual((false, "Invalid student first name, no special or numeric characters allowed!"), ValidationMethods.IsStudentNameValid("M*cheal", "first name"));
            Assert.AreEqual((true, ""), ValidationMethods.IsStudentNameValid("Stuart", "first name"));
        }

        [TestMethod()]
        public void IsSchoolNameValidTest()
        {
            Assert.AreEqual((false, "Missing value for school name!"), ValidationMethods.IsSchoolNameValid("  "));
            Assert.AreEqual((false, "Invalid school name, no special or numeric characters allowed!"), ValidationMethods.IsSchoolNameValid("Spring-well"));
            Assert.AreEqual((false, "Invalid school name!"), ValidationMethods.IsSchoolNameValid("Spring  well"));
            Assert.AreEqual((true, ""), ValidationMethods.IsSchoolNameValid("Springwell"));
        }
    }
}