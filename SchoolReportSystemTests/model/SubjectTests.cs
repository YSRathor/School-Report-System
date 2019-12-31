using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model;
using Xunit.Sdk;

namespace SchoolReportSystemTests.model
{
    // This class is used to test all suitable methods belonging to the 'Subject' model class.
    [TestClass()]
    public class SubjectTests
    {
        private readonly Subject subject = new Subject("Economics", "A*");

        [TestMethod()]
        public void GetSubjectNameTest()
        {
            Assert.AreEqual("Economics", subject.GetSubjectName);
        }

        [TestMethod()]
        public void GetSubjectGradeTest()
        {
            Assert.AreEqual("A*", subject.GetSubjectGrade);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Subject subject2 = new Subject("Media", "B");

            Assert.AreEqual(-1, subject.CompareTo(subject2));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("Economics: A*", subject.ToString());
        }
    }
}