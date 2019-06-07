using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Models
{
    public class Company
    {
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string DbConnStr { get; set; }
        [Required]
        public Boolean Suspended { get; set; }
        [Required]
        public DateTime ExpDate { get; set; }
    }
}
