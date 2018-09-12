using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCMD.Models;
using Dapper;

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

                    var param = new DynamicParameters();
                    param.Add("@id", id[0]);
                    return con.QueryFirstOrDefault<Employee>("SELECT Id, PersonId, LastName, FirstName, Birthday, Phone, Gender, EmployeeSince FROM dbo.viEmployee WHERE Id = @id", param);

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);
                        if (dt.Rows.Count == 0) return null;

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
                    
                    return con.Query<Employee>(cmd.CommandText).ToList();

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
                var param = new DynamicParameters();
                Console.WriteLine(obj.GetType().Name);
                foreach(System.Reflection.PropertyInfo o in obj.GetType().GetProperties())
                {
                    param.Add("@"+o.Name, o.GetValue(obj));
                }
                param.Add("@returnValue", null, DbType.Int32, ParameterDirection.ReturnValue);

                con.Execute("spInsertOrUpdateEmployee", param, commandType: CommandType.StoredProcedure);
                Console.WriteLine(param.Get<Int32>("@returnValue"));
                return;// param.Get<Int32>("@returnValue");

                //param.Add("@eid", obj.Id);
                //param.Add("@lastName", obj.LastName);
                //param.Add("@firstName", obj.FirstName);
                //param.Add("@birthday", obj.Birthday);
                //param.Add("@phone", obj.Phone);
                //param.Add("@gender", obj.Gender);
                //param.Add("@employeeSince", obj.EmployeeSince);

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateEmployee";
                cmd.Parameters.AddWithValue("@eid", obj.Id);
                cmd.Parameters.AddWithValue("@lastName", obj.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@firstName", obj.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@birthday", obj.Birthday ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@phone", obj.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@gender", obj.Gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@employeeSince", obj.EmployeeSince ?? (object)DBNull.Value);
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
