using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.utility;

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
        public void DynamicSeparatorTest()
        {
            Assert.AreEqual("\n-----------", HelperMethods.DynamicSeparator("hello world", "-"));
        }
    }
}