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
       
        private readonly string conStr;
        public RoleDataAccessLayer(IConfiguration configuration)
        {
   
            this.conStr = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IList<string>> GetAllUserRole(ApplicationRole ApplicationRole)
        {
              List<string> rolesList = new List<string>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spViewRole", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rolesList.Add(rdr["Name"].ToString());
                }
                con.Close();
            }
            return rolesList.ToList();
        }


        public List<string> GetAllUserRoleController(ApplicationRole ApplicationRole)
        {
            List<string> rolesList = new List<string>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spViewRole", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rolesList.Add(rdr["Name"].ToString());
                }
                con.Close();
            }
            return rolesList.ToList();
        }


        public async Task<IList<ApplicationUser>> GetUsersInRole(string RoleName)
        {
            List<ApplicationUser> lstUserModel = new List<ApplicationUser>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spGetUsersInRole", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@RoleName", RoleName);
                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ApplicationUser UserModel = new ApplicationUser
                    {
                        ID = rdr["Id"].ToString(),
                        Name = rdr["Name"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Password = rdr["Password"].ToString(),
                        EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]),
                        Suspended = Convert.ToBoolean(rdr["Suspended"]),
                        OutletGroup_Id = rdr["OutletGroup_Id"].ToString()
                    };
                    lstUserModel.Add(UserModel);
                }
                con.Close();
            }
            return lstUserModel.ToList();
        }


        //To Add new employee record    
        public async Task<IdentityResult>AddRole(ApplicationRole ApplicationRole)
        {
             using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spAddRole", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

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
        public async Task<ApplicationRole> GetRolebyId(string id)
        {
            ApplicationRole ApplicationRole = new ApplicationRole();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Roles] WHERE Id LIKE '" + id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                await con.OpenAsync();
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
        public async Task<ApplicationRole> GetRolebyName(string Name)
        {
            ApplicationRole ApplicationRole = new ApplicationRole();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Roles] WHERE Name LIKE '" + Name + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ApplicationRole.RoleName = rdr["Name"].ToString();
                    ApplicationRole.RoleId = rdr["Id"].ToString();
                }
            }
            return ApplicationRole;
        }

        public async Task<IdentityResult> DeleteRole(ApplicationRole ApplicationRole)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spDeleteRole", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", ApplicationRole.UserId);
                cmd.Parameters.AddWithValue("@RoleName", ApplicationRole.RoleName);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return IdentityResult.Success;
        }

        public async Task<bool> GetIsInRole(ApplicationRole roles)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                roles.RoleName = roles.RoleName.ToLower();
                SqlCommand cmd = new SqlCommand("spIsInRole", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", roles.UserId);
                cmd.Parameters.AddWithValue("@RoleName", roles.RoleName);

                await con.OpenAsync();
                int result = (int)cmd.ExecuteScalar();

                if (result == 0)
                    return false;
            }
                return true;
        }
    }
}
