using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Models
{
    public class OutletModel
    {
        public string ID { get; set; }

        public string Company_Id { get; set; }

        public string Group_Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string OutletCode { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Boolean Suspended { get; set; }
     
       
    }
}
