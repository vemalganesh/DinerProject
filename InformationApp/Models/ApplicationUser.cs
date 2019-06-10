using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Models
{
    public class ApplicationUser
    {

        public string ID { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Name { get; set; }
     
        public string Email { get; set; }
    
        public string Password { get; set; }

        public Boolean Suspended { get; set; }
   
        public Boolean EmailVerified { get; set; }

        public Boolean PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string OutletGroup_Id { get; set; }

        public List<ApplicationRole> Roles { get; set; }
    }
}
