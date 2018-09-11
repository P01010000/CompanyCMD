using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCMD.Models;

namespace CompanyCMD.Repository
{
    class EmployeeRepository : IRepository<Employee>
    {
        private static EmployeeRepository instance = new EmployeeRepository();

        public static EmployeeRepository getInstance() { return instance; }
        
        public int Create(Employee obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateEmployee";
                cmd.Parameters.AddWithValue("@eid", DBNull.Value);
                cmd.Parameters.AddWithValue("@lastName", obj.LastName);
                cmd.Parameters.AddWithValue("@firstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@birthday", obj.Birthday);
                cmd.Parameters.AddWithValue("@phone", obj.Phone);
                cmd.Parameters.AddWithValue("@gender", obj.Gender.Equals("male", StringComparison.InvariantCultureIgnoreCase) ? 1 : 2);
                cmd.Parameters.AddWithValue("@employeeSince", obj.EmployeeSince);
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                try
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    return (int)returnParameter.Value;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (SqlException) {}
                }
            }
        }

        public void Delete(params int[] id)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("UPDATE Department SET DeletedTime=getDate() WHERE DepartmentId = {0}", id[0]);
                try
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public Employee Retrieve(params int[] id)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("SELECT Id, PersonId, LastName, FirstName, Birthday, Phone, Gender, EmployeeSince FROM dbo.viEmployee WHERE Id = {0}", id[0]);
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);
                        Employee e = new Employee();
                        e.Id = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("Id")]);
                        e.PersonId = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("PersonId")]);
                        e.LastName = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("LastName")]);
                        e.FirstName = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("FirstName")]);
                        e.Birthday = Convert.ToDateTime(dt.Rows[0][dt.Columns.IndexOf("Birthday")]);
                        e.Phone = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Phone")]);
                        e.Gender = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Gender")]);
                        e.EmployeeSince = Convert.ToDateTime(dt.Rows[0][dt.Columns.IndexOf("EmployeeSince")]);
                        return e;
                    }
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public IEnumerable<Employee> RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, PersonId, LastName, FirstName, Birthday, Phone, Gender, EmployeeSince FROM dbo.viEmployee";
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);

                        List<Employee> list = new List<Employee>();

                        foreach(DataRow row in dt.Rows)
                        {
                            Employee e = new Employee();
                            e.Id = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("Id")]);
                            e.PersonId = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("PersonId")]);
                            e.LastName = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("LastName")]);
                            e.FirstName = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("FirstName")]);
                            e.Birthday = Convert.ToDateTime(dt.Rows[0][dt.Columns.IndexOf("Birthday")]);
                            e.Phone = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Phone")]);
                            e.Gender = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Gender")]);
                            e.EmployeeSince = Convert.ToDateTime(dt.Rows[0][dt.Columns.IndexOf("EmployeeSince")]);
                            list.Add(e);
                        }
                        return list;
                    }
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void Update(Employee obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateEmployee";
                cmd.Parameters.AddWithValue("@eid", DBNull.Value);
                cmd.Parameters.AddWithValue("@lastName", obj.LastName);
                cmd.Parameters.AddWithValue("@firstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@birthday", obj.Birthday);
                cmd.Parameters.AddWithValue("@phone", obj.Phone);
                cmd.Parameters.AddWithValue("@gender", obj.Gender);
                try
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void UpdateAll(IEnumerable<Employee> list)
        {
            foreach(Employee e in list)
            {
                Update(e);
            }
        }
    }
}
