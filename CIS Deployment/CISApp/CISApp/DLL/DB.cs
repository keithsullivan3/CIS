using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace CISApp.DLL
{
    class DB
    {
        SQLiteConnection conn = new SQLiteConnection("data source=../../CISdb");
                     
        #region Connection

        //Constructor
        public DB()
        {
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string query)
        {            
            if (this.OpenConnection() == true)
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;                 
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }                    
        }

        //Update statement
        public void Update(string query)
        {
            if (this.OpenConnection() == true)
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;                  
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }         
        }

        //Delete statement
        public void Delete(string query)
        {
            if (this.OpenConnection() == true)
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;                  
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public DataTable Select(string query)
        {
            this.OpenConnection();
            DataSet ds = new DataSet();
            SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
            da.Fill(ds);
            this.CloseConnection();
            return ds.Tables[0];
        }

        #endregion

        #region Couples and Children

        public static DataTable selectMarriedMales(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM Student WHERE StudentGroup = '" + group + "' AND MaritalStatusID = 3 AND Sex = 'Male' ORDER BY gpa DESC");
            return dt;
        }

        public static DataTable selectMarriedFemales(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM Student WHERE StudentGroup = '" + group + "' AND MaritalStatusID = 3 AND Sex = 'Female' ORDER BY gpa DESC");
            return dt;
        }

        public static void updateStudentMarriedTo(string name, int id)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET marriedto = '" + name + "' WHERE studentId = " + id + "");
        }

        public static DataTable selectMaleDesireChildren(int id)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT ChildrenNumber FROM Student WHERE StudentId = " + id + "");
            return dt;
        }

        public static void updateChildrenNumber(int id, int children)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET childrenNumber = " + children + ", maritalStatusId = 2 WHERE StudentId = " + id + "");
        }

        public static void updateChildrenNumberto0(string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET childrenNumber = 0 WHERE maritalStatusId != 2 AND maritalStatusId != 4 AND studentGroup = '" + group + "'");
        }

        #endregion

        #region Credit

        public static DataTable selectCreditUse()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM credit");
            return dt;
        }

        #endregion

        #region Divorce and Children

        public static DataTable selectNumberOfSingleStudents(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS single FROM Student WHERE StudentGroup = '" + group + "' AND MaritalStatusId != 3");
            return dt;
        }

        public static DataTable selectNumberOfStudentsNo(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS [No] FROM Student WHERE StudentGroup = '" + group + "' AND Married = 'No'");
            return dt;
        }

        public static void updateStudentMaritalStatusDivorced(int number, string group)
        {
            DB d1 = new DB();
            d1.Update("Update Student SET maritalstatusId = 5 WHERE StudentID IN " +
                       "(SELECT studentId FROM student WHERE studentGroup = '" + group + "' AND Married != 'No' AND maritalStatusId != 3 LIMIT " + number + ")");
        }

        public static void updateStudentMaritalStatusDivorced(string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE Student SET MaritalStatusId = 5 WHERE StudentGroup = '" + group + "' AND Married != 'No' AND MaritalStatusId != 3");
        }

        public static DataTable selectNumberofDivorcedStudentsWithChildren(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(maritalstatusId)*.6 AS number FROM student WHERE maritalstatusId = 5 AND studentGroup = '" + group + "'");
            return dt;
        }

        public static DataTable selectDivorcedStudentsWithChildren(string group, int number)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM Student WHERE StudentGroup = '" + group + "' AND MaritalStatusId = 5 LIMIT " + number + "");
            return dt;
        }

        public static void updateMaritalStatusDivorcedWithChildren(int studentId, int children, int childsupport)
        {
            DB d1 = new DB();
            d1.Update("UPDATE Student SET MaritalStatusId = 4, ChildrenNumber = " + children + ", ChildSupport = " + childsupport + " WHERE StudentId = " + studentId + "");
        }

        public static DataTable selectNumberofDivorcedWithChildrenMales(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS number FROM student WHERE maritalstatusid = 4 AND sex = 'Male' AND studentgroup = '" + group + "'");
            return dt;
        }

        public static DataTable selectNumberofDivorcedWithChildrenFemales(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS number FROM student WHERE maritalstatusid = 4 AND sex = 'Female' AND studentgroup = '" + group + "'");
            return dt;
        }

        public static void updateMaleCustody(string group, int number)
        {
            DB d1 = new DB();
            d1.Update("Update Student SET custody = 'Yes' WHERE studentId IN " +
                        "(SELECT studentId FROM student WHERE studentGroup = '" + group + "' AND sex = 'Male' AND maritalStatusId = 4 LIMIT " + number + ")");
        }

        public static void updateFemaleCustody(string group, int number)
        {
            DB d1 = new DB();
            d1.Update("Update Student SET custody = 'Yes' WHERE studentId IN " +
                        "(SELECT studentId FROM student WHERE studentGroup = '" + group + "' AND sex = 'Female' AND maritalStatusId = 4 LIMIT " + number + ")");
        }

        public static void updateCustodyNo(string group)
        {
            DB d1 = new DB();
            d1.Update("Update Student SET custody = 'No' Where studentgroup = '" + group + "' AND maritalStatusId = 4 AND custody IS null");
        }

        #endregion

        #region Finances

        public static DataTable selectSalary(int occupationId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT annualGrossSalary FROM occupation WHERE Id = " + occupationId + "");
            return dt;
        }

        public static DataTable selectMarriedAfterTaxes(int occupationId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT MarriedAfterTaxes FROM occupation WHERE Id = " + occupationId + "");
            return dt;
        }

        public static DataTable selectSingleAfterTaxes(int occupationId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT SingleAfterTaxes FROM occupation WHERE Id = " + occupationId + "");
            return dt;
        }

        public static DataTable selectStudentLoans(int occupationId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT studentLoans FROM occupation WHERE Id = " + occupationId + "");
            return dt;
        }

        public static void setChildSupportToZero(string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET childsupport = 0 WHERE studentgroup = '" + group + "'");
        }

        public static void setCustodyCheckbook(string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET checkbook = netmonthlyincome + childsupport WHERE Custody = 'Yes' OR Custody IS null AND studentgroup = '" + group + "'");
        }

        public static void setNonCustodyCheckbook(string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET checkbook = netmonthlyincome - childsupport WHERE studentgroup = '" + group + "' AND Custody = 'No'");
        }

        #endregion

        #region Marriage

        public static DataTable selectNumberOfStudents(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS studentNo FROM Student WHERE StudentGroup = '" + group + "'");
            return dt;
        }

        public static DataTable selectNumberOfMalesYes(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS maleYes FROM Student WHERE StudentGroup = '" + group + "' AND Sex = 'Male' AND married = 'Yes'");
            return dt;
        }

        public static DataTable selectMaricalStatusIds()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM MaritalStatus");
            return dt;
        }

        public static DataTable selectNumberOfFemalesYes(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT COUNT(*) AS femalesYes FROM Student WHERE StudentGroup = '" + group + "' AND Sex = 'Female' AND married = 'Yes'");
            return dt;
        }

        public static void updateStudentMaritalStatusMaleMarried(int number, string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE Student SET MaritalStatusId = 3 WHERE StudentID IN " +
                        "(SELECT studentId FROM student WHERE StudentGroup = '" + group + "' AND Married = 'Yes' AND Sex = 'Male' LIMIT " + number + ")");
        }

        public static void updateStudentMaritalStatusFemaleMarried(int number, string group)
        {
            DB d1 = new DB();
            d1.Update("UPDATE Student SET MaritalStatusId = 3 WHERE StudentID IN " +
                        "(SELECT studentId FROM student WHERE StudentGroup = '" + group + "' AND Married = 'Yes' AND Sex = 'Female' LIMIT " + number + ")");
        }

        #endregion       

        #region Occupation

        public static DataTable selectOccupations()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM occupation");
            return dt;
        }

        public static DataTable selectOccupation(int id)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM occupation WHERE Id = " + id + "");
            return dt;
        }

        public static DataTable selectOccupationsByCluster(int Id)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM occupation WHERE clusterid = " + Id + " ORDER BY annualgrosssalary DESC");
            return dt;
        }

        public static DataTable selectOccupationClusterId(int Id)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT ClusterId FROM occupation WHERE Id = " + Id + "");
            return dt;
        }

        public static DataTable selectClusters()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM cluster");
            return dt;
        }

        public static DataTable selectEducationLevel(int occupationId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT RequiredEducationLevelId FROM Occupation WHERE Id = " + occupationId + "");
            return dt;
        }

        public static DataTable selectEducationLevels()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM Education");
            return dt;
        }

        public static DataTable selectNextOccupationId()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT ID FROM occupation ORDER BY ID desc LIMIT 1");
            return dt;
        }
        
        public static void createOccupation(int Id, string occupation, int clusterId, int annualGross, int monthlyGross, int marriedAnnualTaxes,
            int marriedMonthlyTaxes, int marriedAfterTaxes, int singleAnnualTaxes, int singleMonthlyTaxes, int singleAfterTaxes, int studentLoans,
            double requiredGpa, int requiredEducation)
        {
            DB d1 = new DB();
            d1.Insert("INSERT INTO occupation VALUES (" + Id + ", '" + occupation + "' , " + clusterId + " , " + annualGross + ", " + monthlyGross + ", " + marriedAnnualTaxes + ", " +
                marriedMonthlyTaxes + ", " + marriedAfterTaxes + ", " + singleAnnualTaxes + ", " + singleMonthlyTaxes + ", " + singleAfterTaxes + " , " + studentLoans + ", " + 
                requiredGpa + ", " + requiredEducation + ")");
        }

        public static void updateOccupation(int occupationId, string occupation, int clusterId, int annualGross, int monthlyGross, int marriedAnnualTaxes, int marriedMonthlyTaxes, 
            int marriedAfterTaxes, int singleAnnualTaxes, int singleMonthlyTaxes, int singleAfterTaxes, int studentLoans, double requiredGpa, int requiredEducation)
        {
            DB d1 = new DB();
            d1.Update("UPDATE occupation SET occupation = '" + occupation + "', clusterId = " + clusterId + ", annualGrossSalary = " + annualGross + ", monthlyGrossSalary = " + monthlyGross +
                ", marriedAnnualTaxes = " + marriedAnnualTaxes + ", marriedMonthlyTaxes = " + marriedMonthlyTaxes + ", marriedAfterTaxes = " + marriedAfterTaxes +
                ", singleAnnualTaxes = " + singleAnnualTaxes + ", singleMonthlyTaxes = " + singleMonthlyTaxes + ", singleAfterTaxes = " + singleAfterTaxes + ", studentLoans = " + studentLoans +
                ", requiredGpa = " + requiredGpa + ", requiredEducationLevelId = " + requiredEducation + 
                " WHERE Id = " + occupationId + "");
        }

        public static void deleteOccupation(int occupationId)
        {
            DB d1 = new DB();
            d1.Delete("DELETE FROM occupation WHERE Id = " + occupationId + "");
        }

        #endregion

        #region School

        public static DataTable selectSchools()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM schools ORDER BY schoolName");
            return dt;
        }

        public static void createSchool(int schoolId, string school)
        {
            DB d1 = new DB();
            d1.Insert("INSERT INTO schools VALUES("+ schoolId + ", '" + school + "')");
        }

        public static DataTable selectNextSchoolId()
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT SchoolId FROM Schools ORDER BY SchoolId desc LIMIT 1");
            return dt;
        }

        public static DataTable selectSchool(int schoolId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT schoolName FROM schools WHERE schoolId = " + schoolId + "");
            return dt;
        }

        public static void updateSchool(int schoolId, string schoolName)
        {
            DB d1 = new DB();
            d1.Update("UPDATE schools SET schoolName = '" + schoolName + "' WHERE schoolId = " + schoolId + "");
        }

        public static void deleteSchool(int schoolId)
        {
            DB d1 = new DB();
            d1.Delete("DELETE FROM schools WHERE schoolId = " + schoolId + "");
        }

        #endregion

        #region Student

        public static DataTable selectStudents(string group)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT s.studentId, s.FirstName, s.LastName, s.MarriedTo, " +
                "s.Sex, s.GPA, o.Occupation, s.Salary, s.NetMonthlyIncome, m.MaritalStatus, " +
                "s.ChildrenNumber, s.childSupport, s.CreditScore " +
                "FROM student s, schools sch, occupation o, education e, credit c, MaritalStatus m " +
                "WHERE c.creditId = s.creditUseId AND e.EducationId = s.EducationId AND m.Id = s.MaritalStatusId " +
                "AND o.Id = s.occupationId AND sch.SchoolId = s.schoolId AND s.studentGroup = '" + group + "'");
            return dt;
        }

        public static DataTable selectStudent(int studentId)
        {
            DB d1 = new DB();
            DataTable dt = d1.Select("SELECT * FROM student WHERE studentId = " + studentId + "");
            return dt;
        }

        public static void createStudent(string firstName, string lastName, string classPeriod, string teacher, string group, string sex, double gpa, int educationId,
            int occupationId, string married, string children, int childrenNumber, string creditCard, int creditUse, int creditScore, int schoolId, 
            int salary, int netMonthlyIncome, int studentLoans, int maritalStatusId)
        {
            DB d1 = new DB();
            d1.Insert("INSERT INTO student (FirstName, LastName, ClassPeriod, Teacher, StudentGroup, Sex, GPA, EducationId, OccupationId, Married, Children, ChildrenNumber, " +
                "CreditCard, CreditUseId, CreditScore, SchoolId, Salary, NetMonthlyIncome, StudentLoans, MaritalStatusId) " + 
                "VALUES ('" + firstName + "' , '" + lastName + "' , '" + classPeriod + "' , '" + teacher + "' , '" + group + "' , '" + sex +
                "' , " + gpa + " , " + educationId + " , " + occupationId + " , '" + married + "' , '" + children + "' , " + childrenNumber +
                " , '" + creditCard + "' , " + creditUse + " , " + creditScore + " , " + schoolId + " , " + salary + " , " + netMonthlyIncome + 
                " , " + studentLoans + " , " + maritalStatusId + ")");
        }

        public static void updateStudent(int studentId, string studentGroup, string firstName, string lastName, int schoolId, string teacher, string classPeriod, string sex,
            double gpa, int occupationId, int maritalStatusId, string marriedTo, int childrenNumber, int childSupport, int netMonthlyIncome, int studentLoans, int creditScore, int checkbook)
        {
            DB d1 = new DB();
            d1.Update("UPDATE student SET studentgroup = '" + studentGroup + "', firstname = '" + firstName + "', lastname = '" + lastName + "', schoolId = " + schoolId +
                ", teacher = '" + teacher + "', classperiod = '" + classPeriod + "', sex = '" + sex + "', gpa = " + gpa + ", occupationId = " + occupationId +
                ", maritalstatusId = " + maritalStatusId + ", marriedTo = '" + marriedTo + "', childrenNumber = " + childrenNumber + ", childsupport = " + childSupport +
                ", netMonthlyIncome = " + netMonthlyIncome + ", studentLoans = " + studentLoans + ", creditScore = " + creditScore + ", checkbook = " + checkbook + " WHERE studentId = " + studentId + "");
        }

        public static void deleteStudents(string group)
        {
            DB d1 = new DB();
            d1.Delete("DELETE FROM student WHERE studentgroup = '" + group + "'");
        }

        public static void deleteStudent(int studentId)
        {
            DB d1 = new DB();
            d1.Delete("DELETE FROM student WHERE studentId = " + studentId + "");
        }

        #endregion
    }
}