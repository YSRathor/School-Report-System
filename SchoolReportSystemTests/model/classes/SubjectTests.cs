using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model.classes;

namespace SchoolReportSystemTests.model.classes
{
    // This class is used to test all suitable methods belonging to the 'Subject' model class.
    [TestClass()]
    public class SubjectTests
    {
        private readonly Subject subject = new Subject("ECON5A", "Economics", "A*", "B");

        [TestMethod()]
        public void GetSubjectIDTest()
        {
            Assert.AreEqual("ECON5A", subject.GetSubjectID);
        }

        [TestMethod()]
        public void GetSubjectNameTest()
        {
            Assert.AreEqual("Economics", subject.GetSubjectName);
        }

        [TestMethod()]
        public void GetActualGradeTest()
        {
            Assert.AreEqual("A*", subject.GetActualGrade);
        }

        [TestMethod()]
        public void GetExpectedGradeTest()
        {
            Assert.AreEqual("B", subject.GetExpectedGrade);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Subject subject2 = new Subject("MED5B", "Media", "B", "B");

            Assert.AreEqual(-1, subject.CompareTo(subject2));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("Economics: A* (B) <ECON5A>", subject.ToString());
        }
    }
}