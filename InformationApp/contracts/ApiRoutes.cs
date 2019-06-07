using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.contracts
{



    public static class ApiRoutes
    {


        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Auth
        {
        public const string logIn = Base + "/login";
        public const string logOut = Base + "/logout";
        public const string register = Base + "/register";
        }

        public static class Users
        {
            public const string GetUsersList = Base + "/usersList";
        }

    }
}
