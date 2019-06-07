using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Models.AuthViewModel
{
    public class AuthFailResponseModel
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
