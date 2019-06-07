using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using InformationApp.Controllers;
namespace InformationApp.Models
{
    public class CompanyDataAccesLayer
    {
        private IConfiguration configuration;
        private string conStr;
        public CompanyDataAccesLayer(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.conStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Company> GetAllCompany()
        {
           
            List<Company> lstcompany = new List<Company>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spViewCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Company company = new Company();

                    company.ID = rdr["Id"].ToString();
                    company.Name = rdr["Name"].ToString();
                    company.Address = rdr["Address"].ToString();
                    company.DbConnStr = rdr["DbConnStr"].ToString();
                    company.Suspended = Convert.ToBoolean(rdr["Suspended"]);

                    lstcompany.Add(company);
                }
                con.Close();
            }
            return lstcompany;
        }


        //To Add new employee record    
        public void AddCompany(Company company)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spAddCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Guid id = Guid.NewGuid();
                cmd.Parameters.AddWithValue("@Id", id.ToString());
                cmd.Parameters.AddWithValue("@Name", company.Name);
                cmd.Parameters.AddWithValue("@Address", company.Address);
                cmd.Parameters.AddWithValue("@DbConnStr", company.DbConnStr);
                cmd.Parameters.AddWithValue("@Suspended", company.Suspended);
                cmd.Parameters.AddWithValue("@ExpDate", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public void UpdateCompany(Company company)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spUpdateCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", company.ID);
                cmd.Parameters.AddWithValue("@Name", company.Name);
                cmd.Parameters.AddWithValue("@Address", company.Address);
                cmd.Parameters.AddWithValue("@DbConnStr", company.DbConnStr);
                cmd.Parameters.AddWithValue("@Suspended", company.Suspended);
                cmd.Parameters.AddWithValue("@ExpDate", DateTime.UtcNow);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //Get the details of a particular employee  
        public Company GetCompanyData(string id)
        {
            Company company = new Company();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Company] WHERE Id LIKE '" + id +"'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    company.ID = rdr["Id"].ToString();
                    company.Name = rdr["Name"].ToString();
                    company.Address = rdr["Address"].ToString();
                    company.DbConnStr = rdr["DbConnStr"].ToString();
                    company.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                }
            }
            return company;
        }

        public void DeleteCompany(string id)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}
