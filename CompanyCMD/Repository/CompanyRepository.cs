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
    class CompanyRepository : IRepository<Company>
    {
        private static CompanyRepository instance = new CompanyRepository();

        public static CompanyRepository getInstance() { return instance; }
        
        public int Create(Company obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateCompany";
                cmd.Parameters.AddWithValue("@cid", DBNull.Value);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@description", obj.Description);
                cmd.Parameters.AddWithValue("@foundedAt", obj.FoundedAt);
                cmd.Parameters.AddWithValue("@branch", obj.Branch);
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
                cmd.CommandText = String.Format("UPDATE Person SET DeletedTime=getDate() WHERE CompanyId = {0}", id[0]);
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

        public Company Retrieve(params int[] id)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("SELECT Id, PersonId, Name, Description, FoundedAt, Branch FROM dbo.viCompany WHERE Id = {0}", id[0]);
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);
                        if (dt.Rows.Count == 0) return null;
                        Company c = new Company();
                        c.Id = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("Id")]);
                        c.PersonId = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("PersonId")]);
                        c.Name = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Name")]);
                        c.Description = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Description")]);
                        c.FoundedAt = Convert.ToDateTime(dt.Rows[0][dt.Columns.IndexOf("FoundedAt")]);
                        c.Branch = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Branch")]);
                        return c;
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

        public IEnumerable<Company> RetrieveAll()
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, PersonId, Name, Description, FoundedAt, Branch FROM dbo.viCompany";
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);

                        List<Company> list = new List<Company>();

                        foreach(DataRow row in dt.Rows)
                        {
                            Company c = new Company();
                            c.Id = Convert.ToInt32(row[dt.Columns.IndexOf("Id")]);
                            c.PersonId = Convert.ToInt32(row[dt.Columns.IndexOf("PersonId")]);
                            c.Name = Convert.ToString(row[dt.Columns.IndexOf("Name")]);
                            c.Description = Convert.ToString(row[dt.Columns.IndexOf("Description")]);
                            c.FoundedAt = Convert.ToDateTime(row[dt.Columns.IndexOf("FoundedAt")]);
                            c.Branch = Convert.ToString(row[dt.Columns.IndexOf("Branch")]);
                            list.Add(c);
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

        public void Update(Company obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateCompany";
                cmd.Parameters.AddWithValue("@cid", obj.Id);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@description", obj.Description);
                cmd.Parameters.AddWithValue("@foundedAt", obj.FoundedAt);
                cmd.Parameters.AddWithValue("@branch", obj.Branch);
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

        public void UpdateAll(IEnumerable<Company> list)
        {
            foreach(Company c in list)
            {
                Update(c);
            }
        }
    }
}
