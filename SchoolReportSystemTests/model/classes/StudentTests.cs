using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model.classes;
using System.Collections.Generic;

namespace SchoolReportSystemTests.model.classes
{
    // This class is used to test all suitable methods belonging to the 'Student' model class.
    [TestClass()]
    public class StudentTests
    {
        private readonly Student student = new Student("0186", "Jack", "Kallis", 7, "Scarlett");

        [TestMethod()]
        public void AddSubjectToStudentTest()
        {
            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            Assert.AreEqual(3, student.GetSubjects.Count);
        }

        [TestMethod()]
        public void RemoveDuplicateSubjectsTest()
        {
            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            student.RemoveDuplicateSubjects();

            Assert.AreEqual(3, student.GetSubjects.Count);
        }

        [TestMethod()]
        public void RemoveSubjectTest()
        {
            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            student.RemoveSubject(0);

            Assert.AreEqual(2, student.GetSubjects.Count);
        }

        [TestMethod()]
        public void GetStudentIDTest()
        {
            Assert.AreEqual("0186", student.GetStudentID);
        }

        [TestMethod()]
        public void GetForenameTest()
        {
            Assert.AreEqual("Jack", student.GetForename);
        }

        [TestMethod()]
        public void GetSurnameIDTest()
        {
            Assert.AreEqual("Kallis", student.GetSurname);
        }

        [TestMethod()]
        public void GetFullNameIDTest()
        {
            Assert.AreEqual("Jack Kallis", student.GetFullName);
        }

        [TestMethod()]
        public void GetYearTest()
        {
            Assert.AreEqual(7, student.GetYear);
        }

        [TestMethod()]
        public void GetClassNameTest()
        {
            Assert.AreEqual("Scarlett", student.GetClassName);
        }

        [TestMethod()]
        public void GetSubjectTest()
        {
            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            Subject s = student.GetSubjects[0];

            Assert.AreEqual(s, student.GetSubject(0));
        }

        [TestMethod()]
        public void GetSubjectsTest()
        {
            bool allSame = true;

            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            List<Subject> subjects = new List<Subject>
            {
                new Subject("PSY3", "Psychology", "B", "A"),
                new Subject("SOC3", "Sociology", "A", "B"),
                new Subject("PHI3", "Philosophy", "A*", "A*")
            };

            student.GetSubjects.Sort();
            subjects.Sort();

            if (student.GetSubjects.Count == subjects.Count)
            {
                for (int i = 0; i < student.GetSubjects.Count; i++)
                {
                    if (student.GetSubject(i).GetSubjectName != subjects[i].GetSubjectName)
                    {
                        allSame = false;
                    }
                }
            }
            else
            {
                allSame = false;
            }

            Assert.IsTrue(allSame);
        }

        [TestMethod()]
        public void GetFilePathTest()
        {
            Assert.AreEqual("C:/Users/Student Results/Year 7/Jack Kallis.txt", student.GetFilePath);
        }

        [TestMethod()]
        public void GetTargetCountTest()
        {
            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            Assert.AreEqual(1, student.GetTargetCount("above"));
            Assert.AreEqual(1, student.GetTargetCount("equal"));
            Assert.AreEqual(1, student.GetTargetCount("below"));
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Student student2 = new Student("0075", "Abraham", "Markam", 7, "Emerald");

            Assert.AreEqual(1, student.CompareTo(student2));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            student.AddSubjectToStudent(new Subject("PSY3", "Psychology", "B", "A"));
            student.AddSubjectToStudent(new Subject("SOC3", "Sociology", "A", "B"));
            student.AddSubjectToStudent(new Subject("PHI3", "Philosophy", "A*", "A*"));

            string output =
                "\nID: #0186" +
                "\nForename: Jack" +
                "\nSurname: Kallis" +
                "\nYear: 7" +
                "\nClass: Scarlett" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nPsychology: B (A) <PSY3>" +
                "\nSociology: A (B) <SOC3>" +
                "\nPhilosophy: A* (A*) <PHI3>\n";

            Assert.AreEqual(output, student.ToString());
        }
    }
}
