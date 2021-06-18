using Dapper;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            string connstr = @"Provider=sqloledb;Data Source=COOLKERRY\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=TestDB;";
            using (OleDbConnection oleDbConnection = new OleDbConnection(connstr))
            {
                string querystr = @"SELECT EmpID, EmpName FROM Employees";
                List<Employees> employees = oleDbConnection.Query<Employees>(querystr).ToList();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(connstr))
            {
                string querystr = "SELECT * FROM Employees WHERE EmpName like ?EmpName? AND Age = ?Age? AND Sex like ?Sex?";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("EmpName", "%s%");
                parameters.Add("Age", "18");
                parameters.Add("Sex", "%boy%");
                object parameters2 = new
                {
                    EmpName = "%s%",
                    Age = "18",
                    Sex = "%g%"
                };

                List<Employees> employees = oleDbConnection.Query<Employees>(querystr, parameters2).ToList();
                foreach (Employees e in employees)
                {
                    Console.WriteLine($"{e.EmpID},{e.EmpName},{e.Age},{e.Sex},{e.Job}");
                }
                Console.Read();
            }
        }
        public class Employees
        {
            public string EmpID { get; set; }
            public string EmpName { get; set; }
            public string Age { get; set; }
            public string Sex { get; set; }
            public string Job { get; set; }
        }
    }
}
