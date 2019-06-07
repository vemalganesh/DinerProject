using InformationApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.DataAccessLayer
{
    public class RoleDataAccessLayer
    {
        private IConfiguration configuration;
        private string conStr;
        public RoleDataAccessLayer(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.conStr = configuration.GetConnectionString("DefaultConnection");
        }

        public List<string> GetAllUserRole(ApplicationRole ApplicationRole)
        {
              List<string> rolesList = new List<string>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spViewRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rolesList.Add(rdr["Name"].ToString());
                }
                con.Close();
            }
            return rolesList;
        }


        public List<ApplicationUser> GetUsersInRole(string RoleName)
        {
            List<ApplicationUser> lstUserModel = new List<ApplicationUser>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spGetUsersInRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleName", RoleName);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ApplicationUser UserModel = new ApplicationUser();
                    UserModel.ID = rdr["Id"].ToString();
                    UserModel.Name = rdr["Name"].ToString();
                    UserModel.Email = rdr["Email"].ToString();
                    UserModel.Password = rdr["Password"].ToString();
                    UserModel.EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]);
                    UserModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    UserModel.OutletGroup_Id = rdr["OutletGroup_Id"].ToString();
                    lstUserModel.Add(UserModel);
                }
                con.Close();
            }
            return lstUserModel;
        }


        //To Add new employee record    
        public async Task<IdentityResult>AddRole(ApplicationRole ApplicationRole)
        {
             using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spAddRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
                cmd.Parameters.AddWithValue("@RoleName", ApplicationRole.RoleName);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return IdentityResult.Success;
        }



        //public void UpdateUser(ApplicationRole ApplicationRole)
        //{
        //    using (SqlConnection con = new SqlConnection(conStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("spUpdateUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
        //        cmd.Parameters.AddWithValue("@RoleName", ApplicationRole.RoleName);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}


        //Get the details of a particular employee
        public ApplicationRole GetRolebyId(string id)
        {
            ApplicationRole ApplicationRole = new ApplicationRole();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Roles] WHERE Id LIKE '" + id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ApplicationRole.RoleName = rdr["Name"].ToString();
                    ApplicationRole.RoleId = rdr["Id"].ToString();
                }
            }
            return ApplicationRole;
        }

        //Get the details of a particular employee
        public ApplicationRole GetRolebyName(string Name)
        {
            ApplicationRole ApplicationRole = new ApplicationRole();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Roles] WHERE Name LIKE '" + Name + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ApplicationRole.RoleName = rdr["Name"].ToString();
                    ApplicationRole.RoleId = rdr["Id"].ToString();
                }
            }
            return ApplicationRole;
        }

        public void DeleteRole(ApplicationRole ApplicationRole)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spDeleteRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
                cmd.Parameters.AddWithValue("@RoleName", ApplicationRole.RoleName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public bool GetIsInRole(ApplicationRole roles)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                roles.RoleName = roles.RoleName.ToLower();
                SqlCommand cmd = new SqlCommand("spIsInRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", roles.UserId);
                cmd.Parameters.AddWithValue("@RoleName", roles.RoleName);

                con.Open();
                int result = (int)cmd.ExecuteScalar();

                if (result == 0)
                    return false;
            }
                return true;
        }
    }
}
