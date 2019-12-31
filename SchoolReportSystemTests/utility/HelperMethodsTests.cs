using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.utility;
using Xunit.Sdk;

namespace SchoolReportSystemTests.utility
{
    // This class is used to test all suitable methods belonging to the 'HelperMethods' utility class.
    [TestClass()]
    public class HelperMethodsTests
    {
        [TestMethod()]
        public void CapitaliseFirstLetterOfEachWordTest()
        {
            Assert.AreEqual("Lorum Ipsum", HelperMethods.CapitaliseFirstLetterOfEachWord("lOrUm iPsUM"));
        }

        [TestMethod()]
        public void CheckNoSubjectsForYearTest()
        {
            Assert.AreEqual(9, HelperMethods.CheckNoSubjectsForYear(10));
        }

        [TestMethod()]
        public void DynamicSeparatorTest()
        {
            Assert.AreEqual("\n-----------", HelperMethods.DynamicSeparator("hello world", "-"));
        }
    }
}