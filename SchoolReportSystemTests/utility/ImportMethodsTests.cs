using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.utility;

namespace SchoolReportSystemTests.utility
{
    // This class is used to test all suitable methods belonging to the 'ImportMethods' utility class.
    [TestClass()]
    public class ImportMethodsTests
    {
        [TestMethod()]
        public void CheckSubjectIDForStudentTest()
        {
            Assert.AreEqual(false, ImportMethods.CheckSubjectIDForStudent(7, "5A"));
            Assert.AreEqual(true, ImportMethods.CheckSubjectIDForStudent(8, "3"));
            Assert.AreEqual(false, ImportMethods.CheckSubjectIDForStudent(9, "3"));
            Assert.AreEqual(true, ImportMethods.CheckSubjectIDForStudent(10, "4"));
            Assert.AreEqual(false, ImportMethods.CheckSubjectIDForStudent(11, "5"));
            Assert.AreEqual(true, ImportMethods.CheckSubjectIDForStudent(12, "5A"));
            Assert.AreEqual(false, ImportMethods.CheckSubjectIDForStudent(13, "2"));
        }

        [TestMethod()]
        public void CheckNoSubjectsForYearTest()
        {
            Assert.AreEqual(9, ImportMethods.CheckNoSubjectsForYear(10));
        }

        [TestMethod()]
        public void CapitaliseFirstLetterTest()
        {
            Assert.AreEqual("A*", ImportMethods.CapitaliseFirstLetter("a*", true));
            Assert.AreEqual("Debunked", ImportMethods.CapitaliseFirstLetter("dEBunKed", false));
        }

        [TestMethod()]
        public void CapitaliseAlphaNumericTest()
        {
            Assert.AreEqual("PSY5A", ImportMethods.CapitaliseAlphaNumeric("psY5a"));
        }

        [TestMethod()]
        public void ShortenSubjectNameTest()
        {
            Assert.AreEqual("Fur. Maths", ImportMethods.ShortenSubjectName("Further Mathematics"));
            Assert.AreEqual("D&T", ImportMethods.ShortenSubjectName("Design & Technology"));
            Assert.AreEqual("P.E", ImportMethods.ShortenSubjectName("Physical Education"));
            Assert.AreEqual("Maths", ImportMethods.ShortenSubjectName("Mathematics"));
            Assert.AreEqual("Physics", ImportMethods.ShortenSubjectName("Physics"));
        }
    }
}