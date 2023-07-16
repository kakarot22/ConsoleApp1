using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Drawing;


class Student
{
    private string studentName;
    private int studentRollNo;
    private int studentClass;
    private DateTime studentDOB;
    private string studentEmail;
    private string parentNo;
    public bool flag;



    public Student() { }

    public Student(string studentName, int studentRollNo, int studentClass, DateTime studentDOB, string studentEmail, string parentNo)
    {
        this.StudentName = studentName;
        this.StudentRollNo = studentRollNo;
        this.StudentClass = studentClass;
        StudentDOB = studentDOB;
        StudentEmail = studentEmail;
        ParentNo = parentNo;
        this.flag = false;
    }

    public Student(string studentName, int studentRollNo, int studentClass, DateTime studentDOB, string studentEmail, string parentNo, bool flag)
    {
        this.StudentName = studentName;
        this.StudentRollNo = studentRollNo;
        this.StudentClass = studentClass;
        StudentDOB = studentDOB;
        StudentEmail = studentEmail;
        ParentNo = parentNo;
        this.flag = flag;
    }

    public string StudentName
    {
        get;
        set;
    }

    public int StudentRollNo
    {
        get;
        set;
    }

    public int StudentClass
    {
        get;
        set;
    }

    public DateTime StudentDOB
    {
        get;
        set;
    }

    public string StudentEmail
    {
        get;
        set;
    }

    public string ParentNo
    {
        get;
        set;
    }
}

class UsernamePassword
{
    private string us;
    private string ps;

    public UsernamePassword()
    {
        this.us = "a";
        this.ps = "1";
    }

    public string Us
    {
        get
        {
            return this.us;
        }
    }

    public string Ps
    {

        get
        {
            return this.ps;
        }
    }
}

class StudentManagementSystem
{
    List<Student> studentsList = new List<Student>();

    public void AddStudent()
    {
        Student studentObject = new Student();

        //public SqlConnection con;

        System.Console.WriteLine("1. Add student info");
        System.Console.WriteLine();

        System.Console.Write("Enter student name: ");
        string name = System.Console.ReadLine();

        if (IsValidName(name))
        {
            studentObject.StudentName = name;
        }
        else
        {

            System.Console.WriteLine("Invalid input");
            return;
        }

        System.Console.Write("Enter student roll no: ");
        int rollNo;
        if (int.TryParse(Console.ReadLine(), out rollNo))
        {
            studentObject.StudentRollNo = rollNo;
        }
        else
        {
            System.Console.WriteLine("Invalid input");
            return;
        }

        System.Console.Write("Enter student class: ");

        int _class;
        if (int.TryParse(Console.ReadLine(), out _class))
        {
            studentObject.StudentClass = _class;
        }
        else
        {
            System.Console.WriteLine("Invalid input");
            return;
        }

        System.Console.Write("Enter student date of birth: ");
        DateTime d;
        if (DateTime.TryParse(Console.ReadLine(), out d))
        {
            studentObject.StudentDOB = d;
        }
        else
        {
            System.Console.WriteLine("Invalid input");
            return;
        }

        System.Console.Write("Enter student email: ");
        string email = System.Console.ReadLine();

        if (IsValidMail(email))
        {
            studentObject.StudentEmail = email;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Email Id. Student not added.");
            return;
        }

        System.Console.Write("Enter parent's phone no: ");
        string phone = System.Console.ReadLine();


        if (IsValidMobileNumber(phone))
        {
            studentObject.ParentNo = phone;
        }
        else
        {
            System.Console.WriteLine("Invalid input");
            return;
        }


        studentObject.flag = false;
        studentsList.Add(studentObject);

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-O5BN99L\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True");

        SqlCommand cmd;

        con.Open();

        string sql = $"insert into Student values('{studentObject.StudentName}',{studentObject.StudentRollNo},{studentObject.StudentClass},'{studentObject.StudentDOB}','{studentObject.StudentEmail}','{studentObject.ParentNo}','{studentObject.flag}')";

        cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void ViewStudent()
    {
        Student studentObject = new Student();

        System.Console.WriteLine("2. View student info");
        System.Console.WriteLine();

        System.Console.Write("Enter student roll no: ");
        int rollNo = int.Parse(System.Console.ReadLine());

        //string connectionString;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-O5BN99L\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True");

        con.Open();
        SqlCommand cmd = new SqlCommand("Select * from Student where studentRollNo = "+ rollNo.ToString()+";", con);
        SqlDataReader dr = cmd.ExecuteReader();

        Student find = null;
        //Student find = studentsList.Find(s => s.StudentRollNo == rollNo);

        while (dr.Read())
        {
            find = new Student(dr[0].ToString(), dr.GetInt32(1), dr.GetInt32(2), Convert.ToDateTime(dr[3].ToString()), dr[4].ToString(), dr[5].ToString());

        }
        dr.Close();
        con.Close();


        if (find == null)
        {
            System.Console.WriteLine("No result found");
            return;
        }
        else
        {
            string s1 = String.Format("{0} \t {1} \t {2} \t\t\t {3} \t {4} \t\t\t\t {5}", "Student Name", "Roll No", "Class", "Date of birth", "Email", "Phone no");
            System.Console.WriteLine(s1);
            show(find);
        }
    }

    public void ViewAllStudents()
    {

        System.Console.WriteLine("3. View all student info");
        System.Console.WriteLine();

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-O5BN99L\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True");

        con.Open();
        SqlCommand cmd = new SqlCommand("Select * from Student;", con);
        SqlDataReader dr = cmd.ExecuteReader();

        List<Student> sl = new List<Student>();

        while (dr.Read())
        {
            //int i;
            //if (dr[6] )

            sl.Add(new Student(dr[0].ToString(), dr.GetInt32(1), dr.GetInt32(2), Convert.ToDateTime(dr[3].ToString()), dr[4].ToString(), dr[5].ToString(), dr.GetBoolean(dr.GetOrdinal("flag"))));

        } 
        dr.Close();
        con.Close();

        if (sl.Count == 0)
        {
            System.Console.WriteLine("No result found");
            return;
        }
        else
        {
            string s1 = String.Format("{0} \t {1} \t {2} \t\t\t {3} \t {4} \t\t\t\t {5}", "Student Name", "Roll No", "Class", "Date of birth", "Email", "Phone no");
            System.Console.WriteLine(s1);

            foreach (Student i in sl)
            {
                if (i.flag != true)
                {
                    show(i);
                }
            }
        }
    }

    public void DeleteStudent()
    {
        Student studentObject = new Student();

        System.Console.WriteLine("4. Delete student info");
        System.Console.WriteLine();

        System.Console.Write("Enter student roll no: ");
        int rollNo = int.Parse(System.Console.ReadLine());

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-O5BN99L\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True");

        con.Open();

        SqlCommand cmd;

        string sql = $"update Student set flag = 1 where studentRollNo = {rollNo}";
        cmd = new SqlCommand(sql, con);
        int n = cmd.ExecuteNonQuery();
        con.Close();


        if (n == 0)
        {
            System.Console.WriteLine("No result found");
            return;
        }
        else
        {
            //find.flag = false;
            System.Console.WriteLine("Deleted");
        }

    }
    public bool IsValidName(string name)
    {
        string strRegex = @"^[a-zA-Z\s'-]+$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(name))
            return (true);
        else
            return (false);
    }
    public bool IsValidMobileNumber(string Phone)
    {
        string strRegex = @"(^[0-9]{10}$)";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Phone))
            return (true);
        else
            return (false);
    }
    public bool IsValidMail(string Mail)
    {
        try
        {
            MailAddress m = new MailAddress(Mail);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public void show(Student studentObject)
    {
        Object o = String.Format("{0} \t {1} \t\t {2} \t \t\t {3} \t {4} \t\t {5}", studentObject.StudentName, studentObject.StudentRollNo, studentObject.StudentClass, studentObject.StudentDOB.ToShortDateString(), studentObject.StudentEmail, studentObject.ParentNo);
        System.Console.WriteLine(o);

        System.Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {


        UsernamePassword up = new UsernamePassword();

        StudentManagementSystem sm = new StudentManagementSystem();

        System.Console.WriteLine(" Student Management System Application");

        System.Console.WriteLine();

        System.Console.WriteLine("Login page");

        System.Console.WriteLine();

        System.Console.WriteLine("Enter Username: "); //admin
        string us = System.Console.ReadLine();

        System.Console.WriteLine();

        System.Console.WriteLine("Enter password: "); //abc123
        string ps = System.Console.ReadLine();

        System.Console.WriteLine();

        if (us.Equals(up.Us) && ps.Equals(up.Ps))
        {
            bool flag = true;

            while (flag)
            {

                System.Console.WriteLine(" 1. Add student info");
                System.Console.WriteLine(" 2. View student info");
                System.Console.WriteLine(" 3. View all student info");
                System.Console.WriteLine(" 4. Delete student info");
                System.Console.WriteLine(" 5. Exit");
                System.Console.WriteLine();

                System.Console.Write("Enter your choice: ");

                int choice = int.Parse(System.Console.ReadLine());
                System.Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        sm.AddStudent();
                        break;
                    case 2:
                        sm.ViewStudent();
                        break;
                    case 3:
                        sm.ViewAllStudents();
                        break;
                    case 4:
                        sm.DeleteStudent();
                        break;
                    case 5:
                        flag = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }

                System.Console.WriteLine();
            }

        }
        else
        {
            System.Console.WriteLine("Invalid login");
        }

        System.Console.ReadKey();
    }
}
