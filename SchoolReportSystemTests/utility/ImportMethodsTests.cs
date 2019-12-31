using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.utility;
using Xunit.Sdk;

namespace SchoolReportSystemTests.utility
{
    // This class is used to test all suitable methods belonging to the 'ImportMethods' utility class.
    [TestClass()]
    public class ImportMethodsTests
    {
        [TestMethod()]
        public void CapitaliseFirstLetterTest()
        {
            Assert.AreEqual("Debunked", ImportMethods.CapitaliseFirstLetter("dEBunKed"));
        }

        [TestMethod()]
        public void ShortenSubjectNameTest()
        {
            Assert.AreEqual("P.E", ImportMethods.ShortenSubjectName("Physical Education"));
        }
    }
}