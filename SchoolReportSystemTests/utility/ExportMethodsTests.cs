using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model;
using SchoolReportSystem.utility;
using System.Collections.Generic;
using Xunit.Sdk;

namespace SchoolReportSystemTests.utility
{
    // This class is used to test all suitable methods belonging to the 'ExportrMethods' utility class.
    [TestClass()]
    public class ExportMethodsTests
    {
        [TestMethod()]
        public void FixSpacingTest()
        {
            List<Subject> subjects = new List<Subject>
            {
                new Subject("Mathematics", "A"),
                new Subject("Physics", "B"),
                new Subject("Geography", "C")
            };

            Assert.AreEqual("    ", ExportMethods.FixSpacing(subjects, subjects[1]));
        }

        [TestMethod()]
        public void AddSpacesTest()
        {
            Assert.AreEqual("     ".Length, ExportMethods.AddSpaces(5).Length);
        }

        [TestMethod()]
        public void GetCurrentDateTest()
        {
            Assert.AreEqual("31/12/2019", ExportMethods.GetCurrentDate());
        }

        [TestMethod()]
        public void GetCurrentTimeTest()
        {
            Assert.AreEqual("15:32:00", ExportMethods.GetCurrentTime());
        }
    }
}
