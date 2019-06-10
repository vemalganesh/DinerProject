using InformationApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.DataAccessLayer
{
    public class OutletDataAccessLayer
    {
        private IConfiguration configuration;
        private string conStr;
        public OutletDataAccessLayer(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.conStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<OutletModel> GetAllOutlet(string id)
        {

            List<OutletModel> lstOutletModel = new List<OutletModel>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spViewOutlet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Company_Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    OutletModel OutletModel = new OutletModel();

                    OutletModel.ID = rdr["Id"].ToString();
                    OutletModel.Name = rdr["Name"].ToString();
                    OutletModel.Address = rdr["Address"].ToString();
                    OutletModel.Company_Id = rdr["Company_Id"].ToString();
                    OutletModel.Group_Id =  rdr["Group_Id"].ToString();
                    OutletModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    OutletModel.OutletCode = rdr["OutletCode"].ToString();
                    lstOutletModel.Add(OutletModel);
                }
                con.Close();
            }
            return lstOutletModel;
        }


        //To Add new employee record    
        public void AddOutlet(OutletModel OutletModel)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spAddOutlet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Guid id = Guid.NewGuid();
                cmd.Parameters.AddWithValue("@Id", id.ToString());
                cmd.Parameters.AddWithValue("@Name", OutletModel.Name);
                cmd.Parameters.AddWithValue("@OutletCode", OutletModel.OutletCode);
                cmd.Parameters.AddWithValue("@Suspended", OutletModel.Suspended);
                cmd.Parameters.AddWithValue("@Company_Id", OutletModel.Company_Id);
                cmd.Parameters.AddWithValue("@Address", OutletModel.Address);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public void UpdateOutlet(OutletModel OutletModel)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spUpdateOutlet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", OutletModel.ID);
                cmd.Parameters.AddWithValue("@Name", OutletModel.Name);
                cmd.Parameters.AddWithValue("@OutletCode", OutletModel.OutletCode);
                cmd.Parameters.AddWithValue("@Suspended", OutletModel.Suspended);
                cmd.Parameters.AddWithValue("@Address", OutletModel.Address);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //Get the details of a particular employee  
        public OutletModel GetOutletData(string id)
        {
            OutletModel OutletModel = new OutletModel();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Outlets] WHERE Id LIKE '" + id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    OutletModel.ID = rdr["Id"].ToString();
                    OutletModel.Name = rdr["Name"].ToString();
                    OutletModel.Address = rdr["Address"].ToString();
                    OutletModel.Company_Id = rdr["Company_Id"].ToString();
                    OutletModel.Group_Id = rdr["Group_Id"].ToString();
                    OutletModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    OutletModel.OutletCode = rdr["OutletCode"].ToString();
                }
            }
            return OutletModel;
        }

        public void DeleteOutlet(string id)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spDeleteOutlet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}
