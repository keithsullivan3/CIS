using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISApp.DLL;
using System.Data;

namespace CISApp
{
    class Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int schoolId { get; set; }
        public string classPeriod { get; set; }
        public string teacher { get; set; }
        public string group { get; set; }
        public string sex { get; set; }
        public double gpa { get; set; }
        public int educationId { get; set; }
        public int occupationId { get; set; }
        public string married { get; set; }
        public string children { get; set; }
        public int childrenNumber { get; set; } 
        public string creditCard { get; set; }
        public int creditUseId { get; set; }
        public int creditScore { get; set; }
        public int salary { get; set; }
        public int netMonthlyIncome { get; set; }
        public int studentLoans { get; set; }
        public int maritalStatusId { get; set; }
        public string marriedTo { get; set; }

        #region Constructors and Display

        //empty constructor...just incase we need it.
        public Student()
        {
            firstName = "";
            lastName = "";
            schoolId = 0;
            classPeriod = "";
            teacher = "";
            group = "";
            sex = "";
            gpa = 0.0;
            educationId = 0;
            occupationId = 0;
            married = "";
            children = "";
            childrenNumber = 0;
            creditCard = "";
            creditUseId = 0;
            creditScore = 0;
            salary = 0;
            netMonthlyIncome = 0;
            studentLoans = 0;
            maritalStatusId = 0;
            marriedTo = "";
        }

        //this is the main constructor.  This will do some calculations and insert the new student into the database.
        public Student(string firstName, string lastName, int schoolId, string classPeriod, string teacher, string group, string sex, double gpa, 
            int educationId, int occupationId, string married, string children, int childrenNumber, string creditCard, int creditUseId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.schoolId = schoolId;
            this.classPeriod = classPeriod;
            this.teacher = teacher;
            this.group = group;
            this.sex = sex;
            this.gpa = gpa;
            this.occupationId = occupationId;
            this.married = married;
            this.children = children;
            this.childrenNumber = childrenNumber;
            this.creditCard = creditCard;
            this.creditUseId = creditUseId;

            creditScore = CalculateCreditScore(gpa, creditUseId);
            salary = CalculateSalary(occupationId);
            educationId = CalculateEducationLevel(occupationId);
            netMonthlyIncome = CalculateNetMonthlyIncome(occupationId);
            studentLoans = CalculateStudentLoans(occupationId);
            maritalStatusId = 1;

            DB.createStudent(firstName, lastName, classPeriod, teacher, group, sex, gpa, educationId, occupationId, married, children, childrenNumber, 
                creditCard, creditUseId, creditScore, schoolId, salary, netMonthlyIncome, studentLoans, maritalStatusId);
        }

        //simple display method, used mostly for debugging
        public void display()
        {
            Console.WriteLine("Student Name : {0} - School Id : {1} - Class Period : {2} - Teacher : {3} - " 
                + "Group : {4} - Sex : {5} - GPA : {6} - Education ID : {7} - Occupation ID : {8} - Married : {9} - Children : {10}"
                + "Children Number : {11} - Credit Card : {12} - CreditUseId : {13}", firstName, schoolId, classPeriod, 
                teacher, group, sex, gpa, educationId, occupationId, married, children, childrenNumber, creditCard, creditUseId);
        }
        #endregion

        //this method calculates credit score for the student
        private int CalculateCreditScore(double g, int cuid)
        {
            int cs = 0;

            if (g >= 3.5)
                cs = 700;
            if (g >=3 && g < 3.5)
                cs = 675;
            if (g >= 2.5 && g < 3)
                cs = 650;
            if (g >= 2 && g < 2.5)
                cs = 625;
            if (g >= 1.5 && g < 2)
                cs = 600;
            if (g < 1.5)
                cs = 550;
            if (cuid == 3)
                cs = cs - 50;

            return cs;
        }

        private int CalculateSalary(int occupationId)
        {
            DataTable dt = DB.selectSalary(occupationId);
            salary = Int32.Parse(dt.Rows[0]["annualgrosssalary"].ToString());
            return salary;
        }

        private int CalculateEducationLevel(int occupationId)
        {
            DataTable dt = DB.selectEducationLevel(occupationId);
            educationId = Int32.Parse(dt.Rows[0]["RequiredEducationLevelId"].ToString());
            return educationId;
        }

        private int CalculateNetMonthlyIncome(int occupationId)
        {
            if (married == "Yes")
            {
                DataTable dt1 = DB.selectMarriedAfterTaxes(occupationId);
                netMonthlyIncome = Int32.Parse(dt1.Rows[0]["marriedaftertaxes"].ToString());
            }
            else
            {
                DataTable dt2 = DB.selectSingleAfterTaxes(occupationId);
                netMonthlyIncome = Int32.Parse(dt2.Rows[0]["singleaftertaxes"].ToString());                
            }
            return netMonthlyIncome;
        }

        private int CalculateStudentLoans(int occupationId)
        {
            DataTable dt = DB.selectStudentLoans(occupationId);
            studentLoans = Int32.Parse(dt.Rows[0]["studentLoans"].ToString());
            return studentLoans;
        }

    }
}
