using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model;
using System.Collections.Generic;
using Xunit.Sdk;

namespace SchoolReportSystemTests.model
{
    // This class is used to test all suitable methods belonging to the 'Student' model class.
    [TestClass()]
    public class StudentTests
    {
        private readonly Student student = new Student("0186", "Jack", "Kallis", 7, "Scarlett");

        [TestMethod()]
        public void AddSubjectToStudentTest()
        {
            student.AddSubjectToStudent(new Subject("Psychology", "B"));
            student.AddSubjectToStudent(new Subject("Sociology", "A"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));

            Assert.AreEqual(3, student.GetSubjects.Count);
        }

        [TestMethod()]
        public void RemoveDuplicateSubjectsTest()
        {
            student.AddSubjectToStudent(new Subject("Psychology", "B"));
            student.AddSubjectToStudent(new Subject("Sociology", "A"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));

            student.RemoveDuplicateSubjects();

            Assert.AreEqual(3, student.GetSubjects.Count);
        }

        [TestMethod()]
        public void RemoveSubjectTest()
        {
            student.AddSubjectToStudent(new Subject("Psychology", "B"));
            student.AddSubjectToStudent(new Subject("Sociology", "A"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));

            student.RemoveSubject(0);

            Assert.AreEqual(3, student.GetSubjects.Count);
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
            student.AddSubjectToStudent(new Subject("Psychology", "B"));
            student.AddSubjectToStudent(new Subject("Sociology", "A"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));

            Subject s = student.GetSubjects[0];

            Assert.AreEqual(s, student.GetSubject(0));
        }

        [TestMethod()]
        public void GetSubjectsTest()
        {
            bool allSame = true;

            student.AddSubjectToStudent(new Subject("Psychology", "B"));
            student.AddSubjectToStudent(new Subject("Sociology", "A"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));

            List<Subject> subjects = new List<Subject>
            {
                new Subject("Psychology", "B"),
                new Subject("Sociology", "A"),
                new Subject("Philosophy", "A*")
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
            Assert.AreEqual("C:/Users/Yash/Desktop/Student Results/Year 7/Jack Kallis.txt", student.GetFilePath);
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
            student.AddSubjectToStudent(new Subject("Psychology", "B"));
            student.AddSubjectToStudent(new Subject("Sociology", "A"));
            student.AddSubjectToStudent(new Subject("Philosophy", "A*"));

            string output =
                "\nID: #0186" +
                "\nForename: Jack" +
                "\nSurname: Kallis" +
                "\nYear: 7" +
                "\nClass: Scarlett" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nPsychology: B" +
                "\nSociology: A" +
                "\nPhilosophy: A*\n";

            Assert.AreEqual(output, student.ToString());
        }
    }
}