using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCMD.Shared;

namespace CompanyCMD.Repository
{
    class AddressRepository
    {
        /*private static AddressRepository instance = new AddressRepository();

        public static AddressRepository getInstance() { return instance; }
        
        public int Create(Address obj)
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

        public Address Retrieve(params int[] id)
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

        public IEnumerable<Address> RetrieveAll()
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

                        List<Address> list = new List<Address>();

                        foreach(DataRow row in dt.Rows)
                        {
                            Address obj = new Address();
                            obj.Id = Convert.ToInt32(row[dt.Columns.IndexOf("Id")]);
                            obj.Name = Convert.ToString(row[dt.Columns.IndexOf("Name")]);
                            obj.Description = Convert.ToString(row[dt.Columns.IndexOf("Description")]);
                            obj.Supervisor = row[dt.Columns.IndexOf("Supervisor")].Equals(DBNull.Value) ? (int?)null : Convert.ToInt32(row[dt.Columns.IndexOf("Supervisor")]);
                            obj.SuperDepartment = row[dt.Columns.IndexOf("SuperDepartment")].Equals(DBNull.Value) ? (int?)null : Convert.ToInt32(row[dt.Columns.IndexOf("SuperDepartment")]);
                            obj.CompanyId = Convert.ToInt32(row[dt.Columns.IndexOf("CompanyId")]);
                            list.Add(obj);
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

        public void Update(Address obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateAddress";
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

        public void UpdateAll(IEnumerable<Address> list)
        {
            foreach(Address a in list)
            {
                Update(a);
            }
        }
    */
    }
}
