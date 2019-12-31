using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolReportSystem.model;
using System.Collections;
using System.Collections.Generic;
using Xunit.Sdk;

namespace SchoolReportSystemTests.model
{
    // This class is used to test all suitable methods belonging to the 'School' model class.
    [TestClass()]
    public class SchoolTests
    {
        private readonly School school = new School("Slough Grammar", 7, 11);

        [TestMethod()]
        public void AddYearsTest()
        {
            school.AddYears(12, 13);

            Assert.AreEqual(7, school.GetYears.Count);
        }

        [TestMethod()]
        public void AddYearTest()
        {
            school.AddYear(new AcademicYear(12));

            Assert.AreEqual(6, school.GetYears.Count);
        }

        [TestMethod()]
        public void GetSchoolNameTest()
        {
            Assert.AreEqual("Slough Grammar", school.GetSchoolName);
        }

        [TestMethod()]
        public void GetYearByNoTest()
        {
            school.AddYears(12, 13);

            AcademicYear year = school.GetYears[5];

            Assert.AreEqual(year, school.GetYearByNo(12));
        }

        [TestMethod()]
        public void GetYearsTest()
        {
            bool allSame = true;

            List<AcademicYear> years = new List<AcademicYear>
            {
                new AcademicYear(7),
                new AcademicYear(8),
                new AcademicYear(9),
                new AcademicYear(10),
                new AcademicYear(11),
            };

            years.Sort();

            if (school.GetYears.Count == years.Count)
            {
                for (int i = 0; i < school.GetYears.Count; i++)
                {
                    if (school.GetYears[i].GetYearNo != years[i].GetYearNo)
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
            Assert.AreEqual(((IEnumerable)school.GetYears).GetEnumerator(), school.GetEnumerator());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            school.GetYears[0].AddStudentToYear(new Student("0039", "Darren", "Williams", 7, "Violet"));
            school.GetYears[0].GetStudentById("0039").AddSubjectToStudent(new Subject("Chemistry", "A"));
            school.GetYears[0].GetStudentById("0039").AddSubjectToStudent(new Subject("Physics", "B"));
            school.GetYears[0].GetStudentById("0039").AddSubjectToStudent(new Subject("Biology", "B"));

            school.GetYears[1].AddStudentToYear(new Student("1693", "Benjamin", "Tennyson", 8, "Cyan"));
            school.GetYears[1].GetStudentById("1693").AddSubjectToStudent(new Subject("Law", "C"));
            school.GetYears[1].GetStudentById("1693").AddSubjectToStudent(new Subject("Psychology", "B"));
            school.GetYears[1].GetStudentById("1693").AddSubjectToStudent(new Subject("Economics", "C"));

            school.GetYears[2].AddStudentToYear(new Student("3525", "Jason", "Banton", 9, "Orange"));
            school.GetYears[2].GetStudentById("3525").AddSubjectToStudent(new Subject("Textiles", "B"));
            school.GetYears[2].GetStudentById("3525").AddSubjectToStudent(new Subject("History", "A"));
            school.GetYears[2].GetStudentById("3525").AddSubjectToStudent(new Subject("German", "C"));

            school.GetYears[3].AddStudentToYear(new Student("5362", "Marcus", "Leatherfield", 10, "Turquoise"));
            school.GetYears[3].GetStudentById("5362").AddSubjectToStudent(new Subject("Geography", "A"));
            school.GetYears[3].GetStudentById("5362").AddSubjectToStudent(new Subject("Art", "C"));
            school.GetYears[3].GetStudentById("5362").AddSubjectToStudent(new Subject("French", "A"));

            school.GetYears[4].AddStudentToYear(new Student("6952", "Tray", "Jasper", 11, "Maroon"));
            school.GetYears[4].GetStudentById("6952").AddSubjectToStudent(new Subject("Maths", "B"));
            school.GetYears[4].GetStudentById("6952").AddSubjectToStudent(new Subject("Spanish", "B"));
            school.GetYears[4].GetStudentById("6952").AddSubjectToStudent(new Subject("Drama", "A*"));

            string output =
                "School: Slough Grammar" +
                "\n**********************" +
                "\n\n" +
                "Academic Year: 7" +
                "\n----------------" +
                "\n" +
                "\nID: #0039" +
                "\nForename: Darren" +
                "\nSurname: Williams" +
                "\nYear: 7" +
                "\nClass: Violet" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nChemistry: A" +
                "\nPhysics: B" +
                "\nBiology: B" +
                "\n\n\n" +
                "Academic Year: 8" +
                "\n----------------" +
                "\n" +
                "\nID: #1693" +
                "\nForename: Benjamin" +
                "\nSurname: Tennyson" +
                "\nYear: 8" +
                "\nClass: Cyan" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nLaw: C" +
                "\nPsychology: B" +
                "\nEconomics: C" +
                "\n\n\n" +
                "Academic Year: 9" +
                "\n----------------" +
                "\n" +
                "\nID: #3525" +
                "\nForename: Jason" +
                "\nSurname: Banton" +
                "\nYear: 9" +
                "\nClass: Orange" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nTextiles: B" +
                "\nHistory: A" +
                "\nGerman: C" +
                "\n\n\n" +
                "Academic Year: 10" +
                "\n-----------------" +
                "\n" +
                "\nID: #5362" +
                "\nForename: Marcus" +
                "\nSurname: Leatherfield" +
                "\nYear: 10" +
                "\nClass: Turquoise" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nGeography: A" +
                "\nArt: C" +
                "\nFrench: A" +
                "\n\n\n" +
                "Academic Year: 11" +
                "\n-----------------" +
                "\n" +
                "\nID: #6952" +
                "\nForename: Tray" +
                "\nSurname: Jasper" +
                "\nYear: 11" +
                "\nClass: Maroon" +
                "\nSubjects:" +
                "\n+++++++++" +
                "\nMaths: B" +
                "\nSpanish: B" +
                "\nDrama: A*";

            Assert.AreEqual(output, school.ToString());
        }

    }
}