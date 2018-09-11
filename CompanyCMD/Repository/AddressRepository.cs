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
    class AddressRepository
    {
        private static AddressRepository instance = new AddressRepository();

        public static AddressRepository getInstance() { return instance; }
        
        public int Create(Address obj)
        {
            using (SqlConnection con = new SqlConnection(CompanyCMD.Properties.Resources.SqlConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spInsertOrUpdateAddress";
                cmd.Parameters.AddWithValue("@aid", DBNull.Value);
                cmd.Parameters.AddWithValue("@street", obj.Street);
                cmd.Parameters.AddWithValue("@zip", obj.Zip);
                cmd.Parameters.AddWithValue("@place", obj.Place);
                cmd.Parameters.AddWithValue("@countryCode", obj.Country);
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
                cmd.CommandText = String.Format("SELECT Id, Street, Zip, Place, Country FROM dbo.viAddress WHERE Id = {0}", id[0]);
                try
                {
                    con.Open();

                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        a.Fill(dt);
                        Address obj = new Address();
                        obj.Id = Convert.ToInt32(dt.Rows[0][dt.Columns.IndexOf("Id")]);
                        obj.Street = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Street")]);
                        obj.Zip = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Zip")]);
                        obj.Place = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Place")]);
                        obj.Country = Convert.ToString(dt.Rows[0][dt.Columns.IndexOf("Country")]);
                        return obj;
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
                cmd.CommandText = "SELECT Id, Street, Zip, Place, Country FROM dbo.viAddress";
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
                            obj.Street = Convert.ToString(row[dt.Columns.IndexOf("Street")]);
                            obj.Zip = Convert.ToString(row[dt.Columns.IndexOf("Zip")]);
                            obj.Place = Convert.ToString(row[dt.Columns.IndexOf("Place")]);
                            obj.Country = Convert.ToString(row[dt.Columns.IndexOf("Country")]);
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
                cmd.Parameters.AddWithValue("@aid", obj.Id);
                cmd.Parameters.AddWithValue("@street", obj.Street);
                cmd.Parameters.AddWithValue("@zip", obj.Zip);
                cmd.Parameters.AddWithValue("@place", obj.Place);
                cmd.Parameters.AddWithValue("@countryCode", obj.Country);
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
    }
}
