using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model.classes;
using SchoolReportSystem.utility;
using System.Collections.Generic;

namespace SchoolReportSystemTests.utility
{
    // This class is used to test all suitable methods belonging to the 'ExportrMethods' utility class.
    [TestClass()]
    public class ExportMethodsTests
    {
        [TestMethod()]
        public void OutputFeedbackTest()
        {
            Assert.AreEqual("Julia Walters is outstanding, as they are exceeding their target grades for all of their subjects!", ExportMethods.OutputFeedback("Julia Walters", 4, 0, 0));
            Assert.AreEqual("Julia Walters is performing excellently, as they are exceeding their target grades for the majority of their subjects!", ExportMethods.OutputFeedback("Julia Walters", 3, 1, 0));
            Assert.AreEqual("Julia Walters is performing well, as they are exceeding their target grades for some of their subjects!", ExportMethods.OutputFeedback("Julia Walters", 2, 1, 1));

            Assert.AreEqual("Gareth Keenan is performing really well, as they are meeting their target grades for all of their subjects!", ExportMethods.OutputFeedback("Gareth Keenan", 0, 9, 0));
            Assert.AreEqual("Gareth Keenan is performing well, as they are meeting their target grades for most of their subjects!", ExportMethods.OutputFeedback("Gareth Keenan", 2, 6, 1));
            Assert.AreEqual("Gareth Keenan is performing okay, as they are meeting their target grades for some of their subjects!", ExportMethods.OutputFeedback("Gareth Keenan", 2, 4, 3));

            Assert.AreEqual("Brent Pritchard is performing adequately, however they could do more to achieve or exceed their target grades for more subjects!", ExportMethods.OutputFeedback("Brent Pritchard", 1, 1, 1));

            Assert.AreEqual("Mike Riley is performing horribly, as they are below their target grades for all of their subjects!", ExportMethods.OutputFeedback("Mike Riley", 0, 0, 14));
            Assert.AreEqual("Mike Riley is performing very poorly, as they are below their target grades for most of their subjects!", ExportMethods.OutputFeedback("Mike Riley", 2, 4, 8));
            Assert.AreEqual("Mike Riley is performing rather poorly, as they are below their target grades for some of their subjects!", ExportMethods.OutputFeedback("Mike Riley", 4, 3, 7));
            Assert.AreEqual("Mike Riley is performing a bit poorly, as they are below their target grades for a few of their subjects!", ExportMethods.OutputFeedback("Mike Riley", 5, 3, 6));
        }

        [TestMethod()]
        public void FixSpacingTest()
        {
            List<Subject> subjects = new List<Subject>
            {
                new Subject("MAT4", "Mathematics", "A*", "C"),
                new Subject("PHY4", "Physics", "B", "A*"),
                new Subject("GEO4", "Geography", "C", "B")
            };

            Assert.AreEqual("", ExportMethods.FixSpacing(subjects, subjects[0], "name"));
            Assert.AreEqual(" ", ExportMethods.FixSpacing(subjects, subjects[1], "actualGrade"));
            Assert.AreEqual(" ", ExportMethods.FixSpacing(subjects, subjects[2], "expectedGrade"));
        }

        [TestMethod()]
        public void AddSpacesTest()
        {
            Assert.AreEqual("     ".Length, ExportMethods.AddSpaces(5).Length);
        }

        [TestMethod()]
        public void GetCurrentDateTest()
        {
            Assert.AreEqual("06/01/2020", ExportMethods.GetCurrentDate());
        }

        [TestMethod()]
        public void GetCurrentTimeTest()
        {
            Assert.AreEqual("18:22:00", ExportMethods.GetCurrentTime());
        }
    }
}
