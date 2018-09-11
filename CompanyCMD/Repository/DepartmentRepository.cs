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
    class DepartmentRepository : IRepository<Department>
    {
        private static DepartmentRepository instance = new DepartmentRepository();

        public static DepartmentRepository getInstance() { return instance; }
        
        public int Create(Department obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateDepartment";
                cmd.Parameters.AddWithValue("@did", DBNull.Value);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@description", obj.Description);
                cmd.Parameters.AddWithValue("@supervisor", obj.Supervisor);
                cmd.Parameters.AddWithValue("@superDepartment", obj.SuperDepartment);
                cmd.Parameters.AddWithValue("@companyId", obj.CompanyId);
                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                try
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    return (int)returnParameter.Value;
                }
                catch (SqlException)
                {
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

        public Department Retrieve(params int[] id)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("SELECT Id, Name, Description, Supervisor, SuperDepartment, CompanyId FROM dbo.viDepartment WHERE Id = {0}", id[0]);
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);
                        Department d = new Department();
                        d.Id = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("Id")]);
                        d.Name = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Name")]);
                        d.Description = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Description")]);
                        d.Supervisor = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("Supervisor")]);
                        d.SuperDepartment = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("SuperDepartment")]);
                        d.CompanyId = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("CompanyId")]);
                        return d;
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

        public IEnumerable<Department> RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Name, Description, Supervisor, SuperDepartment, CompanyId FROM dbo.viDepartment";
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);

                        List<Department> list = new List<Department>();

                        foreach(DataRow row in dt.Rows)
                        {
                            Department d = new Department();
                            d.Id = Convert.ToInt32(row[dt.Columns.IndexOf("Id")]);
                            d.Name = Convert.ToString(row[dt.Columns.IndexOf("Name")]);
                            d.Description = Convert.ToString(row[dt.Columns.IndexOf("Description")]);
                            d.Supervisor = row[dt.Columns.IndexOf("Supervisor")].Equals(DBNull.Value) ? (int?)null : Convert.ToInt32(row[dt.Columns.IndexOf("Supervisor")]);
                            d.SuperDepartment = row[dt.Columns.IndexOf("SuperDepartment")].Equals(DBNull.Value) ? (int?)null : Convert.ToInt32(row[dt.Columns.IndexOf("SuperDepartment")]);
                            d.CompanyId = Convert.ToInt32(row[dt.Columns.IndexOf("CompanyId")]);
                            list.Add(d);
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

        public void Update(Department obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateDepartment";
                cmd.Parameters.AddWithValue("@did", obj.Id);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@description", obj.Description);
                cmd.Parameters.AddWithValue("@supervisor", obj.Supervisor);
                cmd.Parameters.AddWithValue("@superDepartment", obj.SuperDepartment);
                cmd.Parameters.AddWithValue("@companyId", obj.CompanyId);
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

        public void UpdateAll(IEnumerable<Department> list)
        {
            foreach(Department d in list)
            {
                Update(d);
            }
        }
    }
}
