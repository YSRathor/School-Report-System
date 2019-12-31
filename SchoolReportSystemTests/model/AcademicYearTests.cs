﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model;
using System.Collections;
using System.Collections.Generic;
using Xunit.Sdk;

namespace SchoolReportSystemTests.model
{
    // This class is used to test all suitable methods belonging to the 'AcademicYear' model class.
    [TestClass()]
    public class AcademicYearTests
    {
        private readonly AcademicYear year = new AcademicYear(11);

        [TestMethod()]
        public void AddStudentToYearTest()
        {
            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));

            Assert.AreEqual(3, year.GetStudents.Count);
        }

        [TestMethod()]
        public void RemoveStudentFromYearTest()
        {
            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));
            year.AddStudentToYear(new Student("6003", "David", "Koechner", 11, "Green"));

            year.RemoveStudentFromYear(3);

            Assert.AreEqual(3, year.GetStudents.Count);
        }

        [TestMethod()]
        public void RemoveDuplicateStudentsTest()
        {
            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));

            year.RemoveDuplicateStudents();

            Assert.AreEqual(3, year.GetStudents.Count);
        }

        [TestMethod()]
        public void GetYearNo()
        {
            Assert.AreEqual(11, year.GetYearNo);
        }

        [TestMethod()]
        public void GetStudentTest()
        {
            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));

            Student s = year.GetStudents[2];

            Assert.AreEqual(s, year.GetStudent(2));
        }

        [TestMethod()]
        public void GetStudentByIdTest()
        {
            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));

            Student s = year.GetStudents[1];

            Assert.AreEqual(s, year.GetStudentById("6001"));
        }

        [TestMethod()]
        public void GetStudents()
        {
            bool allSame = true;

            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));

            List<Student> students = new List<Student>
            {
                new Student("6000", "John", "Krascinski", 11, "Peach"),
                new Student("6001", "Rainn", "Wilson", 11, "Blue"),
                new Student("6002", "Jenna", "Fischer", 11, "Crimson")
            };

            year.GetStudents.Sort();
            students.Sort();

            if (year.GetStudents.Count == students.Count)
            {
                for (int i = 0; i < year.GetStudents.Count; i++)
                {
                    if (year.GetStudent(i).GetStudentID != students[i].GetStudentID)
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
        public void GetEnumeratorTest()
        {
            Assert.AreEqual(((IEnumerable)year.GetStudents).GetEnumerator(), year.GetEnumerator());
        }

        [TestMethod()]
        public void CompareToTest()
        {
            AcademicYear year2 = new AcademicYear(13);

            Assert.AreEqual(-1, year.CompareTo(year2));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            year.AddStudentToYear(new Student("6000", "John", "Krascinski", 11, "Peach"));
            year.AddStudentToYear(new Student("6001", "Rainn", "Wilson", 11, "Blue"));
            year.AddStudentToYear(new Student("6002", "Jenna", "Fischer", 11, "Crimson"));

            year.GetStudent(0).AddSubjectToStudent(new Subject("Maths", "A*"));
            year.GetStudent(0).AddSubjectToStudent(new Subject("Physics", "B"));
            year.GetStudent(0).AddSubjectToStudent(new Subject("Psychology", "D"));

            year.GetStudent(1).AddSubjectToStudent(new Subject("Chemistry", "C"));
            year.GetStudent(1).AddSubjectToStudent(new Subject("French", "D"));
            year.GetStudent(1).AddSubjectToStudent(new Subject("I.T", "A"));

            year.GetStudent(2).AddSubjectToStudent(new Subject("German", "B"));
            year.GetStudent(2).AddSubjectToStudent(new Subject("Biology", "C"));
            year.GetStudent(2).AddSubjectToStudent(new Subject("Sociology", "D"));

            string output =
                "\n\nAcademic Year: 11" +
                "\n-----------------" +
                "\n" +
                "\nID: #6000" +
                "\nForename: John" +
                "\nSurname: Krascinski" +
                "\nYear: 11" +
                "\nClass: Peach" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nMaths: A*" +
                "\nPhysics: B" +
                "\nPsychology: D\n" +
                "\n" +
                "\nID: #6001" +
                "\nForename: Rainn" +
                "\nSurname: Wilson" +
                "\nYear: 11" +
                "\nClass: Blue" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nChemistry: C" +
                "\nFrench: D" +
                "\nI.T: A\n" +
                "\n" +
                "\nID: #6002" +
                "\nForename: Jenna" +
                "\nSurname: Fischer" +
                "\nYear: 11" +
                "\nClass: Crimson" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nGerman: B" +
                "\nBiology: C" +
                "\nSociology: D";

            Assert.AreEqual(output, year.ToString());
        }
    }
}