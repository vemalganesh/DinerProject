using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationApp.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace InformationApp.DataAccessLayer
{
    public class UserDataAccessLayer
    {
    
        private readonly string conStr;
        public UserDataAccessLayer(IConfiguration configuration)
        {
           
            this.conStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {

            List<ApplicationUser> lstUserModel = new List<ApplicationUser>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spViewUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();
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
            return lstUserModel;
        }


        //To Add new employee record    
        public async Task<IdentityResult> AddUser(ApplicationUser UserModel)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", UserModel.ID.ToString());
                cmd.Parameters.AddWithValue("@Name", UserModel.Name);
                cmd.Parameters.AddWithValue("@Email", UserModel.Email);
                cmd.Parameters.AddWithValue("@Password", UserModel.Password);
                cmd.Parameters.AddWithValue("@Suspended", UserModel.Suspended);
                cmd.Parameters.AddWithValue("@EmailVerified", UserModel.EmailVerified);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
           return IdentityResult.Success;
        }


        public void UpdateUser(ApplicationUser UserModel)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", UserModel.ID);
                cmd.Parameters.AddWithValue("@Name", UserModel.Name);
                cmd.Parameters.AddWithValue("@Email", UserModel.Email);
                cmd.Parameters.AddWithValue("@Password", UserModel.Password);
                cmd.Parameters.AddWithValue("@Suspended", UserModel.Suspended);
                cmd.Parameters.AddWithValue("@EmailVerified", UserModel.EmailVerified);
                if (UserModel.OutletGroup_Id == null)
                {
                    cmd.Parameters.Add("@OutletGroup_Id", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OutletGroup_Id", UserModel.OutletGroup_Id);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        public async Task<IdentityResult> UpdateUserUserStore(ApplicationUser UserModel)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", UserModel.ID);
                cmd.Parameters.AddWithValue("@Name", UserModel.Name);
                cmd.Parameters.AddWithValue("@Email", UserModel.Email);
                cmd.Parameters.AddWithValue("@Password", UserModel.Password);
                cmd.Parameters.AddWithValue("@Suspended", UserModel.Suspended);
                cmd.Parameters.AddWithValue("@EmailVerified", UserModel.EmailVerified);
                if (UserModel.OutletGroup_Id == null)
                {
                    cmd.Parameters.Add("@OutletGroup_Id", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OutletGroup_Id", UserModel.OutletGroup_Id);
                }
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return IdentityResult.Success;
        }


        //Get the details of a particular employee by Id
        public async Task<ApplicationUser> GetUserData(string id)
        {
            ApplicationUser UserModel = new ApplicationUser();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Users] WHERE Id LIKE '" + id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    UserModel.ID = rdr["Id"].ToString();
                    UserModel.Name = rdr["Name"].ToString();
                    UserModel.Email = rdr["Email"].ToString();
                    UserModel.Password = rdr["Password"].ToString();
                    UserModel.EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]);
                    UserModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    UserModel.OutletGroup_Id = rdr["OutletGroup_Id"].ToString();
                    UserModel.Roles = new List<ApplicationRole>();
                }
            }
            return UserModel;
        }


        public ApplicationUser GetUserDataController(string id)
        {
            ApplicationUser UserModel = new ApplicationUser();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Users] WHERE Id LIKE '" + id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    UserModel.ID = rdr["Id"].ToString();
                    UserModel.Name = rdr["Name"].ToString();
                    UserModel.Email = rdr["Email"].ToString();
                    UserModel.Password = rdr["Password"].ToString();
                    UserModel.EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]);
                    UserModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    UserModel.OutletGroup_Id = rdr["OutletGroup_Id"].ToString();
                    UserModel.Roles = new List<ApplicationRole>();
                }
            }
            return UserModel;
        }



        //Get the details of a particular employee by Email
        public async Task<ApplicationUser> FindUserByPassword(ApplicationUser user)
        {
            ApplicationUser UserModel = new ApplicationUser();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Users] WHERE Email = '" + user.Email + "And Password =" +user.Password+ "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    UserModel.ID = rdr["Id"].ToString();
                    UserModel.Name = rdr["Name"].ToString();
                    UserModel.Email = rdr["Email"].ToString();
                    UserModel.Password = rdr["Password"].ToString();
                    UserModel.EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]);
                    UserModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    UserModel.OutletGroup_Id = rdr["OutletGroup_Id"].ToString();
                }
            }
            return UserModel;
        }

        //Get the details of a particular employee by Email
        public async Task<ApplicationUser> GetUserDataByemail(string email)
        {
            ApplicationUser UserModel = new ApplicationUser();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Users] WHERE Email = '" + email + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    UserModel.ID = rdr["Id"].ToString();
                    UserModel.Name = rdr["Name"].ToString();
                    UserModel.Email = rdr["Email"].ToString();
                    UserModel.Password = rdr["Password"].ToString();
                    UserModel.EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]);
                    UserModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    UserModel.OutletGroup_Id = rdr["OutletGroup_Id"].ToString();
                }
            }
            return UserModel;
        }

        //Get the details of a particular employee by Email
        public async Task<ApplicationUser> GetUserDataByName(string Name)
        {
            ApplicationUser UserModel = new ApplicationUser();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM [dbo].[Users] WHERE Name LIKE '" + Name + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                await con.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    UserModel.ID = rdr["Id"].ToString();
                    UserModel.Name = rdr["Name"].ToString();
                    UserModel.Email = rdr["Email"].ToString();
                    UserModel.Password = rdr["Password"].ToString();
                    UserModel.EmailVerified = Convert.ToBoolean(rdr["EmailVerified"]);
                    UserModel.Suspended = Convert.ToBoolean(rdr["Suspended"]);
                    UserModel.OutletGroup_Id = rdr["OutletGroup_Id"].ToString();
                }
            }
            return UserModel;
        }


        public void DeleteUser(string id)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public async Task<IdentityResult> DeleteUserManager(string id)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return IdentityResult.Success;
        }
    }
}
